using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcadParser.Renderer;

namespace OcadParser.Samples.Renderer
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = new OcadFileReader(@"D:\temp\target.ocd").ReadProject();
            var renderer = new OcadRenderer(project);
            Bitmap bitmap = renderer.GetBitmap();
            bitmap.Save(@"D:\temp\target.png");
        }
    }
}
