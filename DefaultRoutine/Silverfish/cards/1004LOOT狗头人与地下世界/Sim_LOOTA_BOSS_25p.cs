using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOTA_BOSS_25p : SimTemplate //* 复仇吐息 Vindictive Breath
	{
		//<b>Hero Power</b>Deal 1 damage to all enemies for each missing Master Chest.
		//<b>英雄技能</b>每缺少一个大师宝箱，就对所有敌人造成1点伤害。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
	}
}
