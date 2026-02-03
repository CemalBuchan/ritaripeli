using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Ravintola : IKauppa
    {
        private List<TavaraJaHinta> tavarat = new()
        {
            new TavaraJaHinta(new Food("Leipä", 3, 0.5f, 0.3f), 2),
            new TavaraJaHinta(new Food("Lihapata", 6, 1.0f, 0.6f), 5)
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
