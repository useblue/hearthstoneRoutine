using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DRG_270t8 : SimTemplate //* 玛里苟斯的寒冰箭 Malygos's Frostbolt
	{
		//Deal $3 damage to a_character and <b>Freeze</b> it.
		//对一个角色造成$3点伤害，并使其<b>冻结</b>。
		
		

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}
