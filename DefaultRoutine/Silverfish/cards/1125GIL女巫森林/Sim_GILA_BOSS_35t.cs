namespace HREngine.Bots
{
	class Sim_GILA_BOSS_35t : SimTemplate //* 吸血鬼之牙 Vampiric Fangs
//Destroy a minion. Restore its Health to your hero.
//消灭一个随从。为你的英雄恢复生命值，数值相当于此随从的生命值。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}