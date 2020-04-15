using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static void 导出ClassClientLua()
        {
            string path_class = PathClass + "\\" + "ClassClientLua.cs";
            string stread = "";
            if (!全部导出)
            {
                FileStream fsr_class = new FileStream(path_class, FileMode.Open);
                StreamReader sr_class = new StreamReader(fsr_class, Encoding.Unicode);
                stread = sr_class.ReadToEnd();
                sr_class.Close();
            }

            var sbw = new StringBuilder();

            sbw.Append($"using System.Collections.Generic;\r\n\r\n");
            sbw.Append($"using XLua;\r\n\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端))
                {
                    continue;
                }
                var sbnode = new StringBuilder();
                sbnode.Append($"[CSharpCallLua]\r\n");
                sbnode.Append($"public interface {nn.Name}\r\n{{\r\n");
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
                    sbnode.Append($"\t{nn.TypeClient[iii]} {head} {{ get; }}\r\n");
                }
                sbnode.Append($"}}\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"--Class{nn.Name}Start\r\n";
                string stnode2 = $"--Class{nn.Name}End\r\n";
                if (全部导出)
                {
                    sbw.Append(stnode1);
                    sbw.Append(stnode);
                    sbw.Append(stnode2);
                }
                else
                {
                    string stnodeold = stread.GetMidStr(stnode1, stnode2);
                    stread = stread.Replace(stnodeold, stnode);
                }
            }
            
            Dictionary<string, Dictionary<string, string>> DictDictEnums = new Dictionary<string, Dictionary<string, string>>();
            foreach (var kvp in DictDictEnum1) { DictDictEnums[kvp.Key] = kvp.Value; }
            foreach (var kvp in DictDictEnum2) { DictDictEnums[kvp.Key] = kvp.Value; }
            foreach (var kvp in DictDictEnums)
            {
                var sbnode = new StringBuilder();
                sbnode.Append($"public enum E{kvp.Key}\r\n");
                sbnode.Append($"{{\r\n");
                long min = 0, max = 0;
                foreach (var kvp2 in kvp.Value)
                {
                    sbnode.Append($"\t{kvp2.Key} = {kvp2.Value},\r\n");
                    long.TryParse(kvp2.Value, out long tempmax);
                    min = Math.Min(min, tempmax);
                    max = Math.Max(max, tempmax);
                }
                sbnode.Append($"}}\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"--Enum{kvp.Key}Start\r\n";
                string stnode2 = $"--Enum{kvp.Key}End\r\n";
                if (全部导出)
                {
                    sbw.Append(stnode1);
                    sbw.Append(stnode);
                    sbw.Append(stnode2);
                }
                else
                {
                    string stnodeold = stread.GetMidStr(stnode1, stnode2);
                    stread = stread.Replace(stnodeold, stnode);
                }
            }

            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            if (全部导出)
            {
                sw_class.Write(sbw);
            }
            else
            {
                sw_class.Write(stread);
            }
            sw_class.Close();
        }
    }
}
