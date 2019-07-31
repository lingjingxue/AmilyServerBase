using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public static partial class XGlobal
    {
        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static string GetParentFolder(string dir, int layer = 1)
        {
            string result = dir;
            try
            {
                for (int i = 0; i < layer; i++)
                {
                    result = Directory.GetParent(result).FullName;
                }
            }
            catch
            {
            }
            return result;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename"></param>
        public static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
        /// <summary>
        /// 清空目录
        /// </summary>
        /// <param name="dir"></param>
        public static void EmptyFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d);
                    }
                    else
                    {
                        DeleteFolder(d);
                    }
                }
            }
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                EmptyFolder(dir);
                Directory.Delete(dir);
            }
        }


        public static int ToInt(this string str)
        {
            int.TryParse(str, out int result);
            return result;
        }
        public static float ToFloat(this string str)
        {
            float.TryParse(str, out float result);
            return result;
        }
        public static string[] Split(this string s, string separator)
        {
            if (string.IsNullOrEmpty(s))
                return new string[0];

            return s.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static List<string> ToListString(this string str, string strSeparator = "|")
        {
            string[] nodes = str.Split(strSeparator);
            return nodes.ToList();
        }
        public static List<List<string>> ToListListString(this string str, string strSeparator1 = "|", string strSeparator2 = "_")
        {
            string[] nodes1 = str.Split(strSeparator1);
            var result = new List<List<string>>();
            foreach (var nn in nodes1)
            {
                string[] nodes2 = nn.Split(strSeparator2);
                result.Add(nodes2.ToList());
            }
            return result;
        }
        public static List<int> ToListInt(this string str, string strSeparator = "|")
        {
            string[] nodes = str.Split(strSeparator);
            int[] nodesresult = Array.ConvertAll(nodes, delegate (string s) { return s.ToInt(); });
            return nodesresult.ToList();
        }
        public static List<List<int>> ToListListInt(this string str, string strSeparator1 = "|", string strSeparator2 = "_")
        {
            string[] nodes1 = str.Split(strSeparator1);
            var result = new List<List<int>>();
            foreach (var nn in nodes1)
            {
                string[] nodes2 = nn.Split(strSeparator2);
                int[] nodesresult = Array.ConvertAll(nodes2, delegate (string s) { return s.ToInt(); });
                result.Add(nodesresult.ToList());
            }
            return result;
        }
        public static List<float> ToListFloat(this string str, string strSeparator = "|")
        {
            string[] nodes = str.Split(strSeparator);
            float[] nodesresult = Array.ConvertAll(nodes, delegate (string s) { return s.ToFloat(); });
            return nodesresult.ToList();
        }
        public static List<List<float>> ToListListFloat(this string str, string strSeparator1 = "|", string strSeparator2 = "_")
        {
            string[] nodes1 = str.Split(strSeparator1);
            var result = new List<List<float>>();
            foreach (var nn in nodes1)
            {
                string[] nodes2 = nn.Split(strSeparator2);
                float[] nodesresult = Array.ConvertAll(nodes2, delegate (string s) { return s.ToFloat(); });
                result.Add(nodesresult.ToList());
            }
            return result;
        }


    }
}
