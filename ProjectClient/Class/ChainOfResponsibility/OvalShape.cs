using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectClient.Class.ChainOfResponsibility
{
    class OvalShape : DrawShapeHandler
    {
        private const int COIN_ID = 3;

        public override void DrawShape(int blockId, int itemId, int i, int j)
        {
            GameLevel gameLevel = GameLevel.getInstance();
            GameMap gameMap = gameLevel.GetGameMap();
            Graphics g = gameMap.g;
            Rectangle rectangle = new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);

            switch (blockId)
            {
                case COIN_ID:
                    g.FillEllipse(Brushes.Yellow, rectangle);
                    break;
                default:
                    if (next != null)
                    {
                        next.DrawShape(blockId, itemId, i, j);
                    }
                    break;
            }
        }
    }
}
