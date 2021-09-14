using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DALA_Rogue_HP2 : SimTemplate //* 挥砍 Cut-less
	{
		//<b>Hero Power</b>Deal $2 damage to an undamaged minion.
		//<b>英雄技能</b>对一个未受伤的随从造成$2点伤害。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_UNDAMAGED_TARGET),
            };
        }
	}
}
