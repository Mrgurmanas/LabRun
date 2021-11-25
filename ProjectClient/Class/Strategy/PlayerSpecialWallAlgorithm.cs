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
            GameLevel gr = GameLevel.getInstance();
            GameMap gm = gr.GetGameMap();
            if (player.ConnectionId == item.PlayerConnection) {
                if (player.inventory.CanAddItem())
                {
                    gm.connection.AddPlayerItem(item.GetSpecialItemId(), player.ConnectionId, gm.groupName);
                }
            }
        }
    }
}
