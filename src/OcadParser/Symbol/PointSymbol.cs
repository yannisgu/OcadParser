namespace OcadParser
{
    public class PointSymbol : BaseSymbol, IBinaryParsable<PointSymbol>
    {
        public void SetupBinaryParser(BinaryParser<PointSymbol> parser)
        {
            parser.SetPropertyOrder(
                _ => _.Size,
                _ => _.SymNum,
                _ => _.Otp,
                _ => _.Flags,
                _ => _.Selected,
                _ => _.Status,
                _ => _.PreferredDrawingTool,
                _ => _.CsMode,
                _ => _.CsObjType,
                _ => _.CsCdFlags,
                _ => _.Extent,
                _ => _.FilePos,
                _ => _.Group,
                _ => _.nColors,
                _ => _.Colors,
                _ => _.Description,
                _ => _.IconBits,
                _ => _.SymbolTreeGroup,
                _ => _.DataSize);

            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
        }

        public ushort DataSize { get; set; }
        
    }
}