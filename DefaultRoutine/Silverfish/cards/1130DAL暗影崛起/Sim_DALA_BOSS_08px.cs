namespace HREngine.Bots
{
	class Sim_DALA_BOSS_08px : SimTemplate //* 苹果投掷 Apple Toss
//[x]<b>Hero Power</b>Deal $2 damage to thelowest Health enemy.If it dies, repeat this.
//<b>英雄技能</b>对生命值最低的敌人造成$2点伤害。如果该敌人死亡，则重复此效果。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_OR_ENEMY_HERO),
            };
        }
	}
}