using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcadParser
{
    public interface IOcadParser
    {
        OcadFileHeader ReadHeader();
    }

    public class Ocad11Parser : IOcadParser
    {
        private readonly OcadStreamReader reader;

        public Ocad11Parser(OcadStreamReader streamReader)
        {
            this.reader = streamReader;
        }

      

        public OcadFileHeader ReadHeader()
        {
            OcadFileHeader h = new OcadFileHeader
                                   {
                                       OcadMark = this.reader.ReadSmallInt(),
                                       FileType = this.reader.ReadByte(),
                                       FileStatus = this.reader.ReadByte(),
                                       Version = this.reader.ReadSmallInt(),
                                       SubVersion = this.reader.ReadByte(),
                                       SubSubVersion = this.reader.ReadByte(),
                                       PositionFirstSymbolIndexBlock = this.reader.ReadInt(),
                                       PositionObjectIndexBlock = this.reader.ReadInt(),
                                       OfflineSyncSerial = this.reader.ReadInt(),
                                       CurrentFileVersion = this.reader.ReadInt(),
                                       Res2 = this.reader.ReadLong(),
                                       Res3 = this.reader.ReadLong(),
                                       PositionFirstStringIndexBlock = this.reader.ReadLong(),
                                       PositionFileName = this.reader.ReadInt(),
                                       FileNameSize = this.reader.ReadInt(),
                                       Res4 = this.reader.ReadInt()
                                   };
            return h;
        }
    }

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
