using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_FP1_017 : SimTemplate //nerubarweblord
	{

//    diener mit kampfschrei/ kosten (2) mehr.
        // 战吼随从 +2 费
        public override void onAuraStarts(Playfield p, Minion own)
		{
            p.nerubarweblord++;
		}

        public override void onAuraEnds(Playfield p, Minion own)
        {
            p.nerubarweblord--;
        }


	}
}