using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcadParser.Testing
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new FileStream(@"C:\Users\Yannis\SkyDrive\OL\OLGM\MurtnerOl\Murtner OL.ocd", FileMode.Open))
            {
                var reader = new OcadStreamReader(stream);
                var parser = new BinaryParser<OcadFile>();

                var file = parser.Read(reader);
            }
        }
    }
}
