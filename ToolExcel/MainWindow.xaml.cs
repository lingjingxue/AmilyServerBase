using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ToolExcel.XGlobal;
using static ToolExcel.XTool;


namespace ToolExcel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            XTool.FTool = this;

            PathCurrent = Directory.GetCurrentDirectory();
            PathCurrent = XGlobal.GetParentFolder(PathCurrent);

            TextBoxExcel.Text = PathExcel = PathCurrent + @"\Config\Excel";
            TextBoxOutput.Text = PathOutput = PathCurrent + @"\Config";
            PathClass = PathOutput + @"\Class";
            PathXml = PathOutput + @"\Xml";
            PathJson = PathOutput + @"\Json";
            PathLua = PathOutput + @"\Lua";

            PathEnum = PathExcel + @"\A_公共枚举.xlsx";

            if (DictLang.Count > 0)
            {
                foreach (var item in DictLang.Keys)
                {
                    ComboBoxLang.Items.Add(item);
                }
                ComboBoxLang.SelectedIndex = 0;
            }

            DataGridFile.ItemsSource = StuList;
        }
        public void MessageBoxShow(string text, string caption, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Error)
        {
            System.Windows.MessageBox.Show(text, caption, buttons, icon);
        }
        public Dictionary<string, string> DictLang = new Dictionary<string, string>()
        {
            ["简体中文"] = "LangChs",
            ["英语"] = "LangEn",
            ["韩语"] = "LangKo",
        };
        public string GetLang()
        {
            if (DictLang.TryGetValue(ComboBoxLang.Text, out var value))
            {
                return value;
            }
            else
            {
                return "LangChs";
            }
        }

        private void ButtonPathExcel_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathExcel;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TextBoxExcel.Text = PathExcel = fbd.SelectedPath;
            }
        }

        private void ButtonPathOutput_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathOutput;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TextBoxOutput.Text = PathOutput = fbd.SelectedPath;
                PathClass = PathOutput + @"\Class";
                PathXml = PathOutput + @"\Xml";
                PathJson = PathOutput + @"\Json";
                PathLua = PathOutput + @"\Lua";
            }
        }

        private void ButtonGetList_Click(object sender, RoutedEventArgs e)
        {
            DictFiles.Clear();

            GetFileList(PathExcel);

            UpdateFileList();
        }
        public string EditingId { get; set; }
        public ObservableCollection<XFileInfo> StuList { get; set; } = new ObservableCollection<XFileInfo>();
        private void UpdateFileList()
        {
            StuList.Clear();
            foreach (var item in DictFiles.Values)
            {
                StuList.Add(item);
            }
        }
        private void ButtonExportSingle_Click(object sender, RoutedEventArgs e)
        {
            //var item = (XFileInfo)DataGridFile.SelectedItem;
            //item.Ts = TimeSpan.FromHours(1.5);
            //DataGridFile.Items.Refresh();
        }

        private void ButtonExportAll_Click(object sender, RoutedEventArgs e)
        {
            开始导出();
        }
        private void SetEnabled(bool value = true)
        {
        }
        bool 使用多线程 = false;
        public void 开始导出()
        {
            if (DateTime.Now >= new DateTime(2020, 7, 1))
            {
                return;
            }

            if (DictFiles.Count <= 0)
            {
                MessageBoxShow($"需要导出的列表为空！", "提示");
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
            if (!Directory.Exists(PathXml)) { Directory.CreateDirectory(PathXml); }
            if (!Directory.Exists(PathJson)) { Directory.CreateDirectory(PathJson); }
            if (!Directory.Exists(PathLua)) { Directory.CreateDirectory(PathLua); }

            SetEnabled(false);

            XGlobal.EmptyFolder(PathClass);
            XGlobal.EmptyFolder(PathXml);
            XGlobal.EmptyFolder(PathJson);
            XGlobal.EmptyFolder(PathLua);

            try
            {
                TransferFiles();
            }
            catch (Exception ex)
            {
                MessageBoxShow($"导出失败，请重试！错误 ({ex.Message})", "提示");
            }

        }
        public static int progressBar1Value = 0;
        public static int progressBar1Max = 0;
        public int progressBar1ValueAdd(int v = 1)
        {
            progressBar1Value += v;
            progressBar1Value = Math.Min(progressBar1Value, progressBar1Max);
            ProgressBarAll.Dispatcher.BeginInvoke(new Action<System.Windows.Controls.ProgressBar>(UpdateProgressBar), ProgressBarAll);
            return progressBar1Value;
        }
        private void UpdateProgressBar(System.Windows.Controls.ProgressBar dg)
        {
            dg.Value = progressBar1Value;
        }
        public static Stopwatch BenchmarkStopwatch;

        public void ThreadMethod(object obj)
        {
            XFileInfo nn = obj as XFileInfo;
            ReadFile(nn);
            nn.Read = true;
            progressBar1ValueAdd();
        }
        public void TransferFiles()
        {
            BenchmarkStopwatch = Stopwatch.StartNew();

            progressBar1Value = 0;
            progressBar1Max = DictFiles.Count;
            ProgressBarAll.Maximum = progressBar1Max;

            progressBar1ValueAdd();

            //枚举表
            ReadEnums();
            progressBar1ValueAdd();

            CDictPages.Clear();
            DictFilePages.Clear();



            if (使用多线程)
            {
                foreach (var nn in DictFiles.Values)
                {
                    Task task = Task.Run(() =>
                    {
                        ReadFile(nn);
                        nn.Read = true;
                        progressBar1ValueAdd();
                    });
                    //Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod));
                    //thread.Start(nn);
                }
            }
            else
            {
                foreach (var nn in DictFiles.Values)
                {
                    ReadFile(nn);
                    nn.Read = true;
                    progressBar1ValueAdd();
                }
            }

            bool 读取完成 = false;
            while (!读取完成)
            {
                读取完成 = DictFiles.Values.All(it => it.Read);
                if (读取完成)
                {
                    //DictDictEnums = new Dictionary<string, Dictionary<string, string>>(CDictDictEnums);
                    //DictPages = new Dictionary<string, PageInfo>(CDictPages);

                    DictDictEnums = (from it in CDictDictEnums orderby it.Key ascending select it).ToDictionary(it => it.Key, it => it.Value);
                    DictPages = (from it in CDictPages orderby it.Key ascending select it).ToDictionary(it => it.Key, it => it.Value);

                    TransferOutput();
                }
                Thread.Sleep(200);
            }
        }

        public void TransferOutput()
        {
            TransferHtml();
            progressBar1ValueAdd();

            TransferClassServer();
            progressBar1ValueAdd();

            TransferClassServerConf();
            progressBar1ValueAdd();

            TransferClassServerConfRead();
            progressBar1ValueAdd();

            TransferClassClient();
            progressBar1ValueAdd();

            TransferClassClientConf();
            progressBar1ValueAdd();

            TransferClassClientConfRead();
            progressBar1ValueAdd();

            TransferClassClientLua();
            progressBar1ValueAdd();

            TransferData();
            progressBar1Value = progressBar1Max;

            BenchmarkStopwatch.Stop();
            //toolStripStatusLabel1.Text = $"耗时 {BenchmarkStopwatch.ElapsedMilliseconds} 毫秒";

            Thread.Sleep(1000);
            MessageBoxShow($"导出完成！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            SetEnabled(true);

            DataGridFile.Items.Refresh();

            progressBar1ValueAdd(0);
        }
        public void ReadFile(XFileInfo fileinfo)
        {
            string path_excel = fileinfo.Name;

            var time_start = DateTime.Now;
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
                    var NameFile = System.IO.Path.GetFileName(path_excel);
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
                            if (CDictDictEnums.ContainsKey(DictKey))
                            {
                                MessageBoxShow($"重复的枚举表类型 {DictKey}", "提示");
                            }
                            else
                            {
                                CDictDictEnums[DictKey] = DictList;
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
                                if (CDictDictEnums.TryGetValue(page.HeadEnum[k], out var DictList))
                                {
                                    if (DictList.ContainsKey(s_value))
                                    {
                                        s_value = DictList[s_value];
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
            fileinfo.Ts = DateTime.Now - time_start;
        }


    }
}
