using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_318 : SimTemplate //* 妖术领主玛拉卡斯 Hex Lord Malacrass
//<b>Battlecry</b>: Add a copy of your opening hand to your hand <i>(except this card)</i>.
//<b>战吼：</b>将你的起始手牌的复制置入手牌<i>（不包括这张牌）</i>。 
	{


		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.unknown, own.own);
			p.drawACard(CardDB.cardNameEN.unknown, own.own);
			p.drawACard(CardDB.cardNameEN.unknown, own.own);
			p.drawACard(CardDB.cardNameEN.unknown, own.own);
		}


	}
}