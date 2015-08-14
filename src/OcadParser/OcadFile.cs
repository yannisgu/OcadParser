namespace OcadParser
{
    using System.Collections.Generic;
    using System.Linq;

    public class OcadFile : IBinaryParsable<OcadFile>
    {
        public OcadFileHeader FileHeader { get; set; }

        public OcadFileSymbolIndexBlock[] OcadFileSymbolIndex { get; set; }

        public OcadFileObjectIndexBlock[] OcadFileObjectIndex { get; set; }

        public List<OcadFileOcadObject> Objects { get; set; } 

        public List<OcadFileBaseSymbol> Symbols { get; set; }
        
        public OcadFileStringIndexBlocks[] OcadFileStringIndex { get; set; }

        public List<OcadFileSpecialString> Strings { get; set; } 

        public void SetupBinaryParser(BinaryParser<OcadFile> parser)
        {
            parser.SetPropertyOrder((_ => _.FileHeader));
            parser.SetStartIndex((_ => _.OcadFileSymbolIndex), (_ => _.FileHeader.PositionFirstSymbolIndexBlock));
            parser.SetStartIndex((_ => _.OcadFileObjectIndex), (_ => _.FileHeader.PositionObjectIndexBlock));
            parser.SetStartIndex(_ => _.OcadFileStringIndex, _ => (int)_.FileHeader.PositionFirstStringIndexBlock);
            parser.SetIndexes(_ => _.OcadFileSymbolIndex, _ => _.OcadFileSymbolIndex.Select(s => s.NextSymbolIndex));
            parser.SetIndexes(_ => _.OcadFileObjectIndex, _ => _.OcadFileObjectIndex.Select(o => o.NextObjectIndexBlock));
            parser.SetIndexes(_ => _.OcadFileStringIndex, _ => _.OcadFileStringIndex.Select(o => o.NextIndexBlock));

            parser.ConfigureSpecialStringList(_ => _.Strings, _ => _.OcadFileStringIndex.SelectMany(i => i.Table));

            parser.ConfigureList(_ => _.Objects,
                _ => _.OcadFileObjectIndex.SelectMany(i => i.OcadFileObjectIndex.Select(oi => oi.Position)));

            parser.ConfigureList(
                _ => _.Symbols,
                _ => _.OcadFileSymbolIndex.SelectMany(i => i.SymbolPosition),
                _ =>
                {
                        switch (_.Otp)
                        {
                            case OcadFileObjectTypes.Point:
                                return typeof(OcadFilePointSymbol);
                            case OcadFileObjectTypes.Area:
                                return typeof(OcadFileAreaSymbol);
                            case OcadFileObjectTypes.Line:
                                return typeof(OcadFileLineSymbol);
                            case OcadFileObjectTypes.LineText:
                                return typeof(OcadFileLineTextSymbol);
                            case OcadFileObjectTypes.Rectancle:
                                return typeof(OcadFileRectangleSymbol);
                            case OcadFileObjectTypes.Text:
                                return typeof(OcadFileTextSymbol);
                        }
                        return null;
                    });
        }
    }
}