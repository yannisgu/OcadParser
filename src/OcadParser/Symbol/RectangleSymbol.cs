namespace OcadParser
{
    using System;

    public class RectangleSymbol : BaseSymbol, IBinaryParsable<RectangleSymbol>
    {
        public  Int16 LineColor {get;set;}                   // Line color
        public  Int16 LineWidth {get;set;}                   // Line width
        public  Int16 Radius {get;set;}                      // Corner radius
        public  uint GridFlags {get;set;}                       // A combination of the flags
        //   1: Grid On
        //   2: Numbered from the bottom
        public  Int16 CellWidth {get;set;}                   // Cell width
        public  Int16 CellHeight {get;set;}                  // Cell height
        public  Int16 ResGridLineColor {get;set;}            // Reserved
        public  Int16 ResGridLineWidth {get;set;}            // Reserved
        public  Int16 UnnumCells {get;set;}                  // Unnumbered Cells
        public  char[] UnnumText {get;set;}                  // Text in unnumbered Cells
        public  Int16 LineStyle {get;set;}                   // Line style
        //   1: flat caps
        //   0: round caps
        //   4: flat caps
        public   char[] Res2 {get;set;}                      // not used
        public  Int16 ResFontColor {get;set;}                // Reserved
        public  Int16 FontSize {get;set;}                    // font size
        public  Int16 Res3 {get;set;}                        // not used
        public  uint Res4 {get;set;}                        // not used
        public  Int16 Res5 {get;set;}                        // not used
        public  Int16 Res6 {get;set;}                        // not used

        public void SetupBinaryParser(BinaryParser<RectangleSymbol> parser)
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
                _ => _.LineColor,
                _ => _.LineWidth,
                _ => _.Radius,
                _ => _.GridFlags,
                _ => _.CellWidth,
                _ => _.CellHeight,
                _ => _.ResGridLineColor,
                _ => _.ResGridLineWidth,
                _ => _.UnnumCells,
                _ => _.UnnumText,
                _ => _.LineStyle,
                _ => _.Res2,
                _ => _.ResFontColor,
                _ => _.FontSize,
                _ => _.Res3,
                _ => _.Res4,
                _ => _.Res5,
                _ => _.Res6);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetArrayLength(_ => _.Res2, 31);
            parser.SetArrayLength(_ => _.UnnumText, 3);
        }
    }
}