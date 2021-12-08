using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectClient.Class.State
{
    class CasualRound : RoundState
    {
        public override void SpawnItems()
        {
            GameMap gameMap = _gameRounds.gameMap;
            SpecialItemRound specialItemRound = _gameRounds.specialItemRound;
            //spawn coin
            Coin coin = new Coin();
            gameMap.RandomSpawnItem(coin);

            _gameRounds.TransitionTo(specialItemRound);
        }

        public override void UpdateDesign()
        {
            GameMap gameMap = _gameRounds.gameMap;
            gameMap.SetTextColor(Color.Blue);
        }
    }
}
