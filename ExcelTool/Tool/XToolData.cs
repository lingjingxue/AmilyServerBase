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
using System.Collections.Concurrent;

namespace ExcelTool
{
    public static partial class XTool
    {
        public static readonly string Xml_head = "Data";
        public static readonly string Xml_node = "Node";

        public static string PathCurrent = "";
        public static string PathEnum = "";
        public static string PathResult = "";
        public static string PathGlobalId = "";

        public static string PathHtml = "";

        public static string PathExcel = "";
        public static string PathClass = "";
        public static string PathXml = "";
        public static string PathJson = "";
        public static string PathLua = "";

        //文件列表
        public static Dictionary<string, XFileInfo> DictFiles = new Dictionary<string, XFileInfo>();
        //枚举列表
        public static ConcurrentDictionary<string, Dictionary<string, string>> DictDictEnums = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public static StringBuilder SbClassServer = new StringBuilder();





        public static ConcurrentDictionary<string, PageInfo> DictPages = new ConcurrentDictionary<string, PageInfo>();
        public static ConcurrentDictionary<string, List<PageInfo>> DictFilePages = new ConcurrentDictionary<string, List<PageInfo>>();


        public static FormTool FTool;

        public enum EValidType
        {
            无效的 = 0,
            公共 = 1,
            仅服务器 = 2,
            仅客户端 = 3,
        }
        public class XFileInfo
        {
            public string Name;
            public TimeSpan Ts;
            public int rowindex = 0;
            public EValidType ValidType = EValidType.公共;
            public bool Read = false;
            public XFileInfo(string name)
            {
                Name = name;
                Ts = new TimeSpan();
                rowindex = 0;
                Read = false;
            }
        }
        public class PageInfo
        {
            public string Name;
            public string NameCn;
            public string NameFile;
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
                NameCn = name;
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
}
