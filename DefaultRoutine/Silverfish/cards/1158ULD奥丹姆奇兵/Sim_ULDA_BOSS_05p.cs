using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_05p : SimTemplate //* 剧毒蛰针 Venomous Stinger
	{
		//[x]<b>Hero Power</b> Sting a minion. When ittakes damage, destroy it.
		//<b>英雄技能</b>蛰刺一个随从。当该随从受到伤害时，消灭该随从。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}
