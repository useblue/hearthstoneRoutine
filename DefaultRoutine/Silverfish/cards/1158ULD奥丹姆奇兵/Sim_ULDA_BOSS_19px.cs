namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_19px : SimTemplate //* 泰坦仪式 Titan Ritual
//[x]<b>Hero Power</b>Summon two1/1 Mogu Cultists.
//<b>英雄技能</b>召唤两个1/1的魔古信徒。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}