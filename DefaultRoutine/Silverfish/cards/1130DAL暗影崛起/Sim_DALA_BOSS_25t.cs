namespace HREngine.Bots
{
	class Sim_DALA_BOSS_25t : SimTemplate //* 玩具收藏 Hoard of Toys
//Summon 1/1 copies of five random minions.
//随机召唤五个随从的1/1复制。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}