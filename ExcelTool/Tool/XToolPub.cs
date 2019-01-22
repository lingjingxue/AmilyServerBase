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
        public static void Init()
        {
            PathHtml = PathCurrent + @"\Config";

            PathEnum = PathExcel + @"\A_公共枚举.xlsx";
            PathResult = PathExcel + @"\F_返回码.xlsx";
            PathGlobalId = PathExcel + @"\Q_全局常数.xlsx";
        }
        public static void GetFileList(string path, EValidType ValidType)
        {
            if (Directory.Exists(path))
            {
                foreach (string filename in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(filename))
                    {
                        string filename_excel = Path.GetFileNameWithoutExtension(filename);
                        string firs = filename_excel.Substring(0, 1);
                        if (firs != "A")
                        {
                            XFileInfo finfo = new XFileInfo(filename);
                            finfo.ValidType = ValidType;
                            DictFiles.Add(finfo.Name, finfo);
                        }
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
        public static string StrLang = "Lang";
        public static bool IsLang(this string text)
        {
            int len = 4;
            if (text.Length > len)
            {
                var str = text.Substring(0, len);
                return str == StrLang;
            }
            return false;
        }
        public static bool IsLangCur(this string text)
        {
            return text == FTool.GetLang;
        }

    }
}
