using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.ChainOfResponsibility
{
    public abstract class DrawShapeHandler
    {
        protected DrawShapeHandler next;
        protected const int BLOCK_SIZE = 45;

        public void SetNext(DrawShapeHandler next)
        {
            this.next = next;
        }

        public abstract void DrawShape(int blockId, int itemId, int  i, int j);
    }
}
