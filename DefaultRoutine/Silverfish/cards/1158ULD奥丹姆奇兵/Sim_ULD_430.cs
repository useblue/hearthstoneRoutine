namespace HREngine.Bots
{
	class Sim_ULD_430 : SimTemplate //* 沙漠之矛 Desert Spear
	{
		//After your hero attacks, summon a 1/1 Locust with <b>Rush</b>.
		//在你的英雄攻击后，召唤一只1/1并具有<b>突袭</b>的蝗虫。
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ULD_430);
			p.equipWeapon(weapon, ownplay);
		}

	}
}