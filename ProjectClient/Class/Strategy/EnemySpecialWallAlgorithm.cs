using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Strategy
{
    class EnemySpecialWallAlgorithm : Algorithm
    {
        public override void ItemActivated(Player player, Item item)
        {
            GameLevel gr = GameLevel.getInstance();
            GameMap gm = gr.GetGameMap();
            int x = item.X;
            int y = item.Y;
            if (player.ConnectionId != item.PlayerConnection)
            {
                player.Freezed = true;
                if (item is DefaultSpecialWall)
                {
                    item.State = Item.STATE_DESTROYED;
                    gm.connection.SpawnSpecialItem(x, y, item.GetSpecialItemId(), item.PlayerConnection, gm.groupName);
                }
                else if (item is UpgradedSpecialWall)
                {
                    item = new DefaultSpecialWall();
                    gm.connection.SpawnSpecialItem(x, y, item.GetSpecialItemId(), item.PlayerConnection, gm.groupName);
                }
                else if (item is UltimateSpecialWall)
                {
                    item = new UpgradedSpecialWall();
                    gm.connection.SpawnSpecialItem(x, y, item.GetSpecialItemId(), item.PlayerConnection, gm.groupName);
                }
            }
        }
    }
}
