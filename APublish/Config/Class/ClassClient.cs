using System.Collections.Generic;

public class ConfigHelp
{
	public readonly int Id;
	public readonly string Title;
	public readonly string Desc;
}
public class ConfigLanguage
{
	public readonly string Id;
	public readonly string Ch;
	public readonly string En;
	public readonly string r;
	public readonly string e;
	public readonly string f;
	public readonly string d;
	public readonly string p;
	public readonly string x;
	public readonly string a;
}
public class ConfigBag
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Desc;
	public readonly int TypeBag;
}
public class ConfigDraw
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Desc1;
	public readonly string Desc2;
	public readonly string OneCost;
	public readonly string OneItemCost;
	public readonly string TenCost;
	public readonly string TenItemCost;
	public readonly string OneAward;
	public readonly string TenAward;
	public readonly int PoolType;
}
public class ConfigDrawPool
{
	public readonly int Id;
	public readonly int PoolType;
	public readonly int Weight;
	public readonly int MultipleWeight;
	public readonly string Award;
}
public class ConfigDrop
{
	public readonly int Id;
}
public class ConfigDropLimit
{
	public readonly int Id;
}
public class ConfigItem
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Icon;
	public readonly string Intr;
	public readonly int Quality;
	public readonly string ItemType;
	public readonly int BagLabel;
	public readonly int MaxOwn;
	public readonly int CanSell;
	public readonly int SellPrice;
	public readonly int CanUse;
	public readonly int CanUseBatch;
	public readonly int UseEffect;
	public readonly int UseEffectArgs;
	public readonly int CompoundNumber;
	public readonly int CompoundHero;
	public readonly int DayUseLimit;
}
public class ConfigResultCode
{
	public readonly int Id;
	public readonly string Default;
	public readonly string Ch;
	public readonly string En;
	public readonly string r;
	public readonly string e;
	public readonly string f;
	public readonly string d;
	public readonly string p;
	public readonly string x;
	public readonly string a;
}
public class ConfigSkill
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Des;
	public readonly int Lv;
	public readonly string Icon;
	public readonly int SkillType;
	public readonly string Condition;
	public readonly string BattleSkill;
	public readonly string Ability;
	public readonly int SpecialType;
	public readonly int SpecialValue;
	public readonly string SpecialAbility;
	public readonly int SpecialLimit;
}
public class ConfigBattleSkill
{
	public readonly int Id;
	public readonly int SkillId;
	public readonly int BattleSkillType;
	public readonly int RoundType;
	public readonly int RoundValue;
	public readonly int Limit;
	public readonly int odds;
	public readonly string TriggerHit;
	public readonly string TriggerHpType;
	public readonly int TriggerHpValue;
	public readonly int TriggerBuffType;
	public readonly int TriggerBuffValue;
	public readonly int EnemyEffectType;
	public readonly int EnemyEffectValue;
	public readonly int AttackType;
	public readonly int EnemyArmorBreak;
	public readonly int EnemyMustHit;
	public readonly int EnemySuckRatio;
	public readonly string EnemyBuff;
	public readonly int SelfEffectType;
	public readonly int SelfEffectValue;
	public readonly int SelfAttackType;
	public readonly string SelfBuff;
}
public class ConfigBuff
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Des;
	public readonly string Icon;
	public readonly int BuffType;
	public readonly int BuffID;
	public readonly int Priority;
	public readonly int Break;
	public readonly int Pile;
	public readonly int ReplaceRule;
	public readonly int Time;
	public readonly int EffectType;
	public readonly int EffectValue;
}
public class GlobalConfig
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Desc;
	public readonly string Value;
}
public class ConfigExp
{
	public readonly int Id;
	public readonly int BreakLevel;
	public readonly int Level;
	public readonly string ExpCost;
	public readonly int PowerGrow;
}
public class ConfigBreak
{
	public readonly int Id;
	public readonly int BreakPower;
	public readonly int LevelMax;
	public readonly int AfterLevel;
	public readonly string ItemCost;
}
public class ConfigForge
{
	public readonly int Id;
	public readonly string ItemCost;
	public readonly int PowerPer;
}
public class ConfigReforgeCost
{
	public readonly int Id;
	public readonly int LevelNeed;
	public readonly string ItemCost;
}
public class ConfigStarCost
{
	public readonly int Id;
	public readonly int Quality;
	public readonly int StarLevel;
	public readonly string ItemCost;
	public readonly string Decomposed;
}
public class ConfigSurname
{
	public readonly string Id;
}
public class ConfigMalename
{
	public readonly string Id;
}
public class ConfigFemalename
{
	public readonly string Id;
}
public class ConfigBadwords
{
	public readonly string Id;
}
public class ConfigHero
{
	public readonly int Id;
	public readonly string Name;
	public readonly string Des;
	public readonly string Head;
	public readonly string Resource;
	public readonly int HeroSeries;
	public readonly int Quality;
	public readonly int Race;
	public readonly int Sex;
	public readonly int Profession;
	public readonly int BasisPower;
	public readonly double BreakRatio;
	public readonly double GrowRatio;
	public readonly string GroupSkill;
	public readonly int Limit;
	public readonly int ReturnCost;
	public readonly int Star1Skill;
	public readonly string Star1Ability;
	public readonly int Star2Skill;
	public readonly string Star2Ability;
	public readonly int Star3Skill;
	public readonly string Star3Ability;
	public readonly int Star4Skill;
	public readonly string Star4Ability;
	public readonly int Star5Skill;
	public readonly string Star5Ability;
	public readonly int Star6Skill;
	public readonly string Star6Ability;
	public readonly string EquipName;
	public readonly string EquipIcon;
	public readonly string Level1Ability;
	public readonly string Level2Ability;
	public readonly string Level3Ability;
	public readonly string Level4Ability;
	public readonly string Level5Ability;
	public readonly string Level6Ability;
	public readonly string Level7Ability;
	public readonly string Level8Ability;
	public readonly string Level9Ability;
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
