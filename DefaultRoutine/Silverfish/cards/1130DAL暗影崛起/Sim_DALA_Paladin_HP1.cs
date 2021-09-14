using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DALA_Paladin_HP1 : SimTemplate //* 后备力量 Backup
	{
		//<b>Hero Power</b>Add three 1/1 Silver Hand Recruits to your hand.
		//<b>英雄技能</b>将三张1/1的“白银之手新兵”置入你的手牌。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_HAND_NOT_FULL),
            };
        }
	}
}
