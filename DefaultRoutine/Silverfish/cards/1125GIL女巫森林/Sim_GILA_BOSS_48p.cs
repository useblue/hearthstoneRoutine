using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GILA_BOSS_48p : SimTemplate //* 时光加速 Chronoacceleration
	{
		//[x]<b>Hero Power</b>Give a friendly minion<b>Mega-Windfury</b>. At theend of your turn, it dies.
		//<b>英雄技能</b>使一个友方随从获得<b>超级风怒</b>。在你的回合结束时，它将死去。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
	}
}
