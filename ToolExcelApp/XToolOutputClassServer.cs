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
        public static void 导出ClassServer()
        {
            string path_class = PathClass + "\\" + "ClassServer.cs";
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
            sbw.Append($"namespace ServerBase.Config\r\n{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                var sbnode = new StringBuilder();
                sbnode.Append($"\tpublic class {nn.Name} : IConfigBase\t// {nn.NameFile}\r\n\t{{\r\n");
                for (int iii = 0; iii < nn.Head.Count; iii++)
                {
                    if (nn.Head[iii] == "")
                    {
                        continue;
                    }
                    if (nn.TypeServer[iii] == "")
                    {
                        continue;
                    }
                    string tp = nn.TypeServer[iii];
                    switch (tp)
                    {
                        case "List<string>":
                        case "List<int>":
                        case "List<long>":
                        case "List<float>":
                        case "Dictionary<string,string>":
                        case "Dictionary<int,int>":
                        case "Dictionary<long,long>":
                        case "Dictionary<string, string>":
                        case "Dictionary<int, int>":
                        case "Dictionary<long, long>":
                            sbnode.Append($"\t\tpublic {tp} {nn.Head[iii]} = new {tp}(); //{nn.HeadC[iii]}\r\n");
                            break;
                        default:
                            sbnode.Append($"\t\tpublic {nn.TypeServer[iii]} {nn.Head[iii]}; //{nn.HeadC[iii]}\r\n");
                            break;
                    }
                }
                string getkey = "Id";
                try { getkey = nn.Head[0]; } catch { }
                sbnode.Append($"\t\tpublic object GetKey() {{ return {getkey}; }}\r\n");
                sbnode.Append($"\t}}\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"//Class{nn.Name}Start\r\n";
                string stnode2 = $"//Class{nn.Name}End\r\n";
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
                sbnode.Append($"\tpublic enum E{kvp.Key}\r\n");
                sbnode.Append($"\t{{\r\n");
                long min = 0, max = 0;
                foreach (var kvp2 in kvp.Value)
                {
                    sbnode.Append($"\t\t{kvp2.Key} = {kvp2.Value},\r\n");
                    long.TryParse(kvp2.Value, out long tempmax);
                    min = Math.Min(min, tempmax);
                    max = Math.Max(max, tempmax);
                }
                sbnode.Append($"\t}}\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"//Enum{kvp.Key}Start\r\n";
                string stnode2 = $"//Enum{kvp.Key}End\r\n";
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
            sbw.Append($"}}\r\n");

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
        public static void 导出ClassServerConf()
        {
            string path_class = PathClass + "\\" + "ClassServerConf.cs";
            string stread = "";
            if (!全部导出)
            {
                FileStream fsr_class = new FileStream(path_class, FileMode.Open);
                StreamReader sr_class = new StreamReader(fsr_class, Encoding.Unicode);
                stread = sr_class.ReadToEnd();
                sr_class.Close();
            }

            var sbw = new StringBuilder();

            sbw.Append($"using System.Collections.Generic;\r\n");
            sbw.Append($"using UtilLib;\r\n\r\n");
            sbw.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sbw.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                var sbnode = new StringBuilder();
                sbnode.Append($"\t\t//{nn.NameCn}\t// {nn.NameFile}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                var keytype = nn.TypeServer[0];
                sbnode.Append($"\t\tpublic static Dictionary<{keytype}, {nn.Name}> {confname} = new Dictionary<{keytype}, {nn.Name}>();\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"//Class{nn.Name}Start\r\n";
                string stnode2 = $"//Class{nn.Name}End\r\n";
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

            sbw.Append($"\t}}\r\n");
            sbw.Append($"}}\r\n");

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
        public static void 导出ClassServerConfRead()
        {
            string path_class = PathClass + "\\" + "ClassServerConfRead.cs";
            string stread = "";
            if (!全部导出)
            {
                FileStream fsr_class = new FileStream(path_class, FileMode.Open);
                StreamReader sr_class = new StreamReader(fsr_class, Encoding.Unicode);
                stread = sr_class.ReadToEnd();
                sr_class.Close();
            }

            var sbw = new StringBuilder();

            sbw.Append($"using System.Collections.Generic;\r\n");
            sbw.Append($"using UtilLib;\r\n\r\n");
            sbw.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sbw.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");
            sbw.Append($"\t\tpublic static bool InitConfSettings()\r\n\t\t{{\r\n");

            sbw.Append($"\t\t\tbool result = true;\r\n\r\n");
            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                var sbnode = new StringBuilder();
                sbnode.Append($"\t\t\t//{nn.NameCn}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                sbnode.Append($"\t\t\tif (result) {{ result = ReadConfig(typeof({nn.Name}).Name, ref {confname}); }}\r\n");
                string stnode = sbnode.ToString();
                string stnode1 = $"//Class{nn.Name}Start\r\n";
                string stnode2 = $"//Class{nn.Name}End\r\n";
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
            sbw.Append($"\r\n\t\t\tif (result) {{ result = InitConfSettingsExt(); }}\r\n\r\n");
            sbw.Append($"\t\t\treturn result;\r\n");

            sbw.Append($"\t\t}}\r\n");
            sbw.Append($"\t}}\r\n");
            sbw.Append($"}}\r\n");

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
