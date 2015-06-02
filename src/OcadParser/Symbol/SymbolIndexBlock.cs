namespace OcadParser
{
    public class SymbolIndexBlock : IBinaryParsable<SymbolIndexBlock>
    {
        public int NextSymbolIndex { get; set; }
        public int[] SymbolPosition { get; set; }

        public void SetupBinaryParser(BinaryParser<SymbolIndexBlock> parser)
        {
            parser
                .SetPropertyOrder(_ => _.NextSymbolIndex, _ => _.SymbolPosition)
                .SetArrayLength(_ => _.SymbolPosition, 256);
        }
    }
}
