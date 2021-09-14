using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOE_077 : SimTemplate //* 布莱恩·铜须 Brann Bronzebeard
//Your <b>Battlecries</b> trigger twice.
//你的<b>战吼</b>会触发两次。 
	{
		
		
        public override void onAuraStarts(Playfield p, Minion own)
		{
            if (own.own) p.ownBrannBronzebeard++;
            else p.enemyBrannBronzebeard++;
		}

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.ownBrannBronzebeard--;
            else p.enemyBrannBronzebeard--;
        }
	}
}