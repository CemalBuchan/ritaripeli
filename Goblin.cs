using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Goblin : Hirviö
    {
        public Goblin() : base("Goblin", 5)
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
