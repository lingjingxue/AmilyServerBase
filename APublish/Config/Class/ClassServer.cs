using System.Collections.Generic;

namespace ServerBase.Config
{
	public class ConfigHelp : IConfigBase
	{
		public int Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigLanguage : IConfigBase
	{
		public string Id; //ID
		public string Ch; //中文
		public string En; //英文
		public object GetKey() { return Id; }
	}
	public class ConfigBag : IConfigBase
	{
		public int Id; //ID
		public string Name; //名
		public string Desc; //描述
		public int TypeBag; //类别
		public object GetKey() { return Id; }
	}
	public class ConfigDraw : IConfigBase
	{
		public int Id; //ID
		public List<List<int>> OneCost; //单抽消耗
		public List<List<int>> OneItemCost; //单抽优先消耗
		public List<List<int>> TenCost; //连抽消耗
		public List<List<int>> TenItemCost; //连抽优先消耗
		public List<List<int>> OneAward; //单抽奖励
		public List<List<int>> TenAward; //连抽奖励
		public int PoolType; //奖池类型
		public object GetKey() { return Id; }
	}
	public class ConfigDrawPool : IConfigBase
	{
		public int Id; //ID
		public int PoolType; //奖池类型
		public int Weight; //权重
		public int MultipleWeight; //第10次权重
		public List<List<int>> Award; //奖励
		public object GetKey() { return Id; }
	}
	public class ConfigDrop : IConfigBase
	{
		public int Id; //ID
		public int DropType; //掉落类型
		public Dictionary<int, int> Num = new Dictionary<int, int>(); //掉落数量
		public List<List<int>> DropPoll; //掉落池
		public object GetKey() { return Id; }
	}
	public class ConfigDropLimit : IConfigBase
	{
		public int Id; //ID
		public string AwardType; //类型
		public string Type; //具体ID
		public string DropLimitTime; //掉落限制时间
		public int DropLimit; //掉落数量限制
		public object GetKey() { return Id; }
	}
	public class ConfigItem : IConfigBase
	{
		public int Id; //ID
		public int Quality; //品质
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
	public class ConfigResultCode : IConfigBase
	{
		public int Id; //ID
		public string Default; //默认语言
		public string Ch; //中文
		public string En; //英文
		public object GetKey() { return Id; }
	}
	public class ConfigSkill : IConfigBase
	{
		public int Id; //ID
		public int SkillType; //类型
		public List<List<int>> Condition; //合体条件
		public List<int> BattleSkill = new List<int>(); //战斗技能
		public List<int> Ability = new List<int>(); //附加属性
		public int SpecialType; //特殊条件类型
		public int SpecialValue; //特殊条件参数
		public List<int> SpecialAbility = new List<int>(); //特殊附加属性
		public int SpecialLimit; //加成次数上限
		public object GetKey() { return Id; }
	}
	public class ConfigBattleSkill : IConfigBase
	{
		public int Id; //ID
		public int RoundType; //回合触发条件
		public int RoundValue; //回合触发参数
		public int Limit; //次数限制
		public int odds; //触发概率
		public List<int> TriggerHit = new List<int>(); //命中触发条件
		public List<int> TriggerHpType = new List<int>(); //血量触发类型
		public int TriggerHpValue; //血量触发参数
		public List<int> TriggerBuffType = new List<int>(); //状态触发
		public int TriggerBuffValue; //血量触发参数
		public int EnemyEffectType; //对敌-取值类型
		public int EnemyEffectValue; //对敌-取值参数
		public int AttackType; //对敌-效果攻击类型
		public int EnemyArmorBreak; //对敌-无视防御
		public int EnemyMustHit; //对敌-必定命中
		public int EnemySuckRatio; //对敌-吸血比例
		public List<int> EnemyBuff = new List<int>(); //对敌-附加状态ID
		public int SelfEffectType; //对己-取值类型
		public int SelfEffectValue; //对己-取值参数
		public int SelfAttackType; //对己-效果类型
		public List<int> SelfBuff = new List<int>(); //对己-状态ID
		public object GetKey() { return Id; }
	}
	public class ConfigBuff : IConfigBase
	{
		public int Id; //ID
		public int BuffType; //状态分类
		public int BuffID; //状态ID
		public int Priority; //优先级
		public int Break; //可否驱散
		public int Pile; //叠加个数
		public int ReplaceRule; //替换规则
		public int Time; //持续时间
		public int EffectType; //作用效果
		public int EffectValue; //作用效果
		public object GetKey() { return Id; }
	}
	public class GlobalConfig : IConfigBase
	{
		public int Id; //ID
		public string Name; //全局名
		public string Desc; //配置说明
		public string Value; //配置值
		public object GetKey() { return Id; }
	}
	public class ConfigExp : IConfigBase
	{
		public int Id; //ID
		public int BreakLevel; //突破等级
		public int Level; //等级
		public List<List<int>> ExpCost; //经验消耗
		public int PowerGrow; //战力成长
		public object GetKey() { return Id; }
	}
	public class ConfigBreak : IConfigBase
	{
		public int Id; //ID
		public int BreakPower; //突破战力
		public int LevelMax; //最大等级
		public int AfterLevel; //突破后等级
		public List<List<int>> ItemCost; //消耗
		public object GetKey() { return Id; }
	}
	public class ConfigForge : IConfigBase
	{
		public int Id; //ID
		public List<List<int>> ItemCost; //消耗
		public int PowerPer; //战力百分比
		public object GetKey() { return Id; }
	}
	public class ConfigReforgeCost : IConfigBase
	{
		public int Id; //ID
		public int LevelNeed; //要求铸造等级
		public List<List<int>> ItemCost; //道具消耗
		public object GetKey() { return Id; }
	}
	public class ConfigStarCost : IConfigBase
	{
		public int Id; //ID
		public int Quality; //品质
		public int StarLevel; //星级
		public List<List<int>> ItemCost; //消耗
		public List<List<int>> Decomposed; //分解获得精华
		public object GetKey() { return Id; }
	}
	public class ConfigSurname : IConfigBase
	{
		public string Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigMalename : IConfigBase
	{
		public string Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigFemalename : IConfigBase
	{
		public string Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigBadwords : IConfigBase
	{
		public string Id; //ID
		public object GetKey() { return Id; }
	}
	public class ConfigHero : IConfigBase
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
		服务器拥挤 = 2,
		服务器维护中 = 3,
		登陆受限 = 4,
		管理员登陆账号错误 = 50,
		管理员登陆密码错误 = 51,
		管理员登陆账号已在线 = 52,
		发送邮件失败 = 53,
		对象玩家不在线 = 54,
		修改数据失败 = 55,
		调试指令错误 = 56,
		账号为空 = 100,
		账号已经被注册 = 101,
		账号不存在 = 102,
		密码错误 = 103,
		请重新登录服务器 = 104,
		玩家不存在 = 105,
		玩家已存在 = 106,
		创建用户数据错误 = 107,
		创建玩家数据错误 = 108,
		登录玩家数据错误 = 109,
		名字已经被使用 = 110,
		账号长度不符 = 111,
		密码长度不符 = 112,
		名字长度不符 = 113,
		名字长度太短 = 114,
		名字长度太长 = 115,
		名字含有不合法或者不雅字符 = 116,
		ID错误 = 150,
		玩家ID错误 = 151,
		配置ID错误 = 152,
		对象ID错误 = 153,
		错误的道具ID = 154,
		错误的装备ID = 155,
		错误的武将ID = 156,
		错误的城市ID = 157,
		错误的关卡ID = 158,
		经验不足 = 200,
		等级不足 = 201,
		金币不足 = 202,
		元宝不足 = 203,
		道具不足 = 204,
		材料不足 = 205,
		星级不足 = 206,
		没有这封邮件 = 250,
		邮件没有附件 = 251,
		附件已领取 = 252,
		附件未领取 = 253,
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
