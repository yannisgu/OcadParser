namespace OcadParser
{
    using System;

    public class BaseSymbol : IBinaryParsable<BaseSymbol>
    {
        public Int16 Group { get; set; }

        public int Size { get; set; }

        public int SymNum { get; set; }

        public byte Otp { get; set; }

        public byte Flags { get; set; } // 1: rotatable symbol (not oriented to north)
        // 4: belongs to favorites
        public bool Selected { get; set; } // Symbol is selected in the symbol box

        public byte Status { get; set; } // Status of the symbol
        //   0: Normal
        //   1: Protected
        //   2: Hidden
        // AND 16: selected
        public byte PreferredDrawingTool { get; set; } // Preferred drawing tool
        //   0: off
        //   1: Curve mode
        //   2: Ellipse mode
        //   3: Circle mode
        //   4: Rectangular line mode
        //   5: Rectangular area mode
        //   6: Straight line mode
        //   7: Freehand mode
        //   8: Numeric mode
        //   9: Stairway mode                          
        public byte CsMode { get; set; } // Course setting mode
        //   0: not used for course setting
        //   1: course symbol
        //   2: control description symbol
        public byte CsObjType { get; set; } // Course setting object type
        //   0: Start symbol (Point symbol)
        //   1: Control symbol (Point symbol)
        //   2: Finish symbol (Point symbol)
        //   3: Marked route (Line symbol)
        //   4: Control description symbol (Point symbol)
        //   5: Course title (Text symbol)
        //   6: Start number (Text symbol)
        //   7: Relay variant (Text symbol)
        //   8: Text block for control description (Text symbol)
        public byte CsCdFlags { get; set; } // Course setting control description flags
        //   a combination of the flags
        //   64: available in column B
        //   32: available in column C
        //   16: available in column D
        //   8: available in column E
        //   4: available in column F
        //   2: available in column G
        //   1: available in column H
        public int Extent { get; set; }

        // Extent how much the rendered symbols can reach outside the coordinates of an object with
        // this symbol. For a point object it tells how far away from the coordinates of the object
        // anything of the point symbol can appear
        public int FilePos { get; set; } // Used internally. Value in the file is not defined.

        public Byte notUsed1 { get; set; }

        public Byte notUsed2 { get; set; }

        public Int16 nColors { get; set; } // Number of colors of the symbol max. 14, -1: the number of colors is > 14

        public Int16[] Colors { get; set; } // Colors of the symbol

        public Int16[] Description { get; set; } // Description text                        

        public byte[] IconBits { get; set; } // Each byte represents a pixel of the icon in a 256 color palette

        public ushort[] SymbolTreeGroup { get; set; } // Group ID in the symbol tree, max 64 symbol groups 


        public void SetupBinaryParser(BinaryParser<BaseSymbol> parser)
        {
            parser.SetPropertyOrder(_ => _.Size, _ => _.SymNum, _ => _.Otp);
        }
    }
}