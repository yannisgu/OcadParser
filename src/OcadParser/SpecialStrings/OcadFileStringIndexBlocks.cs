namespace OcadParser
{
    public class OcadFileStringIndexBlocks : IBinaryParsable<OcadFileStringIndexBlocks>
    {
        public int NextIndexBlock { get; set; }

        public OcadFileStringIndex[] Table { get; set; }

        public void SetupBinaryParser(BinaryParser<OcadFileStringIndexBlocks> parser)
        {
            parser.SetPropertyOrder(_ => _.NextIndexBlock, _ => _.Table);
            parser.SetArrayLength(_ => _.Table, 256);
        }
    }
}