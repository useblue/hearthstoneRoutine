using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
     class Sim_GIL_672 : SimTemplate //* 幽灵弯刀 Spectral Cutlass
//[x]<b>Lifesteal</b>Whenever you play a cardfrom another class,gain +1 Durability.
//<b>吸血</b>每当你使用一张其他职业的卡牌时，获得+1耐久度。 
    {
        
		
        CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GIL_672);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.equipWeapon(weapon, ownplay);
            
        }
    }
}