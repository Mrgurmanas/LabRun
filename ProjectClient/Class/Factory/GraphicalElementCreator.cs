using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Factory
{
    class GraphicalElementCreator : Creator
    {
        private const string PLAYER = "P";
        private const string COIN = "C";
        private const string ITEM = "I";
        private const string WALL = "W";
        
        public override GraphicalElement FactoryMethod(string element)
        {
            switch (element)
            {
                case PLAYER:
                    return new Player();
                case COIN:
                    return new Coin();
                case ITEM:
                    return new Item();
                case WALL:
                    return new Wall();
            }
            return null;
        }
    }
}
