using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DALA_Rogue_HP1 : SimTemplate //* 妙手空空 Yoink!
	{
		//[x]<b>Hero Power</b>Add a randomcard to your hand<i>(from another class).</i>
		//<b>英雄技能</b>随机将一张<i>（其他职业的）</i>卡牌置入你的手牌。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_HAND_NOT_FULL),
            };
        }
	}
}
