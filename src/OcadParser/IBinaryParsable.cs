namespace OcadParser
{
    public interface IBinaryParsable<T>
        where T : IBinaryParsable<T>
    {
        void SetupBinaryParser(BinaryParser<T> parser);
    }
}