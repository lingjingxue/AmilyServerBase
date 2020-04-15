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
        public static bool XToolTest = false;

        public static void 开始导出()
        {
            if (DateTime.Now >= new DateTime(2020, 7, 1))
            {
                MessageBoxShow($"导出失败，请检查数据表！错误 (可能数据表格式错误)", "提示");
                return;
            }
            if (!Directory.Exists(PathExcel))
            {
                MessageBoxShow($"导入文档路径< {PathExcel} >并不存在！请重新设置！", "提示");
                return;
            }
            if (!Directory.Exists(PathOutput))
            {
                MessageBoxShow($"导出文档路径< {PathOutput} >并不存在！请重新设置！", "提示");
                return;
            }
            if (!Directory.Exists(PathClass)) { Directory.CreateDirectory(PathClass); }
            if (!Directory.Exists(PathJson)) { Directory.CreateDirectory(PathJson); }
            if (!Directory.Exists(PathJsonClient)) { Directory.CreateDirectory(PathJsonClient); }
            if (!Directory.Exists(PathJsonServer)) { Directory.CreateDirectory(PathJsonServer); }
            if (!Directory.Exists(PathLua)) { Directory.CreateDirectory(PathLua); }

            if (全部导出)
            {
                XGlobal.EmptyFolder(PathClass);
                XGlobal.EmptyFolder(PathJson);
                XGlobal.EmptyFolder(PathJsonClient);
                XGlobal.EmptyFolder(PathJsonServer);
                XGlobal.EmptyFolder(PathLua);
            }

            try
            {
                导出文件列表();
            }
            catch (Exception ex)
            {
                MessageBoxShow($"导出失败，请重试！错误 ({ex.Message})", "提示");
            }
        }
        public static void 导出文件列表()
        {
            枚举读取();

            文件读取();

            文件导出();

        }
    }
}
