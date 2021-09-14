using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_VAN_PRO_001b : SimTemplate //* Rogues Do It...
    {
        //Deal $4 damage. Draw a card.

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = (ownplay) ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);
            p.minionGetDamageOrHeal(target, dmg);
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
        }
    }
}
