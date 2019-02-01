using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using NPOI.SS.UserModel;        //NPOI
using NPOI.XSSF.UserModel;      //NPOI
using System.Threading;
using static ToolExcel.XGlobal;

namespace ToolExcel
{
    public static partial class XTool
    {
        public static void TransferClassClientLua()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n\r\n");
            sb.Append($"using XLua;\r\n\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端))
                {
                    continue;
                }
                sb.Append($"[CSharpCallLua]\r\n");
                sb.Append($"public interface {nn.Name}\r\n{{\r\n");
                for (int iii = 0; iii < nn.Head.Count; iii++)
                {
                    var head = nn.Head[iii];
                    if (head == "")
                    {
                        continue;
                    }
                    if (nn.TypeClient[iii] == "")
                    {
                        continue;
                    }
                    if (head.IsLang())
                    {
                        if (head.IsLangCur())
                        {
                            head = StrLang;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    sb.Append($"\t{nn.TypeClient[iii]} {head} {{ get; }}\r\n");
                }
                sb.Append($"}}\r\n");
            }

            foreach (var kvp in DictDictEnums)
            {
                sb.Append($"public enum E{kvp.Key}\r\n");
                sb.Append($"{{\r\n");
                long min = 0, max = 0;
                foreach (var kvp2 in kvp.Value)
                {
                    sb.Append($"\t{kvp2.Key} = {kvp2.Value},\r\n");
                    long.TryParse(kvp2.Value, out long tempmax);
                    min = Math.Min(min, tempmax);
                    max = Math.Max(max, tempmax);
                }
                sb.Append($"}}\r\n");
            }

            string path_class = PathClass + "\\" + "ClassClientLua.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class);
            sw_class.Write(sb);
            sw_class.Close();
        }
    }
}
