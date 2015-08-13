namespace OcadParser
{
    public class OcadFileObjectIndexBlock : IBinaryParsable<OcadFileObjectIndexBlock>
    {
        public int NextObjectIndexBlock { get; set; }

        public OcadFileObjectIndexItem[] OcadFileObjectIndex { get; set; }

        public void SetupBinaryParser(BinaryParser<OcadFileObjectIndexBlock> parser)
        {
            parser.SetPropertyOrder(_ => _.NextObjectIndexBlock, _ => _.OcadFileObjectIndex);
            parser.SetArrayLength(_ => _.OcadFileObjectIndex, 256);
        }
    }
}