namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_04px : SimTemplate //* 喷射 Belch
//[x]<b>Hero Power</b>Summon a minionthat died this game.
//<b>英雄技能</b>召唤一个在本局对战中死亡的随从。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}