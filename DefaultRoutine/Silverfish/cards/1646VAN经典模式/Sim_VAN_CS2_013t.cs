using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_VAN_CS2_013t : SimTemplate //excessmana
	{

//    zieht eine karte. i&gt;(ihr kÃ¶nnt nur 10 mana in eurer leiste haben.)/i&gt;
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
		}

	}
}