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
using System.Windows;

namespace ToolExcel
{
    public static partial class XTool
    {
        public static void MessageBoxShow(string text, string caption, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Error)
        {
            FTool.MessageBoxShow(text, caption, buttons, icon);
        }

        public static void GetFileList(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var dirname in Directory.GetDirectories(path))
                {
                    EValidType ValidType = EValidType.公共;
                    if (dirname.Contains("备份"))
                    {
                        continue;
                    }
                    if (dirname.Contains("服务器"))
                    {
                        ValidType = EValidType.仅服务器;
                    }
                    else if (dirname.Contains("客户端"))
                    {
                        ValidType = EValidType.仅客户端;
                    }
                    foreach (string filename in Directory.GetFileSystemEntries(dirname))
                    {
                        if (File.Exists(filename))
                        {
                            string filename_excel = Path.GetFileNameWithoutExtension(filename);
                            if (!filename_excel.Equals("A_公共枚举"))
                            {
                                XFileInfo finfo = new XFileInfo(filename);
                                finfo.ValidType = ValidType;
                                DictFiles.Add(finfo.Name, finfo);
                            }
                        }
                    }
                }
                foreach (string filename in Directory.GetFileSystemEntries(path))
                {
                    EValidType ValidType = EValidType.公共;
                    if (File.Exists(filename))
                    {
                        string filename_excel = Path.GetFileNameWithoutExtension(filename);
                        if (!filename_excel.Equals("A_公共枚举"))
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
            return text == FTool.GetLang();
        }
        

    }
}
