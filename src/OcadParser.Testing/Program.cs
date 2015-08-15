using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcadParser.Models.CourseSetting;

namespace OcadParser.Testing
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            OcadCourseSettingProject project = new OcadCourseSettingProject();
            using (var stream = new FileStream(@"D:\temp\target.ocd", FileMode.Open))
            {
                var reader = new OcadStreamReader(stream);
                var parser = new BinaryParser<OcadFile>();
                
                var file = parser.Read(reader);
                project.Load(file);
            }

            foreach (var symbol in project.Symbols)
            {
                Console.WriteLine(symbol.Description);
            }
        }
    }
}
