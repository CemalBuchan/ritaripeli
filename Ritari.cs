using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
	internal class Ritari
	{
        public int Osumapisteet { get; private set; }
        public Reppu Reppu { get; private set; }
        public Lompakko Rahapussi { get; private set; }

        public Ase? UsedAse { get; private set; }

        public Ritari(int aloitusOsumapisteet, int aloitusRahat)
        {
            Osumapisteet = aloitusOsumapisteet;
            Rahapussi = new Lompakko(aloitusRahat);
            Reppu = new Reppu();

            UsedAse = new Ase("Miekka", 2, 2.5f, 1.0f);
            Reppu.LisaaTavara(UsedAse);
        }

        public void AseGet(Ase ase)
        {
            UsedAse = ase;
        }

        public int Hyokkaa()
        {
            if (UsedAse == null)
                return 1; 

            return UsedAse.Vahinko;
        }

        public void OtaVahinkoa(int määrä)
        {
            if (määrä < 0) return;

            Osumapisteet -= määrä;
            if (Osumapisteet < 0)
                Osumapisteet = 0;
        }

        public void Paranna(int määrä)
        {
            if (määrä > 0)
                Osumapisteet += määrä;
        }

    }
}
