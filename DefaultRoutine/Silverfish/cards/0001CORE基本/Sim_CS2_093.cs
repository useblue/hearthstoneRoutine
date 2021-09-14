using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CS2_093 : SimTemplate //* 奉献 Consecration
	{
		//Deal $2 damage to all_enemies.
		//对所有敌人造成$2点伤害。
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
            p.allCharsOfASideGetDamage(!ownplay, dmg);
		}

	}
}