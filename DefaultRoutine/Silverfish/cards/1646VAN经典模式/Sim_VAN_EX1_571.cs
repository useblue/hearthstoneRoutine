using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_VAN_EX1_571 : SimTemplate //* Force of Nature
	{
        //�ٻ�����2/2������ ��� �����ˣ��ڻغϽ���ʱ��������Щ���ˡ�

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_158t);//Treant

		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            int pos =(ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

            p.callKid(kid, pos, ownplay, false);
            p.callKid(kid, pos, ownplay);
            p.callKid(kid, pos, ownplay);

            foreach(Minion m in p.ownMinions)
            {
                if(m.handcard.card.cardIDenum == CardDB.cardIDEnum.EX1_158t)
                {
                    p.minionGetCharge(m);
                    m.destroyOnOwnTurnEnd = true;
                }
            }
		}

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}