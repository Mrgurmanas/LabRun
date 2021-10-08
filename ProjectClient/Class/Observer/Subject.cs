using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Observer
{
    abstract class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver item)
        {
            observers.Add(item);
            item.SetConcreteSubject(this);
        }

        public void Deattach(IObserver item)
        {
            observers.Remove(item);
        }

        public void NotifyAll()
        {
            //TODO: 
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}
