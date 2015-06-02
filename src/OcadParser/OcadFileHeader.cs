namespace OcadParser
{
    using System;

    public class OcadFileHeader : IBinaryParsable<OcadFileHeader>
    {
        public Int16 OcadMark { get; set; }

        public Byte FileType { get; set; }

        public byte FileStatus { get; set; }

        public Int16 Version { get; set; }

        public Byte SubVersion { get; set; }

        public Byte SubSubVersion { get; set; }

        public int PositionFirstSymbolIndexBlock { get; set; }

        public int PositionObjectIndexBlock { get; set; }

        public int OfflineSyncSerial { get; set; } // serialNumber for offline work in Server mode

        public int CurrentFileVersion { get; set; } // file version for OCAD version

        public long Res2 { get; set; } // not used

        public long Res3 { get; set; } // not used

        public long PositionFirstStringIndexBlock { get; set; }

        public int PositionFileName { get; set; } // file position of the file name, used for temporary files only

        public int FileNameSize { get; set; } // size of the file name, used for temporary files only

        public int Res4 { get; set; } // not used

        public void SetupBinaryParser(BinaryParser<OcadFileHeader> parser)
        {
            parser.SetPropertyOrder(
                (_ => _.OcadMark),
                (_ => _.FileType),
                (_ => _.FileStatus),
                (_ => _.Version),
                (_ => _.SubVersion),
                (_ => _.SubSubVersion),
                (_ => _.PositionFirstSymbolIndexBlock),
                (_ => _.PositionObjectIndexBlock),
                (_ => _.OfflineSyncSerial),
                (_ => _.CurrentFileVersion),
                (_ => _.Res2),
                (_ => _.Res3),
                (_ => _.PositionFirstStringIndexBlock),
                (_ => _.PositionFileName),
                (_ => _.FileNameSize),
                (_ => _.Res4));
        }
    }
}