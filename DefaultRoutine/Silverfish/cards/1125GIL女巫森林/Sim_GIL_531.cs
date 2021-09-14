using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_531 : SimTemplate //* 女巫的学徒 Witch's Apprentice
//<b>Taunt</b><b>Battlecry:</b> Add a random Shaman spell to your hand.
//<b>嘲讽，战吼：</b>随机将一张萨满祭司法术牌置入你的手牌。 
	{
		
		
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.frostbolt, own.own, true);
		}
	}
}