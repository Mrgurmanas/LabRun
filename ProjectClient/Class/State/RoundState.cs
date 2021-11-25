using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.State
{
    abstract class RoundState
    {
        protected GameRounds _gameRounds;

        public void SetContext(GameRounds gameRounds)
        {
            _gameRounds = gameRounds;
        }

        public  abstract void SpawnItems();
    }
}
