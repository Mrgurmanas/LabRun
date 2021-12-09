using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace ProjectClient.Class.ChainOfResponsibility
{
    class InventoryShape : DrawShapeHandler
    {
        private const int PLAYER1_INV1_ID = 101;
        private const int PLAYER1_INV2_ID = 102;
        private const int PLAYER1_INV3_ID = 103;
        private const int PLAYER2_INV1_ID = 201;
        private const int PLAYER2_INV2_ID = 202;
        private const int PLAYER2_INV3_ID = 203;

        public const int PLAYER_1_ID = 1;
        public const int PLAYER_2_ID = 2;

        Graphics g;

        public override void DrawShape(int blockId, int itemId, int i, int j)
        {
            GameLevel gameLevel = GameLevel.getInstance();
            GameMap gameMap = gameLevel.GetGameMap();
            g = gameMap.g;

            switch (blockId)
            {
                case PLAYER1_INV1_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 0);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 0);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                case PLAYER1_INV2_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 1);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 1);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                case PLAYER1_INV3_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 2);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 2);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                case PLAYER2_INV1_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 0);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 0);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                case PLAYER2_INV2_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 1);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 1);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                case PLAYER2_INV3_ID:
                    if (gameMap.MainPlayer)
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_2_ID, 2);
                    }
                    else
                    {
                        itemId = gameMap.GetPlayerItem(PLAYER_1_ID, 2);
                    }
                    if (itemId != -1)
                    {
                        DrawItem(itemId, i, j);
                    }
                    break;
                default:
                    if (next != null)
                    {
                        next.DrawShape(blockId, itemId, i, j);
                    }
                    break;
            }
        }

        public void DrawItem(int itemId, int i, int j)
        {
            Rectangle rectangle = new Rectangle();
            PointF[] triangle = new PointF[3];

            rectangle = new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);

            PointF point1 = new PointF(i * BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
            PointF point2 = new PointF((i * BLOCK_SIZE) + (BLOCK_SIZE / 2), (j * BLOCK_SIZE));
            PointF point3 = new PointF((i * BLOCK_SIZE) + BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
            triangle[0] = point1;
            triangle[1] = point2;
            triangle[2] = point3;

            switch (itemId)
            {
                case Item.DEFAULT_SPECIAL_WALL_ID:
                    g.FillRectangle(Brushes.DeepSkyBlue, rectangle);
                    break;
                case Item.UPGRADED_SPECIAL_WALL_ID:
                    g.FillRectangle(Brushes.DarkBlue, rectangle);
                    break;
                case Item.ULTIMATE_SPECIAL_WALL_ID:
                    g.FillRectangle(Brushes.DarkMagenta, rectangle);
                    break;
                case Item.DEFAULT_SPIKES_ID:
                    g.FillPolygon(Brushes.DeepSkyBlue, triangle);
                    break;
                case Item.UPGRADED_SPIKES_ID:
                    g.FillPolygon(Brushes.DarkBlue, triangle);
                    break;
                case Item.ULTIMATE_SPIKES_ID:
                    g.FillPolygon(Brushes.DarkMagenta, triangle);
                    break;
                case Item.COIN_ID:
                    break;
                case Item.DESTROYER_ID:
                    g.FillRectangle(Brushes.DarkMagenta, rectangle);
                    break;
            }
        }
    }
}
