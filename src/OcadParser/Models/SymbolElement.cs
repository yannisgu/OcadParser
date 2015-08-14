using System;

namespace OcadParser.Models
{
    public class SymbolElement
    {
        public TdPoly[] Poly { get; set; }
        public OcadColor Color { get; set; }
        public SymbolElementType Type { get; set; }
        public SymbolElementFlag Flags { get; set; }
        public Int16 LineWidth { get; set; }
        public Int16 Diameter { get; set; }

        public enum SymbolElementType
        {
            Line = 1,
            Area = 2,
            Circle = 3,
            Dot = 4
        }

        public enum SymbolElementFlag
        {
            LineWithRoundEnds = 1
        }
    }


}