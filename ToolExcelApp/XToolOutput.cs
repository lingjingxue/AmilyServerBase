using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows;

namespace ToolExcelApp
{
    public static partial class XTool
    {
        public static void 文件导出()
        {
            导出数据();

            导出Html();

            导出ClassServer();

            导出ClassServerConf();

            导出ClassServerConfRead();

            导出ClassClient();

            导出ClassClientConf();

            导出ClassClientConfRead();

            导出ClassClientLua();

            //导出ClassClientJavaScript();

            MessageBoxShow($"导出完成！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
