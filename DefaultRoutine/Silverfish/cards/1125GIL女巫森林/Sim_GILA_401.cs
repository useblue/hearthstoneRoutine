namespace HREngine.Bots
{
	class Sim_GILA_401 : SimTemplate //* 围猎 Sic 'Em
//Summon 3 minions from your deck with the highest attack.
//从你的牌库中召唤攻击力最高的三个随从。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}