using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProjectClient.Class.Builderis
{
    class SpikesBuilder : Builder
    {
        public void AddColor(Brush color)
        {
            specialWall.setColor(color);
        }

        public void addRectangle(Rectangle shape)
        {
            specialWall.setShape(shape);
        }

        public override Builder addVisuals(Brush color)
        {
            AddColor(color);
            return this;
        }

        public override Builder addShape(Rectangle triangle)
        {
            addShape(triangle);
            return this;
        }
    }
}
