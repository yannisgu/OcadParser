using System.Collections.Generic;

namespace OcadParser
{
    public class CsObjectSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Code = value; }
        public string Code { get; set; }
        [OcadFileSpecialStringMapping("Y")]
        public string Type { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string SymbolForFieldB { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string SymbolForFieldC { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string SymbolForFieldD { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string SymbolForFieldE { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string SymbolForFieldF { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string SymbolForFieldG { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string SymbolForFieldH { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string PunchingUnitIDs { get; set; }
        [OcadFileSpecialStringMapping("mf")]
        public string FunnelTapes { get; set; }
        [OcadFileSpecialStringMapping("ot")]
        public string TextControlDescriptionObject { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string ControlDescriptionCorner { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string SizeInformation { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string TextForTextDescriptionAndTextBlock { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string ElevationUser { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string IsElevationUserUsed { get; set; }
    }

    public class CourseSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value)
        {
            CourseName = value;
        }

        public string CourseName { get; set; }

        [OcadFileSpecialStringMapping("C")]
        public string Climb { get; set; }

        [OcadFileSpecialStringMapping("E")]
        public string ExtraDistance { get; set; }

        [OcadFileSpecialStringMapping("F")]
        public string FromStartNumber { get; set; }

        [OcadFileSpecialStringMapping("H")]
        public string ClassNameForControlDescription { get; set; }

        [OcadFileSpecialStringMapping("K")]
        public string Combination { get; set; }

        [OcadFileSpecialStringMapping("M")]
        public string MapFile { get; set; }

        [OcadFileSpecialStringMapping("R")]
        public string NumberOfRunnersTeams { get; set; }

        [OcadFileSpecialStringMapping("S")]
        public string MapScale { get; set; }

        [OcadFileSpecialStringMapping("T")]
        public string ToStartNumber { get; set; }

        [OcadFileSpecialStringMapping("Y")]
        public string CourseType { get; set; }

        [OcadFileSpecialStringMapping("L")]
        public string NumberOfLegs { get; set; }

        [OcadFileSpecialStringMapping("s")]
        public string Start { get; set; }

        [OcadFileSpecialStringMapping("c")]
        public List<string> Controls { get; set; }

        [OcadFileSpecialStringMapping("m")]
        public string MarkedRoute { get; set; }

        [OcadFileSpecialStringMapping("k")]
        public string MandatoryCrossingPoint { get; set; }

        [OcadFileSpecialStringMapping("w")]
        public string MandatoryPassageThroughOutOfBoundsArea { get; set; }

        [OcadFileSpecialStringMapping("g")]
        public string MapChange { get; set; }

        [OcadFileSpecialStringMapping("f")]
        public string Finish { get; set; }

        [OcadFileSpecialStringMapping("l")]
        public string LegVariationStarts { get; set; }

        [OcadFileSpecialStringMapping("b")]
        public string BranchOfALegVariationStarts { get; set; }

        [OcadFileSpecialStringMapping("p")]
        public string EndOfALegVariation { get; set; }

        [OcadFileSpecialStringMapping("r")]
        public string RelayVariationStarts { get; set; }

        [OcadFileSpecialStringMapping("v")]
        public string BranchOfARelayVariationStarts { get; set; }

        [OcadFileSpecialStringMapping("q")]
        public string EndOfARelayVariation { get; set; }

        [OcadFileSpecialStringMapping("e")]
        public string UsedInternallyOnly1 { get; set; }

        [OcadFileSpecialStringMapping("i")]
        public string UsedInternallyOnly2 { get; set; }

        [OcadFileSpecialStringMapping("j")]
        public string UsedInternallyOnly3 { get; set; }

        [OcadFileSpecialStringMapping("n")]
        public string CourseNameObject { get; set; }

        [OcadFileSpecialStringMapping("u")]
        public string StartNumberObject { get; set; }

        [OcadFileSpecialStringMapping("t")]
        public string TextBlockForControlDescription { get; set; }

        [OcadFileSpecialStringMapping("o")]
        public string OtherObject { get; set; }

        [OcadFileSpecialStringMapping("s",
            "c",
            "m",
            "k",
            "w",
            "g",
            "f",
            "l",
            "b",
            "p",
            "r",
            "v",
            "q",
            "e",
            "i",
            "j")]
        public List<string> CourseSettingObjects { get; set; }
    }

    public class CsClassSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { ClassName = value; }
        public string ClassName { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string CourseName { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string FromNumber { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string NumberOfRunners { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ToNumber { get; set; }
    }
    public class DataSetSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { DatasetName = value; }
        public string DatasetName { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string DBaseFile { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string ODBCDataSource { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string DatabaseUserName { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string DatabasePassword { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Table { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string KeyField { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string SecondaryTableReferenceKey { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string SecondaryTableTableKey { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string SecondaryTableKey { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string SymbolField { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string TextField { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string SizeField { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LengthUnit { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AreaUnit { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Decimals { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string EastingField { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string NorthingField { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string AngleField { get; set; }
    }
    public class DbObjectSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Key = value; }
        public string Key { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dataset { get; set; }
    }
    public class OimFileSsr : OcadFileSpecialStringRecord
    {
    }
    public class PrevObjSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { CourseName = value; }
        public string CourseName { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string Object { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Description { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string FromStartpointOfLine { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ToEndPointOfLine { get; set; }
    }
    public class BackgroundMapSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { FileName = value; }
        public string FileName { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AngleOmega { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string AnglePhi { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dim { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string RenderWithSpotColors { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string AssignedToSpotColor { get; set; }
        [OcadFileSpecialStringMapping("q")]
        public string SubtractFromSpotColor { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string VisibleInBackgroundFavorites { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string VisibleInNormalSpotColorAndDraftMode { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Transparent { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string OffsetX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetY { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string PixelSizeX { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string PixelSizeY { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string IsInfraredImage { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string IsWMSOnlineBackgroundMap { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string WMSServerName { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string WMSLayerName { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string WMSLayerScaleRange { get; set; }
    }
    public class ColorSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string Number { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Cyan { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string Magenta { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string Yellow { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string Black { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string Overprint { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Transparency { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string SpotColorSeparationName { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string PercentageInTheSpotColorSeparation { get; set; }
    }
    public class SpotColorSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string Visible { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string Number { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string Frequency { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string Angle { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Cyan { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string Magenta { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string Yellow1 { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string Black { get; set; }
    }
    public class FileInfoOcad10Ssr : OcadFileSpecialStringRecord
    {
    }
    public class ZoomSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("x")]
        public string OffsetX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetY { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string Zoom { get; set; }
    }
    public class ImpLayerSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string LayerNumber { get; set; }
    }
    public class OimFindSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Condition { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dataset { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string FromZoom { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HintField { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string NameField { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string HotspotType { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string PointerType { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string ShowHotspots { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string ListNames { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ToZoom { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string URLField { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string Prefix { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string Postfix { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string Target { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string PointerColorRed { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string PointerColorGreen { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string PointerColorBlue { get; set; }
        [OcadFileSpecialStringMapping("R")]
        public string HotspotColorRed { get; set; }
        [OcadFileSpecialStringMapping("G")]
        public string HotspotColorGreen { get; set; }
        [OcadFileSpecialStringMapping("B")]
        public string HotspotColorBlue { get; set; }
    }
    public class SymTreeSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string GroupId { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string Visible { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string IsNodeExpanded { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string LevelInTree { get; set; }
    }
    public class CryptInfoSSR : OcadFileSpecialStringRecord
    {
    }
    public class BookmarkSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { BookmarkName = value; }
        public string BookmarkName { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Description { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string OffsetX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetY { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string Zoom { get; set; }
    }
    public class SelectionSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { SelectionName = value; }
        public string SelectionName { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string Number { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string ObjectIds { get; set; }
    }
    public class GpsAdjustParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("m")]
        public string GpsAdjustedMode { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string NumberOfGPSAdjustmentPointsGpsAdjustPoint { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string GPSAngle { get; set; }
    }
    public class GpsAdjustPointsSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string OffsetXOnMap { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetYOnMap { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string Longitude { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string Latitude { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Checked { get; set; }
    }
    public class GroupSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string GroupNumber { get; set; }
    }
    public class RecentDocsSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { FileName = value; }
        public string FileName { get; set; }
    }
    public class CsAutoCdAllocationTableSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { MapSymbolNumber = value; }
        public string MapSymbolNumber { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string ControlDescriptionSymbol0 { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string ControlDescriptionSymbol1 { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string ControlDescriptionSymbol2 { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string ControlDescriptionSymbol3 { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string ControlDescriptionSymbol4 { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string ControlDescriptionSymbol5 { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string ControlDescriptionDraggedDirection { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string ControlDescriptionClick { get; set; }
    }
    public class RulerGuidesListSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("h")]
        public string HorizontalGuide { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string XOrYCcordiante { get; set; }
    }
    public class LayoutObjectsSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { PathForImagesDescriptionForVectorObjects = value; }
        public string PathForImagesDescriptionForVectorObjects { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string Type { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string Visible { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AngleOmega { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string AnglePhi { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dim { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Transparent { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string OffsetX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetY { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string PixelSizeX { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string PixelSizeY { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string IsInfraredImage { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string ObjectIndex { get; set; }
    }
    public class LayoutFontAttributesSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { FontName = value; }
        public string FontName { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string FontSize { get; set; }
    }
    public class PrintAndExportRectangleListSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string Bottom { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string Left { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string Right { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Top { get; set; }
    }
    public class DisplayParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("f")]
        public string ShowSymbolFavorites { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string SelectedSymbolGroup { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string SelectedSymbol { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ShowSymbolTree { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HorizontalSplitter { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VerticalSplitter { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string HorizontalSplitterForBackgroundMapPanel { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string DisplayModeForUnsymbolizedObjects { get; set; }
        [OcadFileSpecialStringMapping("j")]
        public string DisplayModeForGraphicObjects { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string DisplayModeForImageObjects { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string DisplayModeForLayoutObjects { get; set; }
    }
    public class OimParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { OpenLayers = value; }
        public string OpenLayers { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AntiAliasing { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string BorderWidth { get; set; }
        [OcadFileSpecialStringMapping("A")]
        public string MapTitle { get; set; }
        [OcadFileSpecialStringMapping("C")]
        public string MapSubtitle { get; set; }
        [OcadFileSpecialStringMapping("D")]
        public string BaseLayerName { get; set; }
        [OcadFileSpecialStringMapping("E")]
        public string EditlayerEnable { get; set; }
        [OcadFileSpecialStringMapping("F")]
        public string SearchboxEnable { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string SearchListboxWithSelection { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string DoNotCreateTiles { get; set; }
        [OcadFileSpecialStringMapping("H")]
        public string ShowLayerSelector { get; set; }
        [OcadFileSpecialStringMapping("I")]
        public string ShowOverviewMap { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string ShowOverviewMapMaximized { get; set; }
        [OcadFileSpecialStringMapping("K")]
        public string ShowMouseCoordinates { get; set; }
        [OcadFileSpecialStringMapping("P")]
        public string ShowPermalink { get; set; }
        [OcadFileSpecialStringMapping("J")]
        public string FontSizeInPt { get; set; }
        [OcadFileSpecialStringMapping("L")]
        public string SubheaderFontsizeInPt { get; set; }
        [OcadFileSpecialStringMapping("R")]
        public string SiteBackgroundColorRed { get; set; }
        [OcadFileSpecialStringMapping("G")]
        public string SiteBackgroundColorGreen { get; set; }
        [OcadFileSpecialStringMapping("B")]
        public string SiteBackgroundColorBlue { get; set; }
        [OcadFileSpecialStringMapping("M")]
        public string BorderColorAsHex { get; set; }
        [OcadFileSpecialStringMapping("N")]
        public string HeaderFontColorAsHexExapmleSeeBorderColor { get; set; }
        [OcadFileSpecialStringMapping("O")]
        public string HeaderBackgroundColorAsHex { get; set; }
        [OcadFileSpecialStringMapping("T")]
        public string SubheaderFontColorAsHex { get; set; }
        [OcadFileSpecialStringMapping("Q")]
        public string SubheaderBackgroundColorAsHex { get; set; }
        [OcadFileSpecialStringMapping("X")]
        public string FilenameOfLastSafeplace { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string GenerateMapFromZoomlevel { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string GenerateMapToZoomlevel { get; set; }
    }
    public class PrintParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string PrintScale { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string Landscape { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Print { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string PrintScreenGrid { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string ScreenGridColor { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string Intensity { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string AdditionalWidthForLinesAndDots { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string Range { get; set; }
        [OcadFileSpecialStringMapping("L")]
        public string PartOfMapLeft { get; set; }
        [OcadFileSpecialStringMapping("B")]
        public string PartOfMapBottom { get; set; }
        [OcadFileSpecialStringMapping("R")]
        public string PartOfMapRight { get; set; }
        [OcadFileSpecialStringMapping("T")]
        public string PartOfMapTop { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string HorizontalOverlap { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string VerticalOverlap { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string PrintBlack { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string Mirror { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string HorScal { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string VerScal { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string ReferencePointForPartOfMapSetup { get; set; }
    }
    public class CdPrintParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("s")]
        public string Size { get; set; }
    }
    public class DefaultBackgroundMapsParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("s")]
        public string DefaultScale { get; set; }
    }
    public class EpsParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("r")]
        public string EPSResolution { get; set; }
    }
    public class ViewParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("b")]
        public string DraftModeIGNForOcadMap { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string DraftModeIGNBackgroundMaps { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string HiddenBackgroundMaps { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string DraftModeForOcadMap { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string DraftModeBackgroundMaps { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string ViewMode { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string OffsetX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string OffsetY { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string Zoom { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HatchAreas { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string KeylineMode { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string MapVisible { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string HiddenLayout { get; set; }
    }
    public class CourseParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string CreateClassesAutomatically { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string BackgroundForControlDescription { get; set; }
        [OcadFileSpecialStringMapping("B")]
        public string DrawWhiteBackgroundAlsoInDraftMode { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Numbering { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string ControlDescriptionsForAllControls { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string EventTitle { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string ControlFrequencies { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string ThickerHorizonalLineInControlDescription { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string MaximalLengthOfControlDescription { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string DistanceToConnectionLine { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string DistanceToNumber { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string LockForCsObjectPositions { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string FullstopAfterControlNumber { get; set; }
        [OcadFileSpecialStringMapping("q")]
        public string LockForCsCourses { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ExportRelayCombinations { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string CellSizeControlDescription { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string CourseTitle { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string PunchingSystem { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string WriteNumberOfStartToControlDescription { get; set; }
    }
    public class TiffParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("c")]
        public string Compression { get; set; }
    }
    public class TilesParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("w")]
        public string Width { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string Height { get; set; }
    }
    public class DbParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("d")]
        public string Dataset { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LastCode { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string CreateNewRecord { get; set; }
    }
    public class ExportParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Format = value; }
        public string Format { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AntiAliasing { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string CombinedSpotColors { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string ColorFormat { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string RasterExportMode { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string PixelSize { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string ColorCorrection { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string SpotColorSeparations { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string PartOfMap { get; set; }
        [OcadFileSpecialStringMapping("q")]
        public string JPEGQuality { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string Resolution { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string Scale { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Tiles { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string WorldFile { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string CompressedSvgFile { get; set; }
    }
    public class CsExpTextParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("C")]
        public string Classes { get; set; }
        [OcadFileSpecialStringMapping("L")]
        public string ExportClimbing { get; set; }
    }
    public class CsExpStatParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("C")]
        public string Classes { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string Separator1 { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string Tab1 { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Separator2 { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Tab2 { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string Separator3 { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string Tab3 { get; set; }
    }
    public class ScaleParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string RealWorldAngle { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string AdditionalLocalEastingOffsetInM { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string AdditionalLocalNorthingOffsetInM { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string GridDistanceForRealWorldInM { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string GridDistanceForPaperCoordinatesInMm { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string GridAndZone { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string MapScale { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string RealWorldCord { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string RealWorldOffsetEasting { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string RealWorldOffsetNorthing { get; set; }
    }
    public class DbCreateObjParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("c")]
        public string Condition { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dataset { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string TextField { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string UnitOfMeasure { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string HorizontalOffset { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VerticalOffset { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string HorizontalField { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string VerticalField { get; set; }
    }
    public class SelectedSpotColorsSSR : OcadFileSpecialStringRecord
    {
    }
    public class XmlScriptParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("f")]
        public string LastUsedFile { get; set; }
    }
    public class BackupParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("p")]
        public string Path { get; set; }
    }
    public class ExportPartOfMapParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("b")]
        public string Boundary { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string ExportWithSelectedObject { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ReferencePointForPartOfMapSetup { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string ExportDatabaseLinks { get; set; }
    }
    public class DemParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { File = value; }
        public string File { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string DEMLoaded { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string FrameVisible { get; set; }
        [OcadFileSpecialStringMapping("i")]
        public string LastUsedImportFolder { get; set; }
    }
    public class GpsImportFileParSsr : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string AssignSymbol { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string SymbolNumberForTracks { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string SymbolNumberForWaypoint { get; set; }
    }
    public class ImportXyzSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("n")]
        public string PointSymbolNumber { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string TextSymbolNumber { get; set; }
    }
    public class RelayCoursesDialogSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { LastExportedOrPrintedCourse = value; }
        public string LastExportedOrPrintedCourse { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string Legs { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string VariantsSelected { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string StartNumber { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VariantsIndex { get; set; }
    }
    public class CsAutoControlDescriptionSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { OcdBackgroundMapFileNameForAutoControlDescription = value; }
        public string OcdBackgroundMapFileNameForAutoControlDescription { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AutoControlDescription { get; set; }
    }
    public class GpxExportParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Description = value; }
        public string Description { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string Author { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string Keywords { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string RoutesOrTracks { get; set; }
    }
    public class KmlInfoSSR : OcadFileSpecialStringRecord
    {
    }
    public class GpsRouteAnalyzerSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { ProjectFile = value; }
        public string ProjectFile { get; set; }
    }
    public class CoordinateSystemParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Description = value; }
        public string Description { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string DatumId { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string EllipsoidId { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string EllipsoidAxis { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string EllipsoidFlattening { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ProjectionId { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string FalseEasting { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string FalseNorthing { get; set; }
        [OcadFileSpecialStringMapping("f")]
        public string ScaleFactor { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string CentralMeridian { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string LongitudeOfOrigin { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string LatitudeOfOrigin { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string StandardParallel1 { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string StandardParallel2 { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string Location { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string Azimuth { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string EPSGCode { get; set; }
    }
    public class GraticuleParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("h")]
        public string HorizontalDistance { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VerticalDistance { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string SymbolNumberForGridLines { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string SymbolNumberForTextLabels { get; set; }
    }
    public class GraticuleNameIndexParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("s")]
        public string Style { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string IndexOriginOfLongitude { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string IndexOriginOfLatitude { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HorizontalDistance { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VerticalDistance { get; set; }
    }
    public class KmzExportParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Name = value; }
        public string Name { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string Tiles { get; set; }
    }
    public class LegendParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("u")]
        public string ShowOnlyUsedSymbols { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string ShowAlsoHiddenSymbols { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string IconHeight { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string IconWidth { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string LineSpacing100 { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string ShowPointSymbolsP { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string ShowPointSymbolsL { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string ShowPointSymbolsA { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ShowPointSymbolsR { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ShowPointSymbolsT { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string ShowPointSymbolsZ { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string DescriptionSymbol { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string ShowSymbolNumber { get; set; }
    }
    public class RulersParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("s")]
        public string Show { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string RulerOriginX { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string RulerOriginY { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string MoveAlsoRulerGuides { get; set; }
    }
    public class RulerGuidesParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("s")]
        public string Show { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string Lock { get; set; }
    }
    public class DbOptionsSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("c")]
        public string CreateRecordWhenCuttingObject { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string DeleteRecordWhenDeletingObject { get; set; }
    }
    public class MapNotesSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Text = value; }
        public string Text { get; set; }
    }
    public class SendFileByEmailSsr : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { Subject = value; }
        public string Subject { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string To { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string AddLoadedDEM { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string AddLoadedDatabasesA { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string AddLoadedDatabasesB { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string AddLoadedDatabasesL { get; set; }
    }
    public class MapGridParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string Angle { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string EastingOffset { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string NorthingOffset { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HorizontalDistance { get; set; }
        [OcadFileSpecialStringMapping("v")]
        public string VerticalDistance { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string CreateEastingGridLines { get; set; }
        [OcadFileSpecialStringMapping("y")]
        public string CreateNorthingGridLines { get; set; }
        [OcadFileSpecialStringMapping("j")]
        public string CreateVerticesAtGridJunctions { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string SymbolNumberForGridLines { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string GridLabels { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string SymbolNumberForTextLabels { get; set; }
    }
    public class DemSlopeParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("m")]
        public string Method { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string SlopeForBlackPixelsInContinuousMethod { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string SlopeForBlackPixelsInBlackWhiteMethod { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LoadSlopeMapAsBackgroundMap { get; set; }
    }
    public class DemProfileParSSR : OcadFileSpecialStringRecord
    {
        public override void SetFirst(string value) { ProfileTemplatePath = value; }
        public string ProfileTemplatePath { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string ScaleOption { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Scale { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string DistanceUnit { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string ShowGrid { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string TimeUnit { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string RoundTimeSteps { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ShowTextAtTheBottomOfTheProfile { get; set; }
        [OcadFileSpecialStringMapping("x")]
        public string LengthResolution { get; set; }
        [OcadFileSpecialStringMapping("z")]
        public string ElevationResolution { get; set; }
        [OcadFileSpecialStringMapping("k")]
        public string ElevationFactor { get; set; }
    }
    public class DemHillshadingParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("m")]
        public string Method { get; set; }
        [OcadFileSpecialStringMapping("a")]
        public string Azimut { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Declination { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string Exaggeration { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LoadHillshadingAsBackgroundMap { get; set; }
    }
    public class DemHypsometricMapParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("m")]
        public string Method { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LoadHillshadingAsBackgroundMap { get; set; }
    }
    public class DemClassifyVegetationParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("m")]
        public string Method { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LoadHillshadingAsBackgroundMap { get; set; }
    }
    public class ShapeExportParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string ItemAreaObjectsSelected { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string CreateProjectionFile { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string Dataset { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string ItemLineObjectsSelected { get; set; }
        [OcadFileSpecialStringMapping("p")]
        public string ItemPointObjectsSelected { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ReplaceWordWrapByTilde { get; set; }
        [OcadFileSpecialStringMapping("t")]
        public string ItemTextObjectsSelected { get; set; }
        [OcadFileSpecialStringMapping("u")]
        public string UTF8Encoding { get; set; }
    }
    public class DxfExportParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("a")]
        public string ConvertTextFromANSIToOEM { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string ConvertTextFromOEMToUnicode { get; set; }
        [OcadFileSpecialStringMapping("s")]
        public string OnlyObjectsWithSelectedSymbol { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string AddSymbolDescriptionInDXFLayerName { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string ExportOCADCurvesAsDXFSplines { get; set; }
        [OcadFileSpecialStringMapping("c")]
        public string Coordinates { get; set; }
    }
    public class DemImportLasParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("u")]
        public string Unclassified { get; set; }
        [OcadFileSpecialStringMapping("g")]
        public string Ground { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string LowVegetation { get; set; }
        [OcadFileSpecialStringMapping("m")]
        public string MeanVegetation { get; set; }
        [OcadFileSpecialStringMapping("h")]
        public string HighVegetation { get; set; }
        [OcadFileSpecialStringMapping("b")]
        public string Buildings { get; set; }
        [OcadFileSpecialStringMapping("w")]
        public string WaterClass { get; set; }
        [OcadFileSpecialStringMapping("o")]
        public string OtherClass { get; set; }
        [OcadFileSpecialStringMapping("r")]
        public string ReturnOption { get; set; }
    }
    public class MapRoutingParSSR : OcadFileSpecialStringRecord
    {
        [OcadFileSpecialStringMapping("f")]
        public string From { get; set; }
        [OcadFileSpecialStringMapping("l")]
        public string FromLocation { get; set; }
        [OcadFileSpecialStringMapping("e")]
        public string FromEasting { get; set; }
        [OcadFileSpecialStringMapping("n")]
        public string FromNorthing { get; set; }
        [OcadFileSpecialStringMapping("T")]
        public string To { get; set; }
        [OcadFileSpecialStringMapping("L")]
        public string ToLocation { get; set; }
        [OcadFileSpecialStringMapping("E")]
        public string ToEasting { get; set; }
        [OcadFileSpecialStringMapping("N")]
        public string ToNorthing { get; set; }
        [OcadFileSpecialStringMapping("d")]
        public string AddDrivingDirections { get; set; }
    }


}