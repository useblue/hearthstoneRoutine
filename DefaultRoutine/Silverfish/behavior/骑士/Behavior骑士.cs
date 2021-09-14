using System.Collections.Generic;

namespace HREngine.Bots
{
    public partial class Behavior骑士 : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;

        public override string BehaviorName() { return "骑士"; }


        // 发现卡的价值
        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            switch (card.nameCN)
            {
                case CardDB.cardNameCN.定罪等级1: 
                    if(p.ownMinions.Count > 2)
                        return 100;
                    return 10;
                case CardDB.cardNameCN.威能祝福:
                case CardDB.cardNameCN.正义圣契:
                case CardDB.cardNameCN.阿达尔之手:
                case CardDB.cardNameCN.王者祝福:
                case CardDB.cardNameCN.智慧圣契:
                case CardDB.cardNameCN.新生入学:
                    return 5;
                case CardDB.cardNameCN.奉献:
                    return 3;
                case CardDB.cardNameCN.流光之赐:
                case CardDB.cardNameCN.荣誉护盾:
                case CardDB.cardNameCN.开赛集结:
                case CardDB.cardNameCN.路障:
                case CardDB.cardNameCN.游园日:
                case CardDB.cardNameCN.惩黑除恶:
                case CardDB.cardNameCN.动员布道:
                    return 2;
                case CardDB.cardNameCN.零食大冲关:
                    return 1;

            }
            return 0;
        }

        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中
            if (target != null && target.untouchable)
            {
                return 100000;
            }
            // 初始惩罚值
            int pen = 0;
            // 腐蚀刀
            foreach(Handmanager.Handcard hc in p.owncards)
            {
                if(hc.card.Corrupt && card.cost > hc.manacost)
                {
                    pen -= 10;
                    break;
                }
            }
            int bonus = 4;
            switch (card.nameCN)
            {
                case CardDB.cardNameCN.神恩术:
                    break;
                case CardDB.cardNameCN.阿达尔之手:
                    if (!target.Ready)
                    {
                        pen += 10;
                    }
                    break;
                case CardDB.cardNameCN.王者祝福:
                    if (!target.Ready)
                    {
                        pen += 10;
                    }
                    if (!target.silenced && target.handcard.card.nameCN == CardDB.cardNameCN.前沿哨所)
                        pen += 1000;
                    break;
                case CardDB.cardNameCN.飞刀杂耍者:
                    pen += 5;
                    break;
                case CardDB.cardNameCN.开赛集结:
                case CardDB.cardNameCN.战斗号角:
                    break;
                case CardDB.cardNameCN.救赎:
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                        {
                            pen -= bonus_mine * 2;
                            if(p.ownMinions.Count == 0) pen -= bonus_mine * 2;
                            break;
                        }
                    }
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.handcard.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                        {
                            pen -= bonus_mine * 4;
                            break;
                        }
                    }
                    pen -= 1;
                    break;
                case CardDB.cardNameCN.迅疾救兵:
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                        {
                            pen -= bonus_mine * 2;
                            break;
                        }
                    }
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.handcard.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                        {
                            pen -= bonus_mine * 2;
                            break;
                        }
                    }
                    pen -= 1;
                    break;
                case CardDB.cardNameCN.战场军官:
                    int canAttackMinion = 0;
                    foreach(Minion m in p.ownMinions)
                    {
                        if (m.Ready) canAttackMinion++;
                    }
                    if (canAttackMinion == 0) pen += bonus_mine * 4;
                    else pen -= bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.联盟旗手:
                    break;
                case CardDB.cardNameCN.圣礼骑士:
                    pen -= bonus_mine * 1;
                    if(p.ownMaxMana == 1) pen -= bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.古墓卫士:
                    pen += bonus_mine * 3;
                    break;
				case CardDB.cardNameCN.棱彩珠宝工具:
                    pen -= bonus_mine * 2;
					if (p.ownMaxMana == 1) pen -= bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.幸运币:
                    pen += bonus * 3;
                    break;
                //-----------------------------超模补正-----------------------------------
                case CardDB.cardNameCN.蛛魔之卵:
                    pen -= bonus ;
                    break;
                case CardDB.cardNameCN.活化扫帚:
                    pen += bonus_mine * 8;
                    break;
                case CardDB.cardNameCN.审判圣契:
                    if (!card.Corrupt && p.ownHero.Hp < 15) pen -= 20;
                    if (!p.ownHero.Ready)
                        pen += 20;
                    break;
                case CardDB.cardNameCN.剧毒魔蝎:
                    pen -= bonus * 3;
                    break;
                case CardDB.cardNameCN.泰兰弗丁:
                    pen -= bonus * 3;
                    break;
                case CardDB.cardNameCN.剑圣萨穆罗:
                    pen += bonus * 7;
                    break;
                case CardDB.cardNameCN.提里奥弗丁:
                    pen -= bonus * 5;
                    break;
                // 异能价值 2 属性
                case CardDB.cardNameCN.苦痛侍僧:
                case CardDB.cardNameCN.战利品贮藏者:
                case CardDB.cardNameCN.麦田傀儡:
                    pen -= bonus * 2;
                    break;
                case CardDB.cardNameCN.莫戈尔莫戈尔格:
                case CardDB.cardNameCN.曼科里克:
                case CardDB.cardNameCN.灵魂之匣:
                case CardDB.cardNameCN.凯恩血蹄:
                    pen -= bonus;
                    break;
                case CardDB.cardNameCN.希望圣契:
                    pen -= bonus * 3;
                    break;
                //-----------------------------武器-----------------------------------
                case CardDB.cardNameCN.逝者之剑:
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.handcard.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                        {
                            pen -= bonus_mine * 2;
                            break;
                        }
                    }
                    break;
                case CardDB.cardNameCN.正义圣契:
                    pen += 20;
                    break;
                //-----------------------------针对卡-----------------------------------
                case CardDB.cardNameCN.异教低阶牧师:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.PRIEST) pen -= 20;
                    break;
                case CardDB.cardNameCN.食人魔巫术师:
                    if (p.enemyMinions.Count == 0 && p.ownMinions.Count < 4)
                        pen -= 9;
                    pen -= 30;
                    break;
                case CardDB.cardNameCN.古神在上:
                    if (p.ownMaxMana <= 3) pen += 4;
                    // 先手第一回合，脏对面任务
                    if( (p.enemyHeroStartClass == TAG_CLASS.WARLOCK 
                        || p.enemyHeroStartClass == TAG_CLASS.MAGE 
                        || p.enemyHeroStartClass == TAG_CLASS.WARRIOR) && p.enemyMaxMana == 0)
                    {
                        pen -= 1000;
                    }
                    pen -= 3;
                    break;
                //-----------------------------配合-----------------------------------
                
                case CardDB.cardNameCN.夺日者间谍:
                    pen += p.ownSecretsIDList.Count > 0 ? -bonus * 2 : bonus;
                    break;
                case CardDB.cardNameCN.银色自大狂:
                    pen += 4 * bonus;
                    break;
                
                case CardDB.cardNameCN.防护长袍:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                        return -100;
                    return 0;
                case CardDB.cardNameCN.火炮长斯密瑟:
                    pen -= 20 * p.ownSecretsIDList.Count;
                    break;
                case CardDB.cardNameCN.钉棍终结者:
                    pen += p.ownSecretsIDList.Count > 0 && p.enemyMinions.Count > 0 ? -20 : 30;
                    break;
                case CardDB.cardNameCN.北卫军指挥官:
                    pen += p.ownSecretsIDList.Count > 0 ? -20 : 1;
                    break;
                case CardDB.cardNameCN.复仇:
                    return -1;
                //-----------------------------英雄技能-----------------------------------
                case CardDB.cardNameCN.援军:
                    // 贪救赎
                    if (p.ownSecretsIDList.Contains(CardDB.cardIDEnum.EX1_136) && p.ownMinions.Count == 0)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if(hc.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴)
                            {
                                return 1000;
                            }
                        }
                    }                    
                    return 9;
            }
            if (card.type == CardDB.cardtype.SPELL)
            {
                pen -= 2;
            }
            return pen;
        }

        private static int costVal = 10;
        // 抽牌价值
        private static int drawCardVal = 10;
        
        PenalityManager penman = PenalityManager.Instance;

        // 核心，场面值

        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -20000) return p.value;
            float retval = 0;
            retval += getGeneralVal(p);
            // 圣契奖励
            retval += p.libram > 2 ? 60 + p.libram * 10 : p.libram * 30;
            // 危险血线
            int hpboarder = 3;
            // 抢脸血线
            int aggroboarder = 12;
            retval += getHpValue(p, hpboarder, aggroboarder);
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            // 出牌序列数量
            int count = p.playactions.Count;
            int ownActCount = 0;
            // 排序问题！！！！
            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i];
                ownActCount++;
                switch (a.actionType)
                {
                    // 英雄攻击
                    case actionEnum.attackWithHero:
                    case actionEnum.attackWithMinion:
                        if(a.target != null && a.target.handcard.card.nameCN == CardDB.cardNameCN.前沿哨所)
                        {
                            retval -= i * 5;
                        }
                        if (a.own.handcard.card.nameCN == CardDB.cardNameCN.伦萨克大王)
                        {
                            retval -= i * 2;
                        }
                        continue;
                    case actionEnum.useHeroPower:
                        continue;
                    case actionEnum.playcard:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    // 排序优先
                    case CardDB.cardNameCN.神恩术:
                    case CardDB.cardNameCN.圣礼骑士:
                    case CardDB.cardNameCN.阿达尔之手:
                    case CardDB.cardNameCN.新生入学:
                        retval -= i * 2;
                        break;
                    case CardDB.cardNameCN.恩佐斯深渊之神:
                        retval += i;
                        break;
                }
            }
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                // 如果手上有侏儒就很赚
                if (hc.card.nameCN == CardDB.cardNameCN.甩笔侏儒)
                {
                    retval += 10;
                }
                if(p.ownMaxMana == 1 && hc.card.nameCN == CardDB.cardNameCN.幸运币)
                {
                    foreach (Handmanager.Handcard hcc in p.owncards)
                    {
                        if (hcc.card.nameCN == CardDB.cardNameCN.十字路口大嘴巴 && p.ownSecretsIDList.Count > 0)
                        {
                            retval += 100;
                            break;
                        }
                    }
                }
            }
            retval += getSecretPenality(p); // 奥秘的影响
            //p.value = retval;
            return retval;
        }

        // 敌方随从价值 主要等于（HP + Angr） * 4  + 5
        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者 && !mm.silenced)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -10;
            }
            if (m.Hp <= 0) return 0;
            int retval = 1;
            if(m.Angr> 0 || p.enemyHeroStartClass == TAG_CLASS.PALADIN || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * 4;
            // 法强相当于 1.5 点属性
            retval += m.spellpower * 6;
            if (!m.frozen && (!m.cantAttack || m.handcard.card.nameCN == CardDB.cardNameCN.邪刃豹) )
            {
                retval += m.Angr * 4;
                if (m.windfury) retval += m.Angr * 2;
            }
            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;

            // 剧毒价值两点属性
            if (m.poisonous)
            {
                retval += 8;
            }
            if (m.lifesteal) retval += m.Angr * 2;

            int bonus = 4;
            switch (m.handcard.card.nameCN){
                case CardDB.cardNameCN.血骨傀儡:
                    return 100;
                // 异能价值每多 1 属性值加权 4 点
                // 异能价值 4 属性值
                case CardDB.cardNameCN.原野联络人:
                case CardDB.cardNameCN.奥:
                case CardDB.cardNameCN.奥的灰烬:
                case CardDB.cardNameCN.科卡尔驯犬者:
                case CardDB.cardNameCN.巡游领队:
                case CardDB.cardNameCN.团伙核心:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.莫尔杉哨所:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.导师火心:
                case CardDB.cardNameCN.伊纳拉碎雷:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.洛卡拉:
                case CardDB.cardNameCN.虚触侍从:
                    retval += bonus * 4;
                    break;
                // 异能价值 2 属性值
                case CardDB.cardNameCN.萨特监工:
                case CardDB.cardNameCN.食人魔巫术师:
                case CardDB.cardNameCN.甩笔侏儒:
                case CardDB.cardNameCN.精英牛头人酋长金属之神:
                case CardDB.cardNameCN.克欧雷:
                case CardDB.cardNameCN.雷欧克:
                case CardDB.cardNameCN.纳兹曼尼织血者:
                case CardDB.cardNameCN.塞泰克织巢者:
                    retval += bonus * 3;
                    break;
                // 异能价值 2 属性值
                case CardDB.cardNameCN.新生刺头:
                    if(m.Spellburst) retval += bonus * 1;
                    break;
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.黑眼:
                    retval += bonus_enemy * 20;
                    break;
                // 解不掉游戏结束
                case CardDB.cardNameCN.洛萨:
                case CardDB.cardNameCN.考内留斯罗姆:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.大领主弗塔根:
                case CardDB.cardNameCN.圣殿蜡烛商:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.魅影歹徒:
                case CardDB.cardNameCN.灵魂窃贼:
                case CardDB.cardNameCN.紫罗兰法师:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.狂欢报幕员:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.火舌图腾:
                    retval += bonus_enemy * 8;
                    break;
                case CardDB.cardNameCN.末日预言者:
                    //foreach(Minion mm in p.ownMinions)
                    //{
                    //    retval += (mm.Hp + mm.Angr) * 5;
                    //}
                    break;
                // 不解巨大劣势
                case CardDB.cardNameCN.拍卖师亚克森:
                case CardDB.cardNameCN.法师猎手:
                case CardDB.cardNameCN.凯瑞尔罗姆:
                case CardDB.cardNameCN.鱼人领军:
                case CardDB.cardNameCN.南海船长:
                case CardDB.cardNameCN.坎雷萨德埃伯洛克:
                case CardDB.cardNameCN.人偶大师多里安:
                case CardDB.cardNameCN.暗鳞先知:
                case CardDB.cardNameCN.灭龙弩炮:
                case CardDB.cardNameCN.神秘女猎手:
                case CardDB.cardNameCN.鲨鳍后援:
                case CardDB.cardNameCN.怪盗图腾:
                case CardDB.cardNameCN.矮人神射手:
                case CardDB.cardNameCN.任务达人:
                case CardDB.cardNameCN.贪婪的书虫:
                case CardDB.cardNameCN.战马训练师:
                case CardDB.cardNameCN.相位追猎者:
                case CardDB.cardNameCN.鱼人宝宝车队:
                case CardDB.cardNameCN.科多兽骑手:
                case CardDB.cardNameCN.奥秘守护者:
                case CardDB.cardNameCN.获救的流民:
                case CardDB.cardNameCN.低阶侍从:
                    retval += bonus_enemy * 3;
                    break;
                case CardDB.cardNameCN.白银之手新兵:
                    if(p.enemyHeroAblility.card.nameCN == CardDB.cardNameCN.白银之手)
                        retval += bonus_enemy * 3;
                    break;
                // 算有点用
                case CardDB.cardNameCN.幽灵狼前锋:
                case CardDB.cardNameCN.战斗邪犬:
                case CardDB.cardNameCN.饥饿的秃鹫:
                case CardDB.cardNameCN.法力浮龙:
                case CardDB.cardNameCN.加基森拍卖师:
                case CardDB.cardNameCN.飞刀杂耍者:
                case CardDB.cardNameCN.锈水海盗:
                case CardDB.cardNameCN.大法师安东尼达斯:
                    retval += bonus_enemy * 2;
                    break;
                case CardDB.cardNameCN.疯狂的科学家:
                    retval -= bonus_enemy * 4;
                    break;

                case CardDB.cardNameCN.月牙:
                //case "明月之牙":
                    retval += bonus_enemy * m.Hp;
                    break;
            }
            return retval;
        }


        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {
            
            return -1; //comment out or remove this to set manual priority
            int sirFinleyChoice = -1;
            int tmp = int.MinValue;
            for (int i = 0; i < discoverCards.Count; i++)
            {
                CardDB.cardNameEN name = discoverCards[i].card.nameEN;
                if (SirFinleyPriorityList.ContainsKey(name) && SirFinleyPriorityList[name] > tmp)
                {
                    tmp = SirFinleyPriorityList[name];
                    sirFinleyChoice = i;
                }
            }
            return sirFinleyChoice;
        }
        // 我方随从价值，大致等于主要等于 （HP + Angr） * 4  + 5
        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者 && !mm.silenced)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -10;
            }
            if (m.Hp <= 0) return 0;
            int retval = 1;
            // 军官在场优先攻击
            if(m.numAttacksThisTurn > 1) retval += m.Angr * m.numAttacksThisTurn;
            retval += m.Hp * 4;
            retval += m.Angr * 4;
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * 3;
            
            // 风怒价值
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            // 圣盾价值
            if (m.divineshild) retval += m.Angr * 3;
            // 潜行价值
            if (m.stealth) retval += m.Angr / 2 + 1;
            // 吸血
            if (m.lifesteal) retval += m.Angr / 2 + 1;
            // 圣盾嘲讽
            if (m.divineshild && m.taunt) retval += 4;

            int bonus = 4;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.考内留斯罗姆:
                    if(p.owncards.Count < 6)
                    {
                        retval += bonus_mine * 10;
                    }
                    break;
                case CardDB.cardNameCN.蛛魔之卵:
                    if (m.Angr > 0)
                        retval += bonus * 3;
                    break;
                // 异能价值 4 属性
                case CardDB.cardNameCN.炎魔之王拉格纳罗斯:
                case CardDB.cardNameCN.伊瑟拉:
                    retval += bonus * 4;
                    break;
                case CardDB.cardNameCN.尼鲁巴蛛网领主:
                    if ( (p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.WARLOCK || p.enemyHeroStartClass == TAG_CLASS.WARRIOR)) retval += bonus_mine * 3;
                    break;
                case CardDB.cardNameCN.月牙:
                //case CardDB.cardNameCN.明月之牙:
                    retval += bonus_mine * m.Hp * 3/ 2;
                    break;
                case CardDB.cardNameCN.大领主弗塔根:
                    retval += bonus_mine * 8;
                    break;
                case CardDB.cardNameCN.战场军官:
                    if(p.ownMinions.Count > 2)
                    {
                        retval += bonus_mine * 3;
                    }
                    break;
                case CardDB.cardNameCN.前沿哨所:
                    retval = bonus_mine * 6;
                    if (p.ownMaxMana <= 2) retval = bonus_mine * 10;
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.DEMONHUNTER || p.enemyHeroStartClass == TAG_CLASS.WARLOCK)
                        retval = bonus_mine * 10;
                    retval -= bonus_mine * (m.Angr - 2)  * 4;
                    break;
                case CardDB.cardNameCN.十字路口大嘴巴:
                    retval += 10 * p.ownSecretsIDList.Count;
                    break;
            }

            return retval;
        }

        private Dictionary<CardDB.cardNameEN, int> SirFinleyPriorityList = new Dictionary<CardDB.cardNameEN, int>
        {
            //{HeroPowerName, Priority}, where 0-9 = manual priority
            { CardDB.cardNameEN.lesserheal, 0 }, 
            { CardDB.cardNameEN.shapeshift, 6 },
            { CardDB.cardNameEN.fireblast, 7 },
            { CardDB.cardNameEN.totemiccall, 1 },
            { CardDB.cardNameEN.lifetap, 9 },
            { CardDB.cardNameEN.daggermastery, 5 },
            { CardDB.cardNameEN.reinforce, 4 },
            { CardDB.cardNameEN.armorup, 2 },
            { CardDB.cardNameEN.steadyshot, 8 }
        };
        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }


        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            int offset_mine = p.calEnemyTotalAngr() + Hrtprozis.Instance.enemyDirectDmg;

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += 5 * (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) / 2;
            }
            // 快死了
            else if (p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
            }
            else
            {
                retval -= 3 * (hpboarder + 1) * (hpboarder + 1) + 100;
            }
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine < 6 && p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 80 / (p.ownHero.Hp + p.ownHero.armor - offset_mine);
            }
            // 对手血线安全
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 开始打脸
            else
            {
                retval += 3 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 进入斩杀线
            // if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy <= 5 && p.enemyHero.Hp + p.enemyHero.armor + offset_enemy > 0)
            // {
            //     retval += 300 / (p.enemyHero.Hp + p.enemyHero.armor - offset_enemy);
            // }
            // 场攻+直伤大于对方生命，预计完成斩杀
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }
            // 下回合斩杀本回合打脸
            if (p.calDirectDmg(p.ownMaxMana + 1, false, true) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 100;
            }
            return retval;
        }


        /// <summary>
        /// 攻击触发的奥秘惩罚
        /// </summary>
        /// <param name="si"></param>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns></returns>
        public override int getSecretPen_CharIsAttacked(Playfield p, SecretItem si, Minion attacker, Minion defender)
        {
            if (attacker.isHero) return 0;
            int pen = 0;
            // 攻击的基本惩罚
            if (si.canBe_explosive && !defender.isHero)
            {
                pen -= 20;
                //pen += (attacker.Hp + attacker.Angr);
                foreach (SecretItem sii in p.enemySecretList)
                {
                    sii.canBe_explosive = false;
                }
            }
            return pen;
        }

    }
}