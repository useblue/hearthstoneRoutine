namespace HREngine.Bots
{
	class Sim_DALA_BOSS_19p : SimTemplate //* 小小刺骨 Lil' Eviscerate
//<b>Hero Power</b>Deal 1 damage<b>Combo:</b> 2 instead.
//<b>英雄技能</b>造成1点伤害。<b>连击：</b>改为造成2点伤害。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}