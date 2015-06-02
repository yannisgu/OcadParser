namespace OcadParser
{
    using System;
    using System.Net.Mime;

    public class AreaSymbol : BaseSymbol, IBinaryParsable<AreaSymbol>
    {
        public int BorderSym { get; set; } // Symbol for border line  activated if BorderOn is true

        public Int16 FillColor { get; set; } // Fill color activated if FillOn is true

        public Int16 HatchMode { get; set; } // Hatch mode
        //   0: None
        //   1: Single hatch
        //   2: Cross hatch
        public Int16 HatchColor { get; set; } // Color (Hatch page)

        public Int16 HatchLineWidth { get; set; } // Line width

        public Int16 HatchDist { get; set; } // Distance

        public Int16 HatchAngle1 { get; set; } // Angle 1

        public Int16 HatchAngle2 { get; set; } // Angle 2

        public bool FillOn { get; set; } // Fill is activated

        public bool BorderOn { get; set; } // Border line is activated

        public Int16 StructMode { get; set; } // Structure
        //   0: None
        //   1: aligned rows
        //   2: shifted rows
        public Int16 StructWidth { get; set; } // Width

        public Int16 StructHeight { get; set; } // Height

        public Int16 StructAngle { get; set; } // Angle

        public Int16 StructRes { get; set; } // Reserved

        public ushort DataSize { get; set; }

        // number of coordinates (each 8 bytes) which follow this structure, each object header
        // counts as 2 Coordinates (16 bytes)

        public void SetupBinaryParser(BinaryParser<AreaSymbol> parser)
        {
            parser.SetPropertyOrder(
                _ => _.Size,
                _ => _.SymNum,
                _ => _.Otp,
                _ => _.Flags,
                _ => _.Selected,
                _ => _.Status,
                _ => _.PreferredDrawingTool,
                _ => _.CsMode,
                _ => _.CsObjType,
                _ => _.CsCdFlags,
                _ => _.Extent,
                _ => _.FilePos,
                _ => _.Group,
                _ => _.nColors,
                _ => _.Colors,
                _ => _.Description,
                _ => _.IconBits,
                _ => _.SymbolTreeGroup,
                _ => _.BorderSym,
                _ => _.FillColor,
                _ => _.HatchMode,
                _ => _.HatchColor,
                _ => _.HatchLineWidth,
                _ => _.HatchDist,
                _ => _.HatchAngle1,
                _ => _.HatchAngle2,
                _ => _.FillOn,
                _ => _.BorderOn,
                _ => _.StructMode,
                _ => _.StructWidth,
                _ => _.StructHeight,
                _ => _.StructAngle,
                _ => _.StructRes,
                _ => _.DataSize);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);

        }
    }
}