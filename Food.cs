using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Food : Tavara
    {
        public int Health { get; }

        public Food(string name, int health, float weight, float volume)
            : base(weight, volume, name)
        {
            Health = health;
        }
    }
}
