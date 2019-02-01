using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        public void MessageBoxShow(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
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
    }
}
