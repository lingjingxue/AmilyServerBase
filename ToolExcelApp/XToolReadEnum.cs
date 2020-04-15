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
        public static void 枚举读取()
        {
            CDictDictEnum1.Clear();

            if (File.Exists(PathEnum))
            {
                FileStream fsExcel = File.OpenRead(PathEnum);
                IWorkbook wk = new XSSFWorkbook(fsExcel);

                int stcount = wk.NumberOfSheets;
                for (int stc = 0; stc < stcount; stc++)
                {
                    int rows;

                    //读取枚举表
                    ISheet sheetenum = wk.GetSheetAt(stc);

                    rows = sheetenum.LastRowNum;
                    int enumcols = 240;
                    for (int j = 0; (j + 2) <= enumcols; j += 3)
                    {
                        IRow rowdictk = sheetenum.GetRow(1);
                        if (rowdictk == null)
                        {
                            break;
                        }
                        ICell celldictk = rowdictk.GetCell(j);
                        if (celldictk == null)
                        {
                            break;
                        }
                        string DictKey = celldictk.ToString();
                        if (DictKey == "")
                        {
                            break;
                        }
                        Dictionary<string, string> DictList = new Dictionary<string, string>();
                        for (int i = 3; i <= rows; i++)
                        {
                            IRow rowdictv = sheetenum.GetRow(i);
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
                        if (CDictDictEnum1.ContainsKey(DictKey))
                        {
                            MessageBoxShow($"重复的枚举表类型 {DictKey}", "提示");
                        }
                        else
                        {
                            CDictDictEnum1[DictKey] = DictList;
                        }
                    }
                }
            }

            DictDictEnum1 = (from it in CDictDictEnum1 orderby it.Key ascending select it).ToDictionary(it => it.Key, it => it.Value);
        }
    }
}
