namespace HREngine.Bots
{
	class Sim_DALA_BOSS_30p : SimTemplate //* 鼠王的故事 A Tale of Kings
//<b>Hero Power</b>Summon an Underbelly Rat.
//<b>英雄技能</b>召唤一个下水道老鼠。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}