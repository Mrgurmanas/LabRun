using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Factory
{
    abstract class Creator
    {
        public abstract GraphicalElement FactoryMethod(int elementId);
    }
}
