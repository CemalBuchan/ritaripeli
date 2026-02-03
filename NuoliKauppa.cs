using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class NuoliKauppa : IKauppa
    {
        private List<TavaraJaHinta> tavarat = new()
    {
        new TavaraJaHinta(new Nuoli("Puunuoli", 2, 0.1f, 0.05f), 1),
        new TavaraJaHinta(new Nuoli("Teräsnuoli", 4, 0.15f, 0.07f), 3)
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
