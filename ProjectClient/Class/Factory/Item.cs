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
