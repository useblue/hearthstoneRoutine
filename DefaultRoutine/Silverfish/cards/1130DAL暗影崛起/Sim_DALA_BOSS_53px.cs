namespace HREngine.Bots
{
	class Sim_DALA_BOSS_53px : SimTemplate //* 布吉舞步 Boogie Woogie
//[x]<b>Hero Power</b>Give a friendly minion "<b>Deathrattle:</b> Resummon this minion."
//<b>英雄技能</b>使一个友方随从获得“<b>亡语：</b>再次召唤该随从。” 
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