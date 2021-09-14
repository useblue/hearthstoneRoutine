namespace HREngine.Bots
{
	class Sim_LOOTA_BOSS_13p : SimTemplate //* 战术撤退 Tactical Retreat
//<b>Hero Power</b>Return a friendly minion to your hand.
//<b>英雄技能</b>将一个友方随从移回你的手牌。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}