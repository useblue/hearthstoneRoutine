namespace HREngine.Bots
{
	class Sim_GILA_608 : SimTemplate //* 避风港湾 Safe Harbor
//Choose a minion and put it into your hand.It costs (0).
//选择一个随从并将其置入你的手牌。该牌的法力值消耗变为（0）点。 
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