using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_087 : SimTemplate //* 学术剽窃 Academic Espionage
//Shuffle 10 cards from your opponent's class into your deck. They_cost (1).
//将十张你对手职业的卡牌洗入你的牌库，其法力值消耗为（1）点。 
	{
		

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay)
            {
                p.ownDeckSize += 10;
                p.evaluatePenality -= 11;
            }
            else p.enemyDeckSize += 10;
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}