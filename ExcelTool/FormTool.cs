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
        private void SetEnabled(bool value = true)
        {
            panel1.Enabled = value;
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
                dgv.Rows[rowindex].Cells[1].Value = $"{item.Ts.TotalMilliseconds} ms";
                if (item.Need)
                {
                    dgv.Rows[rowindex].Cells[1].Style.BackColor = Color.Green;
                }
                else
                {
                    dgv.Rows[rowindex].Cells[1].Style.BackColor = Color.Gray;
                }
            }

            UpdateprogressBar1();
        }
        private void UpdateprogressBar1()
        {
            progressBar1.Maximum = DictFiles.Count + 15;
            progressBar1.Value = 0;
        }
        public void UpdateFileTs(XFileInfo fileinfo, TimeSpan ts)
        {
            try
            {
                var dgv = dataGridView1;
                dgv.Rows[fileinfo.rowindex].Cells[1].Value = $"{ts.TotalMilliseconds} ms";
            }
            catch { }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = dataGridView1;
            if (dgv.Columns[e.ColumnIndex].Name.Equals("Column3"))
            {
                try
                {
                    var row = dgv.Rows[e.RowIndex];
                    string name = (string)(row.Cells[0].Value);
                    if (DictFiles.ContainsKey(name))
                    {
                        var old = DictFiles[name].Need;
                        var need = DictFiles[name].Need = !old;
                        var cell = dgv[e.ColumnIndex - 1, e.RowIndex];
                        if (need)
                        {
                            cell.Style.BackColor = Color.Green;
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Gray;
                        }
                    }
                }
                catch { }
            }
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
            if (DateTime.Now >= new DateTime(2020, 7, 1))
            {
                return;
            }

            UpdateprogressBar1();
            var ageCoun = (from s in DictFiles.Values
                           select s.Need)
                           .Count(a => a == true);
            if (ageCoun <= 0)
            {
                MessageBox.Show($"需要导出的列表为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(PathExcel))
            {
                MessageBox.Show($"导入文档路径< {PathExcel} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathClass))
            {
                MessageBox.Show($"导入文档路径< {PathClass} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathXml))
            {
                MessageBox.Show($"导入文档路径< {PathXml} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathJson))
            {
                MessageBox.Show($"导入文档路径< {PathJson} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(PathLua))
            {
                MessageBox.Show($"导入文档路径< {PathLua} >并不存在！请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"导出失败，请重试！错误 ({ex.Message})", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Thread.Sleep(1000);
            MessageBox.Show($"导出完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetEnabled(true);
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

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (var item in DictFiles.Values)
            {
                item.Need = true;
            }
            UpdateFileList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (var item in DictFiles.Values)
            {
                item.Need = false;
            }
            UpdateFileList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DictFiles.Clear();
            UpdateFileList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = true;
            ofd.InitialDirectory = PathExcel;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in ofd.FileNames)
                {
                    if (File.Exists(filename))
                    {
                        string filename_excel = Path.GetFileNameWithoutExtension(filename);
                        string firs = filename_excel.Substring(0, 1);
                        if (firs != "A")
                        {
                            XFileInfo finfo = new XFileInfo(filename);
                            DictFiles[finfo.Name] = finfo;
                        }
                    }
                }
                UpdateFileList();
            }
        }

        #region 导出

        public string GetLang => (comboBox1.Text);

        public void TransferFiles()
        {
            //枚举表
            ReadEnums();
            progressBar1.Increment(1);

            DictPages.Clear();
            DictFilePages.Clear();
            foreach (var nn in DictFiles.Values)
            {
                if (nn.Need)
                {
                    ReadFile(nn);
                }
                progressBar1.Increment(1);
            }

            TransferHtml();
            progressBar1.Increment(1);

            TransferClassServer();
            progressBar1.Increment(1);
            TransferClassServerConf();
            progressBar1.Increment(1);
            TransferClassServerConfRead();
            progressBar1.Increment(1);

            TransferClassClient();
            progressBar1.Increment(1);
            TransferClassClientConf();
            progressBar1.Increment(1);
            TransferClassClientConfRead();
            progressBar1.Increment(1);

            TransferClassClientLua();
            progressBar1.Increment(1);

            Transfers();
            progressBar1.Value = progressBar1.Maximum;
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

                    var page = new PageInfo(pagename);
                    page.NameCn = pagenamecn;
                    page.NameFile = Path.GetFileName(path_excel);
                    page.ValidType = fileinfo.ValidType;

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
                                if (DictListEnums.TryGetValue(page.HeadEnum[k], out var DictList))
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
                        DictPages.Add(page.Name, page);

                        if (!DictFilePages.TryGetValue(page.NameFile, out var filepages))
                        {
                            filepages = new List<PageInfo>();
                            DictFilePages[page.NameFile] = filepages;
                        }
                        filepages.Add(page);
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
                            if (!DictList.ContainsKey(enumK))
                            {
                                DictList.Add(enumK, enumV);
                            }
                        }
                        DictListEnums.Add(DictKey, DictList);
                    }
                }
            }
            UpdateFileTs(fileinfo, DateTime.Now - time_start);
        }
        #endregion

    }
}
