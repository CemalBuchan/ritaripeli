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
            Random rnd = new Random();
          

            int hirvionumber = rnd.Next(hirviot.Count);
            Hirviö vastustaja;
            if (hirvionumber == 0)
            {
                 vastustaja = new Goblin();
            }
            else 
            {
                vastustaja = new Hamahakki();
            }
           


            Print.LineColor($"Taistelu alkaa! Vastassasi on {vastustaja.Nimi}", ConsoleColor.Red);

            while (vastustaja.Osumapisteet > 0 && pelaaja.Osumapisteet > 0)
            {
                Print.Line("");
                Print.Line($"Sinä: {pelaaja.Osumapisteet} OP | Hirviö: {vastustaja.Osumapisteet} OP");
                Print.Line("Valitse toiminto:");
                Print.Line("1 - Hyökkää");
                Print.Line("2 - Käytä esinettä");
                Print.Line("3 - Pakene");

                string valinta = Console.ReadLine();

                if (valinta == "1")
                {
                    int vahinko = pelaaja.Hyokkaa();
                    vastustaja.OtaVahinkoa(vahinko);
                    Print.Line($"Iskit hirviötä ja aiheutit {vahinko} vahinkoa.");
                }
                else if (valinta == "2")
                {
                    var tavarat = pelaaja.Reppu.Tavarat;

                    if (tavarat.Count == 0)
                    {
                        Print.Line("Reppu on tyhjä.");
                        continue;
                    }

                    Print.Line("Valitse esine:");
                    for (int i = 0; i < tavarat.Count; i++)
                    {
                        Print.Line($"{i} - {tavarat[i].Name}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out int esineIndex))
                        continue;

                    if (esineIndex < 0 || esineIndex >= tavarat.Count)
                        continue;

                    Tavara valittu = tavarat[esineIndex];

                    if (valittu is Food ruoka)
                    {
                        pelaaja.Paranna(ruoka.Health);
                        pelaaja.Reppu.PoistaTavara(ruoka);
                        Print.Line($"Söit ruokaa ja paranit {ruoka.Health} OP.");
                    }
                    else if (valittu is Nuoli nuoli)
                    {
                        vastustaja.OtaVahinkoa(nuoli.Vahinko);
                        pelaaja.Reppu.PoistaTavara(nuoli);
                        Print.Line($"Ammuit nuolen ja aiheutit {nuoli.Vahinko} vahinkoa.");
                    }
                    else
                    {
                        Print.Line("Tätä esinettä ei voi käyttää taistelussa.");
                    }
                }
                else if (valinta == "3")
                {
                    Print.LineColor("Pakenit taistelusta!", ConsoleColor.Yellow);
                    return; 
                }
                else
                {
                    Print.Line("Virheellinen valinta.");
                    continue;
                }

                if (vastustaja.Osumapisteet > 0)
                {
                    int hirvionVahinko = vastustaja.AnnaVahinko();
                    pelaaja.OtaVahinkoa(hirvionVahinko);
                    Print.Line($"Hirviö iski sinua ({hirvionVahinko} vahinkoa).");
                }
            }

            if (pelaaja.Osumapisteet > 0)
            {
                int palkinto = rnd.Next(3, 7);
                pelaaja.Rahapussi.LisääRahaa(palkinto);
                Print.LineColor($"Voitit taistelun! Sait {palkinto} kultaa.", ConsoleColor.Green);
            }
            else
            {
                Print.LineColor("Sinut kukistettiin taistelussa.", ConsoleColor.Red);
            }
        }


        public void KauppaTila()
        {
            Print.Line("Valitse kauppa:");

            for (int i = 0; i < kaupat.Count; i++)
            {
                Print.Line($"{i} - {kaupat[i].GetType().Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out int kauppaIndex) ||
                kauppaIndex < 0 || kauppaIndex >= kaupat.Count)
            {
                Print.LineColor("Virheellinen valinta.", ConsoleColor.Red);
                return;
            }

            var kauppa = kaupat[kauppaIndex];
            var tavarat = kauppa.ListaaTavarat();

            Print.Line("Myynnissä olevat tavarat:");
            for (int i = 0; i < tavarat.Count; i++)
            {
                Print.Line($"{i} - {tavarat[i].Esine.Name} ({tavarat[i].Hinta} kr)");
            }

            Print.Line("Valitse ostettava tavara:");
            if (!int.TryParse(Console.ReadLine(), out int tavaraIndex) ||
                tavaraIndex < 0 || tavaraIndex >= tavarat.Count)
            {
                Print.LineColor("Virheellinen valinta.", ConsoleColor.Red);
                return;
            }

            var ostettu = kauppa.OstaTavara(tavaraIndex, pelaaja.Rahapussi);

            if (ostettu != null)
            {
                if (ostettu is Ase ase)
                {
                    pelaaja.AseGet(ase);
                    pelaaja.Reppu.LisaaTavara(ase);
                    Print.LineColor($"Osto onnistui! Kuusasit silhan: {ase.Name} ({ase.Vahinko} vahinkoa)", ConsoleColor.Green);
                }
                else
                {
                    bool added = pelaaja.Reppu.LisaaTavara(ostettu);
                    if (added)
                        Print.LineColor("Osto onnistui!", ConsoleColor.Green);
                    else
                        Print.LineColor("Tavaraa ei voitu lisätä reppuun.", ConsoleColor.Red);
                }
            }
            else
            {
                Print.LineColor("Osto epäonnistui. Rahaa ei ehkä ollut tarpeeksi.", ConsoleColor.Red);
            }
        }



        private void KaytaTavaraa()
        {
            var tavarat = pelaaja.Reppu.Tavarat;

            if (tavarat.Count == 0)
            {
                Print.Line("Reppu on tyhjä.");
                return;
            }

            Print.Line("Repun tavarat:");
            for (int i = 0; i < tavarat.Count; i++)
            {
                Print.Line($"{i} - {tavarat[i].Name}");
            }

            if (!int.TryParse(Console.ReadLine(), out int valinta))
                return;

            if (valinta < 0 || valinta >= tavarat.Count)
                return;

            if (tavarat[valinta] is Food ruoka)
            {
                pelaaja.Paranna(ruoka.Health);
                pelaaja.Reppu.PoistaTavara(ruoka);
                Print.LineColor("Söit ruokaa ja paranit.", ConsoleColor.Green);
            }
        }


    }
}
