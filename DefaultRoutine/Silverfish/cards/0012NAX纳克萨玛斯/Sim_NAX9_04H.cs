using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_NAX9_04H : SimTemplate //* 瑟里耶克爵士 Sir Zeliek
//Your hero is <b>Immune</b>.
//使你的英雄获得<b>免疫</b>。 
	{
        

        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.ownHero.immune = true;
                if (p.ownWeapon.name == CardDB.cardNameEN.runeblade && p.anzOwnHorsemen < 1)
                {
                    int bonus = (p.ownWeapon.card.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                    p.minionGetBuffed(p.ownHero, -1 * Math.Min(bonus, p.ownWeapon.Angr- 1), 0);
                    p.ownWeapon.Angr= Math.Min(1, p.ownWeapon.Angr- bonus);
                }
                p.anzOwnHorsemen++;
            }
            else
            {
                p.enemyHero.immune = true;
                if (p.enemyWeapon.name == CardDB.cardNameEN.runeblade && p.anzEnemyHorsemen < 1)
                {
                    int bonus = (p.enemyWeapon.card.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                    p.minionGetBuffed(p.enemyHero, -1 * Math.Min(bonus, p.enemyWeapon.Angr - 1), 0);
                    p.enemyWeapon.Angr = Math.Min(1, p.enemyWeapon.Angr - bonus);
                }
                p.anzEnemyHorsemen++;
            }
        }

        public override void onAuraEnds(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.anzOwnHorsemen--;
                if (p.anzOwnHorsemen < 1)
                {
                    p.ownHero.immune = false;
                    if (p.ownWeapon.name == CardDB.cardNameEN.runeblade)
                    {
                        int bonus = (p.ownWeapon.card.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                        p.minionGetBuffed(p.ownHero, bonus, 0);
                        p.ownWeapon.Angr += bonus;
                    }
                }
            }
            else
            {
                p.anzEnemyHorsemen--;
                if (p.anzEnemyHorsemen < 1)
                {
                    p.enemyHero.immune = false;
                    if (p.enemyWeapon.name == CardDB.cardNameEN.runeblade)
                    {
                        int bonus = (p.enemyWeapon.card.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                        p.minionGetBuffed(p.enemyHero, bonus, 0);
                        p.enemyWeapon.Angr += bonus;
                    }
                }
            }
        }
    }
}