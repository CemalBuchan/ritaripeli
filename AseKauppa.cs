using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class AseKauppa : IKauppa
    {
        private List<TavaraJaHinta> tavarat = new()
    {
        new TavaraJaHinta(new Ase("Parempi miekka", 3, 3f, 1.2f), 6),
        new TavaraJaHinta(new Ase("Sotakirves", 4, 4f, 1.5f), 20)
    };

        public List<TavaraJaHinta> ListaaTavarat() => tavarat;

        public Tavara? OstaTavara(int valittuTavara, Lompakko rahapussi)
        {
            if (valittuTavara < 0 || valittuTavara >= tavarat.Count)
                return null;

            var t = tavarat[valittuTavara];

            if (rahapussi.OtaRahaa(t.Hinta) == 0)
                return null;

            return t.Esine;
        }
    }

}
