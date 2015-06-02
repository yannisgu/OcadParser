namespace OcadParser
{
    using System;

    public class ObjectIndexItem :IBinaryParsable<ObjectIndexItem>
    {
        public int RectLLx { get; set; }
        public int RectLLy { get; set; }
        public int RectURx { get; set; }
        public int RectURy { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
        public int Symbol { get; set; }

        public byte ObjectType { get; set; }
        public byte EncrypedMode { get; set; }

        public byte Status { get; set; }

        public byte ViewType { get; set; }

        public Int16 Color { get;set; }

        public Int16 Group { get; set; }
        public Int16 ImpLayer { get; set; }
        public byte LayoutFont { get; set; }
        public byte Res2 { get; set; }

        public void SetupBinaryParser(BinaryParser<ObjectIndexItem> parser)
        {
            parser.SetPropertyOrder(
                _ => _.RectLLx,
                _ => _.RectLLy,
                _ => _.RectURx,
                _ => _.RectURy,
                _ => _.Position,
                _ => _.Length,
                _ => _.Symbol,
                _ => _.ObjectType,
                _ => _.EncrypedMode,
                _ => _.Status,
                _ => _.ViewType,
                _ => _.Color,
                _ => _.Group,
                _ => _.ImpLayer,
                _ => _.LayoutFont,
                _ => _.Res2);
        }
    }
}