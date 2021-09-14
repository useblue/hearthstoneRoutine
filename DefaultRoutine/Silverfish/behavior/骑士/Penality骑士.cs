using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREngine.Bots
{
    //每个策略的 Penality{策略名}文件里面放 三个函数：打牌评分，随从进攻评分；英雄进攻评分 以及他们需要用到的private函数
    //这三个函数用于单动作评估，如果返回值超过500，则被剪枝，不列入候选动作
    public partial class Behavior骑士 : Behavior
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
                case CardDB.cardNameCN.虚灵改装师:
                    if (target != null && target.own ) return 1000;
                    break;
                case CardDB.cardNameCN.火箭改装师:
                    if (target != null && target.own && (!target.playedThisTurn || target.Ready ) ) return 1000;
                    break;
                case CardDB.cardNameCN.叫嚣的中士:
                    if (p.ownMinions.Count > 0 && target != null && !target.own) return 1000;
                    break;
                case CardDB.cardNameCN.流光之赐:
                case CardDB.cardNameCN.威能祝福:
                case CardDB.cardNameCN.阿达尔之手:
                case CardDB.cardNameCN.王者祝福:
                    if (target != null && !target.own)
                        return 1000;
                    break;
                case CardDB.cardNameCN.甩笔侏儒:
                    if (target != null && target.own)
                        return 1000;
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
                return pen;
            // 鼓励拥有智慧圣契的随从送死
            if (m.libramofwisdom > 0 && !target.isHero) { 
                if(target.Angr >= m.Hp) return - m.libramofwisdom * 20;
                return - m.libramofwisdom * 12;
            }
            if (!target.isHero && p.enemyHeroStartClass == TAG_CLASS.PALADIN)
            {
                pen -= 4;
            }
            return pen;
        }

        //英雄攻击惩罚
        public override int getAttackWithHeroPenality(Minion target, Playfield p) 
        {
            int pen = -10;
            return pen;
        }

        private int getSecretPenality(Playfield p)  // 牌序和防奥秘的影响
        {
            // AOE/随机 先打
            int aoed = 0, jg = 0;
            for (int i = 0; i < p.playactions.Count; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.playcard && (a.card.card.nameCN == CardDB.cardNameCN.定罪等级1 
                    || a.card.card.nameCN == CardDB.cardNameCN.定罪等级2
                    || a.card.card.nameCN == CardDB.cardNameCN.定罪等级3))
                {
                    aoed = i;
                    break;
                }
                if (a.actionType == actionEnum.playcard && (a.card.card.nameCN == CardDB.cardNameCN.战场军官))
                {
                    jg = i;
                    break;
                }
            }
            for (int i = 0; i < aoed; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.attackWithHero || a.actionType == actionEnum.attackWithMinion)
                {
                    return -10000;
                }
            }
            for (int i = 0; i < jg; i++)
            {
                Action a = p.playactions[i];
                if (a.actionType == actionEnum.playcard && (a.card.card.Charge))
                {
                    return -10000;
                }
            }

            int bonus = 4;
            if (p.enemySecretCount == 0)
                return 0;
            int pen = 0;
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
                        if (a.actionType == actionEnum.playcard && (a.card.card.type == CardDB.cardtype.MOB || a.card.card.nameCN == CardDB.cardNameCN.战斗号角 || a.card.card.nameCN == CardDB.cardNameCN.开赛集结))
                        {
                            playCardBefore = true;
                        }
                        else if (a.actionType == actionEnum.useHeroPower && (p.ownHeroAblility.card.cardIDenum == CardDB.cardIDEnum.HERO_02bp || p.ownHeroAblility.card.cardIDenum == CardDB.cardIDEnum.HERO_04bp))
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
                    for (int i = first_attack_hero + 1; i < p.playactions.Count; i++)
                    {
                        Action a = p.playactions[i];
                        if (a.actionType == actionEnum.attackWithMinion && !a.target.isHero && a.own.Hp < 4 && !a.own.divineshild)
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
                        if (a.actionType == actionEnum.playcard && (a.card.card.type == CardDB.cardtype.MOB || a.card.card.nameCN == CardDB.cardNameCN.战斗号角 || a.card.card.nameCN == CardDB.cardNameCN.开赛集结 ) )
                        {
                            playCardBefore = true;
                        }
                        else if (a.actionType == actionEnum.useHeroPower && (p.ownHeroAblility.card.cardIDenum == CardDB.cardIDEnum.HERO_02bp || p.ownHeroAblility.card.cardIDenum == CardDB.cardIDEnum.HERO_04bp))
                        {
                            playCardBefore = true;
                        }
                    }
                    if (playCardBefore) return -15000;
                    
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
                        //此后不应该存在hp<=2的随从
                        for (int i = first_attack_hero + 1; i < p.playactions.Count; i++)
                        {
                            Action a = p.playactions[i];
                            if (a.actionType == actionEnum.attackWithMinion)
                            {
                                if (a.own.Hp <= 2)
                                    return -15000;
                            }
                        }
                    }
                }
            }
            return pen;
        }
    }
}
