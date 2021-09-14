using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_406 : SimTemplate //* 超级对撞器 Supercollider
//[x]After you attack a minion,force it to attack oneof its neighbors.
//在你攻击一个随从后，迫使其攻击相邻的一个随从。 
	{
		

        CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BOT_406);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.equipWeapon(weapon, ownplay);
        }
    }
}