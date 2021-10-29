using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Factory
{
    class GraphicalElementCreator : Creator
    {
        private const int SPACE_ID = 0;
        private const int WALL_ID = 1;
        private const int PLAYER_ID = 2;
        private const int COIN_ID = 3;
        private const int ITEM_ID = 4;

        public override GraphicalElement FactoryMethod(int elementId)
        {
            switch (elementId)
            {
                case PLAYER_ID:
                    return new Player();
                case ITEM_ID:
                    return new Item();
                case WALL_ID:
                    return new Wall();
            }
            return null;
        }
    }
}
