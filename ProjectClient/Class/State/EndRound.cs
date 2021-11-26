using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectClient.Class.State
{
    class EndRound : RoundState
    {
        public override void SpawnItems()
        {
            GameMap gameMap = _gameRounds.gameMap;
            //spawn coin
            Coin coin = new Coin();
            gameMap.RandomSpawnItem(coin);

           // _gameRounds.TransitionTo(casualRound);
        }

        public override void UpdateDesign()
        {
            GameMap gameMap = _gameRounds.gameMap;
            gameMap.SetTextColor(Color.Violet);
        }
    }
}
