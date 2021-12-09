using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectClient.Class.ChainOfResponsibility
{
    class TriangleShape : DrawShapeHandler
    {
        private const int SPIKES_ID = 20;
        public const int DEFAULT_SPIKES_ID = 20;
        public const int UPGRADED_SPIKES_ID = 200;
        public const int ULTIMATE_SPIKES_ID = 2000;

        public override void DrawShape(int blockId, int itemId, int i, int j)
        {
            GameLevel gameLevel = GameLevel.getInstance();
            GameMap gameMap = gameLevel.GetGameMap();
            Graphics g = gameMap.g;
            PointF point1 = new PointF(i * BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
            PointF point2 = new PointF((i * BLOCK_SIZE) + (BLOCK_SIZE / 2), (j * BLOCK_SIZE));
            PointF point3 = new PointF((i * BLOCK_SIZE) + BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
            PointF[] triangle = new PointF[3];
            triangle[0] = point1;
            triangle[1] = point2;
            triangle[2] = point3;

            switch (blockId)
            {
                case SPIKES_ID:
                    if (itemId == DEFAULT_SPIKES_ID)
                    {
                        g.FillPolygon(Brushes.DeepSkyBlue, triangle);
                    }
                    else if (itemId == UPGRADED_SPIKES_ID)
                    {
                        g.FillPolygon(Brushes.DarkBlue, triangle);
                    }
                    else if (itemId == ULTIMATE_SPIKES_ID)
                    {
                        g.FillPolygon(Brushes.BlueViolet, triangle);
                    }
                    else
                    {
                        g.FillPolygon(Brushes.White, triangle);
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
    }
}
