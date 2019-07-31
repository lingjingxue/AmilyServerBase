using System.Collections.Generic;
using UtilLib;

namespace ServerBase.Config
{
	public static partial class Conf
	{
		public static bool InitConfSettings()
		{
			bool result = true;

			//背包 
			if (result) { result = ReadConfig(typeof(ConfigBag).Name, ref ConfBag); }
			//突破 
			if (result) { result = ReadConfig(typeof(ConfigBreak).Name, ref ConfBreak); }
			//掉落 掉落2 
			if (result) { result = ReadConfig(typeof(ConfigDrop).Name, ref ConfDrop); }
			//掉落限制 
			if (result) { result = ReadConfig(typeof(ConfigDropLimit).Name, ref ConfDropLimit); }
			//经验 
			if (result) { result = ReadConfig(typeof(ConfigExp).Name, ref ConfExp); }
			//铸造 
			if (result) { result = ReadConfig(typeof(ConfigForge).Name, ref ConfForge); }
			//英雄 
			if (result) { result = ReadConfig(typeof(ConfigHero).Name, ref ConfHero); }
			//文本 
			if (result) { result = ReadConfig(typeof(ConfigLanguage).Name, ref ConfLanguage); }
			//E邮件文本 
			if (result) { result = ReadConfig(typeof(ConfigMailText).Name, ref ConfMailText); }
			//E错误码 
			if (result) { result = ReadConfig(typeof(ConfigProtocolResult).Name, ref ConfProtocolResult); }
			//重铸消耗 
			if (result) { result = ReadConfig(typeof(ConfigReforgeCost).Name, ref ConfReforgeCost); }
			//游戏服务器列表 
			if (result) { result = ReadConfig(typeof(ConfigServerGame).Name, ref ConfServerGame); }
			//服务器平台配置 
			if (result) { result = ReadConfig(typeof(ConfigServerPlatform).Name, ref ConfServerPlatform); }
			//升星消耗 
			if (result) { result = ReadConfig(typeof(ConfigStarCost).Name, ref ConfStarCost); }
			//下兵策略 
			if (result) { result = ReadConfig(typeof(ConfigStrategy).Name, ref ConfStrategy); }
			//E全局常数 
			if (result) { result = ReadConfig(typeof(GlobalConfig).Name, ref GlobalConf); }

			if (result) { result = InitConfSettingsExt(); }

			return result;
		}
	}
}
