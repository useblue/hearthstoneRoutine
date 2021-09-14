using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DRG_223 : SimTemplate //* 遮天雨云 Cumulo-Maximus
	{
		//<b>Battlecry:</b> If you have <b>Overloaded</b> Mana Crystals, deal 5 damage.
		//<b>战吼：</b>如果你有<b>过载</b>的法力水晶，造成5点伤害。
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			if (own.own)
			{
				if (p.ueberladung > 0)
				{
					p.minionGetDamageOrHeal(target, 5);
				}
			}
		}

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}		
}