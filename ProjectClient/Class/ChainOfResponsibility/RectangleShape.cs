using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectClient.Class.ChainOfResponsibility
{
    class RectangleShape : DrawShapeHandler
    {
        private const int WALL_ID = 1;
        private const int PLAYER_ID = 2;
        private const int SPECIAL_WALL_ID = 10;
        private const int DESTROYER_ID = 40;

        public const int DEFAULT_SPECIAL_WALL_ID = 10;
        public const int UPGRADED_SPECIAL_WALL_ID = 100;
        public const int ULTIMATE_SPECIAL_WALL_ID = 1000;

        public override void DrawShape(int blockId, int itemId, int i, int j)
        {
            GameLevel gameLevel = GameLevel.getInstance();
            GameMap gameMap = gameLevel.GetGameMap();
            Graphics g = gameMap.g;
            Rectangle rectangle = new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);

            switch (blockId)
            {
                case PLAYER_ID:
                    g.FillRectangle(Brushes.GreenYellow, rectangle);
                    break;
                case WALL_ID:
                    g.FillRectangle(Brushes.Blue, rectangle);
                    break;
                case SPECIAL_WALL_ID:
                    if (itemId == DEFAULT_SPECIAL_WALL_ID)
                    {
                        g.FillRectangle(Brushes.DeepSkyBlue, rectangle);
                    }
                    else if (itemId == UPGRADED_SPECIAL_WALL_ID)
                    {
                        g.FillRectangle(Brushes.DarkBlue, rectangle);
                    }
                    else if (itemId == ULTIMATE_SPECIAL_WALL_ID)
                    {
                        g.FillRectangle(Brushes.BlueViolet, rectangle);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.AliceBlue, rectangle);
                    }
                    break;
                case DESTROYER_ID:
                    g.FillRectangle(Brushes.DarkMagenta, rectangle);
                    break;
                default:
                    if(next != null)
                    {
                        next.DrawShape(blockId, itemId, i, j);
                    }
                    break;
            }
        }
    }
}
