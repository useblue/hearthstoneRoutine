using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_EX1_248 : SimTemplate //* 野性狼魂 Feral Spirit
	{
		//Summon two 2/3 Spirit Wolves with <b>Taunt</b>. <b>Overload:</b> (1)
		//召唤两只2/3并具有<b>嘲讽</b>的幽灵狼。<b>过载：</b>（1）

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_tk11);//spiritwolf

		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

            p.callKid(kid, pos, ownplay, false);
            p.callKid(kid, pos, ownplay);
            if (ownplay) p.ueberladung += 1;
		}

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}
