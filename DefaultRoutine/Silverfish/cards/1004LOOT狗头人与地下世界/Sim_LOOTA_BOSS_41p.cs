namespace HREngine.Bots
{
	class Sim_LOOTA_BOSS_41p : SimTemplate //* 加入战斗 Join the Fray
//<b>Hero Power</b>Both players <b>Recruit</b> a_minion.
//<b>英雄技能</b>双方玩家各<b>招募</b>一个随从。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}