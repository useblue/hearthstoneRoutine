using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DALA_740ts4 : SimTemplate //* 神奇的智慧之球随机传说标记法术 Wondrous Wisdomball Random Legendary Tokenspell
	{
		//Transform a minion into a random <b>Legendary</b> minion.
		//将一个随从随机变形成为<b>传说</b>随从。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}
