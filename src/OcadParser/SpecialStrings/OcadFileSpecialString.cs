using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OcadParser
{
    public class OcadFileSpecialString
    {
        public int RecordType { get; private set; }
        public string OriginalString { get; private set; }
        

        public OcadFileSpecialStringRecord Record { get; private set; }

        public OcadFileSpecialString(IEnumerable<byte> bytes, int recordType)
        {
            RecordType = recordType;
            OriginalString = System.Text.Encoding.UTF8.GetString(bytes.ToArray());
            OriginalString = OriginalString.TrimEnd('\0');

            var fields = new Dictionary<string, string>();
            var parts = OriginalString.Split('\t');
            var first = parts[0];
            for (var i = 1; i < parts.Length; i++)
            {
                var part = parts[i].Trim();
                if (!string.IsNullOrEmpty(part))
                {
                    fields[part[0].ToString()] = part.Substring(1);
                }
            }

            if (recordTypes.ContainsKey(RecordType))
            {
                Record = recordTypes[RecordType]();
                Record.SetFirst(first);
                foreach (var property in Record.GetType().GetProperties())
                {
                    var attr = 
                        property.GetCustomAttributes()
                            .OfType<OcadFileSpecialStringMappingAttribute>()
                            .FirstOrDefault();
                    if (attr != null)
                    {
                        if (fields.ContainsKey(attr.Letter))
                        {
                            property.SetValue(Record, fields[attr.Letter]);
                        }
                    }
                }
            }
        }

        private static Dictionary<int, Func<OcadFileSpecialStringRecord>> recordTypes = new Dictionary
            <int, Func<OcadFileSpecialStringRecord>>()
        {
            {1, () => new CsObjectSSR()},
            {2, () => new CourseSSR()},
            {3, () => new CsClassSSR()},
            {4, () => new DataSetSSR()},
            {5, () => new DbObjectSSR()},
            {6, () => new OimFileSsr()},
            {7, () => new PrevObjSSR()},
            {8, () => new BackgroundMapSSR()},
            {9, () => new ColorSSR()},
            {10, () => new SpotColorSSR()},
            {11, () => new FileInfoOcad10Ssr()}, //replace in OCAD 11 by si_MapNotes = 1061              
            {12, () => new ZoomSSR()},
            {13, () => new ImpLayerSSR()},
            {14, () => new OimFindSSR()},
            {15, () => new SymTreeSSR()},
            {16, () => new CryptInfoSSR()},
            {18, () => new BookmarkSSR()},
            {19, () => new SelectionSSR()},
            {21, () => new GpsAdjustParSSR()},
            {22, () => new GpsAdjustPointsSSR()},
            {23, () => new GroupSSR()},
            {24, () => new RecentDocsSSR()},
            {25, () => new CsAutoCdAllocationTableSSR()},
            {26, () => new RulerGuidesListSSR()},
            {27, () => new LayoutObjectsSSR()},
            {28, () => new LayoutFontAttributesSSR()},
            {29, () => new PrintAndExportRectangleListSSR()},
            {1024, () => new DisplayParSSR()},
            {1025, () => new OimParSSR()},
            {1026, () => new PrintParSSR()},
            {1027, () => new CdPrintParSSR()},
            {1028, () => new DefaultBackgroundMapsParSSR()},
            {1029, () => new EpsParSSR()},
            {1030, () => new ViewParSSR()},
            {1031, () => new CourseParSSR()},
            {1032, () => new TiffParSSR()},
            {1033, () => new TilesParSSR()},
            {1034, () => new DbParSSR()},
            {1035, () => new ExportParSSR()},
            {1037, () => new CsExpTextParSSR()},
            {1038, () => new CsExpStatParSSR()},
            {1039, () => new ScaleParSSR()},
            {1040, () => new DbCreateObjParSSR()},
            {1041, () => new SelectedSpotColorsSSR()},
            {1042, () => new XmlScriptParSSR()},
            {1043, () => new BackupParSSR()},
            {1044, () => new ExportPartOfMapParSSR()},
            {1045, () => new DemParSSR()},
            {1046, () => new GpsImportFileParSsr()},
            {1047, () => new ImportXyzSSR()},
            {1048, () => new RelayCoursesDialogSSR()},
            {1049, () => new CsAutoControlDescriptionSSR()},
            {1050, () => new GpxExportParSSR()},
            {1051, () => new KmlInfoSSR()},
            {1052, () => new GpsRouteAnalyzerSSR()},
            {1053, () => new CoordinateSystemParSSR()},
            {1054, () => new GraticuleParSSR()},
            {1055, () => new GraticuleNameIndexParSSR()},
            {1056, () => new KmzExportParSSR()},
            {1057, () => new LegendParSSR()},
            {1058, () => new RulersParSSR()},
            {1059, () => new RulerGuidesParSSR()},
            {1060, () => new DbOptionsSSR()},
            {1061, () => new MapNotesSSR()},
            {1062, () => new SendFileByEmailSsr()},
            {1063, () => new MapGridParSSR()},
            {1064, () => new DemSlopeParSSR()},
            {1065, () => new DemProfileParSSR()},
            {1066, () => new DemHillshadingParSSR()},
            {1067, () => new DemHypsometricMapParSSR()},
            {1068, () => new DemClassifyVegetationParSSR()},
            {1069, () => new ShapeExportParSSR()},
            {1070, () => new DxfExportParSSR()},
            {1071, () => new DemImportLasParSSR()},
            {1072, () => new MapRoutingParSSR()}
        };
    }
}