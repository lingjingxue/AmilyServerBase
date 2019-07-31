using System.Collections.Generic;

namespace ServerBase.Config
{
	public class ConfigBag : IConfigBase	// B_背包.xlsx 
	{
		public int Id; //ID
		public string Name; //名
		public string Desc; //描述
		public int TypeBag; //类别
		public string ListInt; //ListInt
		public string ListListInt; //ListListInt
		public string Liststring; //Liststring
		public string ListListstring; //ListListstring
		public object GetKey() { return Id; }
	}
	public class ConfigBreak : IConfigBase	// T_通用.xlsx 
	{
		public int Id; //ID
		public int BreakPower; //突破战力
		public int LevelMax; //最大等级
		public int AfterLevel; //突破后等级
		public string ItemCost; //消耗
		public object GetKey() { return Id; }
	}
	public class ConfigDraw : IConfigBase	// C_抽卡.xlsx 
	{
		public int Id; //ID
		public string Name; //名字
		public string Desc1; //描述1
		public string Desc2; //描述2
		public string OneCost; //单抽消耗
		public string OneItemCost; //单抽优先消耗
		public string TenCost; //连抽消耗
		public string TenItemCost; //连抽优先消耗
		public string OneAward; //单抽奖励
		public string TenAward; //连抽奖励
		public int PoolType; //奖池类型
		public object GetKey() { return Id; }
	}
	public class ConfigDrawPool : IConfigBase	// C_抽卡.xlsx 
	{
		public int Id; //ID
		public int PoolType; //奖池类型
		public int Weight; //权重
		public int MultipleWeight; //第10次权重
		public string Award; //奖励
		public object GetKey() { return Id; }
	}
	public class ConfigDrop : IConfigBase	// D_掉落.xlsx D_掉落.xlsx 
	{
		public int Id; //ID
		public int DropType; //掉落类型
		public object GetKey() { return Id; }
	}
	public class ConfigDropLimit : IConfigBase	// D_掉落.xlsx 
	{
		public int Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigExp : IConfigBase	// T_通用.xlsx 
	{
		public int Id; //ID
		public int BreakLevel; //突破等级
		public int Level; //等级
		public string ExpCost; //经验消耗
		public int PowerGrow; //战力成长
		public object GetKey() { return Id; }
	}
	public class ConfigForge : IConfigBase	// T_通用.xlsx 
	{
		public int Id; //ID
		public string ItemCost; //消耗
		public int PowerPer; //战力百分比
		public object GetKey() { return Id; }
	}
	public class ConfigHelp : IConfigBase	// B_帮助.xlsx 
	{
		public int Id; //ID
		public string Title; //标题
		public string Desc; //描述
		public object GetKey() { return Id; }
	}
	public class ConfigItem : IConfigBase	// D_道具.xlsx 
	{
		public int Id; //ID
		public string Name; //名称
		public string Icon; //图标
		public string Intr; //描述介绍
		public int Quality; //品质
		public string ItemType; //物品分类
		public int BagLabel; //背包标签
		public int MaxOwn; //最大拥有数
		public int CanSell; //可否出售
		public int SellPrice; //卖店价格
		public int CanUse; //可否使用
		public int CanUseBatch; //可否批量使用
		public int UseEffect; //道具使用效果
		public int UseEffectArgs; //使用效果参数
		public int CompoundNumber; //合成消耗数量
		public int CompoundHero; //合成英雄ID
		public int DayUseLimit; //每日使用次数限制
		public object GetKey() { return Id; }
	}
	public class ConfigLanguage : IConfigBase	// B_本地化配置表.xlsx 
	{
		public string Id; //ID
		public string Ch; //中文
		public string En; //英文
		public string r; //日语
		public string e; //俄语
		public string f; //法语
		public string d; //德语
		public string p; //葡萄牙语
		public string x; //西班牙语
		public string a; //阿拉伯语
		public object GetKey() { return Id; }
	}
	public class ConfigMailText : IConfigBase	// B_Lang邮件.xlsx 
	{
		public int MailText; //ID
		public string Lang; //中文简体
		public object GetKey() { return MailText; }
	}
	public class ConfigProtocolResult : IConfigBase	// B_Lang返回码.xlsx 
	{
		public int ProtocolResult; //ID
		public string Lang; //中文简体
		public object GetKey() { return ProtocolResult; }
	}
	public class ConfigReforgeCost : IConfigBase	// T_通用.xlsx 
	{
		public int Id; //ID
		public int LevelNeed; //要求铸造等级
		public string ItemCost; //道具消耗
		public object GetKey() { return Id; }
	}
	public class ConfigServerGame : IConfigBase	// F_服务器配置.xlsx 
	{
		public int Id; //ID
		public int ServerPlatform; //服务器运营平台
		public int ServerType; //服务器类型
		public string ServerArea; //服务器大区名称
		public string ServerName; //服务器名称
		public string ServerIp; //服务器对外地址
		public int ServerPort; //服务器端口
		public int ServerStatus; //模拟服务器状态
		public int Recommend; //是否推荐服务器
		public int NewSever; //是否新服
		public int SeasonNum; //当前赛季
		public int TowerVersion; //武神塔版本
		public object GetKey() { return Id; }
	}
	public class ConfigStarCost : IConfigBase	// T_通用.xlsx 
	{
		public int Id; //ID
		public int Quality; //品质
		public int StarLevel; //星级
		public string ItemCost; //消耗
		public string Decomposed; //分解获得精华
		public object GetKey() { return Id; }
	}
	public class ConfigStrategy : IConfigBase	// G_关卡.xlsx 
	{
		public int Id; //ID
		public string Strategy; //策略配置
		public object GetKey() { return Id; }
	}
	public class GlobalConfig : IConfigBase	// Q_全局常数.xlsx 
	{
		public int GlobalId; //ID
		public string Name; //全局名
		public string Desc; //配置说明
		public string Value; //配置值
		public object GetKey() { return GlobalId; }
	}
	public enum EAccess
	{
		本机调试 = 0,
		内网版本 = 1,
		外网版本 = 2,
	}
	public enum EAction
	{
		拥有武将 = 1,
		武将等级 = 2,
		道具兑换 = 3,
	}
	public enum EActivityType
	{
		其他 = 0,
		打造装备活动 = 1,
		武神塔活动 = 2,
		征讨匪寇活动 = 3,
		攻城兵力消耗减半 = 4,
		交易所活动 = 5,
	}
	public enum EAtkEffectType
	{
		普通命中 = 0,
		暴击 = 1,
		格挡 = 2,
		暴击并格挡 = 3,
	}
	public enum EAttributeType
	{
		攻击 = 1,
		兵力 = 2,
		城防 = 3,
	}
	public enum EAwardType
	{
		元宝 = 2,
		金币 = 20,
		经验 = 3,
		武将经验 = 33,
		势力经验 = 34,
		银两 = 1,
		木材 = 11,
		铁矿 = 13,
		粮草 = 15,
		道具 = 21,
		武将 = 22,
		装备 = 23,
		绑定武将 = 24,
		体力 = 8,
		武神令 = 6,
		竞技次数 = 9,
		军务积分 = 5,
		声望 = 30,
		个人贡献 = 31,
		势力资金 = 32,
		势力金币 = 36,
		势力豪礼宝箱经验 = 35,
		武神积分 = 61,
		活跃度 = 41,
		功勋 = 42,
		匪寇次数 = 91,
		资源点次数 = 92,
		攻城集结次数 = 93,
		全兵种兵力 = 100,
		步兵兵力 = 101,
		骑兵兵力 = 102,
		弓兵兵力 = 103,
		城池均分金币 = 200,
		活动货币 = 300,
	}
	public enum EBagLabel
	{
		装备 = 1,
		道具 = 2,
		材料 = 3,
		经验 = 101,
		货币 = 104,
		资源 = 106,
		副本资源 = 110,
		竞技资源 = 112,
		武神塔资源 = 113,
		军务资源 = 114,
		势力资源 = 115,
		匪寇资源 = 117,
		资源点资源 = 118,
		兵力 = 200,
	}
	public enum EBuildingType
	{
		商会 = 1102,
		交易所 = 1103,
		宴会厅 = 1104,
		剧情副本 = 1105,
		竞技场 = 1106,
		武神塔 = 1107,
		铸币厂 = 1201,
		铸币厂2 = 1211,
		铸币厂3 = 1221,
		农田 = 1202,
		农田2 = 1212,
		农田3 = 1222,
		伐木场 = 1203,
		伐木场2 = 1213,
		伐木场3 = 1223,
		铁矿场 = 1205,
		铁矿场2 = 1215,
		铁矿场3 = 1225,
		步兵营 = 1305,
		骑兵营 = 1306,
		弓兵营 = 1307,
		计策府 = 1308,
		要塞 = 1309,
		仓库 = 1301,
		工坊 = 1303,
		演武场 = 1304,
		府衙 = 2101,
		府库 = 2102,
		书院 = 2104,
		政务所 = 2105,
		军事所 = 2106,
		城墙 = 2202,
		箭塔 = 2204,
		守备营 = 2205,
		武神像 = 2206,
		珍宝阁 = 2207,
		人才府 = 1101,
		募兵所 = 1302,
		物资所 = 2103,
		战争大厅 = 2201,
		哨塔 = 2203,
	}
	public enum ECityScienceType
	{
		科技1 = 101,
		科技2 = 102,
		科技3 = 103,
		科技4 = 104,
		科技5 = 105,
		科技6 = 106,
	}
	public enum ECityType
	{
		王城 = 0,
		郡城 = 1,
		县城 = 2,
	}
	public enum EConditionType
	{
		兑换获得神将 = 1,
		进化获得神将 = 2,
		强化成功 = 3,
		打造获得装备 = 4,
		道具获得物品 = 5,
		道具获得武将 = 6,
		道具获得装备 = 7,
		抽奖获得武将 = 8,
		城池战斗集结 = 11,
	}
	public enum EDropLimitTime
	{
		日 = 1,
		周 = 2,
		月 = 3,
		季 = 4,
		年 = 5,
	}
	public enum EDropType
	{
		固定掉落 = 0,
		独立掉落 = 1,
		抽取放回 = 2,
		抽取不放回 = 3,
	}
	public enum EEliteType
	{
		随机 = 0,
		精英 = 1,
		非精英 = 2,
	}
	public enum EEquipType
	{
		主角武器 = 1,
		主角铠甲 = 2,
		主角头盔 = 3,
		主角战靴 = 4,
		主角马鞍 = 5,
		主角军印 = 6,
		主角时装 = 7,
		武将武器 = 11,
		武将头盔 = 12,
		武将铠甲 = 13,
		武将战靴 = 14,
		神装武器 = 81,
		神装头盔 = 82,
		神装铠甲 = 83,
		神装战靴 = 84,
	}
	public enum EFirstSysTech
	{
		武将培养 = 104,
		日常任务 = 105,
		好友对话 = 107,
		势力对话 = 108,
		军务对话 = 110,
	}
	public enum EForce
	{
		魏 = 1,
		蜀 = 2,
		吴 = 3,
		群 = 4,
	}
	public enum EGlobalId
	{
		账号长度最小 = 1,
		账号长度最大 = 2,
		密码长度最小 = 3,
		密码长度最大 = 4,
		昵称长度最小 = 5,
		昵称长度最大 = 6,
		默认英雄列表 = 8,
		最大突破等级 = 10,
		最大星级 = 11,
		最大重铸等级 = 12,
		防御系数 = 14,
		闪避系数 = 15,
		王者系数 = 16,
		普攻系数1 = 17,
		普攻系数2 = 18,
		先攻增加暴击伤害系数 = 19,
		闪避增加恢复效果系数 = 20,
		最小伤害值 = 21,
		抽卡积分上限 = 23,
		抽卡积分赠送道具 = 24,
	}
	public enum EGoodsType
	{
		武器 = 101,
		铠甲 = 102,
		头盔 = 103,
		战靴 = 104,
		武将 = 201,
		武将将魂 = 301,
		装备材料 = 401,
		神装材料 = 501,
		道具 = 601,
	}
	public enum EGoodsType2
	{
		步将武器 = 10101,
		骑将武器 = 10102,
		弓将武器 = 10103,
		步将铠甲 = 10201,
		骑将铠甲 = 10202,
		弓将铠甲 = 10203,
		步将头盔 = 10301,
		骑将头盔 = 10302,
		弓将头盔 = 10303,
		步将战靴 = 10401,
		骑将战靴 = 10402,
		弓将战靴 = 10403,
		优秀武将 = 50101,
		稀有武将 = 50102,
		史诗武将 = 50103,
		传奇武将 = 50104,
		青铜 = 70101,
		乌金 = 70102,
		弓将秘传 = 70103,
		骑将秘传 = 70104,
		步将秘传 = 70105,
		上古陨金 = 80101,
		残缺的上古箭首 = 80102,
		残缺的上古长枪 = 80103,
		残缺的上古宝刀 = 80104,
		燧石 = 90101,
		锦囊 = 90102,
	}
	public enum EIncidentType
	{
		直接事件 = 1,
		答题事件 = 2,
		战斗事件 = 3,
		资源事件 = 4,
	}
	public enum EItemType
	{
	}
	public enum ELivenessAction
	{
		登陆 = 1,
		日常 = 2,
		府衙 = 3,
		竞技 = 4,
		讨伐 = 5,
	}
	public enum ELotteryType
	{
		神将兑换 = 1,
	}
	public enum EMailText
	{
		发送者系统 = 1,
		红包标题 = 4,
		红包1 = 5,
		红包2 = 6,
		红包3 = 7,
		红包4 = 8,
		红包5 = 9,
		拍卖行成交 = 11,
		拍卖行交易时间 = 12,
		拍卖行物品名称 = 13,
		拍卖行交易数量 = 14,
		拍卖行交易总价 = 15,
		拍卖行成交税 = 16,
		长安夺宝战报 = 18,
		长安夺宝幸运奖励 = 19,
		长安夺宝活动奖励 = 20,
		长安夺宝1 = 22,
		长安夺宝2 = 23,
		长安夺宝3 = 24,
		长安夺宝4 = 25,
		长安夺宝5 = 26,
		交易挂买成交 = 29,
		交易挂卖成交 = 30,
		交易买入成交 = 31,
		交易卖出成交 = 32,
		交易撤销 = 33,
		商会交易时间 = 34,
		商会交易数量 = 35,
		商会交易单价 = 36,
		商会交易总价 = 37,
		商会交易税 = 38,
		诸侯发放俸禄 = 42,
		诸侯发放俸禄1 = 43,
		诸侯发放俸禄2 = 44,
		竞技场排名奖励 = 47,
		竞技场每日奖励 = 48,
		竞技场每周奖励 = 49,
		竞技场时间 = 50,
		竞技场名次 = 51,
		讨伐黄巾终结奖励 = 55,
		讨伐黄巾胜利奖励 = 56,
		讨伐黄巾战斗奖励 = 57,
		讨伐黄巾1 = 58,
		讨伐黄巾2 = 59,
		讨伐黄巾3 = 60,
		讨伐黄巾4 = 61,
		讨伐黄巾5 = 62,
		讨伐黄巾6 = 63,
		资源采集标题 = 67,
		资源采集1 = 68,
		资源采集2 = 69,
		资源采集3 = 70,
		资源采集4 = 71,
		演武每日奖励发送者 = 74,
		演武每日奖励标题 = 75,
		演武每周奖励标题 = 76,
		演武每日奖励正文 = 77,
		演武每周奖励正文 = 78,
		征讨匪寇战报标题 = 82,
		征讨匪寇战报1 = 83,
		征讨匪寇战报2 = 84,
		势力活跃诸侯宝箱 = 89,
		势力活跃太守宝箱 = 90,
		势力活跃成员宝箱 = 91,
		势力活跃正文 = 92,
		郡城排名奖励标题 = 97,
		郡城排名奖励正文 = 98,
		首次攻占城池奖励标题 = 101,
		首次攻占城池奖励正文 = 102,
		流亡军战报标题 = 106,
		流亡军战报发送者 = 107,
		流亡军侦查发送者 = 108,
		流亡军被侦查标题 = 109,
		流亡军侦查失败标题 = 110,
		流亡军侦查成功标题 = 111,
	}
	public enum EMoneyType
	{
		银两 = 1,
		元宝 = 2,
		金币 = 20,
		军务积分 = 5,
		个人贡献 = 31,
		声望 = 30,
		武神积分 = 61,
		活动货币 = 300,
	}
	public enum EOutBuffType
	{
		定值增加全部资源产量 = 1100,
		定值增加银两产量 = 1101,
		定值增加木材产量 = 1102,
		定值增加铁矿产量 = 1104,
		定值增加粮食产量 = 1105,
		比例增加全部资源产量 = 1200,
		比例增加银两产量 = 1201,
		比例增加木材产量 = 1202,
		比例增加铁矿产量 = 1204,
		比例增加粮食产量 = 1205,
		比例增加资源争夺银两产量 = 1211,
		比例增加资源争夺木材产量 = 1212,
		比例增加资源争夺铁矿产量 = 1214,
		比例增加资源争夺粮食产量 = 1215,
		定值增加仓库木材储量 = 1302,
		定值增加仓库铁矿储量 = 1304,
		定值增加仓库粮食储量 = 1305,
		比例增加全局经验获取 = 2011,
		比例增加主线副本经验获取 = 2012,
		比例增加主线副本武将经验获取 = 2013,
		减少剧情体力恢复间隔 = 2021,
		比例减少建筑建造时间 = 2031,
		比例减少建筑建造消耗 = 2032,
		比例减少募兵时间 = 2041,
		比例减少步兵募兵时间 = 2042,
		比例减少骑兵募兵时间 = 2043,
		比例减少弓兵募兵时间 = 2044,
		比例减少研究消耗时间 = 2050,
		在战斗中获取指定ID的BUFF = 3001,
		全局定值增加攻击 = 4001,
		全局定值增加防御 = 4002,
		全局定值增加谋略 = 4003,
		步兵攻击 = 4101,
		步兵防御 = 4102,
		步兵谋略 = 4103,
		步兵兵力 = 4104,
		骑兵攻击 = 4201,
		骑兵防御 = 4202,
		骑兵谋略 = 4203,
		骑兵兵力 = 4204,
		弓兵攻击 = 4301,
		弓兵防御 = 4302,
		弓兵谋略 = 4303,
		弓兵兵力 = 4304,
		步兵攻击百分比 = 5101,
		步兵防御百分比 = 5102,
		步兵谋略百分比 = 5103,
		步兵兵力百分比 = 5104,
		步兵士兵攻击百分比 = 5105,
		步兵士兵血量百分比 = 5106,
		步兵谋略增伤 = 5107,
		骑兵攻击百分比 = 5201,
		骑兵防御百分比 = 5202,
		骑兵谋略百分比 = 5203,
		骑兵兵力百分比 = 5204,
		骑兵士兵攻击百分比 = 5205,
		骑兵士兵血量百分比 = 5206,
		骑兵谋略增伤 = 5207,
		弓兵攻击百分比 = 5301,
		弓兵防御百分比 = 5302,
		弓兵谋略百分比 = 5303,
		弓兵兵力百分比 = 5304,
		弓兵士兵攻击百分比 = 5305,
		弓兵士兵血量百分比 = 5306,
		弓兵谋略增伤 = 5307,
	}
	public enum EPassiveType
	{
		兵力 = 100,
		攻击 = 101,
		防御 = 102,
		谋略 = 103,
		攻城 = 104,
		攻速 = 105,
		移速 = 106,
		攻距 = 107,
		负重 = 108,
		攻速百分比 = 115,
		移速百分比 = 116,
		攻距百分比 = 117,
		步兵攻击百分比 = 140,
		步兵防御百分比 = 141,
		步兵谋略百分比 = 142,
		步兵兵力百分比 = 143,
		步兵士兵攻击百分比 = 144,
		步兵士兵血量百分比 = 145,
		步兵谋略增伤 = 146,
		骑兵攻击百分比 = 150,
		骑兵防御百分比 = 151,
		骑兵谋略百分比 = 152,
		骑兵兵力百分比 = 153,
		骑兵士兵攻击百分比 = 154,
		骑兵士兵血量百分比 = 155,
		骑兵谋略增伤 = 156,
		弓兵攻击百分比 = 160,
		弓兵防御百分比 = 161,
		弓兵谋略百分比 = 162,
		弓兵兵力百分比 = 163,
		弓兵士兵攻击百分比 = 164,
		弓兵士兵血量百分比 = 165,
		弓兵谋略增伤 = 166,
		出场怒气 = 131,
		怒气恢复 = 132,
		命中 = 201,
		抵抗 = 202,
		暴击 = 203,
		韧性 = 204,
		破击 = 205,
		格挡 = 206,
		暴伤 = 211,
		格免 = 212,
		士兵攻击 = 301,
		士兵血量 = 302,
		士兵攻击百分比 = 311,
		士兵血量百分比 = 312,
		对步兵伤害 = 411,
		对骑兵伤害 = 412,
		对弓兵伤害 = 413,
		对城伤害 = 414,
		受步兵伤害 = 421,
		受骑兵伤害 = 422,
		受弓兵伤害 = 423,
		受城伤害 = 424,
		普攻增伤 = 501,
		战法增伤 = 502,
		谋略增伤 = 503,
		攻城增伤 = 504,
		治疗效果 = 505,
		持续伤害 = 506,
		仓库储量 = 601,
		银两产量 = 610,
		银两容量 = 611,
		粮食产量 = 620,
		粮食容量 = 621,
		木材产量 = 630,
		木材容量 = 631,
		铁矿产量 = 640,
		铁矿容量 = 641,
	}
	public enum EProtocolResult
	{
		成功 = 0,
		失败 = 1,
		异常 = 2,
		服务器异常 = 10,
		服务器数据库异常 = 11,
		服务器配置文件异常 = 12,
		游戏服务器未开启 = 13,
		平台数据库异常 = 14,
		平台账号不存在 = 15,
		错误的平台参数 = 16,
		平台类型错误 = 17,
		平台验证失败 = 18,
		平台登录失败 = 19,
		服务器拥挤 = 20,
		服务器维护中 = 21,
		登陆受限 = 22,
		账号被封禁 = 23,
		玩家被封禁 = 24,
		玩家被禁言 = 25,
		游戏版本不一致 = 26,
		服务器数据计算中 = 27,
		管理员登陆账号错误 = 50,
		管理员登陆密码错误 = 51,
		管理员登陆账号已在线 = 52,
		请先登录管理员账号 = 53,
		错误的修改类型 = 54,
		错误的修改参数 = 55,
		返回管理员数据错误 = 56,
		玩家不存在 = 57,
		发送邮件失败 = 58,
		玩家不在线 = 59,
		修改数据失败 = 60,
		调试指令错误 = 61,
		玩家已经被注册 = 70,
		账号不存在 = 71,
		密码错误 = 72,
		验证错误请重新登陆 = 73,
		无玩家数据请创建角色 = 74,
		名字已经被使用 = 75,
		账号长度不符 = 76,
		密码长度不符 = 77,
		名字长度不符 = 78,
		名字长度太短 = 79,
		名字长度太长 = 80,
		名字含有不合法或者不雅字符 = 81,
		类型错误 = 83,
		文本为空 = 84,
		文本太长 = 85,
		数据为空 = 86,
		图片数据太大 = 87,
		语音数据太大 = 88,
		列表为空 = 89,
		ID错误 = 100,
		配置ID错误 = 101,
		对象ID错误 = 102,
		府ID错误 = 103,
		数量不合法 = 110,
		数据类型错误 = 111,
		等级不足 = 112,
		体力不足 = 113,
		元宝不足 = 114,
		金币不足 = 115,
		银两不足 = 116,
		粮草不足 = 117,
		木材不足 = 118,
		铁矿不足 = 119,
		材料不足 = 121,
		道具不足 = 122,
		个人贡献不足 = 123,
		活跃度状态不足 = 124,
		合成材料不足 = 125,
		挑战次数不足 = 126,
		势力资金不足 = 127,
		武神积分不足 = 128,
		出征令不足 = 129,
		制作材料不足 = 130,
		声望不足 = 131,
		体力已满 = 200,
		出征令已满 = 201,
		仓库粮食已满 = 202,
		仓库木头已满 = 203,
		仓库铁矿已满 = 204,
		拥有装备数量已满 = 205,
		拥有武将数量已满 = 206,
		俸禄已领取 = 208,
		不可卖店 = 210,
		不可交易 = 211,
		不可使用 = 212,
		不可批量使用 = 213,
		职位不符 = 214,
		CD未冷却 = 215,
		CD已冷却 = 216,
		使用要求加入势力 = 217,
		使用要求势力拥有城池 = 218,
		验证错误战斗识别码 = 220,
		验证错误战斗结果 = 221,
		关卡尚未开启 = 223,
		关卡次数不足 = 224,
		通关三星以后才能扫荡 = 225,
		已通过此关卡 = 226,
		尚未通过此关卡 = 227,
		事件状态不匹配 = 228,
		军务积分不足 = 229,
		重置材料不足 = 230,
		不需要重置次数 = 231,
		没有这封邮件 = 234,
		没有附件 = 235,
		附件已领取过 = 236,
		错误的邮件类型 = 237,
		只有刺史以上职位才能发送郡邮件 = 238,
		只有知府以上职位才能发送府邮件 = 239,
		错误的郡对象 = 240,
		错误的府对象 = 241,
		不能给自己发邮件 = 242,
		聊天发送太快 = 244,
		聊天频道错误 = 245,
		商城ID错误 = 247,
		快捷购买ID错误 = 248,
		已经卖完 = 249,
		建筑升级队列已满 = 251,
		建筑升级已满级 = 252,
		建筑升级材料不足 = 253,
		建筑不在建造中 = 254,
		封地建筑需求玩家等级不足 = 255,
		府衙等级不足 = 256,
		计策ID错误 = 258,
		计策学习中 = 259,
		计策不在学习中 = 260,
		你已经拥有当前武将栏位数量 = 261,
		你尚未购买前级武将栏位数量 = 262,
		正在募兵中 = 264,
		不在募兵中 = 265,
		募兵已经完成 = 266,
		募兵数量过少 = 267,
		募兵数量过多 = 268,
		错误的武将ID = 270,
		武将锁定中 = 271,
		武将上阵中 = 272,
		武将募兵中 = 273,
		武将出征中 = 274,
		武将进行军务中 = 275,
		兵力不足 = 276,
		武将身上有装备 = 277,
		武将昵称过短 = 278,
		武将昵称过长 = 279,
		上阵武将过多 = 280,
		武将经验已满 = 281,
		武将攻击已满 = 282,
		武将防御已满 = 283,
		武将攻城已满 = 284,
		武将谋略已满 = 285,
		合成武将源不足 = 286,
		武将技能学习失败 = 287,
		相同精英级别才能合成 = 288,
		武将兑换活动已过期 = 289,
		兑换材料不足 = 290,
		没有可募兵的武将 = 291,
		武将数量已达到最大值 = 292,
		神将无法合成 = 293,
		武将资质已满 = 294,
		武将无法进化 = 295,
		武将进阶需要更高星级 = 296,
		星级已满 = 297,
		材料武将ID错误 = 298,
		武将星级错误 = 299,
		武将等级达到当前星级的等级上限 = 300,
		武将正在驻守资源点 = 301,
		武将重复布阵 = 302,
		布阵位置错误 = 303,
		布阵类型错误 = 304,
		前锋阵容超过上限 = 305,
		援军阵容超过上限 = 306,
		阵容编号错误 = 307,
		材料武将不能上阵 = 308,
		武将配置ID未找到 = 309,
		武将征讨匪寇中 = 310,
		任务尚未完成 = 312,
		任务奖励已领取 = 313,
		每日任务尚未完成 = 315,
		每日任务奖励已领取 = 316,
		每日任务活跃度奖励已领完 = 317,
		错误的排行榜类型 = 319,
		错误的交易类型 = 321,
		错误的交易方式 = 322,
		错误的单号 = 323,
		挂单数量太大 = 324,
		挂单数量太小 = 325,
		挂单数量必须为_0_的倍数 = 326,
		单价不能低于_0_ = 327,
		单价不能高于_0_ = 328,
		交易数量太小 = 329,
		交易数量超过单子数量 = 330,
		交易数量必须为_0_的倍数 = 331,
		只能撤销自己的单子 = 332,
		不能买卖自己的交易单 = 333,
		交易单子太多 = 334,
		已经学会此技能 = 336,
		错误的技能ID = 337,
		技能槽位未解锁 = 338,
		技能已装备了 = 339,
		技能升级已满级 = 340,
		技能装备类型错误 = 341,
		错误的装备ID = 343,
		主角只能装备主角类装备 = 344,
		武将只能装备武将类装备 = 345,
		这个位置没有装备 = 346,
		只有特定职业的武将才能装备 = 347,
		只有特定的武将才能装备 = 348,
		交易对象错误 = 350,
		错误的交易单号 = 351,
		交易单子已经交易 = 352,
		不是你出售的物品 = 353,
		不能买入自己出售的物品 = 354,
		交易上架数目太大 = 355,
		出售价格太低 = 356,
		交易所需服务费不足 = 357,
		交易买入所需元宝不足 = 358,
		交易买入所需金币不足 = 359,
		出售单子太多 = 360,
		武将有佩戴装备不能出售 = 361,
		装备名称过短 = 363,
		装备名称过长 = 364,
		购买次数上限 = 366,
		强化上限 = 367,
		赛季结束 = 369,
		不可升级 = 371,
		解锁条件不足 = 372,
		宝库活动未开启 = 374,
		未参与攻打宝库活动 = 375,
		您所在的势力还未报名参加活动 = 376,
		玩家报名无法找到战区ID = 377,
		玩家身份不满足 = 378,
		你已经报过名了 = 379,
		战斗验证不匹配 = 380,
		战斗生命值错误 = 381,
		没有到战区开启时间 = 382,
		没有到战区报名时间 = 383,
		据点已经被攻破 = 384,
		战区已经关闭 = 385,
		据点没有解锁 = 386,
		战区排名前三的势力才能挑战 = 387,
		不在报名时间内 = 388,
		剿匪活动已关闭 = 389,
		武神令牌不足 = 391,
		武神令牌已满 = 392,
		古塔未解锁 = 393,
		古塔已通过 = 394,
		古塔未通过 = 395,
		古塔已领取 = 396,
		活动奖励条件未达成 = 398,
		活动奖励已领取 = 399,
		活动奖励已兑换次数已完 = 400,
		活动奖励已兑换道具不足 = 401,
		您已经领取了该奖励 = 403,
		您没有可以领取的奖励 = 404,
		该奖励不可领取 = 405,
		兑换码不存在 = 407,
		兑换码已过期 = 408,
		兑换码已经被使用过了 = 409,
		兑换码同类型已达到使用个数上限 = 410,
		武将被俘中 = 412,
		您的部队已经成为雇佣兵暂时无法进行该操作 = 414,
		错误的雇佣号 = 415,
		不能雇佣自己 = 416,
		玩家不在队列中 = 418,
		装备已被分解 = 419,
		武将已被流放 = 420,
		七日登录奖励不可领取 = 422,
		七日登录奖励已领取 = 423,
		红包已失效 = 425,
		已经抢过该红包 = 426,
		该红包已抢完 = 427,
		今日发红包达到上限 = 428,
		今日领红包达到上限 = 429,
		活动未开启 = 431,
		活动已结束 = 432,
		未达到活动开启等级 = 433,
		奖励ID未找到 = 435,
		幸运值不够 = 436,
		开启次数不足 = 438,
		今天已签到 = 440,
		今天未签到 = 441,
		已经没有补签次数 = 442,
		刷新次数达到上限 = 444,
		未知的货币类型 = 445,
		武将强化达到上限 = 447,
		武将等级不足 = 448,
		不需要合成 = 450,
		条件未达到 = 452,
		奖励已领取 = 453,
		挑战已经结束 = 455,
		购买数量已达上限 = 456,
		体力已领取 = 458,
		未到达领取时间 = 459,
		领取时间已过 = 460,
		即时对战 = 462,
		尚未加入对战 = 463,
		势力 = 465,
		你已加入势力了 = 466,
		你尚未加入势力 = 467,
		势力诸侯不能脱离势力 = 468,
		对方不是势力成员 = 469,
		势力人数已满 = 470,
		势力加入申请通过 = 471,
		只有诸侯才有权限 = 472,
		只有诸侯或太守才有权限 = 473,
		只有诸侯和太守才能同意申请 = 474,
		对方不在申请列表 = 475,
		势力不存在 = 476,
		您刚退出势力不久不能加入新势力 = 477,
		势力捐献ID错误 = 478,
		对象势力已经是同盟了 = 479,
		对象势力不是同盟 = 480,
		你的势力没有占领这个城池 = 481,
		对方已经是这个城的太守了 = 482,
		你已经是这个城的太守了 = 483,
		您不能逐出自己 = 484,
		势力捐献次数已满 = 485,
		势力收入不足 = 486,
		圣女所属玩家ID未找到 = 488,
		对方没有圣女资格 = 489,
		势力没有圣女 = 490,
		圣女被俘之中 = 491,
		圣女捐献ID错误 = 492,
		圣女今日捐献次数已满 = 493,
		圣女已达到最高等级 = 494,
		圣女已经被释放 = 495,
		圣女赎回ID错误 = 496,
		圣女今日赎回次数已满 = 497,
		圣女已经驻守该城 = 498,
		圣女卫队未完全复活 = 499,
		城池 = 501,
		城池ID错误 = 502,
		守备营ID错误 = 503,
		守备部队ID错误 = 504,
		城池建筑升级条件不足 = 506,
		城墙不需要修复 = 507,
		城墙燃烧中无法修复 = 508,
		器械建造过多 = 509,
		城池战略 = 511,
		只能防守己方城池 = 512,
		城墙已经被攻破 = 513,
		只能进攻相邻的敌方城池 = 514,
		敌方城池在攻击保护期内 = 515,
		尚未攻破任意城墙无法攻击内城 = 516,
		城池战斗集结次数不足 = 517,
		不能进攻郡城 = 519,
		不能防守郡城 = 520,
		此城池不是郡城 = 522,
		郡城攻打尚未开放 = 523,
		只能匹配战斗郡城 = 524,
		尚未参加郡城战斗 = 525,
		已经申请过好友 = 527,
		对方已经是好友 = 528,
		好友数量达到上限 = 529,
		今日体力领取达到上限 = 530,
		已领取过对方赠送的体力 = 531,
		州ID错误 = 533,
		资源点ID错误 = 534,
		资源类型错误 = 535,
		不能挑战自己的资源点 = 536,
		上阵武将已经在其他资源采集队伍中 = 537,
		您的进攻部队已达到上限 = 538,
		无法进入不属于您的势力范围 = 539,
		上阵武将已经在其他征讨队伍中 = 540,
		匪寇已经死亡 = 542,
		征讨匪寇挑战次数不足 = 543,
		当前匪寇已经在征讨 = 544,
		征讨队伍人数不足 = 545,
		您的征讨部队已达到上限 = 546,
		征讨匪寇不可刷新 = 547,
		购买金额不足 = 548,
		演武厅日胜场宝箱已领取 = 551,
		演武厅日胜场不足 = 552,
		演武厅天梯未开启 = 553,
		未激活月卡 = 555,
		月卡已失效 = 556,
		流亡军功能尚未开启 = 559,
		武将流亡军出征中 = 560,
		流亡军出征队伍达到上限 = 561,
		上阵武将已经在其他流亡军队伍中 = 562,
		流亡军防守兵力不足 = 563,
		流亡军防守兵力超过上限 = 564,
		流亡军正在进攻中 = 565,
		流亡军进攻尚未编组队伍 = 566,
		流亡军迁移目标已经被占领 = 567,
		流亡军出征中不能迁移 = 568,
		流亡军迁移道具不足 = 569,
		流亡军进攻目标为空 = 570,
		流亡军不能进攻自己 = 571,
		流亡军不能进攻已方 = 572,
		流亡军侦查目标为空 = 573,
		流亡军不能侦查自己 = 574,
		流亡军不能侦查已方 = 575,
		势力弹劾已发起 = 584,
		不能发起势力弹劾 = 585,
		没有发起势力弹劾 = 586,
		你已参与势力弹劾 = 587,
		势力豪礼宝箱领取经验不足 = 590,
	}
	public enum EQuality
	{
		普通 = 1,
		优秀 = 2,
		稀有 = 3,
		史诗 = 4,
		传奇 = 5,
		神话 = 6,
	}
	public enum ERechargeType
	{
		充值获得会员卡 = 100,
		充值获得月卡 = 200,
		充值获得基金 = 300,
		充值获得元宝 = 400,
		充值获得每月礼包 = 500,
	}
	public enum EScienceType
	{
		基础步兵攻击 = 101,
		基础步兵生命 = 102,
		基础骑兵攻击 = 103,
		基础骑兵生命 = 104,
		基础弓兵攻击 = 105,
		基础弓兵生命 = 106,
		基础全军兵力 = 107,
		步将攻击 = 108,
		步将防御 = 109,
		步将谋略 = 110,
		骑将攻击 = 111,
		骑将防御 = 112,
		骑将谋略 = 113,
		弓将攻击 = 114,
		弓将防御 = 115,
		弓将谋略 = 116,
		基础谋略伤害 = 117,
		高级步兵攻击 = 201,
		高级步兵生命 = 202,
		高级骑兵攻击 = 203,
		高级骑兵生命 = 204,
		高级弓兵攻击 = 205,
		高级弓兵生命 = 206,
		高级全军兵力 = 207,
		步将攻城 = 208,
		步兵克制 = 209,
		步兵被克 = 210,
		骑将攻城 = 211,
		骑兵克制 = 212,
		骑兵被克 = 213,
		弓将攻城 = 214,
		弓兵克制 = 215,
		弓兵被克 = 216,
		高级谋略伤害 = 217,
		粮食产量1 = 301,
		粮食储量1 = 302,
		木材产量1 = 303,
		木材储量1 = 304,
		铁矿产量1 = 305,
		铁矿储量1 = 306,
		银两产量1 = 307,
		银两储量1 = 308,
		仓库容量1 = 309,
		粮食产量2 = 310,
		粮食容量1 = 311,
		木材产量2 = 312,
		木材容量1 = 313,
		铁矿产量2 = 314,
		铁矿容量1 = 315,
		银两产量2 = 316,
		银两储量2 = 317,
		仓库容量2 = 318,
		粮食产量3 = 319,
		木材产量3 = 320,
		铁矿产量3 = 321,
		银两产量3 = 322,
	}
	public enum EServerPlatform
	{
		开发 = 0,
		内网测试 = 1,
		测试 = 2,
		鲸旗 = 10,
		鲸旗九游 = 11,
	}
	public enum EServerStatus
	{
		良好 = 0,
		火爆 = 1,
	}
	public enum EServerType
	{
		客户端 = 0,
		充值 = 1,
		登录 = 2,
		中心 = 3,
		游戏 = 20,
		战斗 = 30,
		后台工具 = 100,
		服务器工具服务器端 = 999,
	}
	public enum EShopQuickType
	{
		体力 = 1,
		挑战令牌 = 2,
	}
	public enum EShopResetMode
	{
		无重置 = -1,
		日重置 = 1,
		周重置 = 2,
	}
	public enum EShopType
	{
		元宝商城 = 1,
		竞技商城 = 2,
		军务商城 = 3,
		武神商城 = 4,
	}
	public enum ESkillType
	{
		主角主动技能 = 11,
		主角光环技能 = 12,
		主角被动技能 = 13,
		武将触发技能 = 21,
		武将被动技能 = 22,
	}
	public enum ETalentType
	{
		攻击强化 = 101,
		兵力强化 = 201,
		城防强化 = 301,
	}
	public enum ETaskAction
	{
		主角等级 = 1,
		建筑等级 = 2,
		武将等级 = 3,
		装备强化等级 = 4,
		穿戴装备 = 5,
		通关主线副本 = 6,
		全建筑等级 = 7,
		完成主线任务 = 8,
		通关武神塔 = 9,
		武将技能等级 = 10,
		收集星级武将 = 11,
		科技等级 = 12,
		当前好友数量 = 13,
		竞技场最高排名 = 14,
		连续登录天数 = 15,
		爵位等级 = 16,
		势力等级 = 17,
		势力科技 = 18,
		指定科技等级 = 19,
		当前占领城池 = 20,
		武将收集 = 21,
		武将强化等级 = 22,
		指定武将技能 = 23,
		全套装备强化 = 24,
		单个技能等级 = 25,
		加入势力达成 = 26,
		完成当前每日 = 27,
		完成章节 = 28,
		是否拥有月卡 = 29,
		资源抢夺 = 150,
		收集资源 = 151,
		装备强化次数 = 152,
		完成军务 = 153,
		参加竞技场 = 154,
		指定制作 = 155,
		制作数量 = 156,
		装备指定装备 = 157,
		消耗指定资源 = 158,
		任意主线副本 = 159,
		累计每日 = 160,
		武将技能 = 161,
		武将强化 = 162,
		武将洗练 = 163,
		武将进阶 = 164,
		科技研究 = 165,
		挑战武神塔 = 166,
		武将宴请 = 167,
		步兵招募 = 168,
		骑兵招募 = 169,
		弓兵招募 = 170,
		士兵招募 = 171,
		武将升级 = 172,
		赠送体力 = 173,
		势力捐献 = 174,
		势力攻城 = 175,
		势力器械 = 176,
		势力建筑 = 177,
		累计每日势力 = 178,
		军务派遣 = 179,
		征讨匪寇 = 180,
		佩戴装备 = 181,
		申请好友 = 182,
		弓兵招募数量 = 183,
		骑兵招募数量 = 184,
		步兵招募数量 = 185,
		武器打造 = 186,
		铠甲打造 = 187,
		头盔打造 = 188,
		战靴打造 = 189,
		武器强化 = 190,
		铠甲强化 = 191,
		头盔强化 = 192,
		战靴强化 = 193,
		消耗指定道具 = 194,
		累计交易所购买 = 300,
		累计交易所上架 = 301,
		累计步兵招募 = 302,
		累计骑兵招募 = 303,
		累计弓兵招募 = 304,
		累计士兵招募 = 305,
		累计武将宴请 = 306,
		累计宴请稀有武将 = 307,
		累计征讨匪寇 = 308,
		累计挑战武神塔 = 309,
		累计占领采集资源 = 310,
		竞技场累计胜场 = 311,
		竞技场累计败场 = 312,
		累计打造稀有装备 = 313,
		累计打造装备 = 314,
		累计强化装备 = 315,
		累计强化失败 = 316,
		累计分解装备 = 317,
		累计完成军务 = 318,
		累计登录天数 = 319,
		累计赠送体力 = 320,
		累计完成周军务 = 321,
		累计参加攻城 = 322,
		累计攻打势力BOSS = 323,
		累计赠送花朵 = 324,
		累计势力捐献 = 325,
		累计势力建筑建造 = 326,
		累计武将进阶 = 327,
		累计武将强化 = 328,
		累计武将洗练 = 329,
		累计武将升星 = 330,
		累计完成每日任务 = 331,
		累计资源抢夺 = 332,
		累计科技研究 = 333,
		累计收集粮草 = 334,
		累计收集木材 = 335,
		累计收集铁矿 = 336,
		累计收集银币 = 337,
		累计征讨稀有匪寇 = 338,
		累计武将升级 = 339,
		累计好友申请 = 340,
		累计交易所上架或购买 = 500,
	}
	public enum EUseEffect
	{
		武将经验书 = 21,
		攻击丹 = 23,
		防御丹 = 24,
		谋略丹 = 25,
		攻城丹 = 26,
		通用加速道具 = 31,
		募兵加速道具 = 32,
		研究加速道具 = 33,
		出征令 = 100,
		绑定游戏币 = 101,
		绑定木材 = 111,
		绑定石料 = 112,
		绑定铁矿 = 113,
		绑定布匹 = 114,
		绑定粮草 = 115,
		银两 = 201,
		木材 = 211,
		石料 = 212,
		铁矿 = 213,
		布匹 = 214,
		粮草 = 215,
		宝箱 = 300,
		武将宝箱 = 301,
		装备宝箱 = 302,
		主角技能书碎片 = 411,
		武将技能书碎片 = 429,
		元宝 = 502,
		金币 = 520,
		玩家经验 = 503,
		势力经验 = 534,
		体力 = 508,
		武神令 = 506,
		竞技次数 = 509,
		军务积分 = 505,
		声望 = 530,
		个人贡献 = 531,
		势力资金 = 532,
		武神积分 = 561,
		活跃度 = 541,
		功勋 = 542,
		匪寇次数 = 591,
		资源点次数 = 592,
		攻城集结次数 = 593,
		全兵种兵力 = 600,
		步兵兵力 = 601,
		骑兵兵力 = 602,
		弓兵兵力 = 603,
		城池均分金币 = 700,
		会员卡 = 801,
		月卡 = 802,
		随机迁城令 = 901,
		固定迁城令 = 902,
	}
	public enum EWakeDataType
	{
		攻击 = 1,
		防御 = 2,
		谋略 = 3,
	}
	public enum EWarriorJobType
	{
		无 = 0,
		步兵 = 1,
		骑兵 = 2,
		弓兵 = 3,
		攻城 = 4,
		全兵种 = 9,
	}
}
