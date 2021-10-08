using ProjectClient.Class.Observer;
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

        public void UseItem()
        {
            
            if(this is Destroyer)
            {
                NotifyConcreteSubject();
            }
        }

        public void NotifyConcreteSubject()
        {
            subject.NotifyAll();
        }

        public void SetConcreteSubject(Subject subject)
        {
            this.subject = subject;
        }

        public void Update()
        {
            //TODO: what to do when observer send message
        }
    }
}
