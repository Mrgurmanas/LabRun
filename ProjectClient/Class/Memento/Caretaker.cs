using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Memento
{
    public class Caretaker
    {
        MementoClass memento;
        public MementoClass Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
