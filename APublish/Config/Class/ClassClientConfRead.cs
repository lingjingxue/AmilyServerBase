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
			//背包
			if (result) { result = ReadConfig(typeof(ConfigBag).Name, ref ConfBag); }
			//掉落
			if (result) { result = ReadConfig(typeof(ConfigDrop).Name, ref ConfDrop); }
			//掉落限制
			if (result) { result = ReadConfig(typeof(ConfigDropLimit).Name, ref ConfDropLimit); }
			//E错误码
			if (result) { result = ReadConfig(typeof(ConfigProtocolResult).Name, ref ConfProtocolResult); }
			//E全局常数
			if (result) { result = ReadConfig(typeof(GlobalConfig).Name, ref GlobalConf); }
			//经验
			if (result) { result = ReadConfig(typeof(ConfigExp).Name, ref ConfExp); }
			//突破
			if (result) { result = ReadConfig(typeof(ConfigBreak).Name, ref ConfBreak); }
			//铸造
			if (result) { result = ReadConfig(typeof(ConfigForge).Name, ref ConfForge); }
			//重铸消耗
			if (result) { result = ReadConfig(typeof(ConfigReforgeCost).Name, ref ConfReforgeCost); }
			//升星消耗
			if (result) { result = ReadConfig(typeof(ConfigStarCost).Name, ref ConfStarCost); }
			//帮助
			if (result) { result = ReadConfig(typeof(ConfigHelp).Name, ref ConfHelp); }
			//抽卡
			if (result) { result = ReadConfig(typeof(ConfigDraw).Name, ref ConfDraw); }
			//抽卡奖池
			if (result) { result = ReadConfig(typeof(ConfigDrawPool).Name, ref ConfDrawPool); }
			//道具
			if (result) { result = ReadConfig(typeof(ConfigItem).Name, ref ConfItem); }

			if (result) { result = InitConfSettingsExt(); }

			return result;
		}
	}
}
