using System.Collections.Generic;
using System;

namespace HREngine.Bots
{
    public partial class Behavior口德 : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;

        public override string BehaviorName() { return "口德"; }
        PenalityManager penman = PenalityManager.Instance;

        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中
            if (target != null && target.untouchable)
            {
                return 100000;
            }

            // 初始惩罚值
            int pen = 0;
            pen += penman.getSilencePenality(card.nameEN, target, p);

            switch (card.nameCN)
            {
                case CardDB.cardNameCN.小型法术玉石:
                    pen += bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.真菌宝藏:
                    if(p.owncards.Count <= 5)
                       pen -= 30;
                    break;
                case CardDB.cardNameCN.激活:
                case CardDB.cardNameCN.日蚀:
                    pen += bonus_mine * 1;
                    if(p.mana == 2) pen += bonus_mine * 10;
                    break;
                case CardDB.cardNameCN.月触项链:
                    if(p.ownHero.numAttacksThisTurn > 0) pen += bonus_mine * 10;
                    if (card.cardIDenum == CardDB.cardIDEnum.DMF_730)
                        pen += bonus_mine * 3;
                    break;
                case CardDB.cardNameCN.游园迷梦:
                    pen -= 100;
                    break;
                case CardDB.cardNameCN.保护甲板:
                    pen -= 50;
                    break;
                case CardDB.cardNameCN.飞扑:
                case CardDB.cardNameCN.爪击:
                case CardDB.cardNameCN.野蛮之击:
                case CardDB.cardNameCN.野性之怒:
                case CardDB.cardNameCN.铁齿铜牙:
                case CardDB.cardNameCN.撕咬:
                case CardDB.cardNameCN.横扫:
                case CardDB.cardNameCN.橡树的召唤:
                case CardDB.cardNameCN.花园猎豹:
                case CardDB.cardNameCN.变形:
                    break;
                case CardDB.cardNameCN.铁肤古夫:
                    if (p.ownHero.numAttacksThisTurn > 0) pen += bonus_mine * 30;
                    break;
            }
            return pen;
        }

        // 核心，场面值
        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;
            float retval = 0;
            retval += getGeneralVal(p);
            retval += p.owncarddraw * 5;
            // 危险血线
            int hpboarder = 3;
            // 不考虑法强了
            if (p.enemyHeroName == HeroEnum.mage) retval += 2 * p.enemyspellpower;
            // 抢脸血线
            int aggroboarder = 30;
            retval += getHpValue(p, hpboarder, aggroboarder);
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
                        continue;
                    case actionEnum.useHeroPower:
                    case actionEnum.playcard:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    // 排序优先
                    case CardDB.cardNameCN.激活:
                    case CardDB.cardNameCN.幸运币:
                        retval -= 6 * i;
                        break;
                    case CardDB.cardNameCN.保护甲板:
                    case CardDB.cardNameCN.游园迷梦:
                        retval -= 4 * i;
                        break;
                }
            }
            // 对手基本随从交换模拟
            //retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;

            // 特殊：优势防亵渎
            if (retval > 50 && p.enemyHeroStartClass == TAG_CLASS.WARLOCK && p.enemyMinions.Count == 0 && p.ownMinions.Count > 2)
            {
                bool found = false;
                // 从 2 开始防，术士自带一堆 1
                for (int i = 1; i <= 10; i++)
                {
                    found = false;
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.Hp == i)
                        {
                            retval -= 2 * (i - 1);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        if (i == 1 ) retval += 10;
                        if (i == 2 ) retval += 10;
                        break;
                    }
                }
            }
            // 特殊处理一费留硬币等炮
            if (Hrtprozis.Instance.gTurn == 2 )
            {
                int flag = 0;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.nameCN == CardDB.cardNameCN.空中炮艇) flag |= 1;
                    if (hc.card.nameCN == CardDB.cardNameCN.幸运币) flag |= 2;
                }
                if (flag == 3) retval += 200;
            }
            //p.value = retval;
            return retval;
        }

        // 敌方随从价值 主要等于（HP + Angr） * 4 + 4
        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 4;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PALADIN || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * 4;
            retval += m.spellpower * 2;
            if (!m.frozen && (!m.cantAttack || m.handcard.card.nameCN == CardDB.cardNameCN.邪刃豹))
            {
                retval += m.Angr * 4;
                if (m.Angr > 5) retval += 10;
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
            if (m.lifesteal) retval += m.Angr * bonus_enemy * 4;

            int bonus = 4;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.无证药剂师:
                case CardDB.cardNameCN.安娜科德拉:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.圣殿蜡烛商:
                case CardDB.cardNameCN.奔逃的魔刃豹:
                case CardDB.cardNameCN.鲨鱼之灵:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.黑眼:
                case CardDB.cardNameCN.虚触侍从:
                case CardDB.cardNameCN.青蛙之灵:
                case CardDB.cardNameCN.农夫:
                case CardDB.cardNameCN.尼鲁巴蛛网领主:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.洛萨:
                case CardDB.cardNameCN.考内留斯罗姆:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.大领主弗塔根:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.魅影歹徒:
                case CardDB.cardNameCN.灵魂窃贼:
                case CardDB.cardNameCN.紫罗兰法师:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.原野联络人:
                case CardDB.cardNameCN.狂欢报幕员:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.导师火心:
                case CardDB.cardNameCN.伊纳拉碎雷:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.洛卡拉:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.团伙核心:
                case CardDB.cardNameCN.巡游领队:
                case CardDB.cardNameCN.科卡尔驯犬者:
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
                case CardDB.cardNameCN.萨特监工:
                case CardDB.cardNameCN.甩笔侏儒:
                case CardDB.cardNameCN.精英牛头人酋长金属之神:
                case CardDB.cardNameCN.莫尔杉哨所:
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
                case CardDB.cardNameCN.白银之手新兵:
                case CardDB.cardNameCN.低阶侍从:
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
                case CardDB.cardNameCN.艾尔文野猪:
                    retval += bonus_enemy * 2;
                    break;
            }
            return retval;
        }

        // 我方随从价值，大致等于主要等于 （HP + Angr） * 4 + 4
        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 4;
            if (m.Hp <= 0) return 0;
            retval += m.Hp * 4;
            retval += m.Angr * 4;
            if (m.Hp <= 1) retval -= (m.Angr - 1) * 3;
            if (m.Angr <= 1)
            {
                if (m.Hp > 3)
                {
                    retval -= (m.Hp - 3) * 4;
                }
            }
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
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                    retval += bonus_mine * 3;
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
        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
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


        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            if (p.anzEnemyTaunt == 0 && p.ownHero.Ready) offset_enemy = p.ownHero.Angr;
            int offset_mine = p.calEnemyTotalAngr() + Hrtprozis.Instance.enemyDirectDmg;

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) * 3 / 2;
            }
            // 快死了
            else if (p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 4 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
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
                retval += 2 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 开始打脸
            else
            {
                retval += 6 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
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
            //if (p.calDirectDmg(p.ownMaxMana + 1, false, true) >= p.enemyHero.Hp + p.enemyHero.armor)
            //{
            //    retval += 100;
            //}
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