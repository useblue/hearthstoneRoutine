using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_038 : SimTemplate //* 烟火技师 Fireworks Tech
	{
		//[x]<b>Battlecry:</b> Give a friendlyMech +1/+1. If it has<b>Deathrattle</b>, trigger it.
		//<b>战吼：</b>使一个友方机械获得+1/+1。如果它具有<b>亡语</b>，则将其触发。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 17),
            };
        }
	}
}
