using System.Collections.Generic;
using System;

namespace HREngine.Bots
{
    public partial class Behavior标准T7猎 : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;

        public override string BehaviorName() { return "标准T7猎"; }
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
            if (card.Secret)
            {
                pen -= 3;
                if (p.ownWeapon.Durability > 0 && p.ownWeapon.card.nameCN == CardDB.cardNameCN.鹰角弓)
                {
                    pen -= 5;
                }
            }

            bool waitForKolkar = false;
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.nameCN == CardDB.cardNameCN.科卡尔驯犬者) waitForKolkar = true;
            }
            foreach (Minion m in p.ownMinions)
            {
                if (m.handcard.card.nameCN == CardDB.cardNameCN.科卡尔驯犬者) waitForKolkar = false;
            }
            if (waitForKolkar && card.type == CardDB.cardtype.SPELL)
            {
                pen += bonus_enemy * 3;
                // 后手等下回合
                //if(p.ownMaxMana == 1 && p.turnCounter % 2 == 0) pen += bonus_enemy * 2; 
            }
            switch (card.nameCN)
            {
                case CardDB.cardNameCN.异教低阶牧师:
                    pen -= bonus_mine;
                    if (p.ownMinions.Count > 2) pen -= bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.狂踏的犀牛:
                    pen -= bonus_mine * 2;
                    if(p.enemyMinions.Count > 0)
                    {
                        pen -= bonus_mine * 4;
                    }
                    break;
                case CardDB.cardNameCN.引月长弓:
                case CardDB.cardNameCN.击伤猎物:
                case CardDB.cardNameCN.瞄准射击:
                    break;
                case CardDB.cardNameCN.穿刺射击:
                    if (target.own)
                    {
                        pen += bonus_mine * 2;
                    }else
                    {
                        pen -= bonus_mine * 4;
                        if (target.Hp < 6 && target.lifesteal) pen -= bonus_enemy * target.Angr;
                    }
                    break;
                case CardDB.cardNameCN.快速射击:
                    if(p.owncards.Count <= 3)
                    {
                        pen += bonus_mine * 4;
                    }
                    break;
                case CardDB.cardNameCN.新生刺头:
                    foreach(Handmanager.Handcard hc in p.owncards)
                    {
                        if(hc.card.type == CardDB.cardtype.SPELL && hc.manacost < 3)
                        {
                            pen -= bonus_mine * 2;
                        }
                    }
                    //pen -= bonus_mine;
                    break;
                case CardDB.cardNameCN.战歌驯兽师:
                    break;
                case CardDB.cardNameCN.鹿角小飞兔:
                    pen += bonus_mine;
                    break;
                case CardDB.cardNameCN.科卡尔驯犬者:
                    pen += bonus_mine * 3;
                    if (p.enemyMinions.Count == 0) pen -= bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.狼骑兵:
                    pen -= 3;
                    break;
                case CardDB.cardNameCN.杀戮命令:
                    if (target != null && !target.isHero && target.Hp < 3) pen += 20;
                    break;
                case CardDB.cardNameCN.曼科里克:
                    pen -= bonus_mine * 2;
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.nameCN == CardDB.cardNameCN.巴拉克科多班恩)
                        {
                            pen -= bonus_enemy * 3;
                        }
                    }
                    break;
                case CardDB.cardNameCN.幸运币:
                    pen += bonus_mine + 1;
                    break;
                case CardDB.cardNameCN.麻风侏儒:
                    //pen -= 5;
                    break;
                case CardDB.cardNameCN.铁喙猫头鹰:
                    //if(target != null && target.handcard.card.chnName == CardDB.cardNameCN.末日预言者 && !target.silenced )
                    //{
                    //    pen -= 100;
                    //}
                    break;
                case CardDB.cardNameCN.森林狼:
                    //pen += 5;
                    //foreach(Minion m in p.ownMinions)
                    //{
                    //    if(m.handcard.card.race == CardDB.Race.BEAST)
                    //    {
                    //        pen -= 3;
                    //    }
                    //}
                    break;
                case CardDB.cardNameCN.动物伙伴:
                    //pen -= 21;
                    break;
                case CardDB.cardNameCN.爆炸陷阱:
                    if (p.enemyHero.Hp <= 2 && p.enemySecretCount > 0 && p.enemyHeroStartClass == TAG_CLASS.MAGE)
                    {
                        pen -= 100;
                        int totalAngr = 0;
                        foreach (Minion m in p.enemyMinions)
                        {
                            totalAngr += m.Angr;
                        }
                        if (p.ownHero.Hp + 2 <= totalAngr)
                        {
                            pen -= 40000;
                        }
                    }
                    if (p.enemyHero.Hp <= 2)
                    {
                        pen -= 40;
                    }
                    //foreach (Minion m in p.enemyMinions)
                    //{
                    //    if (m.Hp <= 2 && !m.divineshild) pen -= getEnemyMinionValue(m, p);
                    //    else pen -= bonus_mine;
                    //}
                    break;
                case CardDB.cardNameCN.飞刀杂耍者:
                    if (p.ownMaxMana < 3 && p.enemyMinions.Count == 0) pen -= 20;
                    // 白送
                    //else if (p.ownMinions.Count == 0 && p.enemyMinions.Count > 0 && p.mana == 2) pen += 10;
                    break;
                case CardDB.cardNameCN.鹰角弓:
                    if (p.ownWeapon.Durability > 0) pen += 10;
                    else pen -= 5;
                    break;
                case CardDB.cardNameCN.照明弹:
                    if (p.enemySecretCount > 0) pen -= 10;
                    break;
                case CardDB.cardNameCN.误导:
                    if (p.enemyMinions.Count > 0) pen -= 3;
                    if (p.enemyMinions.Count > 0 && p.enemyMinions[0].Angr > 4) pen -= 6;
                    break;
                case CardDB.cardNameCN.关门放狗:
                    pen += 11;
                    pen -= p.enemyMinions.Count * 3;
                    break;
                case CardDB.cardNameCN.叫嚣的中士:
                    if (target == null || !target.Ready) pen += 1;
                    break;
                case CardDB.cardNameCN.火车王里诺艾:
                    pen += 60;
                    if (p.mana >= 7)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.关门放狗)
                            {
                                pen -= 60;
                                break;
                            }
                        }
                    }
                    break;
                case CardDB.cardNameCN.奥术傀儡:
                    pen += 8;
                    break;
                case CardDB.cardNameCN.饥饿的秃鹫:
                    pen += bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.猎人印记:
                    pen += bonus_enemy * 4;
                    break;
                //-----------------------------英雄技能-----------------------------------
                case CardDB.cardNameCN.稳固射击:
                    //if(p.ownMaxMana < 5)
                    //foreach(Handmanager.Handcard hc in p.owncards)
                    //{
                    //    if(hc.card.type == CardDB.cardtype.MOB && hc.card.chnName == CardDB.cardNameCN.淡水鳄)
                    //    {
                    //        pen += 100;
                    //        break;
                    //    }
                    //}
                    //pen -= 5;
                    pen -= bonus_mine;
                    if (p.ownMaxMana == 1) pen += bonus_mine * 6;
                    if (p.owncards.Count <= 3)
                    {
                        pen -= bonus_mine * 3 / 2;
                    }
                    if (p.enemyHero.Hp <= 2)
                    {
                        pen -= 30000;
                    }
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
            int hpboarder = 7;
            // 不考虑法强了
            if (p.enemyHeroName == HeroEnum.mage) retval += 2 * p.enemyspellpower;
            // 抢脸血线
            int aggroboarder = 24;
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
                    case CardDB.cardNameCN.饥饿的秃鹫:
                    case CardDB.cardNameCN.追踪术:
                    case CardDB.cardNameCN.照明弹:
                        retval -= 3 * i;
                        break;
                    case CardDB.cardNameCN.稳固射击:
                        retval += i ;
                        break;
                    case CardDB.cardNameCN.恶魔伙伴:
                    case CardDB.cardNameCN.动物伙伴:
                        retval -= i * 2 ;
                        break;
                    case CardDB.cardNameCN.阿古斯防御者:
                        retval += i;
                        break;
                    case CardDB.cardNameCN.关门放狗:
                        foreach(Minion m in p.ownMinions)
                        {
                            if(m.handcard.card.nameCN == CardDB.cardNameCN.饥饿的秃鹫)
                            {
                                retval += 10 - 3 * i;
                            }
                        }
                        break;
                }
            }
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;

            // 鹰角弓
            if (p.ownSecretsIDList.Count > 0 || p.enemySecretCount > 0 && p.ownWeapon.card.nameCN == CardDB.cardNameCN.鹰角弓)
            {
                retval += 20;
            }
            //p.value = retval;
            return retval;
        }

        // 发现卡的价值
        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            int offset = p.ownAbilityReady ? 2 : 0;
            switch (card.nameCN)
            {
                case CardDB.cardNameCN.蠕动的恐魔:
                    if (p.ownMinions.Count > 1) return bonus_mine * 3;
                    return bonus_mine * 1;
                case CardDB.cardNameCN.奥格拉曼科里克的妻子:
                    if (p.enemyHero.Hp <= 3 + offset) return 300;
                    return bonus_mine * 5;
                case CardDB.cardNameCN.小刀商贩:
                    if (p.enemyHero.Hp <= 4 + offset) return 100;
                    return bonus_mine * 1;
                case CardDB.cardNameCN.巴拉克科多班恩:
                    return bonus_mine * 3;
                case CardDB.cardNameCN.瑞林的步枪:
                    if (p.enemyHero.Hp <= 2 + offset) return 100;
                    return bonus_mine * 2;
                case CardDB.cardNameCN.快速射击:
                    if (p.enemyHero.Hp <= 3 + offset) return 110;
                    return bonus_mine ;
                case CardDB.cardNameCN.奥术射击:
                    if (p.enemyHero.Hp <= 2 + offset) return 120;
                    return bonus_mine;
                case CardDB.cardNameCN.鹿角小飞兔:
                    foreach(Minion m in p.ownMinions)
                    {
                        if(m.handcard.card.nameCN == CardDB.cardNameCN.鹿角小飞兔)
                        {
                            return bonus_mine * 4;
                        }
                    }
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.nameCN == CardDB.cardNameCN.鹿角小飞兔)
                        {
                            return bonus_mine * 4;
                        }
                    }
                    if (p.mana >= 1) return bonus_mine * 4;
                    return bonus_mine * 1;
                case CardDB.cardNameCN.异教低阶牧师:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE ) return bonus_mine * 4;
                    return bonus_mine * 2;
                case CardDB.cardNameCN.被禁锢的魔喉:
                    return bonus_mine * 2;
                case CardDB.cardNameCN.战歌驯兽师:
                    return bonus_mine * 3;
                case CardDB.cardNameCN.曼科里克:
                    return bonus_mine * 2;
                case CardDB.cardNameCN.狂踏的犀牛:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE && p.enemyMinions.Count == 0) return 0;
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.handcard.card.nameCN == CardDB.cardNameCN.狂踏的犀牛)
                        {
                            return bonus_mine * 5;
                        }
                    }
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.nameCN == CardDB.cardNameCN.狂踏的犀牛)
                        {
                            return bonus_mine * 5;
                        }
                    }
                    if (p.enemyHero.Hp <= 3 + offset) return 60;
                    return bonus_mine * 2;
                case CardDB.cardNameCN.穿刺射击:
                    if (p.enemyHero.Hp <= 3 + offset) return 70;
                    return bonus_mine * 2;
                case CardDB.cardNameCN.击伤猎物:
                    if (p.enemyHero.Hp <= 1 + offset) return 140;
                    return bonus_mine ;
                case CardDB.cardNameCN.恶魔伙伴:
                    if (p.enemyHero.Hp <= 2 + offset) return 80;
                    return bonus_mine;
                case CardDB.cardNameCN.科卡尔驯犬者:
                    return bonus_mine * (p.owncards.Count - 2);
                case CardDB.cardNameCN.狼骑兵:
                    if (p.enemyHero.Hp <= 3 + offset) return 100;
                    return 20;
                case CardDB.cardNameCN.杀戮命令:
                    if (p.enemyHero.Hp <= 3 + offset) return 100;
                    if (p.enemyHero.Hp <= 5 + offset)
                        foreach (Minion m  in p.ownMinions)
                        {
                            if (m.handcard.card.race == CardDB.Race.BEAST) return 100;
                        }
                    return 20;
                case CardDB.cardNameCN.鹰角弓:
                    if (p.enemyHero.Hp <= 3 + offset && p.ownWeapon.Angr > 0 && p.ownHero.Ready) return 100;
                    return 20;
                case CardDB.cardNameCN.火车王里诺艾:
                    if (p.enemyHero.Hp <= 6 + offset) return 100;
                    return 30;
                case CardDB.cardNameCN.麻风侏儒:
                case CardDB.cardNameCN.飞刀杂耍者:
                case CardDB.cardNameCN.铁喙猫头鹰:
                case CardDB.cardNameCN.森林狼:
                    return 10;
                case CardDB.cardNameCN.奥术傀儡:
                    if (p.enemyHero.Hp <= 4 + offset) return 100;
                    return 40;
                case CardDB.cardNameCN.动物伙伴:
                    if (p.enemyHero.Hp <= 4 + offset) return 50;
                    return 40;
                case CardDB.cardNameCN.关门放狗:
                case CardDB.cardNameCN.爆炸陷阱:
                    return 21 + p.enemyMinions.Count * 10;
                case CardDB.cardNameCN.照明弹:
                case CardDB.cardNameCN.误导:
                case CardDB.cardNameCN.叫嚣的中士:
                case CardDB.cardNameCN.猎人印记:
                    return 1;


                case CardDB.cardNameCN.冰冻陷阱:
                    if(p.enemyMinions.Count == 1)
                    {
                        if (p.enemyMinions[0].Hp > 4) return 30;
                    }
                    return 7;
                case CardDB.cardNameCN.毒蛇陷阱:
                    if (p.ownMinions.Count > 0 && p.enemyMinions.Count > 0) return 18;
                    return 7;
                case CardDB.cardNameCN.集群战术:
                    if (p.ownMinions.Count > 0 && p.enemyMinions.Count > 0) return 19;
                    return 8;
                case CardDB.cardNameCN.打开兽笼:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE && p.enemyMinions.Count == 0) return 20;
                    if (p.ownMinions.Count > 3) return 20;
                    return 5;

            }
            return 0;
        }

        // 敌方随从价值 主要等于（HP + Angr） * 4  + 5
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
            int retval = 1;
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
                    retval += bonus * 4;
                    break;
                case CardDB.cardNameCN.虚触侍从:
                    // 谢谢啊
                    retval -= bonus * 4;
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
                    if (m.Spellburst) retval += bonus * 1;
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
                    if (p.enemyHeroAblility.card.nameCN == CardDB.cardNameCN.白银之手)
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

        // 我方随从价值，大致等于主要等于 （HP + Angr） * 4  + 5
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
            int retval = 1;
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
                case CardDB.cardNameCN.科卡尔驯犬者:
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
            bool canAttackHero = false;
            if (p.anzEnemyTaunt == 0 && !p.enemyHero.untouchable)
            {
                canAttackHero = true;
            }
            int offsetMana = 0;
            if (p.anzEnemyTaunt == 1)
            {
                int tauntHp = 0;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.taunt) tauntHp += m.Hp;
                }
                foreach (Minion m in p.ownMinions)
                {
                    if (m.Ready && m.playedThisTurn && m.handcard.card.Rush) tauntHp -= m.Angr;
                }
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.Rush && tauntHp > 0 && p.mana + offsetMana >= hc.manacost)
                    {
                        offsetMana -= hc.manacost;
                        tauntHp -= hc.addattack + hc.card.Attack;
                    }
                }
                if (tauntHp <= 0) canAttackHero = true;
            }
            int offset_enemy = 0;
            if (canAttackHero && p.mana + offsetMana >= 0)
            {
                foreach (Minion m in p.ownMinions)
                {
                    if (m.Ready && !m.cantAttackHeroes && !m.frozen && !(m.handcard.card.Rush && m.playedThisTurn))
                    {
                        offset_enemy -= m.Angr;
                    }
                }
                offset_enemy -= p.calDirectDmg(p.mana + offsetMana, true);
            }

            int lifesteal = 0;
            foreach (Minion m in p.enemyMinions)
            {
                if (m.lifesteal)
                {
                    lifesteal += m.Angr;
                }
            }

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - hpboarder);
            }
            // 快死了
            else
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
            }
            if (p.ownHero.Hp + p.ownHero.armor < 10 && p.ownHero.Hp + p.ownHero.armor > 0)
            {
                retval -= 200 / (p.ownHero.Hp + p.ownHero.armor);
            }
            // 对手血线安全
            if (p.enemyHero.Hp + p.enemyHero.armor + lifesteal >= aggroboarder)
            {
                retval += 2 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - lifesteal);
            }
            // 开始打脸
            else
            {
                retval += 7 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - lifesteal);
            }
            // 进入斩杀线 / 拖到后期，不考虑随从仅打脸
            //if (p.enemyHero.Hp + p.enemyHero.armor + lifesteal <= 5 && p.enemyHero.Hp + p.enemyHero.armor + lifesteal > 0)
            //{
            //    retval += 1000 / (p.enemyHero.Hp + p.enemyHero.armor + lifesteal);
            //}

            // 预计完成斩杀
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy <= 0)
            {
                retval += 2000;
            }

            // 下回合斩杀
            if(p.calDirectDmg(p.ownMaxMana + 1 > 10 ? 10 : p.ownMaxMana + 1, false) >= lifesteal + p.enemyHero.Hp)
            {
                retval += 1000;
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