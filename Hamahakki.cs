using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Hamahakki : Hirviö
    {
        public Hamahakki() : base("Hamahakki", 3)
        {
        }

        public override int AnnaVahinko()
        {
            return 2;
        }

        public override void OtaVahinkoa(int määrä)
        {
            Osumapisteet -= määrä;
        }
    }
}
