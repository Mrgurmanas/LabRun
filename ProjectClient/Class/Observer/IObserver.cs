using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Observer
{
    interface IObserver
    {
        public void Update();
        public void NotifyServer();
        public void SetServer(Subject subject);
    }
}
