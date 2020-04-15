using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static void 导出数据()
        {
            sbJsonJavaScript = new StringBuilder();

            foreach (var nn in DictPages.Values)
            {
                var sbJson = new StringBuilder();
                var sbJsonClient = new StringBuilder();
                var sbJsonServer = new StringBuilder();
                var sbLua = new StringBuilder();
                var sbJsonJs = new StringBuilder();

                sbJson.Append("{\r\n");
                sbJsonClient.Append("{\r\n");

                sbJsonServer.Append("{\r\n");

                sbLua.Append($"{nn.Name}=\r\n{{\r\n");

                if (true)
                {
                    var confname = nn.Name.Replace("Config", "Conf");
                    sbJsonJs.Append($"var {confname} = {{\r\n");
                }

                var listdataJson = new List<string>();
                var listdataJsonClient = new List<string>();
                var listdataJsonServer = new List<string>();
                var listdataLua = new List<string>();
                var listdataJsonJs = new List<string>();

                foreach (var nnn in nn.ListValue)
                {
                    var listnodeJson = new List<string>();
                    var listnodeJsonClient = new List<string>();
                    var listnodeLua = new List<string>();
                    var listnodeJsonJs = new List<string>();
                    for (int iii = 0; iii < nn.Head.Count; iii++)
                    {
                        var head = nn.Head[iii];
                        if (head == "")
                        {
                            continue;
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
                            string nodeJsonClient = "";
                            string nodeLua = "";
                            string nodeJsonJs = "";
                            if (TypeClient == "int" || TypeClient == "long" || TypeClient == "float" || TypeClient == "double")
                            {
                                nodeJson = $"\t\t\"{head}\":{nnn[iii]}";
                                nodeJsonClient = $"\t\t\"{head}\":{nnn[iii]}";
                                nodeLua = $"{head}={nnn[iii]}";
                                nodeJsonJs = $"\t\t\"{head}\":{nnn[iii]}";
                            }
                            else
                            {
                                nodeJson = $"\t\t\"{head}\":\"{nnn[iii]}\"";
                                nodeJsonClient = $"\t\t\"{head}\":\"{nnn[iii]}\"";
                                nodeLua = $"{head}=\"{nnn[iii]}\"";
                                nodeJsonJs = $"\t\t\"{head}\":\"{nnn[iii]}\"";
                            }
                            listnodeJson.Add(nodeJson);
                            listnodeJsonClient.Add(nodeJsonClient);
                            listnodeLua.Add(nodeLua);
                            listnodeJsonJs.Add(nodeJsonJs);
                        }
                    }
                    string dataJson = $"\t\"{nnn[0]}\":{{\r\n{string.Join(",\r\n", listnodeJson)}\r\n\t}}";
                    listdataJson.Add(dataJson);

                    string dataJsonClientKeyType = nn.TypeClient[0];
                    if (dataJsonClientKeyType == "string")
                    {
                        string dataJsonClient = $"\t\"{nnn[0]}\":{{\r\n{string.Join(",\r\n", listnodeJsonClient)}\r\n\t}}";
                        listdataJsonClient.Add(dataJsonClient);

                        string dataJsonJs = $"\t\"{nnn[0]}\":{{\r\n{string.Join(",\r\n", listnodeJsonJs)}\r\n\t}}";
                        listdataJsonJs.Add(dataJsonJs);
                    }
                    else
                    {
                        string dataJsonClient = $"\t{nnn[0]}:{{\r\n{string.Join(",\r\n", listnodeJsonClient)}\r\n\t}}";
                        listdataJsonClient.Add(dataJsonClient);

                        string dataJsonJs = $"\t{nnn[0]}:{{\r\n{string.Join(",\r\n", listnodeJsonJs)}\r\n\t}}";
                        listdataJsonJs.Add(dataJsonJs);
                    }

                    var listnodeJsonServer = new List<string>();
                    for (int iii = 0; iii < nn.Head.Count; iii++)
                    {
                        var head = nn.Head[iii];
                        if (head == "")
                        {
                            continue;
                        }

                        var TypeServer = nn.TypeServer[iii];
                        if (TypeServer != "")
                        {
                            string nodeJsonServer = "";
                            if (TypeServer == "int" || TypeServer == "long" || TypeServer == "float" || TypeServer == "double")
                            {
                                nodeJsonServer = $"\"{head}\":{nnn[iii]}";
                            }
                            else if (TypeServer == "List<int>")
                            {
                                var ListInt = nnn[iii].ToListInt();
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListInt)}]";
                            }
                            else if (TypeServer == "List<List<int>>")
                            {
                                var ListListInt = nnn[iii].ToListListInt();
                                var ListString = new List<string>();
                                foreach (var item in ListListInt)
                                {
                                    ListString.Add($"[{string.Join(",", item)}]");
                                }
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListString)}]";
                            }
                            else if (TypeServer == "List<float>")
                            {
                                var ListFloat = nnn[iii].ToListFloat();
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListFloat)}]";
                            }
                            else if (TypeServer == "List<List<float>>")
                            {
                                var ListListFloat = nnn[iii].ToListListFloat();
                                var ListString = new List<string>();
                                foreach (var item in ListListFloat)
                                {
                                    ListString.Add($"[{string.Join(",", item)}]");
                                }
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListString)}]";
                            }
                            else if (TypeServer == "List<string>")
                            {
                                var ListString = nnn[iii].ToListString();
                                for (int inodeJsonServer = 0; inodeJsonServer < ListString.Count; inodeJsonServer++)
                                {
                                    ListString[inodeJsonServer] = $"\"{ListString[inodeJsonServer]}\"";
                                }
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListString)}]";
                            }
                            else if (TypeServer == "List<List<string>>")
                            {
                                var ListListString = nnn[iii].ToListListString();
                                var ListString = new List<string>();
                                foreach (var item in ListListString)
                                {
                                    for (int inodeJsonServer = 0; inodeJsonServer < item.Count; inodeJsonServer++)
                                    {
                                        item[inodeJsonServer] = $"\"{item[inodeJsonServer]}\"";
                                    }
                                    ListString.Add($"[{string.Join(",", item)}]");
                                }
                                nodeJsonServer = $"\"{head}\":[{string.Join(",", ListString)}]";
                            }
                            else if (TypeServer == "Dictionary<int, int>" || TypeServer == "Dictionary<int,int>")
                            {
                                var ListListInt = nnn[iii].ToListListInt();
                                var ListString = new List<string>();
                                foreach (var item in ListListInt)
                                {
                                    if (item.Count < 2)
                                    {
                                        continue;
                                    }
                                    ListString.Add($"{item[0]}:{item[1]}");
                                }
                                nodeJsonServer = $"\"{head}\":{{{string.Join(",", ListString)}}}";
                            }
                            else if (TypeServer == "bool")
                            {
                                nodeJsonServer = $"\"{head}\":{nnn[iii].ToLower()}";
                            }
                            else
                            {
                                nodeJsonServer = $"\"{head}\":\"{nnn[iii]}\"";
                            }
                            listnodeJsonServer.Add(nodeJsonServer);
                        }
                    }
                    string dataJsonServer = "";
                    string dataJsonServerKeyType = nn.TypeServer[0];
                    if (dataJsonServerKeyType == "string")
                    {
                        dataJsonServer = $"\t\"{nnn[0]}\":{{\r\n{string.Join(",\r\n", listnodeJsonServer)}\r\n\t}}";
                    }
                    else
                    {
                        dataJsonServer = $"\t{nnn[0]}:{{\r\n{string.Join(",\r\n", listnodeJsonServer)}\r\n\t}}";
                    }
                    listdataJsonServer.Add(dataJsonServer);

                    string dataLua = $"[\"{nnn[0]}\"]={{{string.Join(",", listnodeLua)},}}";
                    listdataLua.Add(dataLua);
                }

                sbJson.Append(string.Join(",\r\n", listdataJson));
                sbJson.Append("\r\n}\r\n");

                sbJsonClient.Append(string.Join(",\r\n", listdataJsonClient));
                sbJsonClient.Append("\r\n}\r\n");

                sbJsonServer.Append(string.Join(",\r\n", listdataJsonServer));
                sbJsonServer.Append("\r\n}\r\n");

                sbLua.Append(string.Join(",\r\n", listdataLua));
                sbLua.Append("\r\n}\r\n");

                sbJsonJs.Append(string.Join(",\r\n", listdataJsonJs));
                sbJsonJs.Append("\r\n}\r\n");

                if (nn.ValidType == EValidType.公共 || nn.ValidType == EValidType.仅服务器)
                {
                    if (true)
                    {
                        string pathJson = $"{PathJsonServer}\\{nn.Name}.json";
                        XGlobal.DeleteFile(pathJson);
                        FileStream fsJson = new FileStream(pathJson, FileMode.OpenOrCreate);
                        StreamWriter swJson = new StreamWriter(fsJson, Encoding.UTF8);
                        swJson.Write(sbJsonServer.ToString());
                        swJson.Close();
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
                        string pathJson = $"{PathJsonClient}\\{nn.Name}.json";
                        XGlobal.DeleteFile(pathJson);
                        FileStream fsJson = new FileStream(pathJson, FileMode.OpenOrCreate);
                        StreamWriter swJson = new StreamWriter(fsJson);
                        swJson.Write(sbJsonClient.ToString());
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
                    if (true)
                    {
                        sbJsonJavaScript.Append(sbJsonJs);
                    }
                }
            }
        }
    }
}
