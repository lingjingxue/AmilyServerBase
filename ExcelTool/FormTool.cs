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
            textBox3.Text = PathClass = PathCurrent + @"\Config\Class";
            textBox4.Text = PathXml = PathCurrent + @"\Config\Xml";
            textBox5.Text = PathJson = PathCurrent + @"\Config\Json";
            textBox1.Text = PathLua = PathCurrent + @"\Config\Lua";

            comboBox1.Items.Add("LangChs");
            comboBox1.Items.Add("LangEn");
            comboBox1.Items.Add("LangKo");
            comboBox1.SelectedIndex = 0;

            XTool.Init();
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

            GetFileList(PathExcel, EValidType.公共);
            GetFileList(PathExcel + @"\服务器", EValidType.仅服务器);
            GetFileList(PathExcel + @"\客户端", EValidType.仅客户端);

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

        private void button4_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathClass;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox3.Text = PathClass = fbd.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathXml;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox4.Text = PathXml = fbd.SelectedPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathJson;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox5.Text = PathJson = fbd.SelectedPath;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathLua;
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                textBox1.Text = PathLua = fbd.SelectedPath;
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
            if (!Directory.Exists(PathClass))
            {
                MessageBoxShow($"导入文档路径< {PathClass} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathXml))
            {
                MessageBoxShow($"导入文档路径< {PathXml} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathJson))
            {
                MessageBoxShow($"导入文档路径< {PathJson} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathLua))
            {
                MessageBoxShow($"导入文档路径< {PathLua} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

        public static Stopwatch BenchmarkStopwatch;

        public void ThreadMethod(object obj)
        {
            XFileInfo nn = obj as XFileInfo;
            ReadFile(nn);
            nn.Read = true;
        }
        public void TransferFiles()
        {
            BenchmarkStopwatch = Stopwatch.StartNew();

            //枚举表
            ReadEnums();

            DictPages.Clear();
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
                }
            }

            bool 读取完成 = false;
            while (!读取完成)
            {
                读取完成 = DictFiles.Values.All(it => it.Read);
                if (读取完成)
                {
                    TransferOutput();
                }
                Thread.Sleep(200);
            }
        }

        public void TransferOutput()
        {
            TransferHtml();

            TransferClassServer();

            TransferClassServerConf();

            TransferClassServerConfRead();

            TransferClassClient();

            TransferClassClientConf();

            TransferClassClientConfRead();

            TransferClassClientLua();

            TransferData();

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
                    if (DictPages.TryGetValue(pagename, out var page))
                    {
                        newpage = false;
                    }
                    else
                    {
                        page = new PageInfo(pagename);
                    }
                    page.NameCn += $"{pagenamecn}";
                    page.NameFile += $"{Path.GetFileName(path_excel)}";
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
                            if (DictDictEnums.ContainsKey(DictKey))
                            {
                                XTool.FTool.MessageBoxShow($"重复的枚举表类型 {DictKey}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DictDictEnums[DictKey] = DictList;
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
                                if (DictDictEnums.TryGetValue(page.HeadEnum[k], out var DictList))
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
                        DictPages[page.Name] = page;

                        if (!DictFilePages.TryGetValue(page.NameFile, out var filepages))
                        {
                            filepages = new List<string>();
                            DictFilePages[page.NameFile] = filepages;
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
