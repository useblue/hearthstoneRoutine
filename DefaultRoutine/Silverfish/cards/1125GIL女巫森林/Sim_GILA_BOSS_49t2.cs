namespace HREngine.Bots
{
	class Sim_GILA_BOSS_49t2 : SimTemplate //* 鸦羽契约 Crowskin Pact
//Transform a minion into a 'Crowskin Faithful' and take control of it.
//将一个随从变形为“鸦羽信徒”并获得其控制权。 
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