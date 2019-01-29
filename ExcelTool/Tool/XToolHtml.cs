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
        public static void TransferHtml()
        {
            string Template_Html = @"<!DOCTYPE html>
<html>
<head>
<link rel=""stylesheet"" href=""http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.css"" >
<script src = ""http://code.jquery.com/jquery-1.8.3.min.js""></script >
<script src=""http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.js"" ></script>
</head>
<body>

<div data-role=""page"" id=""pageone"" >
    <div data-role=""header"" >
        <h1>表格结构</h1>
    </div>

    <div data-role=""content"" >

$CONTENT$

    </div>
  
    <div data-role=""footer"" >
        <h1>完</h1>
    </div>
</div> 

</body>
</html>

";
            var sb = new StringBuilder();

            foreach (var kvp1 in DictFilePages)
            {
                //sb.Append($"<div data-role=\"collapsible\">\r\n");
                sb.Append($"<h3>文件：{kvp1.Key}</h3>\r\n");
                foreach (var itemname in kvp1.Value)
                {
                    if (DictPages.TryGetValue(itemname, out var item))
                    {
                        sb.Append($"<p>页面：{item.NameCn} {item.Name} 有效列：{item.HeadC.Count} 有效行：{item.ListValue.Count}</p>\r\n");
                    }
                }
                //sb.Append($"</div>\r\n");
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
