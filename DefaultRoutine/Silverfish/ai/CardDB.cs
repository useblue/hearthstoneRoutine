using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace HREngine.Bots
{
    public struct targett
    {
        public int target;//目标
        public int targetEntity;//目标实体

        public targett(int targ, int ent)
        {
            this.target = targ;
            this.targetEntity = ent;
        }
    }

    public struct PlayReq
    {
        public CardDB.ErrorType2 errorType;
        public int param;

        public PlayReq(CardDB.ErrorType2 errorType, int param)
        {
            this.errorType = errorType;
            this.param = param;
        }

        public PlayReq(CardDB.ErrorType2 errorType)
        {
            this.errorType = errorType;
            this.param = -1;
        }

        public void UpdateCardAttr(CardDB.Card card)
        {
            switch (errorType)
            {
                case CardDB.ErrorType2.REQ_TARGET_MAX_ATTACK:
                    card.needWithMaxAttackValueOf = param;
                    break;
                case CardDB.ErrorType2.REQ_TARGET_WITH_RACE:
                    card.needRaceForPlaying = param;
                    break;
                case CardDB.ErrorType2.REQ_NUM_MINION_SLOTS:
                    card.needEmptyPlacesForPlaying = param;
                    break;
                case CardDB.ErrorType2.REQ_MINION_CAP_IF_TARGET_AVAILABLE:
                    card.needMinionsCapIfAvailable = param;
                    break;
                case CardDB.ErrorType2.REQ_MINIMUM_ENEMY_MINIONS:
                    card.needMinNumberOfEnemy = param;
                    break;
                case CardDB.ErrorType2.REQ_TARGET_MIN_ATTACK:
                    card.needWithMinAttackValueOf = param;
                    break;
                case CardDB.ErrorType2.REQ_MINIMUM_TOTAL_MINIONS:
                    card.needMinTotalMinions = param;
                    break;
                case CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS:
                    card.needMinOwnMinions = param;
                    break;
                case CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS:
                    card.needControlaSecret = param;
                    break;
            }
        }

    }



    public partial class CardDB
    {
        // Data is stored in hearthstone-folder -> data->win cardxml0
        //(data-> cardxml0 seems outdated (blutelfkleriker has 3hp there >_>)
        //数据存储存储在炉石文件夹 
        public enum cardtype
        {
            NONE = 0,
            GAME = 1,
            PLAYER = 2,
            HERO = 3,//英雄
            MOB = 4,//随从
            SPELL = 5,//法术
            //ABILITY = 5,
            ENCHANTMENT = 6,//增幅（例如：变形术，救赎，力量的代价，自然之力的附加效果）
            WEAPON = 7,//武器
            ITEM = 8,
            TOKEN = 9,
            HEROPWR = 10,//英雄技能
            BLANK = 11,
            GAME_MODE_BUTTON = 12,
            MOVE_MINION_HOVER_TARGET = 22,
        }

        public enum cardtrigers
        {//卡片效果
            newtriger,//新触发
            getBattlecryEffect,//战吼效果
            onAHeroGotHealedTrigger,//一个英雄受到伤害触发
            onAMinionGotHealedTrigger,//随从受到伤害触发
            onAuraEnds,//光环消失
            onAuraStarts,//光环开始
            onCardIsGoingToBePlayed,//卡片即将使用
            onCardPlay,//卡片使用
            onCardWasPlayed,//卡片使用后
            onDeathrattle,//亡语
            onEnrageStart,//激怒开始
            onEnrageStop,//激怒结束
            onMinionDiedTrigger,//随从死亡触发
            onMinionGotDmgTrigger,//随从受到伤害触发
            onMinionIsSummoned,//随从被召唤
            onMinionWasSummoned,//随从召唤过
            onSecretPlay,//奥秘使用
            onTurnEndsTrigger,//回合结束触发
            onTurnStartTrigger,//回合开始触发
            triggerInspire,//触发激发
            chaosha,//超杀
            Strike,//撞击
            xiaomie,//消灭
            onTurnStart,//回合开始
            onTurnEnd //回合结束
        }

        public enum SpellSchool
        {
            NONE = 0,
            ARCANE = 1,
            FIRE = 2,
            FROST = 3,
            NATURE = 4,
            HOLY = 5,
            SHADOW = 6,
            FEL = 7,
            PHYSICAL_COMBAT = 8,
        }

        public enum Race
        {
            INVALID = 0,
            BLOODELF = 1,
            DRAENEI = 2,
            DWARF = 3,
            GNOME = 4,
            GOBLIN = 5,
            HUMAN = 6,
            NIGHTELF = 7,
            ORC = 8,
            TAUREN = 9,
            TROLL = 10,
            UNDEAD = 11,
            WORGEN = 12,
            GOBLIN2 = 13,
            MURLOC = 14,
            DEMON = 15,
            SCOURGE = 16,
            MECHANICAL = 17,
            ELEMENTAL = 18,
            OGRE = 19,
            BEAST = 20,
            PET = 20,
            TOTEM = 21,
            NERUBIAN = 22,
            PIRATE = 23,
            DRAGON = 24,
            BLANK = 25,
            ALL = 26,
            EGG = 38,
            QUILBOAR = 43,
        }

        public Card chnNameToCard(string chnName) // 输入卡牌中文名，输出Card类对象，多个同名，返回第一个
        {
            Card c;
            cardNameCN enumCn;
            if (Enum.TryParse(chnName, out enumCn) && cardNameCNToCardList.TryGetValue(enumCn, out c))
            {
                return c;
            }
            else
            {
                return null;
            }
        }
        public cardIDEnum cardIdstringToEnum(string s)
        {
            CardDB.cardIDEnum CardEnum;
            if (Enum.TryParse<cardIDEnum>(s, false, out CardEnum)) return CardEnum;
            else
            {
                // Triton.Common.LogUtilities.Logger.GetLoggerInstanceForType().ErrorFormat("[Unidentified card ID :" + s + "]");
                return CardDB.cardIDEnum.None;
            }
        }

        public cardNameEN cardNameENstringToEnum(string s)
        {
            CardDB.cardNameEN NameEnum;
            if (Enum.TryParse<cardNameEN>(s, false, out NameEnum)) return NameEnum;
            else return CardDB.cardNameEN.unknown;
        }

        public cardNameCN cardNameCNstringToEnum(string s)
        {
            CardDB.cardNameCN NameEnum;
            if (Enum.TryParse<cardNameCN>(s, false, out NameEnum)) return NameEnum;
            else return CardDB.cardNameCN.未知;
        }

        public enum ErrorType2
        {
            INVALID = -1,
            NONE = 0,
            REQ_MINION_TARGET = 1, //随从目标
            REQ_FRIENDLY_TARGET = 2, //友方目标
            REQ_ENEMY_TARGET = 3, //敌方目标
            REQ_DAMAGED_TARGET = 4, // 损伤
            REQ_MAX_SECRETS = 5, //最大奥秘
            REQ_FROZEN_TARGET = 6, //冻结
            REQ_CHARGE_TARGET = 7, //冲锋
            REQ_TARGET_MAX_ATTACK = 8, //最大攻击力，有参数
            REQ_NONSELF_TARGET = 9, //非自己
            REQ_TARGET_WITH_RACE = 10, //种族 有参数
            REQ_TARGET_TO_PLAY = 11, //小目标
            REQ_NUM_MINION_SLOTS = 12, //随从数量插槽 有参数
            REQ_WEAPON_EQUIPPED = 13, // 武器装备，需要武器
            REQ_ENOUGH_MANA = 14,
            REQ_YOUR_TURN = 15,
            REQ_NONSTEALTH_ENEMY_TARGET = 16,
            REQ_HERO_TARGET = 17,  //英雄
            REQ_SECRET_ZONE_CAP = 18,
            REQ_MINION_CAP_IF_TARGET_AVAILABLE = 19,
            REQ_MINION_CAP = 20,
            REQ_TARGET_ATTACKED_THIS_TURN = 21,
            REQ_TARGET_IF_AVAILABLE = 22, // 有目标如果用（抉择星辰降落，巫医）
            REQ_MINIMUM_ENEMY_MINIONS = 23, // 最少的地方随从，有参数
            REQ_TARGET_FOR_COMBO = 24, //连击有目标
            REQ_NOT_EXHAUSTED_ACTIVATE = 25,
            REQ_UNIQUE_SECRET_OR_QUEST = 26,
            REQ_TARGET_TAUNTER = 27,
            REQ_CAN_BE_ATTACKED = 28,
            REQ_ACTION_PWR_IS_MASTER_PWR = 29,
            REQ_TARGET_MAGNET = 30,
            REQ_ATTACK_GREATER_THAN_0 = 31,
            REQ_ATTACKER_NOT_FROZEN = 32,
            REQ_HERO_OR_MINION_TARGET = 33,
            REQ_CAN_BE_TARGETED_BY_SPELLS = 34,
            REQ_SUBCARD_IS_PLAYABLE = 35,
            REQ_TARGET_FOR_NO_COMBO = 36,
            REQ_NOT_MINION_JUST_PLAYED = 37,
            REQ_NOT_EXHAUSTED_HERO_POWER = 38,
            REQ_CAN_BE_TARGETED_BY_OPPONENTS = 39,
            REQ_ATTACKER_CAN_ATTACK = 40,
            REQ_TARGET_MIN_ATTACK = 41, // 有参数
            REQ_CAN_BE_TARGETED_BY_HERO_POWERS = 42,
            REQ_ENEMY_TARGET_NOT_IMMUNE = 43,
            REQ_ENTIRE_ENTOURAGE_NOT_IN_PLAY = 44,
            REQ_MINIMUM_TOTAL_MINIONS = 45,//需要最少随从数量，有参数
            REQ_MUST_TARGET_TAUNTER = 46, //目标必须是嘲讽
            REQ_UNDAMAGED_TARGET = 47, //目标未受伤
            REQ_CAN_BE_TARGETED_BY_BATTLECRIES = 48,
            REQ_STEADY_SHOT = 49,
            REQ_MINION_OR_ENEMY_HERO = 50,
            REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND = 51, // 有龙牌在手
            REQ_LEGENDARY_TARGET = 52,
            REQ_FRIENDLY_MINION_DIED_THIS_TURN = 53,  // 需要一个死亡的友方随从在当前回合死亡
            REQ_FRIENDLY_MINION_DIED_THIS_GAME = 54,  //需要一个死亡的友方随从
            REQ_ENEMY_WEAPON_EQUIPPED = 55,
            REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS = 56,
            REQ_TARGET_WITH_BATTLECRY = 57,
            REQ_TARGET_WITH_DEATHRATTLE = 58,
            REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS = 59,
            REQ_SECRET_ZONE_CAP_FOR_NON_SECRET = 60,
            REQ_TARGET_EXACT_COST = 61,
            REQ_STEALTHED_TARGET = 62,
            REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT = 63,
            REQ_MAX_QUESTS = 64,
            REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN = 65,
            REQ_TARGET_NOT_VAMPIRE = 66,
            REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS = 67,
            REQ_NOT_DISABLED_HERO_POWER = 68,
            REQ_MUST_PLAY_OTHER_CARD_FIRST = 69,
            REQ_HAND_NOT_FULL = 70,
            REQ_DRAG_TO_PLAY = 71,
            REQ_TARGET_TO_PLAY2 = 75,
        }

        public class Card
        {
            public string dbfId = "";
            //public string CardID = "";
            public cardNameEN nameEN = cardNameEN.unknown;//名称
            public cardNameCN nameCN = cardNameCN.未知;
            /// <summary>
            /// 14鱼人 15恶魔 17机械 18元素 20野兽 21图腾 23海盗 24龙 26融合怪
            /// </summary>
            public Race race = Race.INVALID;//种族
            //14鱼人 15恶魔 17机械 18元素 20野兽 21图腾 23海盗 24龙 43 野猪人
            public int rarity = 0;//稀有度
            public int cost = 0;//费用
            /// <summary>
            /// 2德鲁伊 3猎人 4法师 5圣骑士 6牧师 7潜行者 8萨满 9术士 10战士 11梦境牌 12中立
            /// </summary>
            public int Class = 0;//职业
            /// <summary>
            /// MOB 随从 SPELL 法术 WEAPON 武器
            /// </summary>
            public cardtype type = CardDB.cardtype.NONE;//类别
            //public string description = "";

            public int Attack = 0; //攻击力
            public int Health = 0;//血量
            public int Durability = 0;//for weapons//耐久值
            public bool tank = false;//嘲讽
            public bool Silence = false;//沉默
            public bool choice = false;//抉择
            public bool windfury = false;//风怒
            public bool poisonous = false;//剧毒
            public bool lifesteal = false;//吸血
            public int dormant = 0;//休眠 0表示非休眠生物或者已醒，还有多少回合醒来
            public bool reborn = false;//复生
            public bool deathrattle = false;//亡语
            public bool battlecry = false;//战吼
            public bool discover = false;//发现
            public bool oneTurnEffect = false;
            public bool Enrage = false;//愤怒 激怒
            public bool Aura = false;//光环
            public bool Elite = false;//精华??
            public bool Combo = false;//连击
            public int overload = 0;//超载
            public bool immuneWhileAttacking = false;//攻击时免疫 蜡烛弓?
            public bool untouchable = false;//不可被攻击
            public bool Stealth = false;//潜行
            public bool Freeze = false;//冰冻
            public bool AdjacentBuff = false;//相邻buff 恐狼?
            public bool Shield = false;//圣盾
            public bool Charge = false;//冲锋
            public bool Rush = false;//突袭
            public bool Secret = false;//奥秘
            public bool Quest = false;//任务
            public bool Questline = false;//任务线
            public bool Morph = false;//变形
            public bool Spellpower = false;//法强
            public bool Inspire = false;//激励
            public bool Outcast = false;//流放
            public bool Corrupted = false;//已腐蚀
            public bool Corrupt = false;//可腐蚀
            public bool CantAttack = false; // 不可攻击
            public bool Collectable = false; // 可收藏
            /// <summary>
            /// 6 暗影 5 神圣 4 自然 3 冰冻 2 火焰 1 奥术
            /// </summary>
            public SpellSchool SpellSchool = SpellSchool.NONE;
            public bool Tradeable = false;//可交易
            public int TradeCost = 0;//交易消耗法力值

            //法术迸发
            public bool Spellburst
            {
                get { return _spellburst; }
                set { _spellburst = value; }
            }
            private bool _spellburst = false;


            public int needEmptyPlacesForPlaying = 0;
            public int needWithMinAttackValueOf = 0;
            public int needWithMaxAttackValueOf = 0;
            public int needRaceForPlaying = 0;
            public int needMinNumberOfEnemy = 0;
            public int needMinTotalMinions = 0;
            public int needMinOwnMinions = 0;
            public int needMinionsCapIfAvailable = 0;
            public int needControlaSecret = 0;

            //additional data
            public bool isToken = false;
            public int isCarddraw = 0;
            public bool damagesTarget = false;
            public bool damagesTargetWithSpecial = false;
            public int targetPriority = 0;
            public bool isSpecialMinion = false;

            public int spellpowervalue = 0;
            public cardIDEnum cardIDenum = cardIDEnum.None;

            public List<cardtrigers> trigers;

            public SimTemplate sim_card = new Sim_None();

            public bool ExistErrorType(CardDB.ErrorType2 errorType)
            {
                foreach (var pr in this.sim_card.GetPlayReqs())
                {
                    if (pr.errorType == errorType) return true;
                }
                return false;
            }
            //获得卡牌的目标
            public List<Minion> getTargetsForCard(Playfield p, bool isLethalCheck, bool own)
            {
                //if wereTargets=true and 0 targets at end -> then can not play this card
                List<Minion> retval = new List<Minion>();
                if (this.type == CardDB.cardtype.MOB && ((own && p.ownMinions.Count >= 7) || (!own && p.enemyMinions.Count >= 7))) return retval; // cant play mob, if we have allready 7 mininos
                if (this.Secret && ((own && (p.ownSecretsIDList.Contains(this.cardIDenum) || p.ownSecretsIDList.Count >= 5)) || (!own && p.enemySecretCount >= 5))) return retval;
                //if (p.mana < this.getManaCost(p, 1)) return retval;

                if (this.sim_card.GetPlayReqs().Length == 0) { retval.Add(null); return retval; }

                List<Minion> targets = new List<Minion>();
                bool targetAll = false;
                bool targetAllEnemy = false;
                bool targetAllFriendly = false;
                bool targetEnemyHero = false;
                bool targetOwnHero = false;
                bool targetOnlyMinion = false;
                bool extraParam = false;
                bool wereTargets = false;
                bool REQ_UNDAMAGED_TARGET = false;
                bool REQ_TARGET_WITH_DEATHRATTLE = false;
                bool REQ_TARGET_WITH_RACE = false;
                bool REQ_TARGET_MIN_ATTACK = false;
                bool REQ_TARGET_MAX_ATTACK = false;
                bool REQ_MUST_TARGET_TAUNTER = false;
                bool REQ_STEADY_SHOT = false;
                bool REQ_FROZEN_TARGET = false;
                bool REQ_HERO_TARGET = false;
                bool REQ_DAMAGED_TARGET = false;
                bool REQ_LEGENDARY_TARGET = false;
                bool REQ_TARGET_IF_AVAILABLE = false;
                bool REQ_STEALTHED_TARGET = false;
                bool REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN = false;

                foreach (PlayReq pr in this.sim_card.GetPlayReqs())
                {
                    switch (pr.errorType)
                    {
                        case ErrorType2.REQ_TARGET_TO_PLAY:
                        case ErrorType2.REQ_TARGET_TO_PLAY2:
                            targetAll = true;
                            continue;
                        case ErrorType2.REQ_MINION_TARGET:
                            targetOnlyMinion = true;
                            continue;
                        case ErrorType2.REQ_TARGET_IF_AVAILABLE:
                            REQ_TARGET_IF_AVAILABLE = true;
                            targetAll = true;
                            continue;
                        case ErrorType2.REQ_FRIENDLY_TARGET:
                            if (own) targetAllFriendly = true;
                            else targetAllEnemy = true;
                            continue;
                        case ErrorType2.REQ_NUM_MINION_SLOTS:
                            if ((own ? p.ownMinions.Count : p.enemyMinions.Count) > 7 - this.needEmptyPlacesForPlaying) return retval;
                            continue;
                        case ErrorType2.REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT:
                            if (own) { if (p.ownMinions.Count > 6 & p.ownMaxMana > 9) return retval; }
                            else if (p.enemyMinions.Count > 6 & p.enemyMaxMana > 9) return retval;
                            continue;
                        case ErrorType2.REQ_ENEMY_TARGET:
                            if (own) targetAllEnemy = true;
                            else targetAllFriendly = true;
                            continue;
                        case ErrorType2.REQ_HERO_TARGET:
                            REQ_HERO_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_MINIMUM_ENEMY_MINIONS:
                            if ((own ? p.enemyMinions.Count : p.ownMinions.Count) < this.needMinNumberOfEnemy) return retval;
                            continue;
                        case ErrorType2.REQ_NONSELF_TARGET:
                            targetAll = true;
                            continue;
                        case ErrorType2.REQ_TARGET_WITH_RACE:
                            REQ_TARGET_WITH_RACE = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_DAMAGED_TARGET:
                            REQ_DAMAGED_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_TARGET_MAX_ATTACK:
                            REQ_TARGET_MAX_ATTACK = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_WEAPON_EQUIPPED:
                            if ((own ? p.ownWeapon.Durability : p.enemyWeapon.Durability) == 0) return retval;
                            continue;
                        case ErrorType2.REQ_TARGET_FOR_COMBO:
                            if (p.cardsPlayedThisTurn >= 1) targetAll = true;
                            continue;
                        case ErrorType2.REQ_TARGET_MIN_ATTACK:
                            REQ_TARGET_MIN_ATTACK = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_MINIMUM_TOTAL_MINIONS:
                            if (this.needMinTotalMinions > p.ownMinions.Count + p.enemyMinions.Count) return retval;
                            continue;
                        case ErrorType2.REQ_MINION_CAP_IF_TARGET_AVAILABLE:
                            if ((own ? p.ownMinions.Count : p.enemyMinions.Count) > 7 - this.needMinionsCapIfAvailable) return retval;
                            continue;
                        case ErrorType2.REQ_ENTIRE_ENTOURAGE_NOT_IN_PLAY://不能召唤更多图腾
                            bool searingtotem = false;
                            bool wrathofairtotem = false;
                            bool stoneclawtotem = false;
                            bool healingtotem = false;
                            foreach (Minion m in (own ? p.ownMinions : p.enemyMinions))
                            {
                                if (m.name == CardDB.cardNameEN.healingtotem)
                                {//治疗图腾
                                    healingtotem = true;
                                    continue;
                                }
                                // 力量或者法强图腾
                                if (m.name == CardDB.cardNameEN.wrathofairtotem || m.name == CardDB.cardNameEN.strengthtotem)
                                {
                                    wrathofairtotem = true;
                                    continue;
                                }
                                if (m.name == CardDB.cardNameEN.searingtotem)
                                {
                                    searingtotem = true;
                                    continue;
                                }
                                if (m.name == CardDB.cardNameEN.stoneclawtotem)
                                {
                                    stoneclawtotem = true;
                                    continue;
                                }
                            }
                            if (healingtotem && wrathofairtotem && searingtotem && stoneclawtotem) return retval;
                            continue;
                        case ErrorType2.REQ_MUST_TARGET_TAUNTER:
                            REQ_MUST_TARGET_TAUNTER = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND:
                            if (own)
                            {
                                foreach (Handmanager.Handcard hc in p.owncards)
                                {
                                    if ((TAG_RACE)hc.card.race == TAG_RACE.DRAGON) { targetAll = true; break; }
                                }
                            }
                            else targetAll = true; // apriori the enemy have a dragon
                            continue;
                        case ErrorType2.REQ_LEGENDARY_TARGET:
                            REQ_LEGENDARY_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_UNDAMAGED_TARGET:
                            REQ_UNDAMAGED_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_TARGET_WITH_DEATHRATTLE:
                            REQ_TARGET_WITH_DEATHRATTLE = true;
                            targetOnlyMinion = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN:
                            REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_STEADY_SHOT:
                            REQ_STEADY_SHOT = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_FROZEN_TARGET:
                            REQ_FROZEN_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_MINION_OR_ENEMY_HERO:
                            REQ_STEADY_SHOT = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_STEALTHED_TARGET:
                            REQ_STEALTHED_TARGET = true;
                            extraParam = true;
                            continue;
                        case ErrorType2.REQ_ENEMY_WEAPON_EQUIPPED:
                            if (own)
                            {
                                if (p.enemyWeapon.Durability > 0) targetEnemyHero = true;
                                else return retval;
                            }
                            else
                            {
                                if (p.ownWeapon.Durability > 0) targetOwnHero = true;
                                else return retval;
                            }
                            continue;
                        case ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS:
                            int tmp = (own) ? p.ownMinions.Count : p.enemyMinions.Count;
                            if (tmp >= needMinOwnMinions) targetAll = true;
                            continue;
                        case ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS:
                            if (p.ownSecretsIDList.Count >= needControlaSecret) targetAll = true;
                            continue;
                        case ErrorType2.REQ_MUST_PLAY_OTHER_CARD_FIRST:
                            if (p.cardsPlayedThisTurn == 0) return retval;
                            continue;
                        case ErrorType2.REQ_HAND_NOT_FULL:
                            if (p.owncards.Count == 10) return retval;
                            continue;

                            //default:
                    }
                }

                if (targetAll)
                {
                    wereTargets = true;
                    if (targetAllFriendly != targetAllEnemy)
                    {
                        if (targetAllFriendly)
                        {
                            foreach (Minion m in p.ownMinions) if (!m.untouchable) targets.Add(m);
                        }
                        else
                        {
                            foreach (Minion m in p.enemyMinions) if (!m.untouchable) targets.Add(m);
                        }
                    }
                    else
                    {
                        foreach (Minion m in p.ownMinions) if (!m.untouchable) targets.Add(m);
                        foreach (Minion m in p.enemyMinions) if (!m.untouchable) targets.Add(m);
                    }
                    if (targetOnlyMinion)
                    {
                        targetEnemyHero = false;
                        targetOwnHero = false;
                    }
                    else
                    {
                        if (!p.enemyHero.immune) targetEnemyHero = true;
                        if (!p.ownHero.immune) targetOwnHero = true;
                        if (targetAllEnemy) targetOwnHero = false;
                        if (targetAllFriendly) targetEnemyHero = false;
                    }
                }

                if (extraParam)
                {
                    wereTargets = true;
                    if (REQ_TARGET_WITH_RACE)
                    {
                        foreach (Minion m in targets)
                        {
                            // 不满足使用条件（或者是融合怪）
                            if (m.handcard.card.race != (Race)this.needRaceForPlaying && m.handcard.card.race != Race.ALL) m.extraParam = true;
                        }
                        targetOwnHero = (p.ownHeroName == HeroEnum.lordjaraxxus && (TAG_RACE)this.needRaceForPlaying == TAG_RACE.DEMON);
                        targetEnemyHero = (p.enemyHeroName == HeroEnum.lordjaraxxus && (TAG_RACE)this.needRaceForPlaying == TAG_RACE.DEMON);
                    }
                    if (REQ_HERO_TARGET)
                    {
                        foreach (Minion m in targets)
                        {
                            m.extraParam = true;
                        }
                        targetOwnHero = true;
                        targetEnemyHero = true;
                    }
                    if (REQ_DAMAGED_TARGET)
                    {
                        foreach (Minion m in targets)
                        {
                            if (!m.wounded)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_TARGET_MAX_ATTACK)
                    {
                        foreach (Minion m in targets)
                        {
                            if (m.Angr > this.needWithMaxAttackValueOf)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_TARGET_MIN_ATTACK)
                    {
                        foreach (Minion m in targets)
                        {
                            if (m.Angr < this.needWithMinAttackValueOf)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_MUST_TARGET_TAUNTER)
                    {
                        foreach (Minion m in targets)
                        {
                            if (!m.taunt)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_UNDAMAGED_TARGET)
                    {
                        foreach (Minion m in targets)
                        {
                            if (m.wounded)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_STEALTHED_TARGET)
                    {
                        foreach (Minion m in targets)
                        {
                            if (!m.stealth)
                            {
                                m.extraParam = true;
                            }
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_TARGET_WITH_DEATHRATTLE)
                    {
                        foreach (Minion m in targets)
                        {
                            if (!m.silenced && (m.handcard.card.deathrattle || m.deathrattle2 != null ||
                            m.ancestralspirit + m.desperatestand + m.souloftheforest + m.stegodon + m.livingspores + m.explorershat + m.returnToHand + m.infest > 0)) continue;
                            else m.extraParam = true;
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN)
                    {
                        if (p.anzOwnElementalsLastTurn < 1)
                        {
                            foreach (Minion m in targets) m.extraParam = true;
                            targetOwnHero = false;
                            targetEnemyHero = false;
                        }
                    }
                    if (REQ_LEGENDARY_TARGET)
                    {
                        wereTargets = false;
                        foreach (Minion m in targets)
                        {
                            if (m.handcard.card.rarity != 5) m.extraParam = true;
                        }
                        targetOwnHero = false;
                        targetEnemyHero = false;
                    }
                    if (REQ_STEADY_SHOT)
                    {
                        if ((p.weHaveSteamwheedleSniper && own) || (p.enemyHaveSteamwheedleSniper && !own))
                        {
                            foreach (Minion m in targets)
                            {
                                if (m.cantBeTargetedBySpellsOrHeroPowers && (this.type == cardtype.HEROPWR || this.type == cardtype.SPELL))
                                {
                                    m.extraParam = true;
                                    if (m.stealth && !m.own) m.extraParam = true;
                                }
                            }
                            if (own) targetEnemyHero = true;
                            else targetOwnHero = true;
                        }
                        else wereTargets = false;
                    }
                    if (REQ_FROZEN_TARGET)
                    {

                        foreach (Minion m in targets)
                        {
                            if (!m.frozen) m.extraParam = true;
                        }
                    }
                }

                if (targetEnemyHero && own && p.enemyHero.stealth) targetEnemyHero = false;
                if (targetOwnHero && !own && p.ownHero.stealth) targetOwnHero = false;

                if (isLethalCheck)
                {//斩杀?
                    if (targetEnemyHero && own) retval.Add(p.enemyHero);
                    else if (targetOwnHero && !own) retval.Add(p.ownHero);

                    switch (this.type)
                    {
                        case cardtype.SPELL:
                            if (p.prozis.penman.attackBuffDatabase.ContainsKey(this.nameEN))
                            {
                                if (targetOwnHero && own) retval.Add(p.ownHero);
                                foreach (Minion m in targets)
                                {
                                    if (m.extraParam != true && !m.cantBeTargetedBySpellsOrHeroPowers)
                                    {
                                        if (m.own)
                                        {
                                            if (m.Ready) retval.Add(m);
                                        }
                                        else if (m.taunt) retval.Add(m);
                                    }
                                    m.extraParam = false;
                                }
                            }
                            else
                            {
                                switch (this.nameEN)
                                {
                                    case cardNameEN.polymorphboar://变形术：野猪
                                        foreach (Minion m in targets)
                                        {
                                            m.extraParam = false;
                                            if (m.cantBeTargetedBySpellsOrHeroPowers) continue;
                                            if (m.own) retval.Add(m);
                                            else if (m.taunt) retval.Add(m);
                                        }
                                        break;
                                    case cardNameEN.hex: goto case cardNameEN.polymorph;//妖术
                                    case cardNameEN.polymorph://变形术
                                        foreach (Minion m in targets)
                                        {
                                            m.extraParam = false;
                                            if (!m.own && m.taunt && !m.cantBeTargetedBySpellsOrHeroPowers) retval.Add(m);
                                        }
                                        break;
                                }
                            }
                            break;
                        case cardtype.MOB:
                            foreach (Minion m in targets)
                            {
                                if (m.extraParam != true)
                                {
                                    if (m.stealth && !m.own) continue;
                                    retval.Add(m);
                                }
                                m.extraParam = false;
                            }
                            break;
                        case cardtype.HEROPWR:
                            if (p.prozis.penman.attackBuffDatabase.ContainsKey(this.nameEN))
                            {
                                foreach (Minion m in targets)
                                {
                                    if (m.extraParam != true && !m.cantBeTargetedBySpellsOrHeroPowers)
                                    {
                                        if (m.own)
                                        {
                                            if (m.Ready) retval.Add(m);
                                        }
                                        else if (m.taunt) retval.Add(m);
                                    }
                                    m.extraParam = false;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (targetEnemyHero) retval.Add(p.enemyHero);
                    if (targetOwnHero) retval.Add(p.ownHero);

                    foreach (Minion m in targets)
                    {
                        if (m.extraParam != true)
                        {
                            if (m.stealth && !m.own) continue;
                            if (m.cantBeTargetedBySpellsOrHeroPowers && (this.type == cardtype.SPELL || this.type == cardtype.HEROPWR)) continue;
                            retval.Add(m);
                        }
                        m.extraParam = false;
                    }
                }

                if (retval.Count == 0 && (!wereTargets || REQ_TARGET_IF_AVAILABLE)) retval.Add(null);

                return retval;
            }


            public List<Minion> getTargetsForHeroPower(Playfield p, bool own)
            {
                List<Minion> trgts = getTargetsForCard(p, p.isLethalCheck, own);
                cardNameEN abName = own ? p.ownHeroAblility.card.nameEN : p.enemyHeroAblility.card.nameEN;
                int abType = 0; //0 none, 1 damage, 2 heal, 3 baff
                switch (abName)
                {//此处可添加英雄技能
                    case cardNameEN.heal: goto case cardNameEN.lesserheal;
                    case cardNameEN.lesserheal:
                        if (p.anzOwnAuchenaiSoulpriest > 0 || p.embracetheshadow > 0) abType = 1;
                        else abType = 2;
                        break;
                    case cardNameEN.ballistashot: abType = 1; break;
                    case cardNameEN.steadyshot: abType = 1; break;
                    case cardNameEN.fireblast: abType = 1; break;
                    case cardNameEN.fireblastrank2: abType = 1; break;
                    case cardNameEN.lightningjolt: abType = 1; break;
                    case cardNameEN.mindspike: abType = 1; break;
                    case cardNameEN.mindshatter: abType = 1; break;
                    case cardNameEN.powerofthefirelord: abType = 1; break;
                    case cardNameEN.shotgunblast: abType = 1; break;
                    case cardNameEN.unbalancingstrike: abType = 1; break;
                    case cardNameEN.dinomancy: abType = 3; break;
                }

                switch (abType)
                {
                    case 2:
                        List<Minion> minions = own ? p.ownMinions : p.enemyMinions;
                        int tCount = minions.Count;
                        bool needCut = true;
                        for (int i = 0; i < tCount; i++)
                        {
                            switch (minions[i].name)
                            {
                                case cardNameEN.shadowboxer:
                                    if (own && p.enemyHero.Hp == 1 && p.enemyMinions.Count > 0) needCut = false;
                                    break;
                                case cardNameEN.holychampion: needCut = false; break;
                                case cardNameEN.lightwarden: needCut = false; break;
                                case cardNameEN.northshirecleric: needCut = false; break;


                            }
                        }

                        tCount = trgts.Count;
                        if (tCount > 0)
                        {
                            if (trgts[0] != null)
                            {
                                List<Minion> tmp = new List<Minion>();
                                for (int i = 0; i < tCount; i++)
                                {
                                    Minion m = trgts[i];
                                    if (m.Hp < m.maxHp)
                                    {
                                        if (needCut)
                                        {
                                            if (m.own == own) tmp.Add(m);
                                        }
                                        else tmp.Add(m);
                                    }
                                }
                                return tmp;
                            }
                        }
                        break;
                }
                return trgts;
            }
            //计算费用 会减费的牌需要在里面写
            public int calculateManaCost(Playfield p)//calculates the mana from orginal mana, needed for back-to hand effects and new draw
            {
                int retval = this.cost;//卡牌本身的费用
                int offset = 0;//每个随从的减费

                if (p.anzOwnShadowfiend > 0) offset -= p.anzOwnShadowfiend;//暗影狂乱 需要费用减去抓的怪费用

                switch (this.type)
                {
                    case cardtype.MOB:
                        if (p.anzOwnAviana > 0) retval = 1;//av娜

                        if (p.anzOwnScargil > 0 && (this.race == Race.MURLOC || this.race == Race.ALL)) retval = 1;//斯卡基尔

                        if (p.ownDemonCostLessOnce > 0 && (this.race == Race.DEMON || this.race == Race.ALL))
                            offset -= p.ownDemonCostLessOnce;

                        offset += p.ownMinionsCostMore;//随从消耗更多

                        if (this.deathrattle) offset += p.ownDRcardsCostMore;

                        offset += p.managespenst;

                        int temp = -(p.startedWithbeschwoerungsportal) * 2;
                        if (retval + temp <= 0) temp = -retval + 1;//传送门 负数
                        offset = offset + temp;

                        if (p.mobsplayedThisTurn == 0)
                        {//消耗血
                            offset -= p.winzigebeschwoererin;
                        }

                        if (this.battlecry)
                        {
                            offset += p.nerubarweblord * 2;//尼鲁巴蛛网领主
                        }

                        if ((TAG_RACE)this.race == TAG_RACE.MECHANICAL)
                        { //if the number of zauberlehrlings change
                            offset -= p.anzOwnMechwarper;//Mechwarper机械跃迁
                        }
                        break;
                    case cardtype.SPELL:
                        if (p.nextSpellThisTurnCost0) return 0;//这个标志位在sim卡里
                        offset += p.ownSpelsCostMore;
                        if (p.playedPreparation)//伺机待发
                        { //if the number of zauberlehrlings change
                            offset -= 2;
                        }
                        break;
                    case cardtype.WEAPON:
                        offset -= p.blackwaterpirate * 2;//黑水海盗
                        if (this.deathrattle) offset += p.ownDRcardsCostMore;
                        break;
                }

                offset -= p.myCardsCostLess;

                switch (this.nameCN)
                {
                    case CardDB.cardNameCN.希望圣契:
                    case CardDB.cardNameCN.正义圣契:
                    case CardDB.cardNameCN.智慧圣契:
                    case CardDB.cardNameCN.审判圣契:
                        retval = retval + offset - p.libram;
                        break;
                }
                switch (this.nameEN)
                {
                    // //特殊减费机制的卡
                    //case CardDB.cardName.libramofjudgment://审判圣契
                    //case CardDB.cardName.libramofwisdom://智慧圣契
                    //case CardDB.cardName.libramofjustice://正义圣契
                    //case CardDB.cardName.libramofhope://希望圣契
                    //    retval = retval + offset - p.libram;
                    //    break;
                    //case CardDB.cardNameEN.happyghoul:
                    //    if (p.healTimes > 0)
                    //        retval = 0 + offset;
                    //    break;
                    //case CardDB.cardNameEN.wildmagic:
                    //    retval = offset;
                    //    break;
                    //case CardDB.cardNameEN.dreadcorsair://恐怖海盗
                    //    retval = retval + offset - p.ownWeapon.Angr;
                    //    break;
                    //case CardDB.cardNameEN.volcanicdrake://火山幼龙
                    //    retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                    //    break;
                    //case CardDB.cardNameEN.knightofthewild://荒野骑士
                    //    retval = retval + offset - p.tempTrigger.ownBeastSummoned;
                    //    break;
                    //case CardDB.cardNameEN.seagiant://海巨人
                    //    retval = retval + offset - p.ownMinions.Count - p.enemyMinions.Count;
                    //    break;
                    //case CardDB.cardNameEN.mountaingiant://山岭巨人
                    //    retval = retval + offset - p.owncards.Count;
                    //    break;
                    //case CardDB.cardNameEN.clockworkgiant://发条巨人
                    //    retval = retval + offset - p.enemyAnzCards;
                    //    break;
                    //case CardDB.cardNameEN.moltengiant://熔岩巨人
                    //    retval = retval + offset - p.ownHero.maxHp + p.ownHero.Hp;
                    //    break;
                    //case CardDB.cardNameEN.frostgiant://冰霜巨人
                    //    retval = retval + offset - p.anzUsedOwnHeroPower;
                    //    break;
                    //case CardDB.cardNameEN.arcanegiant://奥术巨人
                    //    retval = retval + offset - p.spellsplayedSinceRecalc;
                    //    break;
                    //case CardDB.cardNameEN.snowfurygiant://雪怒巨人
                    //    retval = retval + offset - p.ueberladung;
                    //    break;
                    //case CardDB.cardNameEN.kabalcrystalrunner://暗金教水晶侍女
                    //    retval = retval + offset - 2 * p.secretsplayedSinceRecalc;
                    //    break;
                    //case CardDB.cardNameEN.secondratebruiser://二流打手
                    //    retval = retval + offset - ((p.enemyMinions.Count < 3) ? 0 : 2);
                    //    break;
                    //case CardDB.cardNameEN.golemagg:
                    //    retval = retval + offset - p.ownHero.maxHp + p.ownHero.Hp;
                    //    break;
                    //case CardDB.cardNameEN.volcaniclumberer://火山邪木
                    //    retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                    //    break;
                    //case CardDB.cardNameEN.skycapnkragg://天空上尉库拉格
                    //    int costBonus = 0;
                    //    foreach (Minion m in p.ownMinions)
                    //    {
                    //        if ((TAG_RACE)m.handcard.card.race == TAG_RACE.PIRATE) costBonus++;
                    //    }
                    //    retval = retval + offset - costBonus;
                    //    break;
                    //case CardDB.cardNameEN.everyfinisawesome://鱼人恩典
                    //    int costBonusM = 0;
                    //    foreach (Minion m in p.ownMinions)
                    //    {
                    //        if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MURLOC) costBonusM++;
                    //    }
                    //    retval = retval + offset - costBonusM;
                    //    break;
                    //// 血肉巨人
                    //case CardDB.cardNameEN.fleshgiant:
                    //    retval = retval + offset - p.healOrDamageTimes;
                    //    break;
                    //case CardDB.cardNameEN.crush:
                    //    // cost 4 less if we have a dmged minion
                    //    bool dmgedminions = false;
                    //    foreach (Minion m in p.ownMinions)
                    //    {
                    //        if (m.wounded) dmgedminions = true;
                    //    }
                    //    if (dmgedminions)
                    //    {
                    //        retval = retval + offset - 4;
                    //    }
                    //    break;
                    //case CardDB.cardNameEN.thingfrombelow://深渊魔物
                    //    if (p.playactions.Count > 0)
                    //    {
                    //        foreach (Action a in p.playactions)
                    //        {
                    //            if (a.actionType == actionEnum.playcard)//使用卡片
                    //            {
                    //                switch (a.card.card.nameEN)
                    //                {
                    //                    case cardNameEN.tuskarrtotemic: //海象人图腾师
                    //                        retval -= (p.ownBrannBronzebeard + 1); break;
                    //                    case cardNameEN.splittingaxe://分裂战斧
                    //                        int ownTotemsCount = 0;
                    //                        foreach (Minion m in p.ownMinions)
                    //                        {
                    //                            if ((TAG_RACE)m.handcard.card.race == TAG_RACE.TOTEM) ownTotemsCount++;
                    //                        }
                    //                        retval -= ownTotemsCount;
                    //                        break;
                    //                    default:
                    //                        if ((TAG_RACE)a.card.card.race == TAG_RACE.TOTEM) retval--;
                    //                        break;
                    //                }
                    //            }
                    //            else if (a.actionType == actionEnum.useHeroPower)//使用英雄技能
                    //            {
                    //                switch (a.card.card.nameEN)
                    //                {
                    //                    case cardNameEN.totemiccall: retval--; break;//图腾召唤
                    //                    case cardNameEN.totemicslam: retval--; break;//图腾崇拜
                    //                }
                    //            }
                    //        }
                    //    }
                    //    retval = retval + offset;
                    //    break;
                    default:
                        retval = retval + offset;
                        break;
                }

                if (this.Secret)
                {
                    if (p.anzOwnCloakedHuntress > 0 || p.nextSecretThisTurnCost0) retval = 0;
                }

                retval = Math.Max(0, retval);

                return retval;
            }

            public int getManaCost(Playfield p, int currentcost)
            {
                int retval = currentcost;

                int offset = 0; // if offset < 0 costs become lower, if >0 costs are higher at the end

                // CARDS that increase/decrease the manacosts of others ##############################
                //卡片增加减少卡片法力消耗
                switch (this.type)
                {
                    case cardtype.HEROPWR:
                        retval += p.ownHeroPowerCostLessOnce;
                        if (retval < 0) retval = 0;
                        return retval;
                    case cardtype.MOB:

                        if (p.ownMinionsCostMore != p.ownMinionsCostMoreAtStart)
                        {
                            offset += (p.ownMinionsCostMore - p.ownMinionsCostMoreAtStart);
                        }//


                        if (this.deathrattle && p.ownDRcardsCostMore != p.ownDRcardsCostMoreAtStart)
                        {
                            offset += (p.ownDRcardsCostMore - p.ownDRcardsCostMoreAtStart);
                        }


                        if (p.managespenst != p.startedWithManagespenst)
                        {
                            offset += (p.managespenst - p.startedWithManagespenst);
                        }


                        if (this.battlecry && p.nerubarweblord != p.startedWithnerubarweblord)
                        {
                            offset += (p.nerubarweblord - p.startedWithnerubarweblord) * 2;
                        }


                        if (p.anzOwnAviana > 0)
                        {
                            retval = 1;
                        }

                        if (p.anzOwnScargil > 0 && (this.race == Race.MURLOC || this.race == Race.ALL))
                        {
                            retval = 1;
                        }

                        if (p.anzOwnMechwarper != p.anzOwnMechwarperStarted && (TAG_RACE)this.race == TAG_RACE.MECHANICAL)
                        {
                            offset += (p.anzOwnMechwarperStarted - p.anzOwnMechwarper);
                        }


                        if (p.startedWithbeschwoerungsportal != p.beschwoerungsportal)
                        {
                            offset += (p.startedWithbeschwoerungsportal - p.beschwoerungsportal) * 2;
                        }


                        if (p.winzigebeschwoererin != p.startedWithWinzigebeschwoererin && ((p.turnCounter == 0 && p.startedWithMobsPlayedThisTurn == 0) || (p.turnCounter > 0 && p.mobsplayedThisTurn == 0)))
                        {
                            offset += (p.startedWithWinzigebeschwoererin - p.winzigebeschwoererin);
                        }


                        if (p.anzOwnDragonConsort != p.anzOwnDragonConsortStarted && (TAG_RACE)this.race == TAG_RACE.DRAGON)
                        {
                            offset += (p.anzOwnDragonConsortStarted - p.anzOwnDragonConsort) * 2;
                        }
                        break;
                    case cardtype.SPELL:

                        if (p.nextSpellThisTurnCost0) return 0;


                        if (p.ownSpelsCostMoreAtStart != p.ownSpelsCostMore)
                        {
                            offset += p.ownSpelsCostMore - p.ownSpelsCostMoreAtStart;
                        }


                        if (p.playedPreparation)
                        {
                            offset -= 2;
                        }
                        break;
                    case cardtype.WEAPON:

                        if (p.blackwaterpirateStarted != p.blackwaterpirate)
                        {
                            offset += (p.blackwaterpirateStarted - p.blackwaterpirate) * 2;
                        }

                        if (this.deathrattle && p.ownDRcardsCostMore != p.ownDRcardsCostMoreAtStart)
                        {
                            offset += (p.ownDRcardsCostMore - p.ownDRcardsCostMoreAtStart);
                        }
                        break;
                }


                if (p.startedWithmyCardsCostLess != p.myCardsCostLess)
                {
                    offset += p.startedWithmyCardsCostLess - p.myCardsCostLess;
                }
                switch (this.nameEN)
                {
                    case CardDB.cardNameEN.frenziedfelwing:
                        if (p.enemyHero.Hp < p.enemyHeroTurnStartedHp)
                        {
                            retval = retval + offset - (p.enemyHeroTurnStartedHp - p.enemyHero.Hp);
                        }
                        break;
                    case CardDB.cardNameEN.libramofjudgment:
                        retval = retval + offset - p.libram;
                        break;
                    case CardDB.cardNameEN.libramofwisdom://智慧圣契
                        retval = retval + offset - p.libram;
                        break;
                    case CardDB.cardNameEN.libramofjustice://正义圣契
                        retval = retval + offset - p.libram;
                        break;
                    case CardDB.cardNameEN.libramofhope://希望圣契
                        retval = retval + offset - p.libram;
                        break;
                    case CardDB.cardNameEN.volcaniclumberer:
                        retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                        break;
                    case CardDB.cardNameEN.solemnvigil:
                        retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                        break;
                    case CardDB.cardNameEN.volcanicdrake:
                        retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                        break;
                    case CardDB.cardNameEN.knightofthewild:
                        retval = retval + offset - p.tempTrigger.ownBeastSummoned;
                        break;
                    case CardDB.cardNameEN.dragonsbreath:
                        retval = retval + offset - p.ownMinionsDiedTurn - p.enemyMinionsDiedTurn;
                        break;
                    case CardDB.cardNameEN.dreadcorsair:
                        retval = retval + offset - p.ownWeapon.Angr + p.ownWeaponAttackStarted; // if weapon attack change we change manacost
                        break;
                    case CardDB.cardNameEN.seagiant:
                        retval = retval + offset - p.ownMinions.Count - p.enemyMinions.Count + p.ownMobsCountStarted + p.enemyMobsCountStarted;
                        break;
                    case CardDB.cardNameEN.mountaingiant:
                        retval = retval + offset - p.owncards.Count + p.ownCardsCountStarted;
                        break;
                    case CardDB.cardNameEN.clockworkgiant:
                        retval = retval + offset - p.enemyAnzCards + p.enemyCardsCountStarted;
                        break;
                    case CardDB.cardNameEN.moltengiant:
                        retval = retval + offset - p.ownHeroHpStarted + p.ownHero.Hp;
                        break;
                    case CardDB.cardNameEN.frostgiant:
                        retval = retval + offset - p.anzUsedOwnHeroPower;
                        break;
                    case CardDB.cardNameEN.arcanegiant:
                        retval = retval + offset - p.spellsplayedSinceRecalc;
                        break;
                    case CardDB.cardNameEN.snowfurygiant:
                        retval = retval + offset - p.ueberladung;
                        break;
                    case CardDB.cardNameEN.kabalcrystalrunner:
                        retval = retval + offset - 2 * p.secretsplayedSinceRecalc;
                        break;
                    case CardDB.cardNameEN.secondratebruiser:
                        retval = retval + offset - ((p.enemyMinions.Count < 3) ? 0 : 2) + ((p.enemyMobsCountStarted < 3) ? 0 : 2);
                        break;
                    case CardDB.cardNameEN.golemagg:
                        retval = retval + offset - p.ownHeroHpStarted + p.ownHero.Hp;
                        break;
                    case CardDB.cardNameEN.skycapnkragg:
                        int costBonus = 0;
                        foreach (Minion m in p.ownMinions)
                        {
                            if (m.handcard.card.race == CardDB.Race.PIRATE || m.handcard.card.race == CardDB.Race.ALL) costBonus++;
                        }
                        retval = retval + offset - costBonus + p.anzOwnPiratesStarted;
                        break;
                    // 恩典
                    case CardDB.cardNameEN.everyfinisawesome:
                        int costBonusM = 0;
                        foreach (Minion m in p.ownMinions)
                        {
                            if (m.untouchable || m.dormant > 0) continue;
                            if (m.handcard.card.race == Race.MURLOC || m.handcard.card.race == Race.ALL) costBonusM++;
                        }
                        retval = retval + offset - costBonusM + p.anzOwnMurlocStarted;
                        break;
                    // 血肉巨人
                    case CardDB.cardNameEN.fleshgiant:
                        retval = retval + offset - p.healOrDamageTimes;
                        break;
                    case CardDB.cardNameEN.crush:
                        // cost 4 less if we have a dmged minion
                        bool dmgedminions = false;
                        foreach (Minion m in p.ownMinions)
                        {
                            if (m.wounded) dmgedminions = true;
                        }
                        if (dmgedminions != p.startedWithDamagedMinions)
                        {
                            if (dmgedminions)
                            {
                                retval = retval + offset - 4;
                            }
                            else
                            {
                                retval = retval + offset + 4;
                            }
                        }
                        break;
                    case CardDB.cardNameEN.happyghoul:
                        if (p.healTimes > 0)
                            retval = 0 + offset;
                        break;
                    case CardDB.cardNameEN.wildmagic:
                        retval = 0;
                        break;
                    case CardDB.cardNameEN.thingfrombelow:
                        if (p.playactions.Count > 0)
                        {
                            foreach (Action a in p.playactions)
                            {
                                if (a.actionType == actionEnum.playcard)
                                {
                                    switch (a.card.card.nameEN)
                                    {
                                        case cardNameEN.tuskarrtotemic: retval -= p.ownBrannBronzebeard + 1; break;
                                        case cardNameEN.splittingaxe://分裂战斧
                                            int ownTotemsCount = 0;
                                            foreach (Minion m in p.ownMinions)
                                            {
                                                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.TOTEM) ownTotemsCount++;
                                            }
                                            retval -= ownTotemsCount;
                                            break;
                                        default:
                                            if ((TAG_RACE)a.card.card.race == TAG_RACE.TOTEM) retval--;
                                            break;
                                    }
                                }
                                else if (a.actionType == actionEnum.useHeroPower)
                                {
                                    switch (a.card.card.nameEN)
                                    {
                                        case cardNameEN.totemiccall: retval--; break;
                                        case cardNameEN.totemicslam: retval--; break;
                                    }
                                }
                            }
                        }
                        retval = retval + offset;
                        break;
                    default:
                        retval = retval + offset;
                        break;
                }

                if (this.Secret && (p.anzOwnCloakedHuntress > 0 || p.nextSecretThisTurnCost0))
                {
                    retval = 0;
                }

                retval = Math.Max(0, retval);

                return retval;
            }

            public bool canplayCard(Playfield p, int manacost, bool own)
            {//能否使用牌
                if (p.mana < this.getManaCost(p, manacost)) return false;
                if (this.getTargetsForCard(p, false, own).Count == 0) return false;
                return true;
            }

            public string chnInfo()  //打印中文信息用，中文名 + 身材，方便辨识
            {
                if (type == cardtype.MOB) //随从
                    return nameCN.ToString() + "(" + Attack + "," + Health + ")";
                else
                    return nameCN.ToString();
            }

        }

        List<Card> cardlist = new List<Card>();
        Dictionary<cardIDEnum, Card> cardidToCardList = new Dictionary<cardIDEnum, Card>();
        Dictionary<string, Card> carddbfidToCardList = new Dictionary<string, Card>();        
        Dictionary<cardNameCN, Card> cardNameCNToCardList = new Dictionary<cardNameCN, Card>();
        Dictionary<cardNameEN, Card> cardNameENToCardList = new Dictionary<cardNameEN, Card>();


        public Card unknownCard;
        public bool installedWrong = false;

        public Card burlyrockjaw;
        private static CardDB instance;

        public static CardDB Instance
        {
            get
            {
                if (instance == null)
                {
                    Helpfunctions.Instance.ErrorLog("开始加载CardDB");
                    var dt = DateTime.Now;
                    instance = new CardDB();
                    //instance.enumCreator();// only call this to get latest cardids
                    // have to do it 2 times (or the kids inside the simcards will not have a simcard :D
                    Helpfunctions.Instance.ErrorLog("开始加载Sim");
                    foreach (Card c in instance.cardlist)
                    {
                        if (c.cardIDenum != cardIDEnum.None)  // 增加非None判断
                            c.sim_card = CardHelper.GetCardSimulation(c.cardIDenum);
                        if (c.sim_card == null) continue;
                        foreach (var pr in c.sim_card.GetPlayReqs()) pr.UpdateCardAttr(c);
                    }
                    Helpfunctions.Instance.ErrorLog("加载完毕,总计用时: " + (DateTime.Now - dt).TotalSeconds + " s");
                    instance.setAdditionalData();
                }
                return instance;
            }
        }
        //解析 carddefs.xml的函数
        CardDB()
        {
            this.cardlist.Clear();
            this.cardidToCardList.Clear();
            this.carddbfidToCardList.Clear();
            this.cardNameCNToCardList.Clear();
            this.cardNameENToCardList.Clear();

            //placeholdercard
            this.cardlist.Add(new Card { nameEN = cardNameEN.unknown, cost = 10 });
            this.unknownCard = cardlist[0];


            var filePath = Path.Combine(Settings.Instance.path, "CardDefs.xml");
            if (!File.Exists(filePath))
            {
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("在{0}下找不到 CardDefs.xml!" + Settings.Instance.path);
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                Helpfunctions.Instance.ErrorLog("ERROR#################################################");
                return;
            }
            var reNameEN = new Regex("[a-zA-Z0-9]");
            var reNameCN = new Regex("[a-zA-Z0-9]|[\\u4e00-\\u9fa5]");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            var entities = doc.SelectNodes("CardDefs/Entity");
            foreach (XmlElement entity in entities)
            {
                var cardId = entity.GetAttribute("CardID");
                var card = new Card();
                card.dbfId = entity.GetAttribute("ID");
                card.cardIDenum = this.cardIdstringToEnum(cardId);
                if (cardId.EndsWith("t") ||
                    cardId.Equals("ds1_whelptoken") ||
                    cardId.Equals("CS2_mirror") ||
                    cardId.Equals("CS2_050") ||
                    cardId.Equals("CS2_052") ||
                    cardId.Equals("CS2_051") ||
                    cardId.Equals("NEW1_009") ||
                    cardId.Equals("CS2_152") ||
                    cardId.Equals("CS2_boar") ||
                    cardId.Equals("EX1_tk11") ||
                    cardId.Equals("EX1_506a") ||
                    cardId.Equals("skele21") ||
                    cardId.Equals("EX1_tk9") ||
                    cardId.Equals("EX1_finkle") ||
                    cardId.Equals("EX1_598") ||
                    cardId.Equals("EX1_tk34"))
                {
                    card.isToken = true;
                }
                #region parse tags
                foreach (XmlElement tag in entity.ChildNodes)
                {
                    if (!tag.HasAttribute("enumID"))
                        continue;
                    if ("ReferencedTag".Equals(tag.Name))
                    {
                        if (tag.GetAttribute("enumID") == "1518")
                        {
                            card.dormant = 1;
                        }
                        if (tag.GetAttribute("enumID") == "190")
                        {
                            card.tank = true;
                        }
                        continue;
                    }

                    switch (tag.GetAttribute("enumID"))
                    {
                        case "321":
                            {
                                card.Collectable = true;
                            }
                            break;
                        case "227":
                            {
                                card.CantAttack = true;
                            }
                            break;
                        case "45":
                            {
                                card.Health = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "199":
                            {
                                card.Class = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "47":
                            {
                                card.Attack = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "200":
                            {
                                card.race = (Race)int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "203":
                            {
                                card.rarity = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "48":
                            {
                                card.cost = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "202":
                            {
                                card.type = (cardtype)int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "185":
                            {
                                foreach (XmlElement node in tag.ChildNodes)
                                {
                                    if (node.Name == "enUS")
                                    {
                                        var nameEN = "";
                                        foreach (Match m in reNameEN.Matches(node.InnerText))
                                        {
                                            if (m.Success)
                                                nameEN += m.Value;
                                        }
                                        if (nameEN == "continue" || nameEN == "protected")
                                            nameEN = "@" + nameEN;
                                        if (nameEN.Length > 0 && char.IsDigit(nameEN[0]))
                                            nameEN = "_" + nameEN;
                                        nameEN = nameEN.ToLower();
                                        card.nameEN = this.cardNameENstringToEnum(nameEN);
                                    }
                                    else if (node.Name == "zhCN")
                                    {
                                        var nameCN = "";
                                        foreach (Match m in reNameCN.Matches(node.InnerText))
                                        {
                                            if (m.Success)
                                                nameCN += m.Value;
                                        }
                                        if (nameCN == "continue" || nameCN == "protected")
                                            nameCN = "@" + nameCN;
                                        if (nameCN.Length > 0 && char.IsDigit(nameCN[0]))
                                            nameCN = "_" + nameCN;
                                        card.nameCN = this.cardNameCNstringToEnum(nameCN);
                                    }
                                }
                            }
                            break;
                        case "443":
                            {
                                card.choice = true;
                            }
                            break;
                        case "363":
                            {
                                card.poisonous = true;
                            }
                            break;
                        case "212":
                            {
                                card.Enrage = true;
                            }
                            break;
                        case "338":
                            {
                                card.oneTurnEffect = true;
                            }
                            break;
                        case "362":
                            {
                                card.Aura = true;
                            }
                            break;
                        case "190":
                            {
                                card.tank = true;
                            }
                            break;
                        case "218":
                            {
                                card.battlecry = true;
                            }
                            break;
                        case "415":
                            {
                                card.discover = true;
                            }
                            break;
                        case "189":
                            {
                                card.windfury = true;
                            }
                            break;
                        case "217":
                            {
                                card.deathrattle = true;
                            }
                            break;
                        case "403":
                            {
                                card.Inspire = true;
                            }
                            break;
                        case "187":
                            {
                                card.Durability = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "114":
                            {
                                card.Elite = true;
                            }
                            break;
                        case "220":
                            {
                                card.Combo = true;
                            }
                            break;
                        case "215":
                            {
                                card.overload = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "685":
                            {
                                card.lifesteal = true;
                            }
                            break;
                        case "448":
                            {
                                card.untouchable = true;
                            }
                            break;
                        case "191":
                            {
                                card.Stealth = true;
                            }
                            break;
                        case "219":
                            {
                                card.Secret = true;
                            }
                            break;
                        case "462":
                            {
                                card.Quest = true;
                            }
                            break;
                        case "1725":
                            {
                                card.Questline = true;
                            }
                            break;
                        case "208":
                            {
                                card.Freeze = true;
                            }
                            break;
                        case "350":
                            {
                                card.AdjacentBuff = true;
                            }
                            break;
                        case "194":
                            {
                                card.Shield = true;
                            }
                            break;
                        case "197":
                            {
                                card.Charge = true;
                            }
                            break;
                        case "339":
                            {
                                card.Silence = true;
                            }
                            break;
                        case "293":
                            {
                                card.Morph = true;
                            }
                            break;
                        case "192":
                            {
                                card.Spellpower = true;
                                card.spellpowervalue = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "791":
                            {
                                card.Rush = true;
                            }
                            break;
                        case "1085":
                            {
                                card.reborn = true;
                            }
                            break;
                        case "1427":
                            {
                                card.Spellburst = true;
                            }
                            break;
                        case "1452":
                            {
                                card.Corrupted = true;
                            }
                            break;
                        case "1524":
                            {
                                card.Corrupt = true;
                            }
                            break;
                        case "1635":
                            {
                                card.SpellSchool = (SpellSchool)int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                        case "1518":
                            {
                                card.dormant = 1;
                            }
                            break;
                        case "1333":
                            {
                                card.Outcast = true;
                            }
                            break;
                        case "1720":
                            {
                                card.Tradeable = true;
                            }
                            break;
                        case "1743":
                            {
                                card.TradeCost = int.Parse(tag.GetAttribute("value"));
                            }
                            break;
                    }
                }
                #endregion
                cardlist.Add(card);
                if (card.dbfId != null && card.dbfId != "")
                    carddbfidToCardList[card.dbfId] = card;
                if (card.cardIDenum != cardIDEnum.None)
                    cardidToCardList[card.cardIDenum] = card;
                if (card.nameCN != cardNameCN.未知)
                {
                    cardNameCNToCardList[card.nameCN] = card;
                }
                // else
                // {
                // Helpfunctions.Instance.ErrorLog("未知卡牌中文名: " + cardId);
                // }
                if (card.nameEN != cardNameEN.unknown)
                {
                    cardNameENToCardList[card.nameEN] = card;
                }
                // else
                // {
                // Helpfunctions.Instance.ErrorLog("未知卡牌英文名: " + cardId);
                // }
            }
        }

        // 根据卡名获取卡
        public Card getCardData(CardDB.cardNameEN cardname)
        {
            Card c;
            if (this.cardNameENToCardList.TryGetValue(cardname, out c))
                return c;
            return this.unknownCard;
        }

        // 根据卡名获取卡
        public Card getCardData(CardDB.cardNameCN cardname)
        {
            Card c;
            if (this.cardNameCNToCardList.TryGetValue(cardname, out c))
                return c;
            return this.unknownCard;
        }

        // 根据id获取卡
        public Card getCardDataFromID(cardIDEnum id)
        {
            Card c;
            if (this.cardidToCardList.TryGetValue(id, out c))
                return c;
            return this.unknownCard;
        }

        // 根据dbfID获取卡
        public Card getCardDataFromDbfID(String dbfID)
        {
            Card c;
            if (this.carddbfidToCardList.TryGetValue(dbfID, out c))
                return c;
            return this.unknownCard;
        }

        private void setAdditionalData()
        {
            PenalityManager pen = PenalityManager.Instance;

            foreach (Card c in this.cardlist)
            {
                if (c.cardIDenum == cardIDEnum.None)
                    continue;                             //Todo: 为了确保Test能跑通

                if (pen.cardDrawBattleCryDatabase.ContainsKey(c.nameEN))
                {
                    c.isCarddraw = pen.cardDrawBattleCryDatabase[c.nameEN];
                }

                if (pen.DamageTargetSpecialDatabase.ContainsKey(c.nameEN))
                {
                    c.damagesTargetWithSpecial = true;
                }

                if (pen.DamageTargetDatabase.ContainsKey(c.nameEN))
                {
                    c.damagesTarget = true;
                }

                if (pen.priorityTargets.ContainsKey(c.nameEN))
                {
                    c.targetPriority = pen.priorityTargets[c.nameEN];
                }

                if (pen.specialMinions.ContainsKey(c.nameEN))
                {
                    c.isSpecialMinion = true;
                }

                c.trigers = new List<cardtrigers>();
                Type trigerType = c.sim_card.GetType();
                foreach (string trigerName in Enum.GetNames(typeof(cardtrigers)))
                {
                    try
                    {
                        foreach (var m in trigerType.GetMethods().Where(e => e.Name.Equals(trigerName, StringComparison.Ordinal)))
                        {
                            if (m.DeclaringType == trigerType)
                                c.trigers.Add((cardtrigers)Enum.Parse(typeof(cardtrigers), trigerName));
                        }
                    }
                    catch
                    {
                    }
                }
                if (c.trigers.Count > 10) c.trigers.Clear();
            }
        }

    }

}