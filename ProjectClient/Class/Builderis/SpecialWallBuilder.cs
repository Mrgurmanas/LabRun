using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProjectClient.Class.Builderis
{
    class SpecialWallBuilder : Builder
    {

        public void AddColor(Brush color)
        {
            specialWall.setColor(color);
        }

        public void addRectangle(Rectangle rectangle)
        {
            specialWall.setShape(rectangle);
        }

        public override Builder addVisuals(Brush color)
        {
            AddColor(color);
            return this;
        }

        public override Builder addShape(Rectangle rectangle)
        {
            addRectangle(rectangle);
            return this;
        }
    }
}
