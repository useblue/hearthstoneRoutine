using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOT_161 : SimTemplate //* 食肉魔块 Carnivorous Cube
	{
		//<b>Battlecry:</b> Destroya friendly minion.<b>Deathrattle:</b> Summon 2 copies of it.
		//<b>战吼：</b>消灭一个友方随从。<b>亡语：</b>召唤两个被消灭随从的复制。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
	}
}
