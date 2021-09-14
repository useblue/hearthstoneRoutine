namespace HREngine.Bots
{
	class Sim_DRG_035 : SimTemplate //* 血帆飞贼 Bloodsail Flybooter
	{
		//<b>Battlecry:</b> Add two 1/1 Pirates to your hand.
		//<b>战吼：</b>将两张1/1的海盗牌置入你的手牌。
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		
        {
			p.drawACard(CardDB.cardNameEN.skypirate, own.own, true);
			p.drawACard(CardDB.cardNameEN.skypirate, own.own, true);
        }

	}
}