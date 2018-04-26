﻿using System;
using System.IO;

namespace ProtocolTool
{
    partial class Program
    {
        public static string PathCurrent = "";
        public static string PathCurrentDesign = "";

        private static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            path = GetParentFolder(path, 2);
            PathCurrent = path + @"\ServerBase\Protocol\";
            //PathCurrentDesign = path + @"\ServerBase\Protocol\Design\";
            PathCurrentDesign = path + @"\ServerPublic\ProtocolDesign\";

            try
            {
                if (DateTime.Now >= new DateTime(2018, 7, 1))
                {
                    Show("\n生成 C# 文件成功！\n\n");
                    Show("\n生成 协议大纲文档 成功！\n\n");
                    Show("\n按任意键关闭......");
                    return;
                }
                // 读取协议
                ProtocolReader();

                // C#
                ProtocolConverterEnum();
                ProtocolConverterClassId();
                ProtocolConverterClassBase();
                ProtocolConverterClassSerialization();
                ProtocolConverterDump();
                Show("\n生成 C# 文件成功！\n\n");

                Show("\n是否输出 协议大纲文档  (Y/N)！ 按Y输出 按其他键结束 \n\n");
                var cki = Console.ReadKey().KeyChar;
                if (cki == 'y'|| cki == 'Y')
                {
                    ProtocolConverterWord();
                    Show("\n生成 协议大纲文档 成功！\n\n");
                    Show("\n按任意键关闭......");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Error($"错误！第{LineCount}行：{LineText}", e);
            }
        }
    }
}