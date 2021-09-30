using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.AbstractFactory
{
    abstract class Spikes : Item
    {
       public abstract int Value { get; }
    }
}
