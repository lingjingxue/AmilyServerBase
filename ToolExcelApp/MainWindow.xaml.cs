using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ToolExcelApp.XTool;

namespace ToolExcelApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            XTool.Wd = this;

            InitializeComponent();

            //全局
            PathCurrent = Directory.GetCurrentDirectory();
            PathCurrent = XGlobal.GetParentFolder(PathCurrent);
            //PathCurrent = XGlobal.GetParentFolder(PathCurrent);

            //组件
            this.Width = 1400;
            this.Height = 900;

            //TextBoxExcel.Text = PathExcel = PathCurrent + @"\ConfigV2\Excel";
            //TextBoxOutput.Text = PathOutput = PathCurrent + @"\ConfigV2";
            TextBoxExcel.Text = PathExcel = PathCurrent + @"\Config\Excel";
            TextBoxOutput.Text = PathOutput = PathCurrent + @"\Config";

            PathClass = PathOutput + @"\Class";
            PathJson = PathOutput + @"\Json";
            PathJsonClient = PathOutput + @"\JsonClient";
            PathJsonServer = PathOutput + @"\JsonServer";
            PathLua = PathOutput + @"\Lua";

            PathEnum = PathExcel + @"\A_公共枚举.xlsx";

            ComboBoxLang.Items.Add("LangChs");
            ComboBoxLang.Items.Add("LangEn");
            ComboBoxLang.Items.Add("LangKo");
            ComboBoxLang.SelectedIndex = 0;

            //if (true)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        var fff1 = new XFileInfo() { FileName = "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" + i };
            //        DictXFileInfo[fff1.FileName] = fff1;
            //    }
            //    ObservableCollectionXFileInfo = new ObservableCollection<XFileInfo>(DictXFileInfo.Values);
            //}
            DataGridFile.ItemsSource = ObservableCollectionXFileInfo;

            Dump();
        }
        public void Dump()
        {
            if (!XToolTest)
            {
                return;
            }
            StringBuilder sbDump = new StringBuilder();
            foreach (var item in ObservableCollectionXFileInfo)
            {
                sbDump.Append(item.NeedRead);
            }
            sbDump.Append(" ");
            foreach (var item in DictXFileInfo.Values)
            {
                sbDump.Append(item.NeedRead);
            }
            LabelDump.Content = sbDump.ToString();
        }
        public void MessageBoxShow(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            MessageBox.Show(text, caption, buttons, icon);
        }
        public string GetLang => (ComboBoxLang.Text);
        private void Button_Click_Select(object sender, RoutedEventArgs e)
        {
            foreach (var item in ObservableCollectionXFileInfo)
            {
                item.NeedRead = true;
            }
            DataGridFile.ItemsSource = null;
            DataGridFile.ItemsSource = ObservableCollectionXFileInfo;
            Dump();
        }
        private void Button_Click_Unselect(object sender, RoutedEventArgs e)
        {
            foreach (var item in ObservableCollectionXFileInfo)
            {
                item.NeedRead = false;
            }
            DataGridFile.ItemsSource = null;
            DataGridFile.ItemsSource = ObservableCollectionXFileInfo;
            Dump();
        }
        private void Button_Click_OutputSelect(object sender, RoutedEventArgs e)
        {
            全部导出 = false;
            开始导出();

            Dump();
        }
        private void Button_Click_GetFiles(object sender, RoutedEventArgs e)
        {
            DictXFileInfo.Clear();

            GetFileList(PathExcel);
            ObservableCollectionXFileInfo = new ObservableCollection<XFileInfo>(DictXFileInfo.Values);

            DataGridFile.ItemsSource = null;
            DataGridFile.ItemsSource = ObservableCollectionXFileInfo;
            Dump();
        }
        private void Button_Click_OutputAll(object sender, RoutedEventArgs e)
        {
            foreach (var item in ObservableCollectionXFileInfo)
            {
                item.NeedRead = true;
            }
            DataGridFile.ItemsSource = null;
            DataGridFile.ItemsSource = ObservableCollectionXFileInfo;

            全部导出 = true;
            开始导出();

            Dump();
        }


        


    }
}
