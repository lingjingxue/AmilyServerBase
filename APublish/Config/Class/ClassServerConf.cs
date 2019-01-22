using System.Collections.Generic;
using UtilLib;

namespace ServerBase.Config
{
	public static partial class Conf
	{
		//文本	// B_本地化配置表.xlsx
		public static Dictionary<string, ConfigLanguage> ConfLanguage = new Dictionary<string, ConfigLanguage>();
		//背包	// B_背包.xlsx
		public static Dictionary<int, ConfigBag> ConfBag = new Dictionary<int, ConfigBag>();
		//掉落	// D_掉落.xlsx
		public static Dictionary<int, ConfigDrop> ConfDrop = new Dictionary<int, ConfigDrop>();
		//掉落限制	// D_掉落.xlsx
		public static Dictionary<int, ConfigDropLimit> ConfDropLimit = new Dictionary<int, ConfigDropLimit>();
		//E错误码	// F_返回码.xlsx
		public static Dictionary<int, ConfigProtocolResult> ConfProtocolResult = new Dictionary<int, ConfigProtocolResult>();
		//E全局常数	// Q_全局常数.xlsx
		public static Dictionary<int, GlobalConfig> GlobalConf = new Dictionary<int, GlobalConfig>();
		//经验	// T_通用.xlsx
		public static Dictionary<int, ConfigExp> ConfExp = new Dictionary<int, ConfigExp>();
		//突破	// T_通用.xlsx
		public static Dictionary<int, ConfigBreak> ConfBreak = new Dictionary<int, ConfigBreak>();
		//铸造	// T_通用.xlsx
		public static Dictionary<int, ConfigForge> ConfForge = new Dictionary<int, ConfigForge>();
		//重铸消耗	// T_通用.xlsx
		public static Dictionary<int, ConfigReforgeCost> ConfReforgeCost = new Dictionary<int, ConfigReforgeCost>();
		//升星消耗	// T_通用.xlsx
		public static Dictionary<int, ConfigStarCost> ConfStarCost = new Dictionary<int, ConfigStarCost>();
		//返回码带多语言	// F_返回码.xlsx
		public static Dictionary<int, ConfigResultCode> ConfResultCode = new Dictionary<int, ConfigResultCode>();
		//姓	// X_姓名表.xlsx
		public static Dictionary<string, ConfigSurname> ConfSurname = new Dictionary<string, ConfigSurname>();
		//男名	// X_姓名表.xlsx
		public static Dictionary<string, ConfigMalename> ConfMalename = new Dictionary<string, ConfigMalename>();
		//女名	// X_姓名表.xlsx
		public static Dictionary<string, ConfigFemalename> ConfFemalename = new Dictionary<string, ConfigFemalename>();
		//屏蔽字	// X_姓名表.xlsx
		public static Dictionary<string, ConfigBadwords> ConfBadwords = new Dictionary<string, ConfigBadwords>();
		//英雄	// Y_英雄.xlsx
		public static Dictionary<int, ConfigHero> ConfHero = new Dictionary<int, ConfigHero>();
	}
}
