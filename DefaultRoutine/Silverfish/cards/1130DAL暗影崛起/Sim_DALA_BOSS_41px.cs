namespace HREngine.Bots
{
	class Sim_DALA_BOSS_41px : SimTemplate //* 召唤伙伴 Summon Companions
//<b>Hero Power</b>Summon two randomAnimal Companions.
//<b>英雄技能</b>随机召唤两个动物伙伴。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_OR_ENEMY_HERO),
            };
        }
	}
}