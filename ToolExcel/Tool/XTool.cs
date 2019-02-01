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
        public static void TransferData()
        {
            foreach (var nn in DictPages.Values)
            {
                var sbXml = new StringBuilder();
                var sbJson = new StringBuilder();
                var sbLua = new StringBuilder();

                sbXml.Append($"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
                sbXml.Append($"<{Xml_head}>\r\n");

                sbJson.Append("{\r\n");

                sbLua.Append($"{nn.Name}=\r\n{{\r\n");

                var listdataJson = new List<string>();
                var listdataLua = new List<string>();

                foreach (var nnn in nn.ListValue)
                {
                    sbXml.Append($"\t<{Xml_node}>\r\n");

                    var listnodeJson = new List<string>();
                    var listnodeLua = new List<string>();
                    for (int iii = 0; iii < nn.Head.Count; iii++)
                    {
                        var head = nn.Head[iii];
                        if (head == "")
                        {
                            continue;
                        }

                        if (nn.TypeServer[iii] != "")
                        {
                            sbXml.Append($"\t\t<{head}>{nnn[iii]}</{head}>\r\n");
                        }

                        var TypeClient = nn.TypeClient[iii];
                        if (TypeClient != "")
                        {
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
                            string nodeJson = "";
                            string nodeLua = "";
                            if (TypeClient == "int" || TypeClient == "long" || TypeClient == "float" || TypeClient == "double")
                            {
                                nodeJson = $"\t\t\"{head}\":{nnn[iii]}";
                                nodeLua = $"{head}={nnn[iii]}";
                            }
                            else
                            {
                                nodeJson = $"\t\t\"{head}\":\"{nnn[iii]}\"";
                                nodeLua = $"{head}=\"{nnn[iii]}\"";
                            }
                            listnodeJson.Add(nodeJson);
                            listnodeLua.Add(nodeLua);
                        }
                    }
                    string dataJson = $"\t\"{nnn[0]}\":{{\r\n{string.Join(",\r\n", listnodeJson)}\r\n\t}}";
                    listdataJson.Add(dataJson);

                    string dataLua = $"[\"{nnn[0]}\"]={{{string.Join(",", listnodeLua)},}}";
                    listdataLua.Add(dataLua);

                    sbXml.Append($"\t</{Xml_node}>\r\n");
                }

                sbXml.Append($"</{Xml_head}>\r\n");

                sbJson.Append(string.Join(",\r\n", listdataJson));
                sbJson.Append("\r\n}\r\n");

                sbLua.Append(string.Join(",\r\n", listdataLua));
                sbLua.Append("\r\n}\r\n");

                if (nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器)
                {
                    if (true)
                    {
                        string pathXml = $"{PathXml}\\{nn.Name}.xml";
                        XGlobal.DeleteFile(pathXml);
                        FileStream fsXml = new FileStream(pathXml, FileMode.OpenOrCreate);
                        StreamWriter swXml = new StreamWriter(fsXml);
                        swXml.Write(sbXml.ToString());
                        swXml.Close();
                    }
                }
                if (nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅客户端)
                {
                    if (true)
                    {
                        string pathJson = $"{PathJson}\\{nn.Name}.json";
                        XGlobal.DeleteFile(pathJson);
                        FileStream fsJson = new FileStream(pathJson, FileMode.OpenOrCreate);
                        StreamWriter swJson = new StreamWriter(fsJson);
                        swJson.Write(sbJson.ToString());
                        swJson.Close();
                    }
                    if (true)
                    {
                        string pathLua = $"{PathLua}\\{nn.Name}.lua";
                        XGlobal.DeleteFile(pathLua);
                        FileStream fsLua = new FileStream(pathLua, FileMode.OpenOrCreate);
                        StreamWriter swLua = new StreamWriter(fsLua);
                        swLua.Write(sbLua.ToString());
                        swLua.Close();
                    }
                }
            }
        }

    }
}
