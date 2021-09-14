using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_069 : SimTemplate //* 砰砰飞艇 The Boomship
//Summon 3 random minions from your hand. Give them <b>Rush</b>.
//随机从你的手牌中召唤三个随从，并使其获得<b>突袭</b>。 
	{
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}