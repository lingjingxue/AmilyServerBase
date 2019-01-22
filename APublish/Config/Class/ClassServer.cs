using System.Collections.Generic;

namespace ServerBase.Config
{
	public class ConfigProtocolResult : IConfigBase	// B_Lang返回码.xlsx
	{
		public int ProtocolResult; //ID
		public string LangChs; //中文简体
		public string LangEn; //英语
		public string LangKo; //韩语
		public object GetKey() { return ProtocolResult; }
	}
	public class ConfigMailText : IConfigBase	// B_Lang邮件.xlsx
	{
		public int MailText; //ID
		public string LangChs; //中文简体
		public string LangEn; //英语
		public string LangKo; //韩语
		public object GetKey() { return MailText; }
	}
	public class ConfigLanguage : IConfigBase	// B_本地化配置表.xlsx
	{
		public string Id; //ID
		public string Ch; //中文
		public string En; //英文
		public object GetKey() { return Id; }
	}
	public class ConfigBag : IConfigBase	// B_背包.xlsx
	{
		public int Id; //ID
		public string Name; //名
		public string Desc; //描述
		public int TypeBag; //类别
		public object GetKey() { return Id; }
	}
	public class ConfigDrop : IConfigBase	// D_掉落.xlsx
	{
		public int Id; //ID
		public int DropType; //掉落类型
		public Dictionary<int, int> Num = new Dictionary<int, int>(); //掉落数量
		public List<List<int>> DropPoll; //掉落池
		public object GetKey() { return Id; }
	}
	public class ConfigDropLimit : IConfigBase	// D_掉落.xlsx
	{
		public int Id; //ID
		public string AwardType; //类型
		public string Type; //具体ID
		public string DropLimitTime; //掉落限制时间
		public int DropLimit; //掉落数量限制
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
	public class ConfigExp : IConfigBase	// T_通用.xlsx
	{
		public int Id; //ID
		public int BreakLevel; //突破等级
		public int Level; //等级
		public List<List<int>> ExpCost; //经验消耗
		public int PowerGrow; //战力成长
		public object GetKey() { return Id; }
	}
	public class ConfigBreak : IConfigBase	// T_通用.xlsx
	{
		public int Id; //ID
		public int BreakPower; //突破战力
		public int LevelMax; //最大等级
		public int AfterLevel; //突破后等级
		public List<List<int>> ItemCost; //消耗
		public object GetKey() { return Id; }
	}
	public class ConfigForge : IConfigBase	// T_通用.xlsx
	{
		public int Id; //ID
		public List<List<int>> ItemCost; //消耗
		public int PowerPer; //战力百分比
		public object GetKey() { return Id; }
	}
	public class ConfigReforgeCost : IConfigBase	// T_通用.xlsx
	{
		public int Id; //ID
		public int LevelNeed; //要求铸造等级
		public List<List<int>> ItemCost; //道具消耗
		public object GetKey() { return Id; }
	}
	public class ConfigStarCost : IConfigBase	// T_通用.xlsx
	{
		public int Id; //ID
		public int Quality; //品质
		public int StarLevel; //星级
		public List<List<int>> ItemCost; //消耗
		public List<List<int>> Decomposed; //分解获得精华
		public object GetKey() { return Id; }
	}
	public class ConfigHero : IConfigBase	// Y_英雄.xlsx
	{
		public int Id; //ID
		public string Name; //名字
		public string Des; //描述
		public string Head; //头像
		public string Resource; //资源
		public int HeroSeries; //归属
		public int Quality; //品质
		public int Race; //种族
		public int Sex; //性别
		public int Profession; //职业
		public int BasisPower; //基础战力
		public float BreakRatio; //突破系数
		public float GrowRatio; //成长系数
		public List<int> GroupSkill = new List<int>(); //技能列表
		public int Limit; //上阵数量限制
		public int ReturnCost; //合成消耗
		public int Star1Skill; //1星技能
		public List<int> Star1Ability = new List<int>(); //1星属性
		public int Star2Skill; //2星技能
		public List<int> Star2Ability = new List<int>(); //2星属性
		public int Star3Skill; //3星技能
		public List<int> Star3Ability = new List<int>(); //3星属性
		public int Star4Skill; //4星技能
		public List<int> Star4Ability = new List<int>(); //4星属性
		public int Star5Skill; //5星技能
		public List<int> Star5Ability = new List<int>(); //5星属性
		public int Star6Skill; //6星技能
		public List<int> Star6Ability = new List<int>(); //6星属性
		public List<int> Level1Ability = new List<int>(); //宝具1阶属性
		public List<int> Level2Ability = new List<int>(); //宝具2阶属性
		public List<int> Level3Ability = new List<int>(); //宝具3阶属性
		public List<int> Level4Ability = new List<int>(); //宝具4阶属性
		public List<int> Level5Ability = new List<int>(); //宝具5阶属性
		public List<int> Level6Ability = new List<int>(); //宝具6阶属性
		public List<int> Level7Ability = new List<int>(); //宝具7阶属性
		public List<int> Level8Ability = new List<int>(); //宝具8阶属性
		public List<int> Level9Ability = new List<int>(); //宝具9阶属性
		public object GetKey() { return Id; }
	}
	public enum EServerType
	{
		客户端 = 0,
		中心 = 1,
		世界 = 2,
		社交 = 3,
		平台 = 4,
		登陆 = 10,
		游戏 = 20,
		战斗 = 30,
		HTTP = 31,
		后台工具 = 100,
	}
	public enum EAccess
	{
		本机调试 = 0,
		内网版本 = 1,
		外网版本 = 2,
	}
	public enum EAwardType
	{
		经验 = 1,
		银两 = 2,
		元宝 = 3,
		木材 = 4,
		食物 = 5,
		铁矿 = 6,
		道具 = 7,
		武将 = 10,
		VIP经验 = 11,
	}
	public enum EDropType
	{
		固定掉落 = 0,
		独立掉落 = 1,
		抽取放回 = 2,
		抽取不放回 = 3,
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
}
