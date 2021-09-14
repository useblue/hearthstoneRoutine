using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_NAX14_04 : SimTemplate //* 极寒之击 Pure Cold
//Deal $8 damage to the enemy hero, and <b>Freeze</b> it.
//对敌方英雄造成$8点伤害，并使其<b>冻结</b>。 
	{
		

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = (ownplay) ? p.getSpellDamageDamage(8) : p.getEnemySpellDamageDamage(8);
			target = ownplay ? p.enemyHero : p.ownHero;
            p.minionGetDamageOrHeal(target, dmg, true);
            p.minionGetFrozen(target);
        }
	}
}