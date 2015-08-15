using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcadParser.Models;
using OcadParser.Models.CourseSetting;
using OcadParser.Models.OMap;

namespace OcadParser
{
    public class OcadFileReader
    {
        private readonly string _filePath;

        public OcadFileReader(string filePath)
        {
            _filePath = filePath;
        }

        public OcadFile ReadFile()
        {
            using (var stream = new FileStream(_filePath, FileMode.Open))
            {
                var reader = new OcadStreamReader(stream);
                var parser = new BinaryParser<OcadFile>();

                return parser.Read(reader);
            }
        }

        public OcadBaseProject ReadProject()
        {
            var file = ReadFile();
            OcadBaseProject project;
            if (file.FileHeader.FileType == 0)
            {
                project = new OcadOMap();
            }
            else if (file.FileHeader.FileType == 1)
            {
                project = new OcadCourseSettingProject();
            }
            else
            {
                throw new Exception("Only normal OCAD maps or course setting files are supported.");
            }

            project.Load(file);
            return project;
        }
    }
}
