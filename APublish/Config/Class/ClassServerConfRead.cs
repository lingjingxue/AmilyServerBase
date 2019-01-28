using System.Collections.Generic;
using UtilLib;

namespace ServerBase.Config
{
	public static partial class Conf
	{
		public static bool InitConfSettings()
		{
			bool result = true;

			//文本
			if (result) { result = ReadConfig(typeof(ConfigLanguage).Name, ref ConfLanguage); }
			//升星消耗
			if (result) { result = ReadConfig(typeof(ConfigStarCost).Name, ref ConfStarCost); }
			//背包
			if (result) { result = ReadConfig(typeof(ConfigBag).Name, ref ConfBag); }
			//掉落
			if (result) { result = ReadConfig(typeof(ConfigDrop).Name, ref ConfDrop); }
			//重铸消耗
			if (result) { result = ReadConfig(typeof(ConfigReforgeCost).Name, ref ConfReforgeCost); }
			//英雄
			if (result) { result = ReadConfig(typeof(ConfigHero).Name, ref ConfHero); }
			//经验
			if (result) { result = ReadConfig(typeof(ConfigExp).Name, ref ConfExp); }
			//E全局常数
			if (result) { result = ReadConfig(typeof(GlobalConfig).Name, ref GlobalConf); }
			//掉落限制
			if (result) { result = ReadConfig(typeof(ConfigDropLimit).Name, ref ConfDropLimit); }
			//E邮件文本
			if (result) { result = ReadConfig(typeof(ConfigMailText).Name, ref ConfMailText); }
			//突破
			if (result) { result = ReadConfig(typeof(ConfigBreak).Name, ref ConfBreak); }
			//铸造
			if (result) { result = ReadConfig(typeof(ConfigForge).Name, ref ConfForge); }
			//E错误码
			if (result) { result = ReadConfig(typeof(ConfigProtocolResult).Name, ref ConfProtocolResult); }

			if (result) { result = InitConfSettingsExt(); }

			return result;
		}
	}
}
