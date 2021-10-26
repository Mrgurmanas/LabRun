using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Strategy
{
    class PlayerSpecialWallAlgorithm : Algorithm
    {
        public override void ItemActivated(Player player, Item item)
        {
            GameRound gr = GameRound.getInstance();
            GameMap gm = gr.GetGameMap();
            if (player.ConnectionId == item.PlayerConnection)
            {
                gm.AddPlayerPoints(0, item.PlayerConnection);
            }
        }
    }
}
