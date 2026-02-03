using ritaripeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Ase : Tavara
{
    public int Vahinko { get; }

    public Ase(string name, int vahinko, float weight, float volume)
        : base(weight, volume, name)
    {
        Vahinko = vahinko;
    }
}
