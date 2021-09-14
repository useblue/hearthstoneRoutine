using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_025 : SimTemplate //* 独眼欺诈者 One-eyed Cheat
//Whenever you summon a Pirate, gain <b>Stealth</b>.
//每当你召唤一个海盗，便获得<b>潜行</b>。 
    {

        

        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            if ((TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.PIRATE)
            {
                triggerEffectMinion.stealth = true;
            }
        }


    }

}