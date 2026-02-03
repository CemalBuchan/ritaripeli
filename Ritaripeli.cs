using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
	internal class Ritaripeli
	{
		Ritari pelaaja;
		List<Hirviö> hirviot;
		List<IKauppa> kaupat;
		public Ritaripeli()
		{
			pelaaja = new Ritari(aloitusOsumapisteet: 10, aloitusRahat: 10);
            hirviot = new List<Hirviö>();

            hirviot.Add(new Goblin());
            hirviot.Add(new Hamahakki());


            kaupat = new List<IKauppa>
			{
			    new Ravintola(),
			    new AseKauppa(),
			    new NuoliKauppa()
			};
		}

        public void PeliSilmukka()
        {
            Print.Line("Tervetuloa suureen seikkailuun!");

            while (true)
            {
                // Pelaajan tilanne
                Print.WriteColor("Tilanne: Osumapisteitä: ", ConsoleColor.White);
                Print.WriteColor($"{pelaaja.Osumapisteet} op ", ConsoleColor.Green);
                Print.WriteColor("Kultaa: ", ConsoleColor.White);
                Print.LineColor($"{pelaaja.Rahapussi.Rahoja} kr", ConsoleColor.Yellow);

                Print.Line("");
                Print.Line("Mitä haluat tehdä?");
                Print.Line("1 - Mene kauppaan");
                Print.Line("2 - Taistele hirviötä vastaan");
                Print.Line("3 - Käytä tavaraa repusta");
                Print.Line("0 - Lopeta peli");

                string valinta = Console.ReadLine();

                switch (valinta)
                {
                    case "1":
                        KauppaTila();
                        break;

                    case "2":
                        TaisteluTila();
                        break;

                    case "3":
                        KaytaTavaraa();
                        break;

                    case "0":
                        Print.Line("Peli päättyi. Kiitos pelaamisesta!");
                        return;

                    default:
                        Print.Line("Virheellinen valinta!");
                        break;
                }

                // Pelin loppuehto: pelaaja kuolee
                if (pelaaja.Osumapisteet <= 0)
                {
                    Print.LineColor("Olet kuollut. Peli loppui.", ConsoleColor.Red);
                    return;
                }
            }
        }





        public void TaisteluTila()
		{
			// TODO arvo pelaajaa vastaan taisteleva hirviö
			Hirviö vastustaja = null;
			while (vastustaja.Osumapisteet > 0 && pelaaja.Osumapisteet > 0)
			{
				// TODO anna pelaajan valita toiminto:
				// 1. hyökkää : aiheuta vahinkoa hirviölle
				// 2. käytä esinettä ; näytä Repun sisältö ja anna pelaajan valita tavara
				// Jos pelaaja käyttää ruoka-annosta, lisää pelaajan osumapisteitä
				// Jos pelaaja käyttää nuolta, ammu nuoli kohti vihollista
				// Jos pelaaja käyttää jotain muuta tavaraa, toimi valinnan mukaan
				// 3. pakene : poistu TaisteluTilasta

				// TODO Jos hirviöllä on osumapisteitä jäljellä
				// arvo hirviön tekemä vahinko ja vähennä se pelaajan osumapisteistä
			}
			// Kun taistelu loppuu, palaa PeliSilmukkaan
		}

		public void KauppaTila()
		{
            Print.Line("Valitse kauppa:");

            for (int i = 0; i < kaupat.Count; i++)
            {
                Print.Line($"{i} - {kaupat[i].GetType().Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out int valinta))
                return;

            if (valinta < 0 || valinta >= kaupat.Count)
                return;

            var kauppa = kaupat[valinta];
            var tavarat = kauppa.ListaaTavarat();

            Print.Line("Myynnissä olevat tavarat:");
            for (int i = 0; i < tavarat.Count; i++)
            {
                Print.Line($"{i} - {tavarat[i].Esine.Name} ({tavarat[i].Hinta} kr)");
            }

            Print.Line("Valitse ostettava tavara:");
            if (!int.TryParse(Console.ReadLine(), out int tavaraIndex))
                return;

            var ostettu = kauppa.OstaTavara(tavaraIndex, pelaaja.Rahapussi);

            if (ostettu != null)
            {
                pelaaja.Reppu.addToBag(ostettu);
                Print.LineColor("Osto onnistui!", ConsoleColor.Green);
            }
            else
            {
                Print.LineColor("Osto epäonnistui.", ConsoleColor.Red);
            }
        }

        private void KaytaTavaraa()
        {

        }


    }
}
