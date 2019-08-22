using System.IO;
using System.Text;
using System;
using Microsoft.Office.Interop.Word;

namespace ProtocolTool
{
    partial class Program
    {
        //ClassTypeScript
        public static readonly string Filepath_ClassTypeScript = @"ProtocolClassTypeScript.ts";

        public static readonly string Template_ClassTypeScript = @"
class ProtocolMsgBase {
    // 协议号
    _protocol:number;
    // 结果码
    _result:number;
    // Pin码
    Pin:number;
    // 玩家唯一ID
    Puid:number;
    // 附加中转信息
    Shuttle:string;

    // 构造函数
    constructor(protocol:number) {
        this._protocol = protocol
    }
}

$Class$

";

        public static string TypeScriptCheckNumbaer(string v)
        {
            if (v == "byte"
                || v == "short"
                || v == "int"
                || v == "uint"
                || v == "long"
                || v == "float"
                || v == "DateTime"
                || v == "TimeSpan"
                )
            {
                return "number";
            }
            return v;
        }

        //成员和初始化
        public static string GetClassBodyTypeScriptFromNode(CClassNode v)
        {
            var sb = new StringBuilder();

            if (v.Desc != "")
            {
                sb.Append($"    // {v.Desc}\r\n");
            }
            switch (v.Ctype)
            {
                case "string":
                    sb.Append($"    {v.Body}:string;");
                    break;

                case "list":
                    //sb.Append($"    var {v.Body}:{v.Ctype1}[];");
                    sb.Append($"    {v.Body}:{TypeScriptCheckNumbaer(v.Ctype1)}[];");
                    break;

                default:
                    sb.Append($"    {v.Body}:number;");
                    break;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成结构体文件 TypeScript
        /// </summary>
        private static void ProtocolConverterClassTypeScript()
        {
            Show("ProtocolConverterClassTypeScript");
            var sb = new StringBuilder();

            foreach (var kvp in DictClass)
            {
                if (kvp.Value.Desc != "")
                {
                    sb.Append($"// {kvp.Value.Desc} \r\n");
                }
                if (kvp.Value.ClassType == 0)
                {
                    sb.Append($"class {kvp.Value.Name} extends ProtocolMsgBase {{\r\n");
                }
                else
                {
                    sb.Append($"class {kvp.Value.Name} {{\r\n");
                }
                //结构体
                foreach (var kvp2 in kvp.Value.DictBody)
                {
                    sb.Append($"{GetClassBodyTypeScriptFromNode(kvp2.Value)}\r\n");
                }
                sb.Append("};\r\n");
            }
            string txt = Template_ClassTypeScript;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassTypeScript, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Class$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassTypeScript);
        }

    }
}