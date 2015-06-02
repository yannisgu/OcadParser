namespace OcadParser
{
    public class ObjectIndexBlock : IBinaryParsable<ObjectIndexBlock>
    {
        public int NextObjectIndexBlock { get; set; }

        public ObjectIndexItem[] ObjectIndex { get; set; }

        public void SetupBinaryParser(BinaryParser<ObjectIndexBlock> parser)
        {
            parser.SetPropertyOrder(_ => _.NextObjectIndexBlock, _ => _.ObjectIndex);
            parser.SetArrayLength(_ => _.ObjectIndex, 256);
        }
    }
}