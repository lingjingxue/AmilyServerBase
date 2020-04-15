using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        //窗口
        public static MainWindow Wd;

        public static string PathCurrent = "";

        public static string PathExcel = "";
        public static string PathOutput = "";

        public static string PathClass = "";
        public static string PathJson = "";
        public static string PathJsonClient = "";
        public static string PathJsonServer = "";
        public static string PathLua = "";

        public static string PathEnum = "";

        //全部导出 部分导出
        public static bool 全部导出 = true;

        //文件列表
        public static Dictionary<string, XFileInfo> DictXFileInfo = new Dictionary<string, XFileInfo>();
        public static ObservableCollection<XFileInfo> ObservableCollectionXFileInfo = new ObservableCollection<XFileInfo>();

        //枚举列表
        //公共枚举
        public static ConcurrentDictionary<string, Dictionary<string, string>> CDictDictEnum1 = new ConcurrentDictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> DictDictEnum1 = new Dictionary<string, Dictionary<string, string>>();
        //表格里的枚举
        public static ConcurrentDictionary<string, Dictionary<string, string>> CDictDictEnum2 = new ConcurrentDictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> DictDictEnum2 = new Dictionary<string, Dictionary<string, string>>();


        //文件读取
        public static ConcurrentDictionary<string, PageInfo> CDictPages = new ConcurrentDictionary<string, PageInfo>();
        public static Dictionary<string, PageInfo> DictPages = new Dictionary<string, PageInfo>();
        public static ConcurrentDictionary<string, List<string>> DictFilePages = new ConcurrentDictionary<string, List<string>>();

    }

    public enum EValidType
    {
        无效的 = 0,
        公共 = 1,
        仅服务器 = 2,
        仅客户端 = 3,
    }
    public class XFileInfo
    {
        public string FileName { get; set; } = "文件名";
        public string FileTime { get; set; } = "修改时间";
        public bool NeedRead { get; set; } = false;

        public EValidType ValidType = EValidType.公共;
    }
    public class PageInfo
    {
        public string Name = "";
        public string NameCn = "";
        public string NameFile = "";
        public EValidType ValidType = EValidType.公共;
        public List<string> HeadC;
        public List<string> Head;
        public List<string> HeadEnum;
        public List<string> TypeClient;
        public List<string> TypeServer;
        public List<List<string>> ListValue;

        public PageInfo(string name)
        {
            Name = name;
            ValidType = EValidType.公共;

            HeadC = new List<string>();
            Head = new List<string>();
            HeadEnum = new List<string>();
            TypeClient = new List<string>();
            TypeServer = new List<string>();
            ListValue = new List<List<string>>();
        }
        public bool IsLegal()
        {
            if (Head.Count <= 0)
            {
                return false;
            }
            if (ListValue.Count <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
