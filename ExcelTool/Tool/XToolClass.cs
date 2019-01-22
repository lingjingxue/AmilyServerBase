using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;        //NPOI
using NPOI.XSSF.UserModel;      //NPOI
using System.Threading;
using static ExcelTool.XGlobal;

namespace ExcelTool
{
    public static partial class XTool
    {
        public static void TransferClassServer()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                sb.Append($"\tpublic class {nn.Name} : IConfigBase\t// {nn.NameFile}\r\n\t{{\r\n");
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
                            sb.Append($"\t\tpublic {tp} {nn.Head[iii]} = new {tp}(); //{nn.HeadC[iii]}\r\n");
                            break;
                        default:
                            sb.Append($"\t\tpublic {nn.TypeServer[iii]} {nn.Head[iii]}; //{nn.HeadC[iii]}\r\n");
                            break;
                    }
                }
                string getkey = "Id";
                try { getkey = nn.Head[0]; } catch { }
                sb.Append($"\t\tpublic object GetKey() {{ return {getkey}; }}\r\n");
                sb.Append($"\t}}\r\n");
            }

            foreach (var kvp in DictListEnums)
            {
                sb.Append($"\tpublic enum E{kvp.Key}\r\n");
                sb.Append($"\t{{\r\n");
                long min = 0, max = 0;
                foreach (var kvp2 in kvp.Value)
                {
                    sb.Append($"\t\t{kvp2.Key} = {kvp2.Value},\r\n");
                    long.TryParse(kvp2.Value, out long tempmax);
                    min = Math.Min(min, tempmax);
                    max = Math.Max(max, tempmax);
                }
                sb.Append($"\t}}\r\n");
            }
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassServer.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }
        public static void TransferClassServerConf()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n");
            sb.Append($"using UtilLib;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sb.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                sb.Append($"\t\t//{nn.NameCn}\t// {nn.NameFile}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                var keytype = nn.TypeServer[0];
                sb.Append($"\t\tpublic static Dictionary<{keytype}, {nn.Name}> {confname} = new Dictionary<{keytype}, {nn.Name}>();\r\n");
            }

            sb.Append($"\t}}\r\n");
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassServerConf.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }
        public static void TransferClassServerConfRead()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n");
            sb.Append($"using UtilLib;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sb.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");
            sb.Append($"\t\tpublic static bool InitConfSettings()\r\n\t\t{{\r\n");

            sb.Append($"\t\t\tbool result = true;\r\n\r\n");
            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器))
                {
                    continue;
                }
                sb.Append($"\t\t\t//{nn.NameCn}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                sb.Append($"\t\t\tif (result) {{ result = ReadConfig(typeof({nn.Name}).Name, ref {confname}); }}\r\n");
            }
            sb.Append($"\r\n\t\t\tif (result) {{ result = InitConfSettingsExt(); }}\r\n\r\n");
            sb.Append($"\t\t\treturn result;\r\n");

            sb.Append($"\t\t}}\r\n");
            sb.Append($"\t}}\r\n");
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassServerConfRead.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }
        public static void TransferClassClient()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端))
                {
                    continue;
                }
                sb.Append($"\tpublic class {nn.Name} : IConfigBase\t// {nn.NameFile}\r\n\t{{\r\n");
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
                    string tp = nn.TypeClient[iii];
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
                            sb.Append($"\t\tpublic {tp} {head} = new {tp}(); //{nn.HeadC[iii]}\r\n");
                            break;
                        default:
                            sb.Append($"\t\tpublic {nn.TypeClient[iii]} {head}; //{nn.HeadC[iii]}\r\n");
                            break;
                    }
                }
                string getkey = "Id";
                try { getkey = nn.Head[0]; } catch { }
                sb.Append($"\t\tpublic object GetKey() {{ return {getkey}; }}\r\n");
                sb.Append($"\t}}\r\n");
            }

            foreach (var kvp in DictListEnums)
            {
                sb.Append($"\tpublic enum E{kvp.Key}\r\n");
                sb.Append($"\t{{\r\n");
                long min = 0, max = 0;
                foreach (var kvp2 in kvp.Value)
                {
                    sb.Append($"\t\t{kvp2.Key} = {kvp2.Value},\r\n");
                    long.TryParse(kvp2.Value, out long tempmax);
                    min = Math.Min(min, tempmax);
                    max = Math.Max(max, tempmax);
                }
                sb.Append($"\t}}\r\n");
            }
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassClient.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }
        public static void TransferClassClientConf()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n");
            sb.Append($"using UtilLib;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sb.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");

            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端))
                {
                    continue;
                }
                sb.Append($"\t\t//{nn.NameCn}\t// {nn.NameFile}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                var keytype = nn.TypeClient[0];
                sb.Append($"\t\tpublic static Dictionary<{keytype}, {nn.Name}> {confname} = new Dictionary<{keytype}, {nn.Name}>();\r\n");
            }

            sb.Append($"\t}}\r\n");
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassClientConf.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }
        public static void TransferClassClientConfRead()
        {
            var sb = new StringBuilder();

            sb.Append($"using System.Collections.Generic;\r\n");
            sb.Append($"using UtilLib;\r\n\r\n");
            sb.Append($"namespace ServerBase.Config\r\n{{\r\n");
            sb.Append($"\tpublic static partial class Conf\r\n\t{{\r\n");
            sb.Append($"\t\tpublic static bool InitConfSettings()\r\n\t\t{{\r\n");

            sb.Append($"\t\t\tbool result = true;\r\n\r\n");
            foreach (var nn in DictPages.Values)
            {
                if (!(nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端))
                {
                    continue;
                }
                sb.Append($"\t\t\t//{nn.NameCn}\r\n");
                var confname = nn.Name.Replace("Config", "Conf");
                sb.Append($"\t\t\tif (result) {{ result = ReadConfig(typeof({nn.Name}).Name, ref {confname}); }}\r\n");
            }
            sb.Append($"\r\n\t\t\tif (result) {{ result = InitConfSettingsExt(); }}\r\n\r\n");
            sb.Append($"\t\t\treturn result;\r\n");

            sb.Append($"\t\t}}\r\n");
            sb.Append($"\t}}\r\n");
            sb.Append($"}}\r\n");

            string path_class = PathClass + "\\" + "ClassClientConfRead.cs";
            XGlobal.DeleteFile(path_class);
            FileStream fs_class = new FileStream(path_class, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, Encoding.Unicode);
            sw_class.Write(sb);
            sw_class.Close();
        }

    }
}
