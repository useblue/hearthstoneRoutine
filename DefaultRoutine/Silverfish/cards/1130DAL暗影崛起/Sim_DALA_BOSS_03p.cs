namespace HREngine.Bots
{
	class Sim_DALA_BOSS_03p : SimTemplate //* 神奇魔术 Prestidigitation
//<b>Hero Power</b>Add a random magical feat to your hand.
//<b>英雄技能</b>随机将一张神奇的魔术牌置入你的手牌。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_OR_ENEMY_HERO),
            };
        }
	}
}