using System.Collections.Generic;
using System;

namespace HREngine.Bots
{
    public partial class Behavior黑眼任务术 : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;

        public override string BehaviorName() { return "黑眼任务术"; }
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

            bool foundDefile = false;
            foreach(Handmanager.Handcard hc in p.owncards)
            {
                if(hc.card.nameCN == CardDB.cardNameCN.亵渎)
                {
                    foundDefile = true;
                }
            }

            switch (card.nameCN)
            {
                case CardDB.cardNameCN.卑劣的脏鼠:
                    if (p.mana <= 2) pen += bonus_mine * 4;
                    if(p.enemyHeroStartClass == TAG_CLASS.MAGE)
                    {
                        // 点燃法
                        if (!Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_450) && p.mana > 4) pen -= bonus_mine * 12;
                    }
                    break;
                case CardDB.cardNameCN.血肉巨人:
                    if(nowHandcard.getManaCost(p) <= 0)
                    {
                        foreach(Handmanager.Handcard hc in p.owncards)
                        {
                            if(hc.card.nameCN == CardDB.cardNameCN.活化扫帚 && p.mana > 0)
                            {
                                return -100;
                            }
                        }
                        return -50;
                    }
                    break;
                case CardDB.cardNameCN.凯雷塞斯王子:
                    pen -= 50;
                    if (Hrtprozis.Instance.gTurn <= 6) pen -= 400;
                    break;
                case CardDB.cardNameCN.熔核巨人:
                    if (nowHandcard.getManaCost(p) <= 0)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.活化扫帚 && p.mana > 0)
                            {
                                return -150;
                            }
                        }
                        return -100;
                    }
                    break;
                case CardDB.cardNameCN.力量的代价:
                    pen += 20;
                    if (p.enemySecretCount == 0 && p.anzEnemyTaunt == 0)
                    {
                        int plus = 0;
                        int total = 0;
                        bool jg = false;
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.力量的代价)
                            {
                                plus++;
                            }
                        }
                        plus = plus > p.mana ? p.mana : plus;
                        if (p.anzTamsinroame > 0) plus *= p.anzTamsinroame;

                        for (int i = 0; i < p.ownMinions.Count; i++)
                        {
                            if (p.ownMinions[i].numAttacksThisTurn > 0) return pen;
                            if (p.ownMinions[i].Ready && !p.ownMinions[i].cantAttackHeroes)
                            {
                                total += p.ownMinions[i].Angr;
                                // 判断军官
                                if (i > 0 && p.ownMinions[i - 1].handcard.card.nameCN == CardDB.cardNameCN.战场军官 && !p.ownMinions[i - 1].silenced || i < p.ownMinions.Count - 1 && p.ownMinions[i + 1].handcard.card.nameCN == CardDB.cardNameCN.战场军官 && !p.ownMinions[i + 1].silenced)
                                {
                                    total += p.ownMinions[i].Angr;
                                    if (p.ownMinions[i] == target && target.Ready)
                                    {
                                        jg = true;
                                    }
                                }
                            }


                        }
                        if (total + plus * 4 * (jg ? 2 : 1) >= p.enemyHero.Hp)
                        {
                            return -2000;
                        }
                    }
                    break;
                case CardDB.cardNameCN.符文秘银杖:
                    if ( Hrtprozis.Instance.gTurn <= 6 && p.ownMaxMana <= 3) pen -= 120;
                    pen -= bonus_mine * 10;
                    break;
                case CardDB.cardNameCN.古尔丹之手:
                case CardDB.cardNameCN.大灾变:
                    break;
                case CardDB.cardNameCN.开心的食尸鬼:
                    if (p.healTimes == 0) pen += bonus_mine * 3;
                    break;
                case CardDB.cardNameCN.洛欧塞布:
                    int nextTurnAngr = 5;

                    if (p.enemyHeroStartClass == TAG_CLASS.WARLOCK || p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.DRUID || p.enemyHeroStartClass == TAG_CLASS.ROGUE || p.enemyHeroStartClass == TAG_CLASS.HUNTER)
                    {
                        if(p.enemyMinions.Count == 0) pen -= bonus_mine * 10;
                        pen -= bonus_mine * 4;
                    }
                    foreach(Minion m in p.ownMinions){
                        nextTurnAngr += m.Angr;
                    }
                    // 下回合能斩
                    if (p.enemyHero.immune || ( p.enemyMinions.Count == 0 && p.calTotalAngr() + nextTurnAngr + p.calDirectDmg(p.ownMaxMana + 1, false) >= p.enemyHero.Hp + p.enemyHero.armor ) )
                    {
                        pen -= 1500;
                    }
                    // 超凡德
                    if(p.enemyHeroStartClass == TAG_CLASS.DRUID && p.enemyMaxMana >= 5)
                    {
                        foreach(Minion m in p.enemyMinions)
                        {
                            if(m.handcard.card.nameCN == CardDB.cardNameCN.奔逃的魔刃豹)
                            {
                                pen -= 1500;
                            }
                        }
                    }
                    if (this.getPlayfieldValue(p) > 1000) pen -= 1000;
                    else if (p.value > 200) pen -= 100;
                    if (p.ownHero.Hp <= 10 && p.enemyMinions.Count == 0) pen -= 100;
                    break;
                case CardDB.cardNameCN.战场军官:
                    // 斩杀了
                    if (p.enemySecretCount == 0 && p.anzEnemyTaunt == 0)
                    {
                        int max = 0, plus = 0;
                        int total = 0;
                        for (int i = 1; i < p.ownMinions.Count; i++)
                        {
                            if (!p.ownMinions[i].Ready) return 0;
                            total += p.ownMinions[i].Angr;
                            if (max < p.ownMinions[i - 1].Angr + p.ownMinions[i].Angr)
                            {
                                max = p.ownMinions[i - 1].Angr + p.ownMinions[i].Angr;
                            }
                        }
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.力量的代价)
                            {
                                plus++;
                            }
                        }
                        plus = plus > p.mana - 6 ? p.mana - 6 : plus;
                        if (p.anzTamsinroame > 0) plus *= p.anzTamsinroame;
                        max += p.mana + plus * 8;
                        if (max + total > p.enemyHero.Hp)
                        {
                            return -2000;
                        }
                    }
                    break;
                case CardDB.cardNameCN.火焰之灾祸:
                    pen += 50;
                    break;
                case CardDB.cardNameCN.亵渎:
                    pen += bonus_mine * 4 + 10;
                    if(p.anzEnemyTaunt == 0 && !p.enemyHero.immune)
                    {
                        foreach(Minion m in p.ownMinions)
                        {
                            if (m.Ready && !m.cantAttackHeroes)
                            {
                                return 1000;
                            }
                        }
                    }
                    foreach(Minion m in p.ownMinions)
                    {
                        if (m.playedThisTurn)
                        {
                            foreach(Minion mm in p.enemyMinions)
                            {
                                if(mm.Hp == m.Hp && !mm.divineshild)
                                {
                                    return 1000;
                                }
                            }
                        }
                    }
                    //if (p.enemyMinions.Count >= 3 && p.ownMinions.Count <= 1) pen -= 40;
                    break;
                case CardDB.cardNameCN.巡游向导:
                    if (p.ownHeroPowerCostLessOnce <= -99) pen += 1000;
                    // 如果可以直接使用英雄技能就没必要了
                    if (p.mana == 2 && p.ownAbilityReady) pen += 1000;
                    pen += 12;
                    if (p.anzDark > 0) pen -= 20;
                    break;
                case CardDB.cardNameCN.烈焰小鬼:
                    if (p.ownQuest.maxProgress == 1000 && !p.anzTamsin) pen += bonus_mine * 4;
                    pen += bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.晶化师:
                    if (p.ownQuest.maxProgress < 8 && p.ownQuest.maxProgress - p.ownQuest.questProgress > 5 && p.ownHero.armor == 0)
                    {
                        pen += bonus_mine * 4 + 15;
                    }
                    pen -= p.healOrDamageTimes * 2;
                    break;
                case CardDB.cardNameCN.拉法姆的诅咒:
                    if (p.enemyHero.Hp <= 2)
                    {
                        pen -= 5000;
                    }
                    break;
                case CardDB.cardNameCN.异教低阶牧师:
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE || p.enemyHeroStartClass == TAG_CLASS.PRIEST || p.enemyHeroStartClass == TAG_CLASS.WARLOCK
                    || p.enemyHeroStartClass == TAG_CLASS.HUNTER || p.enemyHeroStartClass == TAG_CLASS.DRUID) pen -= 20;
                    else pen += bonus_mine;
                    break;
                case CardDB.cardNameCN.亡者复生:
                    if (nowHandcard.enchs.Count == 0 && (!p.anzTamsin && p.mana == 0 && p.anzDark == 0)) pen += 1000;
                    pen += bonus_mine * 3;
                    break;
                case CardDB.cardNameCN.纳斯雷兹姆之触:
                    if (foundDefile) pen += bonus_mine * 3;
                    pen += bonus_enemy * 2;
                    if(target.Hp > 2 || target.divineshild) pen += bonus_enemy * 3;
                    // 对冲一下
                    if (target.handcard.card.nameCN == CardDB.cardNameCN.疯狂的科学家) pen -= 90;
                    break;
                case CardDB.cardNameCN.火焰之雨:
                    pen += bonus_enemy * 5;
                    // 别用这玩意触发黑眼效果，没啥用
                    if (p.anzDark > 0)
                    {
                        pen += 10;
                        if (p.owncarddraw > 0) pen += 15;
                    }
                    break;
                case CardDB.cardNameCN.死亡缠绕:
                    if (foundDefile) pen += bonus_mine * 3;
                    pen += bonus_enemy * 2;
                    break;
                case CardDB.cardNameCN.护甲商贩:
                    if (p.ownHero.Hp + p.ownHero.armor > 20 && p.enemyMinions.Count == 0 && p.ownQuest.maxProgress != 1000)
                    {
                        pen += 1000;
                    }
                    if (p.ownHero.Hp + p.ownHero.armor > 15 && p.ownQuest.maxProgress != 1000)
                    {
                        if (p.enemyMinions.Count <= 2)
                            pen += 14;
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.血肉巨人) pen += 100;
                        }
                    }
                    break;
                case CardDB.cardNameCN.枯萎化身塔姆辛:
                    //pen -= bonus_mine * 100;
                    break;
                case CardDB.cardNameCN.生命分流:
                    if (p.owncards.Count >= 9) pen += 1000;
                    if(p.ownQuest.maxProgress == 1000)
                    {
                        foreach(Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.恶魔之种) return 1000;
                        }
                    }
                    if (Hrtprozis.Instance.gTurn <= 4 && p.ownMaxMana == 2 && p.ownHeroPowerCostLessOnce == 0)
                    {
                        if (p.enemyMinions.Count == 0)
                            pen -= bonus_mine * 11;
                        else if(p.enemyMinions.Count < 3)
                            pen -= bonus_mine * 8;
                    }
                    else
                    {
                        //pen -= bonus_mine * 2;
                        if(p.enemyHeroStartClass == TAG_CLASS.HUNTER)
                            pen += bonus_mine * 2;
                        // 任务完成后是不可能先技能的
                        if(p.anzDark > 0 && p.ownQuest.maxProgress == 1000 && !p.anzTamsin && Probabilitymaker.Instance.ownGraveyard.ContainsKey(CardDB.cardIDEnum.SW_091))
                        {
                            pen += 1000;
                        }
                    }
                    if (p.ownQuest.maxProgress == 8 && p.ownQuest.questProgress >= 5 && p.owncards.Count > 8) pen += 1000;
                    break;
                case CardDB.cardNameCN.赛车回火:
                    if (p.owncards.Count <= 7 && p.ownHero.Hp >= 18)
                    {
                        if (p.enemyMinions.Count < 3 && Hrtprozis.Instance.gTurn <= 6 && p.ownMaxMana <= 3) pen -= 100;
                        if (p.enemyMinions.Count < 5) pen -= bonus_mine * 6;
                    }
                    if (p.ownQuest.maxProgress == 1000 && !p.anzTamsin) pen += bonus_mine * 4 + 20;
                    if (p.ownQuest.maxProgress == 8 && p.ownQuest.questProgress >= 5 && p.owncards.Count > 6) pen += 1000;
                    break;
                case CardDB.cardNameCN.无证药剂师:
                    // 打封印骑/超凡德/点燃法就别管了，下吧
                    if(p.ownMaxMana < 5 && (p.enemyHeroStartClass == TAG_CLASS.PALADIN  || p.enemyHeroStartClass == TAG_CLASS.DRUID && !Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_428) || p.enemyHeroStartClass == TAG_CLASS.MAGE && !Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_450)) )
                    {
                        pen -= 100;
                    }
                    pen += bonus_mine * 2;
                    if (p.ownHero.Hp + p.ownHero.armor < 20 && !p.anzTamsin)
                    {
                        pen += bonus_mine * 5;
                    }
                    break;
                case CardDB.cardNameCN.灵魂炸弹:
                    if (foundDefile) pen += bonus_mine * 3;
                    pen += bonus_mine * 2;
                    if (p.ownQuest.maxProgress == 1000 && !p.anzTamsin) pen += bonus_mine * 8;
                    if (target.own && target.handcard.card.nameCN == CardDB.cardNameCN.黑眼) pen += bonus_mine * 20;
                    if (target.own && (p.ownQuest.maxProgress - p.ownQuest.questProgress > 4 || p.ownQuest.maxProgress - p.ownQuest.questProgress < 3 ) )
                    {
                        if(p.enemyHeroStartClass != TAG_CLASS.HUNTER && p.enemyHeroStartClass != TAG_CLASS.DRUID && p.enemyHeroStartClass != TAG_CLASS.MAGE)
                        {
                            pen += bonus_mine * 18;
                        }else
                        {
                            pen += bonus_mine * 5;
                        }
                    }
                    if (target.divineshild || target.Hp <= 1 || target.own) pen += bonus_mine * 5;
                    break;
                case CardDB.cardNameCN.恶魔之种:
                    pen -= bonus_mine * 10;
                    break;
                case CardDB.cardNameCN.塔姆辛罗姆:
                    pen += bonus_mine * 5;
                    break;
                case CardDB.cardNameCN.黑眼:
                    // 补丁后当白板打就行
                    break;
                case CardDB.cardNameCN.幸运币:
                    if (p.enemySecretCount > 0 && (p.enemyHeroStartClass == TAG_CLASS.PALADIN || p.enemyHeroStartClass == TAG_CLASS.MAGE))
                        pen -= 100;
                    pen += bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.莫瑞甘的灵界:
                    if (p.owncards.Count > 6) pen += bonus_mine * 8;
                    if (p.anzDark > 0)
                    {
                        if (p.mana <= 2) pen += bonus_mine * 8;
                    }
                    else if (p.mana < 4 && p.enemyHero.Hp > 5) pen += bonus_mine * 8;
                    break;
                case CardDB.cardNameCN.活化扫帚:
                    pen = 50;
                    if (p.enemyMinions.Count == 0)
                    {
                        pen = 50;
                        break;
                    }
                    int tmpAtk = 0, tmpHp = 0;
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.playedThisTurn && m.rush != 1 && m.charge != 1)
                        {
                            tmpAtk += m.Angr;
                        }
                    }
                    foreach (Minion m in p.enemyMinions)
                    {
                        if(!m.stealth && !m.untouchable)
                        tmpHp += m.Hp;
                    }
                    if (p.ownMinions.Count <= 5)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.getManaCost(p) <= 0 && hc.card.type == CardDB.cardtype.MOB)
                            {
                                return 1000;
                            }
                            if (hc.getManaCost(p) <= 1 && hc.card.Attack > 7 && p.mana > 1)
                            {
                                return 1000;
                            }
                        }
                    }
                    pen += -(tmpAtk > tmpHp ? tmpHp : tmpAtk) * 10;
                    break;
            }
            return pen;
        }

        // 核心，场面值
        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;
            float retval = 0;
            if (p.anzTamsin) retval += 250;
            retval += getGeneralVal(p);
            retval += p.owncarddraw * 5;
            // 危险血线
            int hpboarder = 5;
            // 不考虑法强了
            if (p.enemyHeroName == HeroEnum.mage) retval += 4 * p.enemyspellpower;
            // 抢脸血线
            int aggroboarder = 15;
            retval += getHpValue(p, hpboarder, aggroboarder);
            // 出牌序列数量
            int count = p.playactions.Count;
            int ownActCount = 0;
            bool useAb = false;
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
                        useAb = true;
                        break;
                    case actionEnum.playcard:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    case CardDB.cardNameCN.枯萎化身塔姆辛:
                        if (i != 0 && p.enemySecretCount == 0) retval -= 100;
                        break;
                    case CardDB.cardNameCN.恶魔之种:
                        if (p.enemySecretCount == 0)
                            retval += 300 - i * 100;
                        break;
                    case CardDB.cardNameCN.幸运币:
                    case CardDB.cardNameCN.生命分流:
                        retval -= i;
                        break;
                    case CardDB.cardNameCN.亡者复生:
                    case CardDB.cardNameCN.死亡缠绕:
                    case CardDB.cardNameCN.狗头人图书管理员:
                        retval -= 2 * i;
                        break;
                    case CardDB.cardNameCN.凯雷塞斯王子:
                        retval -= 4 * i;
                        break;
                    case CardDB.cardNameCN.赛车回火:
                    case CardDB.cardNameCN.莫瑞甘的灵界:
                        retval -= 3 * i;
                        break;
                    case CardDB.cardNameCN.护甲商贩:
                    case CardDB.cardNameCN.晶化师:
                        retval += i;
                        break;

                }
            }
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;

            int flag1 = 0;
            // 总之确保巨人减费的部分别被剪掉吧
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.nameCN == CardDB.cardNameCN.活化扫帚) flag1 |= 1;
                if ( (hc.card.nameCN == CardDB.cardNameCN.熔核巨人 || hc.card.nameCN == CardDB.cardNameCN.血肉巨人) && hc.getManaCost(p) <= 1) flag1 |= 2;
            }
            if (flag1 == 3) retval += 80;

            // 特殊处理一费场面
            if (Hrtprozis.Instance.gTurn <= 2 && p.ownMaxMana == 1 && p.ownHeroPowerCostLessOnce <= -99 && !useAb)
            {
                int flag = 0;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.nameCN == CardDB.cardNameCN.恶魔之种) flag |= 1;
                    if (hc.card.nameCN == CardDB.cardNameCN.幸运币) flag |= 2;
                    if (hc.getManaCost(p) == 1 && hc.card.type == CardDB.cardtype.MOB && hc.card.nameCN != CardDB.cardNameCN.活化扫帚) flag |= 4;
                }
                if (flag == 5) retval += 4000;
                else if (flag == 7) retval += 6000;
            }
            // 留着技能下回合出的情况
            if (p.mana < 2 && p.ownHeroPowerCostLessOnce <= -99)
            {
                retval += 10;
                if (!useAb) retval += 20;
                // 如果任务下回合可以直接完成
                if (p.ownQuest.maxProgress == 8 && p.ownQuest.questProgress >= 6) retval += 20;
                // 黑眼在手费不够的情况
                if(!p.anzTamsin && p.ownMaxMana <= 3 && p.anzDark == 0 && Hrtprozis.Instance.gTurn <= 6)
                {
                    foreach(Handmanager.Handcard hc in p.owncards)
                    {
                        if(hc.card.nameCN == CardDB.cardNameCN.黑眼)
                        {
                            retval += 30;
                            break;
                        }
                    }
                } 
            }
            // 特殊：优势防亵渎
            if (retval > 100 && p.enemyHeroStartClass == TAG_CLASS.WARLOCK)
            {
                bool found = false;
                // 从 2 开始防，术士自带一堆 1
                for(int i = 2; i <= 10; i++)
                {
                    found = false;
                    foreach(Minion m in p.ownMinions)
                    {
                        if (m.Hp == i)
                        {
                            retval -= 5 * i;
                            found = true;
                        }
                    }
                    foreach (Minion m in p.enemyMinions)
                    {
                        if (m.Hp == i)
                        {
                            found = true;
                        }
                    }
                    if (!found) break;
                }
            }
            //p.value = retval;
            return retval;
        }


        // 敌方随从价值 主要等于 （HP + Angr） * 4  
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
            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
            {
                if (s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610)
                {
                    if (m.Hp <= 2)
                    {
                        dieNextTurn = true;
                        break;
                    }
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 12;
            if (p.ownHero.Hp < 15) retval += bonus_mine * 3;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * bonus_enemy;
            retval += m.spellpower * bonus_enemy * 3 / 2;
            if (!m.frozen && !m.cantAttack)
            {
                retval += m.Angr * bonus_enemy;
                if (m.windfury) retval += m.Angr * bonus_enemy / 2;
                if (p.anzTamsin) retval += m.Angr * bonus_enemy;
            }
            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;

            if (m.lifesteal) retval += m.Angr * bonus_enemy;

            if (m.poisonous)
            {
                retval += 4;
                if (p.ownMinions.Count < p.enemyMinions.Count) retval += 10;
            }
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.无证药剂师:
                    if(Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_091t3))
                    {
                        retval += bonus_enemy * 20;
                    }
                    break;
                case CardDB.cardNameCN.聒噪怪:
                    if (m.Spellburst) retval += bonus_enemy * 20;
                    break;
                case CardDB.cardNameCN.安娜科德拉:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.圣殿蜡烛商:
                case CardDB.cardNameCN.奔逃的魔刃豹:
                case CardDB.cardNameCN.鲨鱼之灵:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                    retval += bonus_enemy * 90;
                    break;
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.黑眼:
                    retval += bonus_enemy * 20;
                    break;
                // 解不掉游戏结束
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
            }
            // 血量越低，解怪优先度越高
            if (p.ownHero.Hp <= 15)
            {
                retval += (16 - p.ownHero.Hp) * 3;
                if (p.ownHero.Hp <= 6) retval *= 2;
            }
            return retval;
        }
        /// <summary>
        /// 我方随从价值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
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
            int retval = 5;
            if (m.Hp <= 0) return 0;
            retval += m.Hp * bonus_mine;
            retval += m.Angr * bonus_mine;
            if (m.Hp <= 1 && !m.divineshild) retval -= (m.Angr - 1) * (bonus_mine - 1);
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * (bonus_mine - 1);
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
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.塔姆辛罗姆:
                    retval += bonus_mine * 5;
                    break;
                case CardDB.cardNameCN.黑眼:
                    if (!p.anzTamsin)
                        retval += bonus_mine * 2;
                    break;
                case CardDB.cardNameCN.无证药剂师:
                    if (p.anzTamsin) retval += bonus_mine * 16;
                    else
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.枯萎化身塔姆辛 && p.ownHero.Hp + p.ownHero.armor >= 15)
                            {
                                retval += bonus_mine * 12;
                            }
                        }
                    break;
            }
            if (m.dormant > 0)
            {
                retval -= bonus_mine * m.dormant;
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
            int offset_mine = p.calEnemyTotalAngr();
            switch (p.enemyHeroStartClass)
            {
                case TAG_CLASS.MAGE:
                    offset_mine += 1;
                    if (p.ownMaxMana >= 4) offset_mine += 6;
                    break;
                case TAG_CLASS.HUNTER:
                    offset_mine += 2;
                    if( Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_322t4))
                    {
                        offset_mine += 10;
                    }else
                    {
                        offset_mine += 5;
                    }
                    break;
                case TAG_CLASS.PRIEST:
                    offset_mine += 2;
                    offset_mine += p.enemyMinions.Count + 5 + (p.ownMaxMana > 4 ? 5 : 0);
                    break;
                case TAG_CLASS.DRUID:
                    offset_mine += 1;
                    if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_428))
                    {
                        offset_mine += 4;
                        if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_428t2))
                        {
                            offset_mine += 3;
                        }
                    }
                    break;
                case TAG_CLASS.WARRIOR:
                    if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_028))
                    {
                        offset_mine += 4;
                    }
                    break;
                case TAG_CLASS.WARLOCK:
                    if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_091t3)
                        || p.enemyQuest.Id == CardDB.cardIDEnum.SW_091t3)
                    {
                        offset_mine += 5;
                        if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(CardDB.cardIDEnum.SW_091t4))
                        {
                            offset_mine += 7;
                        }
                    }else if (p.enemyQuest.Id == CardDB.cardIDEnum.SW_091t || p.enemyQuest.Id == CardDB.cardIDEnum.SW_091)
                    {
                        offset_mine += 3;
                    }
                    break;
                case TAG_CLASS.ROGUE:
                    offset_mine += 6;
                    break;
                case TAG_CLASS.SHAMAN:
                    offset_mine += 6;
                    break;
                case TAG_CLASS.DEMONHUNTER:
                    offset_mine += 6;
                    break;
            }
            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) * 3 / 2;
            }
            // 快死了
            else if(p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 4 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
            }else
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
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
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
            // 红石头在手，优先降低到 10 血
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.nameCN == CardDB.cardNameCN.熔核巨人 && p.ownHero.Hp > 10)
                {
                    retval -= (p.ownHero.Hp - 10) * 3 - 30;
                }
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