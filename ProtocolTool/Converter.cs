using System.IO;
using System.Text;
using System;
using Microsoft.Office.Interop.Word;

namespace ProtocolTool
{
    partial class Program
    {
        /// <summary>
        /// 生成枚举表文件
        /// </summary>
        private static void ProtocolConverterEnum()
        {
            Show("ProtocolConverterEnum");
            var sb = new StringBuilder();

            foreach (var kvp in DictEnum)
            {
                if (kvp.Value.Desc != "")
                {
                    //sb.Append("    /// <summary>\r\n");
                    //sb.Append($"    /// {kvp.Value.Desc}\r\n");
                    //sb.Append("    /// <summary>\r\n");
                    sb.Append($"    [Desc(\"{kvp.Value.Desc}\")]\r\n");
                }
                sb.Append($"    public enum {kvp.Value.Name}\r\n");
                sb.Append("    {\r\n");
                foreach (var kvp2 in kvp.Value.DictBody)
                {
                    if (kvp2.Value.Desc != "")
                    {
                        //sb.Append("        /// <summary>\r\n");
                        //sb.Append($"        /// {kvp2.Value.Desc}\r\n");
                        //sb.Append("        /// <summary>\r\n");
                        sb.Append($"        [Desc(\"{kvp2.Value.Desc}\")]\r\n");
                    }
                    sb.Append($"        {kvp2.Value.Body}");
                    if (kvp2.Value.Value != -100000)
                    {
                        sb.Append($" = {kvp2.Value.Value}");
                    }
                    sb.Append(",\r\n");
                }
                sb.Append("    };\r\n\r\n");
            }
            string txt = Template_Enum;
            FileStream fs = new FileStream(PathCurrent + Filepath_Enum, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Enum$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_Enum);
        }

        /// <summary>
        /// 生成协议号文件
        /// </summary>
        private static void ProtocolConverterClassId()
        {
            Show("ProtocolConverterClassId");
            var sb = new StringBuilder();

            foreach (var nn in DictClass.Values)
            {
                if (nn.ClassType != 0)
                {
                    continue;
                }
                if (nn.Id != -100000)
                {
                    sb.Append($"        // {nn.Name} - {nn.EventName}\r\n");
                    sb.Append($"        {nn.NameId + " = " + nn.Id + ", "}\r\n");
                }
                else
                {
                    sb.Append($"        // {nn.Name} - {nn.EventName}\r\n");
                    sb.Append($"        {nn.NameId + ",  "}\r\n");
                }
            }
            string txt = Template_ClassId;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassId, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$ClassId$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassId);
        }

