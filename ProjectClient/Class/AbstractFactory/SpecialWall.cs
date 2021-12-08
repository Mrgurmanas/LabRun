using System;
using System.Collections.Generic;
using System.Text;
using ProjectClient.Class.Factory;
using System.Drawing;

namespace ProjectClient.Class.AbstractFactory
{
    abstract class SpecialWall : Item
    {
        public abstract int Value { get; }

        private Brush color;
        private Rectangle shape;

        public Brush getColor()
        {
            return color;
        }

        public Rectangle getShape()
        {
            return shape;
        }

        public void setColor(Brush color)
        {
            this.color = color;
        }

        public void setShape(Rectangle shape)
        {
            this.shape = shape;
        }
    }
}
