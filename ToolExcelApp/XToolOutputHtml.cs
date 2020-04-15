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
        public static void 导出Html()
        {
            string Template_Html = @"<!DOCTYPE html>
<html>
<head>
<title id=""titlegamename"">表格结构</title>
<meta charset=""utf - 8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1"">
<link rel=""stylesheet"" href=""https://cdn.staticfile.org/twitter-bootstrap/4.1.0/css/bootstrap.min.css"">
<script src=""https://cdn.staticfile.org/jquery/3.2.1/jquery.min.js""></script >
<script src=""https://cdn.staticfile.org/popper.js/1.12.5/umd/popper.min.js""></script >
<script src=""https://cdn.staticfile.org/twitter-bootstrap/4.1.0/js/bootstrap.min.js""></script>
</head>
<body>
        <h1>表格结构</h1>

<div>

$CONTENT$

</div> 

</body>
</html>

";
            var sb = new StringBuilder();

            foreach (var kvp1 in DictFilePages)
            {
                sb.Append($"<h3>文件：{kvp1.Key}</h3>\r\n");
                foreach (var itemname in kvp1.Value)
                {
                    if (DictPages.TryGetValue(itemname, out var item))
                    {
                        sb.Append($"<p>页面：{item.NameCn} {item.Name} 有效列：{item.HeadC.Count} 有效行：{item.ListValue.Count}</p>\r\n");
                    }
                }
            }

            string txt = Template_Html;
            string path_html = PathOutput + @"\表格结构.html";
            XGlobal.DeleteFile(path_html);
            FileStream fs_class = new FileStream(path_html, FileMode.OpenOrCreate);
            StreamWriter sw_class = new StreamWriter(fs_class, new System.Text.UTF8Encoding(false));
            txt = txt.Replace("$CONTENT$", sb.ToString());
            sw_class.Write(txt);
            sw_class.Close();
        }
    }
}
