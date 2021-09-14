using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULDA_602 : SimTemplate //* 研究中断 Study Break
	{
		//<b>Discover</b> a spell. Add it to your Adventure deck.It always starts in your hand.
		//<b>发现</b>一张法术牌。将其置入你的冒险模式套牌，它将始终出现在你的起始手牌中。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
	}
}
