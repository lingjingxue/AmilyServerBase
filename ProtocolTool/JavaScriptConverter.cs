using System.IO;
using System.Text;
using System;
using Microsoft.Office.Interop.Word;

namespace ProtocolTool
{
    partial class Program
    {
        //ClassTypeScript
        public static readonly string Filepath_ClassJavaScript = @"DispatcherPtl.js";

        public static readonly string Template_ClassJavaScript = @"


function DispatcherProtocol(Msg) {
    var ProtocolId = Msg._protocol;
    switch (ProtocolId) {

$Class1$

        default:
    }
}


var MapProtocolId = {
$Class31$
    properties: {
$Class32$
    }
};


$Class2$


";

        /// <summary>
        /// 生成结构体文件 JavaScript
        /// </summary>
        private static void ProtocolConverterClassJavaScript()
        {
            Show("ProtocolConverterClassJavaScript");
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var sb31 = new StringBuilder();
            var sb32 = new StringBuilder();

            foreach (var kvp in DictClass)
            {
                if (kvp.Value.ClassType == 0)
                {
                    if (kvp.Value.Name.StartsWith("G2C")
                        || kvp.Value.Name.StartsWith("L2C")
                        || kvp.Value.Name.StartsWith("ALL")
                        || kvp.Value.Name.StartsWith("All")
                        )
                    {
                        if (kvp.Value.Desc != "")
                        {
                            sb1.Append($"        // {kvp.Value.Desc}\r\n");
                        }
                        sb1.Append($"        case MapProtocolId.{kvp.Value.NameId}: On_{kvp.Value.Name}(Msg); break;\r\n");
                    }

                    //sb2.Append($"var {kvp.Value.NameId} = {kvp.Value.Id} // {kvp.Value.Desc}\r\n");

                    sb31.Append($"    {kvp.Value.NameId}: {kvp.Value.Id}, // {kvp.Value.Desc}\r\n");

                    sb32.Append($"        {kvp.Value.Id}: {{ name: \"{kvp.Value.NameId}\", value: {kvp.Value.Id}, desc: \"{kvp.Value.Desc}\" }},\r\n");
                }
            }

            foreach (var kvp in DictEnum)
            {
                if (kvp.Value.Desc != "")
                {
                    sb2.Append($"// {kvp.Value.Desc}\r\n");
                }
                sb2.Append($"var Map{kvp.Value.Name} = {{\r\n");
                foreach (var kvp2 in kvp.Value.DictBody)
                {
                    sb2.Append($"    {kvp2.Value.Body}");
                    if (kvp2.Value.Value != -100000)
                    {
                        sb2.Append($": {kvp2.Value.Value},");
                    }
                    if (kvp2.Value.Desc != "")
                    {
                        sb2.Append($"// {kvp2.Value.Desc}");
                    }
                    sb2.Append("\r\n");
                }
                sb2.Append("}\r\n\r\n");
            }


            string txt = Template_ClassJavaScript;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassJavaScript, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Class1$", sb1.ToString());
            txt = txt.Replace("$Class2$", sb2.ToString());
            txt = txt.Replace("$Class31$", sb31.ToString());
            txt = txt.Replace("$Class32$", sb32.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassJavaScript);
        }

    }
}