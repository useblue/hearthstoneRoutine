using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREngine.Bots
{
    //每个策略的 Penality{策略名}文件里面放 三个函数：打牌评分，随从进攻评分；英雄进攻评分 以及他们需要用到的private函数
    //这三个函数用于单动作评估，如果返回值超过500，则被剪枝，不列入候选动作
    public partial class Behavior黑眼任务术 : Behavior
    {
        public override int getPlayCardPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            switch (card.nameCN)
            {
                case CardDB.cardNameCN.灵魂炸弹:
                    // 一费手上有亵渎不急着解怪
                    if (p.ownMaxMana <= 2)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.亵渎) return 1000;
                        }
                    }
                    if (target.own && p.enemyMinions.Count > 0)
                    {
                        foreach (Minion m in p.enemyMinions)
                        {
                            if (!m.stealth && !m.untouchable) return 1000;
                        }
                    }
                    break;
                case CardDB.cardNameCN.亵渎:
                    if (p.enemyMinions.Count == 0) return 1000;
                    break;
                case CardDB.cardNameCN.力量的代价:
                    if (target.own && !target.Ready && target.handcard.card.nameCN != CardDB.cardNameCN.麻风侏儒) return 1000;
                    break;
                case CardDB.cardNameCN.黑铁矮人:
                case CardDB.cardNameCN.叫嚣的中士:
                    if (p.ownMinions.Count > 0 && target != null && !target.own) return 1000;
                    break;
                case CardDB.cardNameCN.灵魂之火:
                    if (target.own) return 1000;
                    if (target.isHero && ((p.ownMaxMana < 4 || target.Hp + target.armor > 20) && p.owncards.Count > 2)) return 1000;
                    break;
                case CardDB.cardNameCN.纳斯雷兹姆之触:
                    if (target.own && p.ownHero.Hp > 2) return 1000;
                    // 一费手上有亵渎不急着解怪
                    if (p.ownMaxMana <= 2)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == CardDB.cardNameCN.亵渎) return 1000;
                        }
                    }
                    break;
                case CardDB.cardNameCN.死亡缠绕:
                    if (target.own && target.handcard.card.nameCN != CardDB.cardNameCN.麻风侏儒) return 1000;
                    break;
                case CardDB.cardNameCN.梦境:
                    if (target != null && target.own && target.charge == 0) return 1000;
                    break;
                case CardDB.cardNameCN.梦魇:
                    if (target != null && !target.own) return 1000;
                    break;
            }
            return getComboPenality(card, target, p, nowHandcard);
        }

        public override int getAttackWithMininonPenality(Minion m, Playfield p, Minion target)
        {
            if (!m.silenced && m.handcard.card.CantAttack || target.untouchable)
                return 1000;
            int pen = -10;
            if (target.isHero && p.calTotalAngr() >= p.enemyHero.Hp)
            {
                return -1000;
            }
            if(target.isHero)
            foreach(Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.nameCN == CardDB.cardNameCN.亵渎)
                {
                    pen -= 15;
                    break;
                }
            }
            return pen;
        }

        //英雄攻击惩罚
        public override int getAttackWithHeroPenality(Minion target, Playfield p) 
        {
            if (target.untouchable)
                return 1000;
            int pen = -10;
            if (target.isHero && p.calTotalAngr() >= p.enemyHero.Hp) return -1000;
            return pen;
        }

        private int getSecretPenality(Playfield p)  // 牌序和防奥秘的影响
        {
            int Defile = 0;
            int bonus = 4;
            int pen = 0;
            // 硬币不用来跳一些卡
            int flag = 0;
            for (int i = 0; i < p.playactions.Count; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.playcard)
                {
                    if (a.card.card.nameCN == CardDB.cardNameCN.幸运币) flag |= 1;
                    if (a.card.card.nameCN == CardDB.cardNameCN.巡游向导) flag |= 2;
                    if (a.card.card.nameCN == CardDB.cardNameCN.亵渎)
                    {
                        Defile = i;
                    }
                }
            }
            for (int i = 0; i < Defile; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.playcard)
                {
                    if (a.card.card.nameCN == CardDB.cardNameCN.灵魂炸弹 && a.target.Hp <= 0
                        || a.card.card.nameCN == CardDB.cardNameCN.不稳定的暗影震爆 && a.target.Hp <= 0
                        || a.card.card.nameCN == CardDB.cardNameCN.纳斯雷兹姆之触 && a.target.Hp <= 0) return -15000;
                }
            }
            if ( (flag & 1) > 0  && (flag & 2) > 0 || (flag & 4) > 0 && (flag & 8) > 0) pen -= 1100;

            if (p.enemySecretCount == 0)
                return pen;
            bool canBe_flameward = false;
            foreach (SecretItem si in p.enemySecretList)  //Todo: 是否要判断己方回合还是敌方回合？？？
            {
                if (si.canBe_flameward) { canBe_flameward = true; break; }
            }
            if (canBe_flameward)
            {

                // 如果存在<=3的随从，用其中攻击最大的（会死的里面最大的）；否则无所谓
                // 奖励这种情况1分就好

                //先攻击随从，再攻击英雄，再出牌 这个顺序最适合奥秘法，因为没有buff手牌
                int first_attack_hero = -2;
                for (int i = 0; i < p.playactions.Count; i++)
                {
                    Action a = p.playactions[i];
                    if (a.actionType == actionEnum.attackWithMinion && a.target.isHero) // 随从攻击敌方英雄
                    {
                        first_attack_hero = i;
                        pen += 3;
                        break;
                    }
                }
                if (first_attack_hero >= 0) // 存在随从攻击英雄
                {
                    bool playCardBefore = false;
                    //如果此前出牌了，扣分，容易被炸
                    for (int i = 0; i < first_attack_hero; i++)
                    {
                        Action a = p.playactions[i];
                        if (a.actionType == actionEnum.playcard && a.card.card.type == CardDB.cardtype.MOB)
                        {
                            playCardBefore = true;
                        }
                    }
                    if (playCardBefore) return -15000;
                    // 用攻击力最高的先攻击
                    if (p.playactions[first_attack_hero].own.Hp < 4)
                    {
                        pen += p.playactions[first_attack_hero].own.Angr * 5;
                    }
                    pen += 20;
                    for (int i = first_attack_hero + 1; i < p.playactions.Count; i++)
                    {
                        Action a = p.playactions[i];
                        if (a.actionType == actionEnum.attackWithMinion && !a.target.isHero && a.own.Hp < 3 && !a.own.divineshild)
                        {
                            return -15000; // 不可接受，抛弃本牌面以及子牌面
                        }
                    }
                    if (p.playactions[first_attack_hero].own.Hp > 3)
                    {
                        //此后不应该存在hp<=3的随从
                        for (int i = first_attack_hero + 1; i < p.playactions.Count; i++)
                        {
                            Action a = p.playactions[i];
                            if (a.actionType == actionEnum.attackWithMinion)
                            {
                                if (a.own.Hp <= 3)
                                    return -15000;
                            }
                        }
                    }
                }
            }
           
            bool canBe_explosive = false;  //防止是猎人的爆炸陷阱
            foreach (SecretItem si in p.enemySecretList)
            {
                if (si.canBe_explosive) { canBe_explosive = true; break; }
            }
            if (canBe_explosive)
            {
                int first_attack_hero = -2;
                for (int i = 0; i < p.playactions.Count; i++)
                {
                    Action a = p.playactions[i];
                    if ((a.actionType == actionEnum.attackWithMinion || a.actionType == actionEnum.attackWithHero) && a.target.isHero) // 随从攻击敌方英雄
                    {
                        first_attack_hero = i;
                        if (p.ownHero.Hp <= 2)
                            pen -= 500;
                        break;
                    }
                    // Todo: 这里还要考虑法术伤害敌方英雄 待Fix
                }
                if (first_attack_hero >= 0) // 存在随从攻击英雄
                {
                    //如果此前出牌了，扣分，容易被炸
                    for (int i = 0; i < first_attack_hero; i++)
                    {
                        Action a = p.playactions[i];
                        if (a.actionType == actionEnum.playcard)
                        {
                            if (a.card.card.type == CardDB.cardtype.MOB) //出了随从
                                return -15000; // 不可接受，抛弃本牌面以及子牌面
                        }
                    }
                    for (int i = first_attack_hero + 1; i < p.playactions.Count; i++)
                    {
                        Action a = p.playactions[i];
                        if (a.actionType == actionEnum.attackWithMinion && !a.target.isHero && a.own.Hp < 3 && !a.own.divineshild)
                        {
                            return -15000; // 不可接受，抛弃本牌面以及子牌面
                        }
                    }
                    // 尽量少留 2 血以下生物
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.Hp < 3 && !m.divineshild)
                        {
                            pen -= m.Angr * 6;
                        }
                    }
                }
            }
            return pen;
        }
    }
}
