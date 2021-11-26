using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.State
{
    class GameRounds
    {
        private RoundState _round = null;
        public GameMap gameMap = GameLevel.getInstance().GetGameMap();

        public CasualRound casualRound = new CasualRound();
        public SpecialItemRound specialItemRound = new SpecialItemRound();

        public GameRounds()
        {
            _round = new StartRound();
            _round.SetContext(this);
        }

        public void TransitionTo(RoundState round)
        {
            this._round = round;
            this._round.SetContext(this);
        }

        public void SpawnItems()
        {
            this._round.SetupRound();
            //this._round.SpawnItems();
        }
    }
}
