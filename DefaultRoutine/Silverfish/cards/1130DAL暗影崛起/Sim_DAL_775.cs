using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DAL_775 : SimTemplate //* 坑道爆破师 Tunnel Blaster
//[x]<b>Taunt</b><b>Deathrattle:</b> Deal 3 damageto all minions.
//<b>嘲讽，亡语：</b>对所有随从造成3点伤害。 
    {

        

        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.allMinionsGetDamage(3);
        }


    }

}