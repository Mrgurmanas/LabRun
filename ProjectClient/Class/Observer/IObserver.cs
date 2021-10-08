using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Observer
{
    interface IObserver
    {
        public void Update();
        public void NotifyConcreteSubject();
        public void SetConcreteSubject(Subject subject);
    }
}
