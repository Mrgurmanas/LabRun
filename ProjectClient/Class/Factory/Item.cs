using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Observer;
using ProjectClient.Class.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Factory
{
    class Item : GraphicalElement, IObserver
    {
        /*private const int SPECIAL_WALL_ID = 1;
        private const int SPIKES_ID = 2;
        private const int COIN_ID = 3;
        private const int DESTROYER_ID = 4;*/

        public const int DEFAULT_SPECIAL_WALL_ID = 10;
        public const int UPGRADED_SPECIAL_WALL_ID = 100;
        public const int ULTIMATE_SPECIAL_WALL_ID = 1000;
        public const int DEFAULT_SPIKES_ID = 20;
        public const int UPGRADED_SPIKES_ID = 200;
        public const int ULTIMATE_SPIKES_ID = 2000;
        public const int COIN_ID = 3;
        public const int DESTROYER_ID = 40;

        public int State { get; set; }
        public bool Visibility { get; set; }

        public Subject subject;

        public Algorithm itemActivatedAlgorithm;

        public void SetAlgorithm(Algorithm algorithm)
        {
            itemActivatedAlgorithm = algorithm;
        }

        public Algorithm GetAlgorithm()
        {
            return itemActivatedAlgorithm;
        }

        public int GetSpecialItemId()
        {
            if (this is DefaultSpecialWall)
            {
                return DEFAULT_SPECIAL_WALL_ID;
            }

            if (this is UpgradedSpecialWall)
            {
                return UPGRADED_SPECIAL_WALL_ID;
            }

            if (this is UltimateSpecialWall)
            {
                return ULTIMATE_SPECIAL_WALL_ID;
            }

            if (this is DefaultSpikes)
            {
                return DEFAULT_SPIKES_ID;
            }

            if (this is UpgradedSpikes)
            {
                return UPGRADED_SPIKES_ID;
            }

            if (this is UltimateSpikes)
            {
                return ULTIMATE_SPIKES_ID;
            }

            if (this is Coin)
            {
                return COIN_ID;
            }

            if (this is Destroyer)
            {
                return DESTROYER_ID;
            }
            return -1;
        }

        public void ItemActivated()
        {
            itemActivatedAlgorithm.ItemActivated();
        }

        public void UseItem()
        {
            
            if(this is Destroyer)
            {
                NotifyServer();
            }
        }

        public void NotifyServer()
        {
            subject.NotifyAll();
        }

        public void SetServer(Subject subject)
        {
            this.subject = subject;
        }

        public void Update()
        {
            //TODO: what to do when observer send message 
        }
    }
}
