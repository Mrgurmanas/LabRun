using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.State
{
    class SpecialItemRound : RoundState
    {
        private int count = 0;

        public override void SpawnItems()
        {
            GameMap gameMap = _gameRounds.gameMap;
            CasualRound casualRound = _gameRounds.casualRound;
            count += 1;

            // spawn coin
            Coin coin = new Coin();
            gameMap.RandomSpawnItem(coin);
            
            //spawn random special item
            gameMap.RandomSpawnSpecialItem();

            if (count >= 9)
            {
                _gameRounds.TransitionTo(new EndRound());
            }
            else
            {
                _gameRounds.TransitionTo(casualRound);
            }
        }
    }
}
