namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_74p : SimTemplate //* 强光迸发 Burst of Light
//[x]<b>Hero Power</b><b>Lifesteal</b>. Deal $1 damageto all enemy minions.
//<b>英雄技能</b><b>吸血</b>，对所有敌方随从造成$1点伤害。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_ENEMY_MINIONS, 1),
            };
        }
	}
}