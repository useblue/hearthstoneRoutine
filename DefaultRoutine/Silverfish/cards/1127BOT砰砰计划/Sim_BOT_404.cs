using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_404 : SimTemplate //* 香甜的灵力瓜 Juicy Psychmelon
//Draw a 7, 8, 9, and10-Cost minion from your deck.
//从你的牌库中抽取法力值消耗为（7），（8），（9）和（10）的随从牌各一张。 
	{
		
		
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.drawACard(CardDB.cardNameEN.unknown, ownplay, true);
            p.drawACard(CardDB.cardNameEN.unknown, ownplay, true);
			p.drawACard(CardDB.cardNameEN.unknown, ownplay, true);
            p.drawACard(CardDB.cardNameEN.unknown, ownplay, true);
        }
    }
}