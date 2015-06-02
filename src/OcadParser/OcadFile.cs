namespace OcadParser
{
    using System.Collections.Generic;
    using System.Linq;

    public class OcadFile : IBinaryParsable<OcadFile>
    {
        public OcadFileHeader FileHeader { get; set; }

        public SymbolIndexBlock[] SymbolIndex { get; set; }

        public ObjectIndexBlock[] ObjectIndex { get; set; }

        public List<BaseSymbol> Symbols { get; set; } 

        public void SetupBinaryParser(BinaryParser<OcadFile> parser)
        {
            parser.SetPropertyOrder((_ => _.FileHeader));
            parser.SetStartIndex((_ => _.SymbolIndex), (_ => _.FileHeader.PositionFirstSymbolIndexBlock));
            parser.SetStartIndex((_ => _.ObjectIndex), (_ => _.FileHeader.PositionObjectIndexBlock));
            parser.SetIndexes(_ => _.SymbolIndex, _ => _.SymbolIndex.Select(s => s.NextSymbolIndex));
            parser.SetIndexes(_ => _.ObjectIndex, _ => _.ObjectIndex.Select(o => o.NextObjectIndexBlock));

            parser.ConfigureList(
                _ => _.Symbols,
                _ => _.SymbolIndex.SelectMany(i => i.SymbolPosition),
                _ =>
                    {
                        switch (_.Otp)
                        {
                            case ObjectTypes.Point:
                                return typeof(PointSymbol);
                            case ObjectTypes.Area:
                                return typeof(AreaSymbol);
                            case ObjectTypes.Line:
                                return typeof(LineSymbol);
                            case ObjectTypes.LineText:
                                return typeof(LineTextSymbol);
                            case ObjectTypes.Rectancle:
                                return typeof(RectangleSymbol);
                            case ObjectTypes.Text:
                                return typeof(TextSymbol);
                        }
                        return null;
                    });
        }
    }
}