using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULDA_402 : SimTemplate //* 布莱恩的鞍座 Brann's Saddle
	{
		//Give a friendly Beast +3/+3 and "<b>Deathrattle:</b> Give Brann's Saddle to another friendly Beast."
		//使一个友方野兽获得+3/+3，以及“<b>亡语：</b>随机将布莱恩的鞍座甩给另一个友方野兽。”
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 20),
            };
        }
	}
}