        /// <summary>
        /// 生成结构体文件
        /// </summary>
        private static void ProtocolConverterClassBase()
        {
            Show("ProtocolConverterClassBase");
            var sb = new StringBuilder();

            foreach (var kvp in DictClass)
            {
                //sb.Append("    /// <summary>\r\n");
                //if (kvp.Value.ClassType == 0)
                //{
                //    sb.Append($"    /// 对应协议枚举-> {kvp.Value.NameId}\r\n");
                //}
                //if (kvp.Value.Desc != "")
                //{
                //    sb.Append($"    /// {kvp.Value.Desc}\r\n");
                //}
                //sb.Append("    /// <summary>\r\n");
                if (kvp.Value.Desc != "")
                {
                    sb.Append($"    [Desc(\"{kvp.Value.Desc}\")]\r\n");
                }
                if (kvp.Value.ClassType == 0)
                {
                    sb.Append($"    public partial class {kvp.Value.Name} : ProtocolMsgBase, INbsSerializer\r\n");
                }
                else
                {
                    sb.Append($"    public partial class {kvp.Value.Name}\r\n");
                }
                sb.Append("    {\r\n");
                //结构体
                foreach (var kvp2 in kvp.Value.DictBody)
                {
                    sb.Append($"{GetClassBodyFromNode(kvp2.Value)}\r\n");
                }
                if (kvp.Value.ClassType == 0)
                {
                    sb.Append($"        public {kvp.Value.Name}() {{ ProtocolId = EProtocolId.{kvp.Value.NameId}; }}\r\n");
                    sb.Append($"        public {kvp.Value.Name}(byte[] buffer) {{ Unserialize(buffer); }}\r\n");
                }
                sb.Append("    };\r\n");
            }
            string txt = Template_ClassBase;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassBase, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Class$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassBase);
        }

        /// <summary>
        /// 生成结构体序列化文件
        /// </summary>
        private static void ProtocolConverterClassSerialization()
        {
            Show("ProtocolConverterClassSerialization");
            var sb = new StringBuilder();

            foreach (var kvp in DictClass)
            {
                sb.Append("    /// <summary>\r\n");
                if (kvp.Value.ClassType == 0)
                {
                    sb.Append($"    /// 对应协议枚举-> {kvp.Value.NameId}\r\n");
                }
                if (kvp.Value.Desc != "")
                {
                    sb.Append($"    /// {kvp.Value.Desc}\r\n");
                }
                sb.Append("    /// <summary>\r\n");
                if (kvp.Value.ClassType == 0)
                {
                    sb.Append($"    public partial class {kvp.Value.Name} : ProtocolMsgBase, INbsSerializer\r\n");
                }
                else
                {
                    sb.Append($"    public partial class {kvp.Value.Name}\r\n");
                }
                sb.Append("    {\r\n");
                if (kvp.Value.ClassType == 0)
                {
                    //序列化
                    sb.Append($"        public NetBitStream Serialize()\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            NetBitStream nbs = new NetBitStream();\r\n");
                    sb.Append("            nbs.Write(_protocol);\r\n");
                    sb.Append("            nbs.Write(_result);\r\n");
                    sb.Append("            nbs.Write(Pin);\r\n");
                    sb.Append("            nbs.Write(Puid);\r\n");
                    sb.Append("            nbs.Write(Shuttle);\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassSerializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("            nbs.WriteEnd();\r\n");
                    sb.Append("            return nbs;\r\n");
                    sb.Append("        }\r\n");

                    //反序列化
                    sb.Append($"        public void Unserialize(byte[] buffer)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            NetBitStream nbs = new NetBitStream();\r\n");
                    sb.Append("            nbs.BeginRead(buffer);\r\n");
                    sb.Append("            nbs.Read(out _protocol);\r\n");
                    sb.Append("            nbs.Read(out _result);\r\n");
                    sb.Append("            nbs.Read(out Pin);\r\n");
                    sb.Append("            nbs.Read(out Puid);\r\n");
                    sb.Append("            nbs.Read(out Shuttle);\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassUnserializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("        }\r\n");

                    //序列化
                    sb.Append($"        public void Serialize(ref NetBitStream nbs)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            nbs.Write(_protocol);\r\n");
                    sb.Append("            nbs.Write(_result);\r\n");
                    sb.Append("            nbs.Write(Pin);\r\n");
                    sb.Append("            nbs.Write(Puid);\r\n");
                    sb.Append("            nbs.Write(Shuttle);\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassSerializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("        }\r\n");

                    //反序列化
                    sb.Append($"        public void Unserialize(ref NetBitStream nbs)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            nbs.Read(out _protocol);\r\n");
                    sb.Append("            nbs.Read(out _result);\r\n");
                    sb.Append("            nbs.Read(out Pin);\r\n");
                    sb.Append("            nbs.Read(out Puid);\r\n");
                    sb.Append("            nbs.Read(out Shuttle);\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassUnserializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("        }\r\n");

                }
                else
                {
                    //序列化
                    sb.Append($"        public void Serialize(ref NetBitStream nbs)\r\n");
                    sb.Append("        {\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassSerializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("        }\r\n");

                    //反序列化
                    sb.Append($"        public void Unserialize(ref NetBitStream nbs)\r\n");
                    sb.Append("        {\r\n");
                    foreach (var kvp2 in kvp.Value.DictBody)
                    {
                        sb.Append($"{GetClassUnserializeFromNode(kvp2.Value, "            ")}");
                    }
                    sb.Append("        }\r\n");

                }
                sb.Append("    };\r\n");
            }
            string txt = Template_ClassSerialization;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassSerialization, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Class$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassSerialization);
        }


        /// <summary>
        /// 生成结构体打印文件
        /// </summary>
        private static void ProtocolConverterDump()
        {
            Show("ProtocolConverterDump");
            var sb = new StringBuilder();

            int idx = 0;
            foreach (var kvp in DictClass)
            {
                if (kvp.Value.ClassType == 0)
                {
                    idx++;
                    sb.Append($"                case EProtocolId.{kvp.Value.NameId}:\r\n");
                    sb.Append($"                    var Rsp{idx} = new {kvp.Value.Name}(buffer);\r\n");
                    sb.Append($"                    sb.Append(BasePropManager.GetFieldsDesc(Rsp{idx}));\r\n");
                    sb.Append($"                    break;\r\n");
                }
            }
            string txt = Template_ClassDump;
            FileStream fs = new FileStream(PathCurrent + Filepath_ClassDump, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            txt = txt.Replace("$Class$", sb.ToString());
            sw.Write(txt);
            sw.Close();
            Show("输出文件：" + Filepath_ClassDump);
        }

        #region WORD

        //Word文件
        public static Microsoft.Office.Interop.Word.Application WordApp = null;
        public static Microsoft.Office.Interop.Word.Document WordDoc = null;
        public static Object Nothing = System.Reflection.Missing.Value;

        /// <summary>
        /// 添加标题1
        /// </summary>
        /// <param name="s"></param>
        public static void AddTitle1(string s)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;
            p = WordDoc.Content.Paragraphs.Add(ref Nothing);
            //设置段落中的内容文本
            p.Range.Text = s;
            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading1;
            p.set_Style(ref style);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }
        /// <summary>
        /// 添加标题2
        /// </summary>
        /// <param name="s"></param>
        public static void AddTitle2(string s)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;
            p = WordDoc.Content.Paragraphs.Add(ref Nothing);
            //设置段落中的内容文本
            p.Range.Text = s;
            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading2;
            p.set_Style(ref style);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }
        /// <summary>
        /// 添加标题3
        /// </summary>
        /// <param name="s"></param>
        public static void AddTitle3(string s)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;
            p = WordDoc.Content.Paragraphs.Add(ref Nothing);
            //设置段落中的内容文本
            p.Range.Text = s;
            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading3;
            p.set_Style(ref style);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }
        /// <summary>
        /// 添加标题4
        /// </summary>
        /// <param name="s"></param>
        public static void AddTitle4(string s)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;
            p = WordDoc.Content.Paragraphs.Add(ref Nothing);
            //设置段落中的内容文本
            p.Range.Text = s;
            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading4;
            p.set_Style(ref style);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }
        /// <summary>
        /// 添加普通段落
        /// </summary>
        /// <param name="s"></param>
        public static void AddParagraph(string s)
        {
            Microsoft.Office.Interop.Word.Paragraph p;
            p = WordDoc.Content.Paragraphs.Add(ref Nothing);
            p.Range.Text = s;
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleBodyText;
            p.set_Style(ref style);
            p.Range.InsertParagraphAfter();
        }

        /// <summary>
        /// 生成协议大纲文档文件
        /// </summary>
        private static void ProtocolConverterWord()
        {
            Show("ProtocolConverterWord");
            Show("正在生成协议大纲。。。。。。。请勿退出！");

            //创建word
            WordApp = new Microsoft.Office.Interop.Word.Application();
            //创建word文档
            WordDoc = WordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            string md1 = "";
            string md2 = "";
            foreach (var nn1 in DictClass.Values)
            {
                if (nn1.Mode1 != md1)
                {
                    md1 = nn1.Mode1;
                    AddTitle1(md1);
                }
                if (nn1.Mode2 != md2)
                {
                    md2 = nn1.Mode2;
                    AddTitle2(md2);
                }
                if (nn1.ClassType == 0)
                {
                    AddTitle3("协议类 " + nn1.Name + " " + nn1.Desc);
                    AddParagraph("对应ID：  " + nn1.NameId);
                }
                else
                {
                    AddTitle3("公共类 " + nn1.Name + " " + nn1.Desc);
                }
                foreach (var nn2 in nn1.DictBody.Values)
                {
                    AddParagraph("  " + GetClassBodyFromNode(nn2));
                }
            }

            //文件保存
            object FileName = PathCurrent + @"协议大纲.docx";  //文件保存路径
            WordDoc.SaveAs(ref FileName, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            WordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            WordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
        }

        #endregion


    }
}