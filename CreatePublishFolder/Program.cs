using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePublishFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = DateTime.Now.ToString("yyyy-MM-dd HHmmss");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
