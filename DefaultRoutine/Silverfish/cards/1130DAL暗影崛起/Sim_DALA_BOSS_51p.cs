namespace HREngine.Bots
{
	class Sim_DALA_BOSS_51p : SimTemplate //* 修补匠 Tinker
//<b>Hero Power</b>Summon a 0/4 Squirrel Bomb.
//<b>英雄技能</b>召唤一个0/4的松鼠炸弹。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}