using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static void MessageBoxShow(string text, string caption)
        {
            Wd.MessageBoxShow(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void MessageBoxShow(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            Wd.MessageBoxShow(text, caption, buttons, icon);
        }
        public static void GetFileList(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var dirname in Directory.GetDirectories(path))
                {
                    EValidType ValidType = EValidType.公共;
                    if (dirname.Contains("备份") || dirname.Contains("backup") || dirname.Contains("Backup"))
                    {
                        continue;
                    }
                    if (dirname.Contains("服务器") || dirname.Contains("server") || dirname.Contains("Server"))
                    {
                        ValidType = EValidType.仅服务器;
                    }
                    else if (dirname.Contains("客户端") || dirname.Contains("client") || dirname.Contains("Client"))
                    {
                        ValidType = EValidType.仅客户端;
                    }
                    foreach (string filename in Directory.GetFileSystemEntries(dirname))
                    {
                        if (File.Exists(filename))
                        {
                            string filename_excel = Path.GetFileNameWithoutExtension(filename);
                            FileInfo fi = new FileInfo(filename);
                            if (!filename_excel.Equals("A_公共枚举"))
                            {
                                XFileInfo finfo = new XFileInfo()
                                {
                                    FileName = filename,
                                    FileTime = fi.LastWriteTime.ToString("u"),
                                    ValidType = ValidType,
                                    NeedRead = true,
                                };
                                DictXFileInfo[finfo.FileName] = finfo;
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
                            FileInfo fi = new FileInfo(filename);
                        if (!filename_excel.Equals("A_公共枚举"))
                        {
                            XFileInfo finfo = new XFileInfo()
                            {
                                FileName = filename,
                                FileTime = fi.LastWriteTime.ToString("u"),
                                ValidType = ValidType,
                                NeedRead = true,
                            };
                            DictXFileInfo[finfo.FileName] = finfo;
                        }
                    }
                }
            }
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
            return text == Wd.GetLang;
        }
    }
}
