using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CORE_EX1_250 : SimTemplate //earthelemental
	{

//    spott/, Ã¼berladung:/ (3)
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            if (own.own) p.ueberladung += 3;
		}


	}
}