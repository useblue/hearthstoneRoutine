using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOT_364 : SimTemplate //* 鲁莽风暴 Reckless Flurry
//Spend all your Armor. Deal that much damage to all minions.
//消耗你所有的护甲值。对所有随从造成等同于所消耗护甲值数量的伤害。 
	{



		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{


            int dmg = (ownplay) ? p.getSpellDamageDamage(p.ownHero.armor) : p.getEnemySpellDamageDamage(p.enemyHero.armor);
            p.allMinionsGetDamage(dmg);
			p.ownHero.armor=0;
		}

	}
}