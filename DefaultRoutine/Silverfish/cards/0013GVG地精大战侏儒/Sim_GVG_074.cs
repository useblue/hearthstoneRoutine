using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_074 : SimTemplate //Kezan Mystic¿ÆÔÞÃØÊõÊ¦
    {
        //todo better!
        //  Battlecry: Take control of a random enemy Secret;. 

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (own.own)
            {
                if (p.enemySecretList.Count >= 1)
                {
                    if (p.enemyHeroStartClass == TAG_CLASS.HUNTER) p.ownSecretsIDList.Add(CardDB.cardIDEnum.EX1_610);
                    if (p.enemyHeroStartClass == TAG_CLASS.MAGE) p.ownSecretsIDList.Add(CardDB.cardIDEnum.EX1_594);
                    if (p.enemyHeroStartClass == TAG_CLASS.PALADIN) p.ownSecretsIDList.Add(CardDB.cardIDEnum.EX1_130);

                    if (p.enemyHeroStartClass != TAG_CLASS.HUNTER && p.enemyHeroStartClass != TAG_CLASS.MAGE && p.enemyHeroStartClass != TAG_CLASS.PALADIN) p.ownSecretsIDList.Add(CardDB.cardIDEnum.EX1_130);
                    
                    p.enemySecretList.RemoveAt(0);
                }
            }
            else
            {
                if (p.ownSecretsIDList.Count >= 1)
                {
                    p.ownSecretsIDList.RemoveAt(0);
                    SecretItem s = new SecretItem();
                    s.canBe_avenge = false;
                    s.canBe_sacredtrial = false;
                    s.canBe_counterspell = false;
                    s.canBe_cattrick = false;
                    s.canBe_duplicate = false;
                    s.canBe_explosive = false;
                    s.canBe_beartrap = false;
                    s.canBe_eyeforaneye = false;
                    s.canBe_freezing = false;
                    s.canBe_icebarrier = false;
                    s.canBe_iceblock = false;
                    s.canBe_mirrorentity = false;
                    s.canBe_missdirection = false;
                    s.canBe_darttrap = false;
                    s.canBe_noblesacrifice = false;
                    s.canBe_redemption = false;
                    s.canBe_repentance = false;
                    s.canBe_snaketrap = false;
                    s.canBe_snipe = false;
                    s.canBe_spellbender = false;

                    s.entityId = 1050;
                    s.canBe_explosive=true;

                    p.enemySecretList.Add(s);
                }
            }
        }


    }

}