using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Factory
{
    class Player : GraphicalElement
    {
        public int Color { get; set; }
        public Inventory inventory = new Inventory();
    }
}
