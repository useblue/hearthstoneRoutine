using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_549 : SimTemplate //* 时光修补匠托奇 Toki, Time-Tinker
//[x]<b>Battlecry:</b> Add a random<b>Legendary</b> minion fromthe past to your hand.
//<b>战吼：</b>随机将一张狂野<b>传说</b>随从牌置入你的手牌。 
	{
        


		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{

            p.drawACard(CardDB.cardNameEN.unknown, own.own, true);
		}
	}
}