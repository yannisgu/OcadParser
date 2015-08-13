namespace OcadParser
{
    public class StringIndexBlocks : IBinaryParsable<StringIndexBlocks>
    {
        public int NextIndexBlock { get; set; }

        public StringIndex[] Table { get; set; }

        public void SetupBinaryParser(BinaryParser<StringIndexBlocks> parser)
        {
            parser.SetPropertyOrder(_ => _.NextIndexBlock, _ => _.Table);
            parser.SetArrayLength(_ => _.Table, 256);
        }
    }

    public class StringIndex : IBinaryParsable<StringIndex>
    {
        public int Pos { get; set; } // file position of string

        public int Len { get; set; } // length reversed for the string

        public int RecType { get; set; } // string typ number, if < 0 then deleted string

        public int ObjIndex { get; set; } // index of the object

        public void SetupBinaryParser(BinaryParser<StringIndex> parser)
        {
            parser.SetPropertyOrder(_ => _.Pos, _ => _.Len, _ => _.RecType, _ => _.ObjIndex);
        }
    }
}