namespace OcadParser
{
    public class OcadFileSymbolIndexBlock : IBinaryParsable<OcadFileSymbolIndexBlock>
    {
        public int NextSymbolIndex { get; set; }
        public int[] SymbolPosition { get; set; }

        public void SetupBinaryParser(BinaryParser<OcadFileSymbolIndexBlock> parser)
        {
            parser
                .SetPropertyOrder(_ => _.NextSymbolIndex, _ => _.SymbolPosition)
                .SetArrayLength(_ => _.SymbolPosition, 256);
        }
    }
}
