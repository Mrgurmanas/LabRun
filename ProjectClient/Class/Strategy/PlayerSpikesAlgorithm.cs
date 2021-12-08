using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Strategy
{
    class PlayerSpikesAlgorithm : Algorithm
    {
        public override void ItemActivated(Player player, Item item)
        {
            GameLevel gr = GameLevel.getInstance();
            GameMap gm = gr.GetGameMap();
            if(player.ConnectionId == item.PlayerConnection)
            {
                gm.AddPlayerPoints(-(item as Spikes).Value / 10, player.ConnectionId);
            }
        }
    }
}
