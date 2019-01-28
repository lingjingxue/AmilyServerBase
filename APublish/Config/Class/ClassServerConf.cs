using System.Collections.Generic;
using UtilLib;

namespace ServerBase.Config
{
	public static partial class Conf
	{
		//文本	// B_本地化配置表.xlsx
		public static Dictionary<string, ConfigLanguage> ConfLanguage = new Dictionary<string, ConfigLanguage>();
		//升星消耗	// T_通用.xlsx
		public static Dictionary<int, ConfigStarCost> ConfStarCost = new Dictionary<int, ConfigStarCost>();
		//背包	// B_背包.xlsx
		public static Dictionary<int, ConfigBag> ConfBag = new Dictionary<int, ConfigBag>();
		//掉落	// D_掉落.xlsx
		public static Dictionary<int, ConfigDrop> ConfDrop = new Dictionary<int, ConfigDrop>();
		//重铸消耗	// T_通用.xlsx
		public static Dictionary<int, ConfigReforgeCost> ConfReforgeCost = new Dictionary<int, ConfigReforgeCost>();
		//英雄	// Y_英雄.xlsx
		public static Dictionary<int, ConfigHero> ConfHero = new Dictionary<int, ConfigHero>();
		//经验	// T_通用.xlsx
		public static Dictionary<int, ConfigExp> ConfExp = new Dictionary<int, ConfigExp>();
		//E全局常数	// Q_全局常数.xlsx
		public static Dictionary<int, GlobalConfig> GlobalConf = new Dictionary<int, GlobalConfig>();
		//掉落限制	// D_掉落.xlsx
		public static Dictionary<int, ConfigDropLimit> ConfDropLimit = new Dictionary<int, ConfigDropLimit>();
		//E邮件文本	// B_Lang邮件.xlsx
		public static Dictionary<int, ConfigMailText> ConfMailText = new Dictionary<int, ConfigMailText>();
		//突破	// T_通用.xlsx
		public static Dictionary<int, ConfigBreak> ConfBreak = new Dictionary<int, ConfigBreak>();
		//铸造	// T_通用.xlsx
		public static Dictionary<int, ConfigForge> ConfForge = new Dictionary<int, ConfigForge>();
		//E错误码	// B_Lang返回码.xlsx
		public static Dictionary<int, ConfigProtocolResult> ConfProtocolResult = new Dictionary<int, ConfigProtocolResult>();
	}
}
