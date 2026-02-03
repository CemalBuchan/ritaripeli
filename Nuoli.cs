using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Nuoli : Tavara
    {
        public int Vahinko { get; }

        public Nuoli(string name, int vahinko, float weight, float volume)
            : base(weight, volume, name)
        {
            Vahinko = vahinko;
        }
    }
}
