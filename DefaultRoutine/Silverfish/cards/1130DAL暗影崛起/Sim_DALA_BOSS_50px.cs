using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DALA_BOSS_50px : SimTemplate //* 沙尘吐息 Sand Breath
	{
		//<b>Hero Power</b>Deal $4 damage to a random enemy.
		//<b>英雄技能</b>随机对一个敌人造成$4点伤害。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}
