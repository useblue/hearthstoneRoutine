using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DRG_302 : SimTemplate //* 墓地符文 Grave Rune
	{
		//Give a minion "<b>Deathrattle:</b> Summon 2 copies of this."
		//使一个随从获得“<b>亡语：</b>召唤该随从的两个复制。”
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}
