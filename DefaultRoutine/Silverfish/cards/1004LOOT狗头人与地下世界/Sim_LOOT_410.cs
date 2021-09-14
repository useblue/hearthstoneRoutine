using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOT_410 : SimTemplate //* 破晓之龙 Duskbreaker
	{
		//<b>Battlecry:</b> If you're holdinga Dragon, deal 3 damage to all other minions.
		//<b>战吼：</b>如果你的手牌中有龙牌，则对所有其他随从造成3点伤害。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }
	}
}
