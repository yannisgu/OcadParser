namespace OcadParser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class OcadFile : IBinaryParsable<OcadFile>
    {
        public OcadFileHeader FileHeader { get; set; }

        public SymbolIndexBlock[] SymbolIndex { get; set; }

        public ObjectIndexBlock[] ObjectIndex { get; set; }

        public List<BaseSymbol> Symbols { get; set; } 

        public void SetupBinaryParser(BinaryParser<OcadFile> parser)
        {
            parser.SetPropertyOrder((_ => _.FileHeader));
            parser.SetStartIndex((_ => _.SymbolIndex), (_ => _.FileHeader.PositionFirstSymbolIndexBlock));
            parser.SetStartIndex((_ => _.ObjectIndex), (_ => _.FileHeader.PositionObjectIndexBlock));
            parser.SetIndexes(_ => _.SymbolIndex, _ => _.SymbolIndex.Select(s => s.NextSymbolIndex));
            parser.SetIndexes(_ => _.ObjectIndex, _ => _.ObjectIndex.Select(o => o.NextObjectIndexBlock));

            parser.ConfigureList(
                _ => _.Symbols,
                _ => _.SymbolIndex.SelectMany(i => i.SymbolPosition),
                _ =>
                    {
                        switch (_.Otp)
                        {
                            case ObjectTypes.Point:
                                return typeof(PointSymbol);
                            case ObjectTypes.Area:
                                return typeof(AreaSymbol);
                            case ObjectTypes.Line:
                                return typeof(LineSymbol);
                            case ObjectTypes.LineText:
                                return typeof(LineTextSymbol);
                            case ObjectTypes.Rectancle:
                                return typeof(RectangleSymbol);
                            case ObjectTypes.Text:
                                return typeof(TextSymbol);
                        }
                        return null;
                    });
        }
    }

    public class TextSymbol : BaseSymbol, IBinaryParsable<TextSymbol>
    {
        public char[] FontName { get; set; } // TrueType font

        public Int16 FontColor { get; set; } // Color

        public Int16 FontSize { get; set; } // 10 times the size in pt

        public Int16 Weight { get; set; } // Bold as used in the Windows GDI
        //   400: normal
        //   700: bold
        public bool Italic { get; set; } // true if Italic is checked

        public byte Res1 { get; set; } // not used

        public Int16 CharSpace { get; set; } // Char. spacing

        public Int16 WordSpace { get; set; } // Word spacing

        public Int16 Alignment { get; set; } // Alignment
        //   0: Bottom Left
        //   1: Bottom Center
        //   2: Bottom Right
        //   3: Bottom Justified
        //   4: Middle Left 
        //   5: Middle Center 
        //   6: Middle Right
        //   7: only in LText!
        //   8: Top Left
        //   9: Top Center
        //  10: Top Right
        //  11: only in LText!
        public Int16 LineSpace { get; set; } // Line spacing

        public Int16 ParaSpace { get; set; } // Space after Paragraph

        public Int16 IndentFirst { get; set; } // Indent first line

        public Int16 IndentOther { get; set; } // Indent other lines

        public Int16 nTabs { get; set; } // number of tabulators for text symbol

        public long[] Tabs { get; set; } // Tabulators

        public uint LBOn { get; set; } // true if Line below On is checked

        public Int16 LBColor { get; set; } // Line color (Line below)

        public Int16 LBWidth { get; set; } // Line width (Line below)

        public Int16 LBDist { get; set; } // Distance from text

        public Int16 Res2 { get; set; }

        public byte FrMode { get; set; } // Framing mode
        //   0: no framing
        //   1: shadow framing
        //   2: line framing
        //   3: rectangle framing
        public byte FrLineStyle { get; set; } // Framing line style
        //   0: default OCAD 8 Miter
        //   2: ps_Join_Bevel
        //   1: ps_Join_Round
        //   4: ps_Join_Miter
        public bool PointSymOn { get; set; } // Point symbol is activated

        public int PointSymNumber { get; set; } // Point symbol for text symbol activated if PointSymOn is true

        public char[] Res3 { get; set; } // not used

        public Int16 FrLeft { get; set; } // Left border for rectangle framing

        public Int16 FrBottom { get; set; } // Bottom border for rectangle framing

        public Int16 FrRight { get; set; } // Right border for rectangle framing

        public Int16 FrTop { get; set; } // Top border for rectangle framing

        public Int16 FrColor { get; set; } // Framing color

        public Int16 FrWidth { get; set; } // Framing width for line framing

        public Int16 Res4 { get; set; } // not used

        public uint Res5 { get; set; } // not used

        public Int16 FrOfX { get; set; } // Horizontal offset for shadow framing

        public Int16 FrOfY { get; set; } // Vertical offset for shadow framing

        public void SetupBinaryParser(BinaryParser<TextSymbol> parser)
        {
            parser.SetPropertyOrder(
                _ => _.FontName,
                _ => _.FontColor,
                _ => _.FontSize,
                _ => _.Weight,
                _ => _.Italic,
                _ => _.Res1,
                _ => _.CharSpace,
                _ => _.WordSpace,
                _ => _.Alignment,
                _ => _.LineSpace,
                _ => _.ParaSpace,
                _ => _.IndentFirst,
                _ => _.IndentOther,
                _ => _.nTabs,
                _ => _.Tabs,
                _ => _.LBOn,
                _ => _.LBColor,
                _ => _.LBWidth,
                _ => _.LBDist,
                _ => _.Res2,
                _ => _.FrMode,
                _ => _.FrLineStyle,
                _ => _.PointSymOn,
                _ => _.PointSymNumber,
                _ => _.Res3,
                _ => _.FrLeft,
                _ => _.FrBottom,
                _ => _.FrRight,
                _ => _.FrTop,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.Res4,
                _ => _.Res5,
                _ => _.FrOfX,
                _ => _.FrOfY);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetArrayLength(_ => _.FontName, 31);
            parser.SetArrayLength(_ => _.Tabs, 32);
            parser.SetArrayLength(_ => _.Res3, 18);
        }
    }

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

    public class LineTextSymbol : BaseSymbol, IBinaryParsable<LineTextSymbol>
    {
        public char[] FontName { get; set; } // TrueType font

        public Int16 FontColor { get; set; } // Color

        public Int16 FontSize { get; set; } // 10 times the value entered in Size

        public Int16 Weight { get; set; } // Bold as used in the Windows GDI
        //   400: normal
        //   700: bold
        public bool Italic { get; set; } // true if Italic is checked

        public byte Res1 { get; set; } // not used

        public Int16 CharSpace { get; set; } // Char. spacing

        public Int16 WordSpace { get; set; } // Word spacing

        public Int16 Alignment { get; set; } // Alignment     --> constant.pas
        //   0: Bottom Left
        //   1: Bottom Center
        //   2: Bottom Right
        //   3: Bottom All line
        //   4: Middle Left
        //   5: Middle Center
        //   6: Middle Right
        //   7: Middle All line
        //   8: Top Left
        //   9: Top Center
        //  10: Top Right
        //  11: Top All line 
        public byte FrMode { get; set; } // Framing mode
        //   0: no framing
        //   1: shadow framing
        //   2: line framing
        public byte FrLineStyle { get; set; } // Framing line style
        //   0: default OCAD 8 and 9.0 Miter
        //   2: ps_Join_Bevel
        //   1: ps_Join_Round
        //   4: ps_Join_Miter
        public char[] Res2 { get; set; } // not used

        public Int16 FrColor { get; set; } // Framing color

        public Int16 FrWidth { get; set; } // Framing width for line framing

        public Int16 Res3 { get; set; } // not used

        public uint Res4 { get; set; } // not used

        public Int16 FrOfX { get; set; } // Horizontal offset for shadow framing

        public Int16 FrOfY { get; set; } // Vertical offset for shadow framing

        public void SetupBinaryParser(BinaryParser<LineTextSymbol> parser)
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
                _ => _.FontName,
                _ => _.FontColor,
                _ => _.FontSize,
                _ => _.Weight,
                _ => _.Italic,
                _ => _.Res1,
                _ => _.CharSpace,
                _ => _.WordSpace,
                _ => _.Alignment,
                _ => _.FrMode,
                _ => _.FrLineStyle,
                _ => _.Res2,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.Res3,
                _ => _.Res4,
                _ => _.FrOfX,
                _ => _.FrOfY);
            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetArrayLength(_ => _.FontColor, 14);
            parser.SetArrayLength(_ => _.Res2, 14);
        }
    }

    public class LineSymbol : BaseSymbol, IBinaryParsable<LineSymbol>
    {
        public Int16 LineColor { get; set; } // Line color

        public Int16 LineWidth { get; set; } // Line width

        public Int16 LineStyle { get; set; } // Line style
        //   0: bevel joins/flat caps
        //   1: round joins/round caps
        //   4: miter joins/flat caps
        public Int16 DistFromStart { get; set; } // Distance from start

        public Int16 DistToEnd { get; set; } // Distance to the end

        public Int16 MainLength { get; set; } // Main length a

        public Int16 EndLength { get; set; } // End length b

        public Int16 MainGap { get; set; } // Main gap C

        public Int16 SecGap { get; set; } // Gap D

        public Int16 EndGap { get; set; } // Gap E

        public Int16 MinSym { get; set; } // -1: at least 0 gaps/symbols
        //  0: at least 1 gap/symbol
        //  1: at least 2 gaps/symbols
        //  etc.
        public Int16 nPrimSym { get; set; } // No. of symbols

        public Int16 PrimSymDist { get; set; } // Distance

        public uint DblMode { get; set; } // Mode (Double line page)

        public uint DblFlags { get; set; } // Double line flags
        //    1: Fill color on
        //    2: Background color on
        public Int16 DblFillColor { get; set; } // Fill color

        public Int16 DblLeftColor { get; set; } // Left line/Color

        public Int16 DblRightColor { get; set; } // Right line/Color

        public Int16 DblWidth { get; set; } // Width

        public Int16 DblLeftWidth { get; set; } // Left line/Line width

        public Int16 DblRightWidth { get; set; } // Right line/Line width

        public Int16 DblLength { get; set; } // Dashed/Distance a

        public Int16 DblGap { get; set; } // Dashed/Gap

        public Int16 DblBackgroundColor { get; set; } // Reserved

        public Int16 DblRes1 { get; set; } // Reserved

        public Int16 DblRes2 { get; set; } // Reserved

        public uint DecMode { get; set; } // Decrease mode
        //   0: off
        //   1: decreasing towards the end
        //   2: decreasing towards both ends
        public Int16 DecLast { get; set; } // Last symbol

        public Int16 DecRes { get; set; } // Reserved

        public Int16 FrColor { get; set; } // Color of the framing line

        public Int16 FrWidth { get; set; } // Line width of the framing line

        public Int16 FrStyle { get; set; } // Line style of the framing line
        //   0: bevel joins/flat caps
        //   1: round joins/round caps
        //   4: miter joins/flat caps
        //  PointedEnd := LineStyle and 2 > 0;
        public uint PrimDSize { get; set; }

        // number or coordinates (8 bytes) for the Main symbol A which follow this structure.
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint SecDSize { get; set; }

        // number or coordinates (8 bytes) for the Secondary symbol which follow the Main symbol A
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint CornerDSize { get; set; }

        // number or coordinates (8 bytes) for the Corner symbol which follow the Secondary symbol
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint StartDSize { get; set; }

        // number or coordinates (8 bytes) for the Start symbol C which follow the Corner symbol
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint EndDSize { get; set; }

        // number or coordinates (8 bytes) for the End symbol D which follow the Start symbol C
        // Each symbol header counts as 2 coordinates (16 bytes).
        public Byte UseSymbolFlags { get; set; }

        // 1 = end symbol, 2 = start symbol, 4 = corner symbol, 8 = secondary symbol   

        public void SetupBinaryParser(BinaryParser<LineSymbol> parser)
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
                _ => _.LineStyle,
                _ => _.DistFromStart,
                _ => _.DistToEnd,
                _ => _.MainLength,
                _ => _.EndLength,
                _ => _.MainGap,
                _ => _.SecGap,
                _ => _.EndGap,
                _ => _.MinSym,
                _ => _.nPrimSym,
                _ => _.PrimSymDist,
                _ => _.DblMode,
                _ => _.DblFlags,
                _ => _.DblFillColor,
                _ => _.DblLeftColor,
                _ => _.DblRightColor,
                _ => _.DblWidth,
                _ => _.DblLeftWidth,
                _ => _.DblRightWidth,
                _ => _.DblLength,
                _ => _.DblGap,
                _ => _.DblBackgroundColor,
                _ => _.DblRes1, _ => _.DblRes2,
                _ => _.DecMode,
                _ => _.DecLast,
                _ => _.DecRes,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.FrStyle,
                _ => _.PrimDSize,
                _ => _.SecDSize,
                _ => _.CornerDSize,
                _ => _.StartDSize,
                _ => _.EndDSize,
                _ => _.UseSymbolFlags);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
        }
    }

    public class AreaSymbol : BaseSymbol, IBinaryParsable<AreaSymbol>
    {

        public void SetupBinaryParser(BinaryParser<AreaSymbol> parser)
        {
            parser.SetPropertyOrder(_ => _.Size, _ => _.SymNum, _ => _.Otp);
        }
    }

    public class PointSymbol : BaseSymbol, IBinaryParsable<PointSymbol>
    {
        public void SetupBinaryParser(BinaryParser<PointSymbol> parser)
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
                _ => _.DataSize);

            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
        }

        public ushort DataSize { get; set; }
        
    }

    public class ObjectTypes
    {
        public const byte Point = 1;
        public const byte Line = 2;
        public const byte Area = 3;
        public const byte Text = 4;
        public const byte LineText = 6;
        public const byte Rectancle = 7;
    }

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

    public interface IBinaryParsable<T>
        where T : IBinaryParsable<T>
    {
        void SetupBinaryParser(BinaryParser<T> parser);
    }

    

    public class BinaryParser<T> 
        where T : IBinaryParsable<T>
    {
        private Expression<Func<T, object>>[] propertyOrder;
        private Dictionary<string, int> arrayLengths = new Dictionary<string, int>();
        private Dictionary<string, Expression<Func<T, int>>> propertyStartIndexes = new Dictionary<string, Expression<Func<T, int>>>();

        private Dictionary<string, Expression<Func<T, IEnumerable<int>>>> propertyIndexes =
            new Dictionary<string, Expression<Func<T, IEnumerable<int>>>>();

        public BinaryParser<T> SetStartIndex(Expression<Func<T, object>> property, Expression<Func<T, int>> startIndexProperty)
        {
            propertyStartIndexes[GetPropertyName(property)] = startIndexProperty;
            return this;
        }

        public BinaryParser<T> SetIndexes(Expression<Func<T, object>> property, Expression<Func<T, IEnumerable<int>>> startIndexProperty)
        {
            propertyIndexes[GetPropertyName(property)] = startIndexProperty;
            return this;
        }

        public BinaryParser<T> SetPropertyOrder(params Expression<Func<T, object>>[] property)
        {
            propertyOrder = property;
            return this;
        }

        public T Read(OcadStreamReader reader) 
        {
            var returnValue = Activator.CreateInstance<T>();

            returnValue.SetupBinaryParser(this);
            if (propertyOrder != null)
            {
                foreach (var propertyExpression in propertyOrder)
                {
                    this.ReadAndSetPropertyValue(reader, GetPropertyName(propertyExpression), returnValue);
                }
            }


            while (propertyStartIndexes.Any())
            {
                var nextProperty = this.propertyStartIndexes.OrderBy(_ => _.Value.Compile().Invoke(returnValue)).First();
                reader.ReadUntil(nextProperty.Value.Compile().Invoke(returnValue));
                ReadAndSetArrayValue(reader, nextProperty.Key, returnValue);
                propertyStartIndexes.Remove(nextProperty.Key);
            }

            var alreadyReadIndexes = new List<int>();
            while (
                propertyIndexes.SelectMany(_ => _.Value.Compile().Invoke(returnValue))
                    .Any(_ => _ != 0 && !alreadyReadIndexes.Contains(_)))
            {
                var property =
                    propertyIndexes.Select(_ => new { Property = _.Key, Value = _.Value.Compile().Invoke(returnValue) })
                        .First(_ => !_.Value.All(v => v == 0 || alreadyReadIndexes.Contains(v)));
                var index = property.Value.First(_ => _ != 0 && !alreadyReadIndexes.Contains(_));
                reader.ReadUntil(index);
                ReadAndSetArrayValue(reader, property.Property, returnValue);
                alreadyReadIndexes.Add(index);
            }

            ReadAllLists(returnValue, reader);
            
            return returnValue;
        }

        private void ReadAllLists(T returnValue, OcadStreamReader reader)
        {
            foreach (var listPropertyName in listIndexes.Keys)
            {
                var property = typeof(T).GetProperty(listPropertyName);
                var itemType = property.PropertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(itemType);
                var list = Activator.CreateInstance(listType);
                var getIndexes = listIndexes[listPropertyName];
                var indexes = getIndexes(returnValue);
                foreach (var index in indexes)
                {
                    reader.ReadUntil(index);

                    var itemValue = ReadPropertyValue(reader, itemType, null);
                    if (listTypeMappings.ContainsKey(listPropertyName))
                    {
                        var newType = listTypeMappings[listPropertyName](itemValue);
                        if (newType != null)
                        {
                            reader.ReadUntil(index);
                            itemValue = ReadPropertyValue(reader, newType, null);
                        }

                    }

                    listType.GetMethod("Add").Invoke(list, new[] { itemValue });



                }

                property.SetValue(returnValue, list);
            }
        }

        private void ReadAndSetArrayValue(OcadStreamReader reader, string key, T item)
        {
            var property = typeof(T).GetProperty(key);
            var type = property.PropertyType.GetElementType();
            var value = ReadPropertyValue(reader, type, key);
            var oldArray = (Array)property.GetValue(item);
            if (oldArray == null)
            {
                oldArray = Array.CreateInstance(type, 0);
            }

            var newArray = Array.CreateInstance(type, oldArray.Length + 1);
            for (var i = 0; i < oldArray.Length; i++)
            {
                newArray.SetValue(oldArray.GetValue(i), i);
            }
            newArray.SetValue(value, newArray.Length -1);
            property.SetValue(item, newArray);
        }

        protected virtual void ReadAndSetPropertyValue(OcadStreamReader reader, string propertyName, T value)
        {
            var property = typeof(T).GetProperty(propertyName);
            var type = property.PropertyType;
            var propertyValue = this.ReadPropertyValue(reader, type, propertyName);

            property.SetValue(value, propertyValue);
        }

        private object ReadPropertyValue(OcadStreamReader reader, Type type, string propertyName)
        {
            object propertyValue = null;
            if (type == typeof(Int16))
            {
                propertyValue = reader.ReadSmallInt();
            }
            else if (type == typeof(Byte))
            {
                propertyValue = reader.ReadByte();
            }
            else if (type == typeof(double))
            {
                propertyValue = reader.ReadDouble();
            }
            else if (type == typeof(int))
            {
                propertyValue = reader.ReadInt();
            }
            else if (type == typeof(long))
            {
                propertyValue = reader.ReadLong();
            }
            else if (type == typeof(string))
            {
                propertyValue = reader.ReadString();
            }
            else if (type == typeof(TdPoly))
            {
                propertyValue = reader.ReadTdPoly();
            }
            else if (type == typeof(ushort))
            {
                propertyValue = reader.ReadWord();
            }
            else if (type == typeof(bool))
            {
                propertyValue = reader.ReadWordBool();
            }
            else if (type.IsArray)
            {
                var length = this.arrayLengths[propertyName];
                var array = Array.CreateInstance(type.GetElementType(), length);

                for (var i = 0; i < length; i ++)
                {
                    array.SetValue(ReadPropertyValue(reader, type.GetElementType(), null), i);
                }

                propertyValue = array;
            }
            else if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IBinaryParsable<>)))
            {
                var childParserType = typeof(BinaryParser<>).MakeGenericType(type);
                var childParser = Activator.CreateInstance(childParserType);
                var readMethod = childParserType.GetMethod("Read");
                propertyValue = readMethod.Invoke(childParser, new object[] { reader });
            }
            return propertyValue;
        }

        private static string GetPropertyName
        (Expression<Func<T, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");
            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }
            return null;
        }

        private static string GetPropertyName<T2>
        (Expression<Func<T, IList<T2>>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");
            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }
            return null;
        }

        public BinaryParser<T> SetArrayLength(Expression<Func<T, object>> property, int length)
        {
            arrayLengths[GetPropertyName(property)] = length;
            return this;
        }

        Dictionary<string, Func<T, IEnumerable<int>>> listIndexes = new Dictionary<string, Func<T, IEnumerable<int>>>();
        Dictionary<string, Func<object, Type>> listTypeMappings = new Dictionary<string, Func<object, Type>>();

        public BinaryParser<T> ConfigureList<T2>(Expression<Func<T, IList<T2>>> property, Func<T, IEnumerable<int>> getIndexes, Func<T2, Type> castMapping)
        {
            var name = GetPropertyName(property);
            listIndexes[name] = getIndexes;
            listTypeMappings[name] = BuildAccessor<object, Type>(castMapping.Method);
            return this;
        }

        private static Func<T1, T2> BuildAccessor<T1, T2>(MethodInfo method)
        {
            ParameterExpression obj = Expression.Parameter(typeof(T1), "obj");

            Expression<Func<T1, T2>> expr =
                Expression.Lambda<Func<T1, T2>>(
                    Expression.Convert(
                        Expression.Call(null,
                            method,
                            Expression.Convert(obj, method.GetParameters()[0].ParameterType)),
                        typeof(T2)),
                    obj);

            return expr.Compile();
        }
    }



    public class SymbolIndexBlock : IBinaryParsable<SymbolIndexBlock>
    {
        public int NextSymbolIndex { get; set; }
        public int[] SymbolPosition { get; set; }

        public void SetupBinaryParser(BinaryParser<SymbolIndexBlock> parser)
        {
            parser
                .SetPropertyOrder(_ => _.NextSymbolIndex, _ => _.SymbolPosition)
                .SetArrayLength(_ => _.SymbolPosition, 256);
        }
    }
}
