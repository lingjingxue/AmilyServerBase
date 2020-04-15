using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static StringBuilder sbJsonJavaScript = new StringBuilder();

        public static void 导出ClassClientJavaScript()
        {
            if (true)
            {
                string path_class = PathClass + "\\" + "ConfClientConf.js";
                XGlobal.DeleteFile(path_class);
                FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
                StreamWriter sw_class = new StreamWriter(fs_class);
                sw_class.Write(sbJsonJavaScript);
                sw_class.Close();
            }
            if (true)
            {
                var sbenum = new StringBuilder();
                foreach (var kvp in DictDictEnum1)
                {
                    sbenum.Append($"var ConfEnum{kvp.Key} = {{\r\n");
                    long min = 0, max = 0;
                    foreach (var kvp2 in kvp.Value)
                    {
                        sbenum.Append($"\t{kvp2.Key}: {kvp2.Value},\r\n");
                        long.TryParse(kvp2.Value, out long tempmax);
                        min = Math.Min(min, tempmax);
                        max = Math.Max(max, tempmax);
                    }
                    sbenum.Append($"}};\r\n");
                }
                foreach (var kvp in DictDictEnum1)
                {
                    sbenum.Append($"var ConfEnumMap{kvp.Key} = {{\r\n");
                    long min = 0, max = 0;
                    foreach (var kvp2 in kvp.Value)
                    {
                        sbenum.Append($"\t{kvp2.Value}: \"{kvp2.Key}\",\r\n");
                        long.TryParse(kvp2.Value, out long tempmax);
                        min = Math.Min(min, tempmax);
                        max = Math.Max(max, tempmax);
                    }
                    sbenum.Append($"}};\r\n");
                }

                string path_class = PathClass + "\\" + "ConfClientEnum.js";
                XGlobal.DeleteFile(path_class);
                FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
                StreamWriter sw_class = new StreamWriter(fs_class);
                sw_class.Write(sbenum);
                sw_class.Close();
            }
        }
    }
}
