using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Linq;


namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static void 文件读取()
        {
            CDictPages.Clear();
            DictFilePages.Clear();

            foreach (var item in DictXFileInfo.Values)
            {
                if (!item.NeedRead)
                {
                    continue;
                }
                文件读取(item);
            }

            DictDictEnum2 = (from it in CDictDictEnum2 orderby it.Key ascending select it).ToDictionary(it => it.Key, it => it.Value);
            DictPages = (from it in CDictPages orderby it.Key ascending select it).ToDictionary(it => it.Key, it => it.Value);

        }
        public static void 文件读取(XFileInfo fileinfo)
        {
            string path_excel = fileinfo.FileName;

            if (File.Exists(path_excel))
            {
                FileStream fsExcel = File.OpenRead(path_excel);
                IWorkbook wk = new XSSFWorkbook(fsExcel);

                int stcount = wk.NumberOfSheets;
                for (int stc = 0; stc < stcount; stc++)
                {
                    ISheet sheet = wk.GetSheetAt(stc);

                    string sheetName = sheet.SheetName;
                    string[] nodes = sheetName.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    bool 导出枚举 = false;
                    string pagename = sheet.SheetName;
                    string pagenamecn = sheet.SheetName;
                    if (nodes.Length >= 2)
                    {
                        pagename = nodes[1];
                        pagenamecn = nodes[0];
                        if (pagenamecn[0] == 'e' || pagenamecn[0] == 'E')
                        {
                            导出枚举 = true;
                        }
                    }

                    bool newpage = true;
                    if (CDictPages.TryGetValue(pagename, out var page))
                    {
                        newpage = false;
                    }
                    else
                    {
                        page = new PageInfo(pagename);
                    }
                    var NameFile = Path.GetFileName(path_excel);
                    page.NameCn += $"{pagenamecn} ";
                    page.NameFile += $"{NameFile} ";
                    page.ValidType = fileinfo.ValidType;

                    if (newpage)
                    {
                        IRow rowHeadC = sheet.GetRow(1);
                        if (rowHeadC == null)
                        {
                            continue;
                        }
                        for (int k = 0; k <= rowHeadC.LastCellNum; k++)
                        {
                            ICell cell = rowHeadC.GetCell(k);
                            if (cell != null)
                            {
                                page.HeadC.Add(cell.ToString());
                            }
                            else
                            {
                                page.HeadC.Add("");
                            }
                        }
                        IRow rowHead = sheet.GetRow(2);
                        if (rowHead == null)
                        {
                            continue;
                        }
                        for (int k = 0; k <= rowHead.LastCellNum; k++)
                        {
                            ICell cell = rowHead.GetCell(k);
                            if (cell != null)
                            {
                                string strheadenum = cell.ToString();
                                string[] strheadenumnodes = strheadenum.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                                if (strheadenumnodes.Length >= 2)
                                {
                                    page.Head.Add(strheadenumnodes[0]);
                                    page.HeadEnum.Add(strheadenumnodes[1]);
                                }
                                else
                                {
                                    page.Head.Add(strheadenum);
                                    page.HeadEnum.Add(strheadenum);
                                }
                            }
                            else
                            {
                                page.Head.Add("");
                                page.HeadEnum.Add("");
                            }
                        }
                        IRow rowTypeClient = sheet.GetRow(3);
                        if (rowTypeClient == null)
                        {
                            continue;
                        }
                        for (int k = 0; k <= page.Head.Count; k++)
                        {
                            ICell cell = rowTypeClient.GetCell(k);
                            if (cell != null)
                            {
                                string TypeClient = cell.ToString();
                                if (TypeClient == "float")
                                {
                                    TypeClient = "double";
                                }
                                page.TypeClient.Add(TypeClient);
                            }
                            else
                            {
                                page.TypeClient.Add("");
                            }
                        }
                        IRow rowTypeServer = sheet.GetRow(4);
                        if (rowTypeServer == null)
                        {
                            continue;
                        }
                        for (int k = 0; k <= page.Head.Count; k++)
                        {
                            ICell cell = rowTypeServer.GetCell(k);
                            if (cell != null)
                            {
                                page.TypeServer.Add(cell.ToString());
                            }
                            else
                            {
                                page.TypeServer.Add("");
                            }
                        }

                        if (导出枚举)
                        {
                            string DictKey = "EnumAAA";
                            ICell cell = rowHead.GetCell(0);
                            if (cell != null)
                            {
                                DictKey = cell.StringCellValue;
                            }
                            else
                            {
                                page.Head.Add("");
                                page.HeadEnum.Add("");
                            }

                            int j = 0;
                            Dictionary<string, string> DictList = new Dictionary<string, string>();
                            for (int i = 5; i <= sheet.LastRowNum; i++)
                            {
                                IRow rowdictv = sheet.GetRow(i);
                                if (rowdictv == null)
                                {
                                    continue;
                                }
                                ICell cell1 = rowdictv.GetCell(j);
                                ICell cell2 = rowdictv.GetCell(j + 1);
                                if (cell1 == null || cell2 == null)
                                {
                                    continue;
                                }
                                string enumK = cell2.ToString();
                                string enumV = cell1.ToString();
                                if (enumK == "" || enumV == "")
                                {
                                    continue;
                                }
                                if (DictList.ContainsKey(enumK))
                                {
                                    MessageBoxShow($"重复的枚举表Key {DictKey} {enumK}", "提示");
                                    continue;
                                }
                                if (DictList.ContainsKey(enumV))
                                {
                                    MessageBoxShow($"重复的枚举表Value {DictKey} {enumV}", "提示");
                                    continue;
                                }
                                DictList[enumK] = enumV;
                            }
                            if (CDictDictEnum2.ContainsKey(DictKey))
                            {
                                MessageBoxShow($"重复的枚举表类型 {DictKey}", "提示");
                            }
                            else
                            {
                                CDictDictEnum2[DictKey] = DictList;
                            }
                        }
                    }

                    for (int j = 5; j <= sheet.LastRowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        if (row == null)
                        {
                            continue;
                        }
                        if (row.GetCell(0) == null)
                        {
                            continue;
                        }
                        if (row.GetCell(0).ToString() == "")
                        {
                            continue;
                        }
                        var listrowvalue = new List<string>();
                        for (int k = 0; k < page.Head.Count; k++)
                        {
                            ICell cell = row.GetCell(k);
                            string s_value = GetTextFromCell(cell);
                            if (s_value == "")
                            {
                                s_value = "0";
                            }

                            if (!string.IsNullOrEmpty(page.HeadEnum[k]))
                            {
                                if (CDictDictEnum1.TryGetValue(page.HeadEnum[k], out var DictList1))
                                {
                                    if (DictList1.ContainsKey(s_value))
                                    {
                                        s_value = DictList1[s_value];
                                    }
                                }
                                if (CDictDictEnum2.TryGetValue(page.HeadEnum[k], out var DictList2))
                                {
                                    if (DictList2.ContainsKey(s_value))
                                    {
                                        s_value = DictList2[s_value];
                                    }
                                }
                            }
                            listrowvalue.Add(s_value);
                        }
                        page.ListValue.Add(listrowvalue);
                    }
                    if (page.IsLegal())
                    {
                        CDictPages[page.Name] = page;

                        if (!DictFilePages.TryGetValue(NameFile, out var filepages))
                        {
                            filepages = new List<string>();
                            DictFilePages[NameFile] = filepages;
                        }
                        filepages.Add(page.Name);
                    }

                }
            }
        }
        public static string GetTextFromCell(ICell cell)
        {
            string result = "";
            if (cell == null)
            {
                return result;
            }
            if (cell.CellType == CellType.Formula)
            {
                switch (cell.CachedFormulaResultType)
                {
                    case CellType.String:
                        string strFORMULA = cell.StringCellValue;
                        if (strFORMULA != null && strFORMULA.Length > 0)
                        {
                            result = strFORMULA.ToString();
                        }
                        break;

                    case CellType.Numeric:
                        result = Convert.ToString(cell.NumericCellValue);
                        break;

                    case CellType.Boolean:
                        result = Convert.ToString(cell.BooleanCellValue);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                result = cell.ToString();
            }
            return result;
        }
    }
}
