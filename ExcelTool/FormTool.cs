using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;        //NPOI
using NPOI.XSSF.UserModel;      //NPOI
using System.Threading;
using static ExcelTool.XGlobal;
using static ExcelTool.XTool;
using System.Diagnostics;

namespace ExcelTool
{
    public partial class FormTool : Form
    {
        
        public FormTool()
        {
            InitializeComponent();
        }

        private void FormTool_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            label2.Text = "";

            PathCurrent = Directory.GetCurrentDirectory();
            PathCurrent = XGlobal.GetParentFolder(PathCurrent);

            textBox2.Text = PathExcel = PathCurrent + @"\Config\Excel";
            textBox1.Text = PathOutput = PathCurrent + @"\Config";
            PathClass = PathOutput + @"\Class";
            PathXml = PathOutput + @"\Xml";
            PathJson = PathOutput + @"\Json";
            PathLua = PathOutput + @"\Lua";

            PathEnum = PathExcel + @"\A_公共枚举.xlsx";

            comboBox1.Items.Add("LangChs");
            comboBox1.Items.Add("LangEn");
            comboBox1.Items.Add("LangKo");
            comboBox1.SelectedIndex = 0;

            FlushClient fc = new FlushClient(ThreadFunction);
            fc.BeginInvoke(null, null);
        }
        public void MessageBoxShow(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(text, caption, buttons, icon);
        }
        private void SetEnabled(bool value = true)
        {
            button1.Enabled = value;
            button2.Enabled = value;
            dataGridView1.Enabled = value;
        }
        
        private void UpdateFileList()
        {
            var dgv = dataGridView1;

            dgv.Rows.Clear();

            foreach (var item in DictFiles.Values)
            {
                int rowindex = dgv.Rows.Add();
                item.rowindex = rowindex;
                dgv.Rows[rowindex].Cells[0].Value = item.Name;
                dgv.Rows[rowindex].Cells[1].Value = item.Ts.ToString();
            }
        }
        public void UpdateFileTs(XFileInfo fileinfo, TimeSpan ts)
        {
            try
            {
                var dgv = dataGridView1;
                dgv.Rows[fileinfo.rowindex].Cells[1].Value = ts.ToString();
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DictFiles.Clear();

            //GetFileList(PathExcel, EValidType.公共);
            //GetFileList(PathExcel + @"\服务器", EValidType.仅服务器);
            //GetFileList(PathExcel + @"\客户端", EValidType.仅客户端);

            GetFileList(PathExcel);

            UpdateFileList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            使用多线程 = false;
            开始导出();
            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            使用多线程 = true;
            开始导出();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathExcel;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox2.Text = PathExcel = fbd.SelectedPath;
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathOutput;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox1.Text = PathOutput = fbd.SelectedPath;
                PathClass = PathOutput + @"\Class";
                PathXml = PathOutput + @"\Xml";
                PathJson = PathOutput + @"\Json";
                PathLua = PathOutput + @"\Lua";
            }
        }
        #region 导出
        bool 使用多线程 = false;
        public string GetLang => (comboBox1.Text);
        public void 开始导出()
        {
            if (DateTime.Now >= new DateTime(2020, 7, 1))
            {
                return;
            }

            if (DictFiles.Count <= 0)
            {
                MessageBoxShow($"需要导出的列表为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(PathExcel))
            {
                MessageBoxShow($"导入文档路径< {PathExcel} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathOutput))
            {
                MessageBoxShow($"导出文档路径< {PathOutput} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBoxShow($"导出失败，请重试！错误 ({ex.Message})", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public int progressBar1Value = 0;
        public int progressBar1Max = 0;
        public int progressBar1ValueAdd(int v = 1)
        {
            progressBar1Value += v; progressBar1Value = Math.Min(progressBar1Value, progressBar1Max); return progressBar1Value;
        }
        private delegate void FlushClient();//代理 
        private void ThreadFunction()
        {
            while (true)
            {
                progressBar1.Value = progressBar1Value;
                Thread.Sleep(100);
            }
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

            progressBar1Value = 2;
            progressBar1Max = DictFiles.Count + 12;
            progressBar1.Maximum = progressBar1Max;

            //枚举表
            ReadEnums();
            progressBar1ValueAdd();

            CDictPages.Clear();
            DictFilePages.Clear();

            

            if (使用多线程)
            {
                foreach (var nn in DictFiles.Values)
                {
                    //Task task = Task.Run(() => {
                    //    ReadFile(nn);
                    //    nn.Read = true;
                    //});
                    Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod));
                    thread.Start(nn);
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

                    DictDictEnums = (from it in CDictDictEnums orderby it.Key ascending select it).ToDictionary(it=>it.Key, it => it.Value);
                    DictPages = (from it in CDictPages orderby it.Key ascending select it).ToDictionary(it=>it.Key, it => it.Value);

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
            label2.Text = $"耗时 {BenchmarkStopwatch.ElapsedMilliseconds} 毫秒";

            Thread.Sleep(1000);
            MessageBoxShow($"导出完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetEnabled(true);
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
                                    XTool.FTool.MessageBoxShow($"重复的枚举表Key {DictKey} {enumK}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                if (DictList.ContainsKey(enumV))
                                {
                                    XTool.FTool.MessageBoxShow($"重复的枚举表Value {DictKey} {enumV}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                DictList[enumK] = enumV;
                            }
                            if (CDictDictEnums.ContainsKey(DictKey))
                            {
                                XTool.FTool.MessageBoxShow($"重复的枚举表类型 {DictKey}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            UpdateFileTs(fileinfo, DateTime.Now - time_start);
        }

        #endregion

        
    }
}
