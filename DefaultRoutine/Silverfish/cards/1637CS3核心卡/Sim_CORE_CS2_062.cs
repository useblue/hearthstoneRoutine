using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CORE_CS2_062 : SimTemplate //hellfire
	{

//    fÃ¼gt allen charakteren $3 schaden zu.
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            int dmg = (ownplay) ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
            p.allCharsGetDamage(dmg);
		}

	}
}