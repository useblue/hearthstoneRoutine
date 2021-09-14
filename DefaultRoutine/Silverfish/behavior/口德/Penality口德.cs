using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREngine.Bots
{
    //每个策略的 Penality{策略名}文件里面放 三个函数：打牌评分，随从进攻评分；英雄进攻评分 以及他们需要用到的private函数
    //这三个函数用于单动作评估，如果返回值超过500，则被剪枝，不列入候选动作
    public partial class Behavior口德 : Behavior
    {
        public override int getPlayCardPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            switch (card.nameCN)
            {
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
            if (target.isHero)
                pen -= 6;
            if (p.enemyHero.Hp + p.enemyHero.armor <= 15 && p.enemyHero.Hp + p.enemyHero.armor > 0 || p.ownMaxMana > 7 && p.enemyHero.Hp + p.enemyHero.armor > 0)
            {
                if(target.isHero)
                    pen -= 50;
            }
            return pen;
        }

        //英雄攻击惩罚
        public override int getAttackWithHeroPenality(Minion target, Playfield p) 
        {
            if (target.untouchable)
                return 1000;
            int pen = -10;
            // 不可能说手上还有牌可以出然后就攻击了吧
            if(p.anzEnemyTaunt == 0)
            {
                if (p.ownAbilityReady && p.mana >= 2) return 1000;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    switch (hc.card.nameCN)
                    {
                        case CardDB.cardNameCN.飞扑:
                        case CardDB.cardNameCN.爪击:
                        case CardDB.cardNameCN.野性之怒:
                        case CardDB.cardNameCN.铁齿铜牙:
                        case CardDB.cardNameCN.撕咬:
                            if (p.mana >= hc.getManaCost(p)) return 1000;
                            break;
                    }
                }
            }
            if (target.isHero)
                pen -= 10;
            else if (!target.taunt)
            {
                pen += 15;
            }
            if (p.enemyHero.Hp + p.enemyHero.armor <= 15 && p.enemyHero.Hp + p.enemyHero.armor > 0 || p.ownMaxMana > 7 && p.enemyHero.Hp + p.enemyHero.armor > 0)
            {
                if (target.isHero)
                    pen -= 50;
            }
            return pen;
        }

        private int getSecretPenality(Playfield p)  // 牌序和防奥秘的影响
        {
            // 硬币不用来跳一些卡
            int first = 0;
            for (int i = 0; i < p.playactions.Count; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.attackWithMinion)
                {
                    if (a.own.handcard.card.nameCN == CardDB.cardNameCN.花园猎豹) first = i;
                }
            }
            for (int i = 0; i < first; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.attackWithHero)
                {
                    return -15000;
                }
            }
            // 设置奥秘
            bool set_explosive = false;
            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
            {
                if (s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610)
                {
                    set_explosive = true;
                }
            }
            if (set_explosive && p.enemyMinions.Count >= 2)
            {
                for (int i = 0; i < p.playactions.Count; i++)
                {
                    Action a = p.playactions[i];
                    // 设置爆炸陷阱的情况下不下怪
                    if (a.actionType == actionEnum.playcard && a.card.card.type == CardDB.cardtype.MOB && !a.card.card.Charge && !a.card.card.Stealth)
                    {
                        return -15000; // 不可接受，抛弃本牌面以及子牌面
                    }
                    // 不打对手随从
                    if (a.actionType == actionEnum.playcard && (a.card.card.nameCN == CardDB.cardNameCN.杀戮命令 || a.card.card.nameCN == CardDB.cardNameCN.奥术射击) && a.target != null && a.target != p.enemyHero && a.target.Hp <= 2)
                    {
                        return -15000; // 不可接受，抛弃本牌面以及子牌面
                    }
                    if (a.actionType == actionEnum.attackWithHero || a.actionType == actionEnum.attackWithMinion)
                    {
                        if (a.target != null && a.target.Hp < 3) return -15000;
                    }
                }
            }

            if (p.enemySecretCount == 0)
                return 0;
           
            int pen = 0;

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
