namespace OcadParser
{
    public class OcadFileStringIndex : IBinaryParsable<OcadFileStringIndex>
    {
        public int Pos { get; set; } // file position of string

        public int Len { get; set; } // length reversed for the string

        public int RecType { get; set; } // string typ number, if < 0 then deleted string

        public int ObjIndex { get; set; } // index of the object

        public void SetupBinaryParser(BinaryParser<OcadFileStringIndex> parser)
        {
            parser.SetPropertyOrder(_ => _.Pos, _ => _.Len, _ => _.RecType, _ => _.ObjIndex);
        }
    }
}