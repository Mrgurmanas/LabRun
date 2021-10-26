using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Strategy
{
    abstract class Algorithm
    {
        public abstract void ItemActivated(Player player, Item item);
    }
}
