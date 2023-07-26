using System;
using System.IO;
using System.Numerics;
using System.Text;


namespace Kalkulačka
{
	class Program
	{
		static void Main(string[] args)
		{   //											PRIKAZ NA UKAZOVANIE ZNAKOV
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			//											HLAVNÉ TELO
			PRAVIDLÁ_POUŽÍVANIA_PROGRAMU();
			HLAVNÁ_PONUKA();
			//HRA_TIK_TAK_TOU();
			KONIEC_PROGRAMU(); COPYRIGHT();
		}
		public static class FastConsole
		{
			static readonly BufferedStream str;

			static FastConsole()
			{
				Console.OutputEncoding = Encoding.Unicode;  // crucial

				// avoid special "ShadowBuffer" for hard-coded size 0x14000 in 'BufferedStream' 
				str = new BufferedStream(Console.OpenStandardOutput(), 0x15000);
			}

			public static void WriteLine(String s) => Write(s + "\r\n");

			public static void Write(String s)
			{
				// avoid endless 'GetByteCount' dithering in 'Encoding.Unicode.GetBytes(s)'
				var rgb = new byte[s.Length << 1];
				Encoding.Unicode.GetBytes(s, 0, s.Length, rgb, 0);

				lock (str)   // (optional, can omit if appropriate)
					str.Write(rgb, 0, rgb.Length);
			}

			public static void Flush() { lock (str) str.Flush(); }
		};
		//												┌───────────────┐
		//												│< PODPROGRAMY >│
		//												└───────────────┘
		static void HLAVNÁ_PONUKA()
		{
			bool HlavnýCyklus = true;
			do {
				string odpoved = VÝBER_HLAVNÝ();
				odpoved = odpoved.ToLower();
				if (odpoved == "1") { MENU_TELESÁ(); }
				if (odpoved == "2") { VÝPOČET_REZISTOROV(); }
				if (odpoved == "3") { VÝPOČET_KONDENZÁDOR(); }
				if (odpoved == "4") { PREMENY_JEDNOTIEK_HLAVNÉ_TELO(); }
                if (odpoved == "5") { HRA_TIK_TAK_TOU(); }
                if (odpoved == "6") { HlavnýCyklus = false; }
			} while (HlavnýCyklus); }
		static void PRAVIDLÁ_POUŽÍVANIA_PROGRAMU() {
			Console.WriteLine("┌──────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Pravidlá používania programu.                        │");
			Console.WriteLine("│──────────────────────────────────────────────────────│");
			Console.WriteLine("│ 1) Pred počítaním si musíte vybrať jednu z možností. │");
			Console.WriteLine("│ Každá odpoveď musí byť gramaticky správne napísaná.  │");
			Console.WriteLine("│ Po zadaní odpovede vždy stlačte klávesu ENTER        │");
			Console.WriteLine("│──────────────────────────────────────────────────────│");
			Console.WriteLine("│ 2) Po zadaní odpovede vždy stlačte klávesu ENTER     │");
			Console.WriteLine("│──────────────────────────────────────────────────────│");
			Console.WriteLine("│ Pokiaľ si si prečítal pravidlá môžeš stlačiť ENTER   │");
			Console.WriteLine("└──────────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear(); }
		static string VÝBER_HLAVNÝ() {
			Console.WriteLine("┌──────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Hlavné menu, zvoľ si jednu z možností.               │");
			Console.WriteLine("└──────────────────────────────────────────────────────┘");
			Console.WriteLine("┌───────────────────────────┐");
			Console.WriteLine("│ Na výber sú ->            │");
			Console.WriteLine("│ Telesá       -> (1)       │");
			Console.WriteLine("│ Rezistory    -> (2)       │");
			Console.WriteLine("│ Kondenzátory -> (3)       │");
			Console.WriteLine("│ Premeny Jednotiek -> (4)  │");
            Console.WriteLine("│ Hra Tic Tac Toe   -> (5)  │");
            Console.WriteLine("│ Ukončiť Program   -> (6)  │");
			Console.WriteLine("└───────────────────────────┘");
			Console.Write("Tvoj výber -> ");
			string odpoved = Console.ReadLine();
			Console.Clear();
			return odpoved; }
		static string PONUKA_TELESÁ() 
		{
			Console.WriteLine("┌──────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Hlavné menu, zvoľ si jednu z možností.               │");
			Console.WriteLine("└──────────────────────────────────────────────────────┘");
			Console.WriteLine("┌────────────────────────────┐");
			Console.WriteLine("│ Na výber sú ->             │");
			Console.WriteLine("│ Kocka   -> (1)             │");
			Console.WriteLine("│ Kváder  -> (2)             │");
			Console.WriteLine("│ Valec   -> (3)             │");
			Console.WriteLine("│ Kužeľ   -> (4)             │");
			Console.WriteLine("│ Guľa    -> (5)             │");
			Console.WriteLine("│ Hranol  -> (6)             │");
			Console.WriteLine("└────────────────────────────┘");
			Console.WriteLine("┌────────────────────────────┐");
			Console.WriteLine("│ Pre vrátenie napíš -> Späť │");
			Console.WriteLine("└────────────────────────────┘");
			Console.Write("Tvoj výber -> ");
			string odpoved = Console.ReadLine();
			Console.Clear();
			return odpoved;
		}
		static void MENU_TELESÁ()
		{
			bool cyklus = true;
			do {
			string odpoved = PONUKA_TELESÁ();
			odpoved = odpoved.ToLower();
			if (odpoved == "1") { VÝPOČET_KOCKA(); }
			if (odpoved == "2") { VÝPOČET_KVÁDER(); }
			if (odpoved == "3") { VÝPOČET_VALEC(); }
			if (odpoved == "4") { VÝPOČET_KUŽEĽ(); }
			if (odpoved == "5") { VÝPOČET_GUĽA(); }
			if (odpoved == "6") { VÝPOČET_HRANOL(); }
			if (odpoved == "späť" | odpoved == "spať" | odpoved == "spät" | odpoved == "spat") cyklus = false; 
			} while (cyklus); }
		static void VÝPOČET_KOCKA() {
			double Kocka_Výsledok_Objem, Kocka_Výsledok_Povrch, Kocka_A;
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku strany -> A │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			Kocka_A = double.Parse(Console.ReadLine());
			Kocka_Výsledok_Objem = Kocka_A * Kocka_A * Kocka_A;
			Kocka_Výsledok_Povrch = Kocka_A * Kocka_A * 6;
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Výpočet Objemu          │");
			Console.WriteLine("└─────────────────────────┘");
			Console.WriteLine("\tS = A * A * A");
			Console.WriteLine("\tS = " + Kocka_A + " * " + Kocka_A + " * " + Kocka_A + " = " + Kocka_Výsledok_Objem);
			Console.WriteLine("\tS = " + Kocka_Výsledok_Objem);
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Výpočet Povrchu         │");
			Console.WriteLine("└─────────────────────────┘");
			Console.WriteLine("\tV = A * A * 6");
			Console.WriteLine("\tV = " + Kocka_A + " * " + Kocka_A + " * 6 = " + Kocka_Výsledok_Povrch);
			Console.WriteLine("\tV = " + Kocka_Výsledok_Povrch);
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_KVÁDER() {
			double Kváder_Výsledok_Objem, Kváder_Výsledok_Povrch;
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku strany -> A │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double Kváder_A = double.Parse(Console.ReadLine());
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku strany -> B │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double Kváder_B = double.Parse(Console.ReadLine());
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku strany -> C │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double Kváder_C = double.Parse(Console.ReadLine());
			Console.Clear();
			Kváder_Výsledok_Objem = Kváder_A * Kváder_B * Kváder_C;
			Kváder_Výsledok_Povrch = 2 * ((Kváder_A * Kváder_B) + (Kváder_B * Kváder_C) + (Kváder_C * Kváder_A));
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Výpočet Objemu          │");
			Console.WriteLine("└─────────────────────────┘");
			Console.WriteLine("\tS = A * B * C");
			Console.WriteLine("\tS = " + Kváder_A + " * " + Kváder_B + " * " + Kváder_C + " = " + Kváder_Výsledok_Objem);
			Console.WriteLine("\tS = " + Kváder_Výsledok_Objem);
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Výpočet Povrchu         │");
			Console.WriteLine("└─────────────────────────┘");
			Console.WriteLine("\tV = 2 * ((A * B) + (B * C) + (C * A))");
			Console.WriteLine("\tV = 2 * ((" + Kváder_A + " * " + Kváder_B + ") + (" + Kváder_B + " * " + Kváder_C + ") + (" + Kváder_C + " * " + Kváder_A + ")) = " + Kváder_Výsledok_Povrch);
			Console.WriteLine("\tV = " + Kváder_Výsledok_Povrch);
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_VALEC() {
			double PI = Math.PI;
			double Valec_Výsledok_Objem, Valec_Výsledok_Povrch;
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku polomeru podstáv │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double VALEC_R = double.Parse(Console.ReadLine());
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Zadaj výšku valca            │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double VALEC_V = double.Parse(Console.ReadLine());
			Console.Clear();
			Valec_Výsledok_Objem = PI * VALEC_R * VALEC_R * VALEC_V;
			Valec_Výsledok_Povrch = 2 * PI * VALEC_R * (VALEC_R + VALEC_V);
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Výpočet Objemu               │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.WriteLine("\tS = PÍ * r * r * v");
			Console.WriteLine("\tS = PÍ * " + VALEC_R + " * " + VALEC_R + " * " + VALEC_V + " = " + Valec_Výsledok_Objem);
			Console.WriteLine("\tS = " + Valec_Výsledok_Objem);
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Výpočet Povrchu              │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.WriteLine("\tV = 2 * PÍ * r * ( r + v )");
			Console.WriteLine("\tV = 2 * PÍ * " + VALEC_R + " * (" + VALEC_R + " + " + VALEC_V + " ) = " + Valec_Výsledok_Povrch);
			Console.WriteLine("\tV = " + Valec_Výsledok_Povrch);
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_KUŽEĽ() {
			double PI = Math.PI;
			double Kužeľ_Výsledok_Objem, Kužeľ_Výsledok_Povrch, Kužeľ_Výpočet_s, Kužeľ_Výpočet_1časť, Kužeľ_Výpočet_2časť;
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Zadaj dĺžku polomeru podstáv │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double KUŽEĽ_R = double.Parse(Console.ReadLine());
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Zadaj výšku kužeľu           │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double KUŽEĽ_V = double.Parse(Console.ReadLine());
			Console.Clear();
			Kužeľ_Výpočet_1časť = 1;
			Kužeľ_Výpočet_2časť = 3;
			Kužeľ_Výsledok_Objem = PI * (Kužeľ_Výpočet_1časť / Kužeľ_Výpočet_2časť) * KUŽEĽ_R * KUŽEĽ_R * KUŽEĽ_V;
			Kužeľ_Výpočet_s = Math.Sqrt((KUŽEĽ_R * KUŽEĽ_R) + (KUŽEĽ_V * KUŽEĽ_V));
			Kužeľ_Výsledok_Povrch = PI * KUŽEĽ_R * (KUŽEĽ_R + Kužeľ_Výpočet_s);
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Výpočet Objemu               │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.WriteLine("\tS = PÍ * 1/3 * r * r * v");
			Console.WriteLine("\tS = PÍ * 1/3 * " + KUŽEĽ_R + " * " + KUŽEĽ_R + " * " + KUŽEĽ_V + " = " + Kužeľ_Výsledok_Objem);
			Console.WriteLine("\tS = " + Kužeľ_Výsledok_Objem);
			Console.WriteLine("┌──────────────────────────────┐");
			Console.WriteLine("│ Výpočet Povrchu              │");
			Console.WriteLine("└──────────────────────────────┘");
			Console.WriteLine("\ts = Druhá Odmocnina z (( " + KUŽEĽ_R + " * " + KUŽEĽ_R + " ) + ( " + KUŽEĽ_V + " * " + KUŽEĽ_V + " ))");
			Console.WriteLine("\tV = PÍ * r * ( r + s )");
			Console.WriteLine("\tV = PÍ * " + KUŽEĽ_R + " * (" + KUŽEĽ_R + " + " + Kužeľ_Výpočet_s + " ) = " + Kužeľ_Výsledok_Povrch);
			Console.WriteLine("\tV = " + Kužeľ_Výsledok_Povrch);
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_GUĽA() {
			double PI = Math.PI;
			double Guľa_Výsledok_Objem, Guľa_Výsledok_Povrch, Guľa_Výpočet_1časť, Guľa_Výpočet_2časť;
			Console.WriteLine("┌─────────────────────┐");
			Console.WriteLine("│ Zadaj polomeru guľi │");
			Console.WriteLine("└─────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double GUĽA_R = double.Parse(Console.ReadLine());
			Console.Clear();
			Guľa_Výpočet_1časť = 4;
			Guľa_Výpočet_2časť = 3;
			Guľa_Výsledok_Objem = PI * ((float)Guľa_Výpočet_1časť / Guľa_Výpočet_2časť) * GUĽA_R * GUĽA_R * GUĽA_R;
			Guľa_Výsledok_Povrch = PI * 4 * GUĽA_R * GUĽA_R;
			Console.WriteLine("┌─────────────────────┐");
			Console.WriteLine("│ Výpočet Objemu      │");
			Console.WriteLine("└─────────────────────┘");
			Console.WriteLine("\tS = PÍ * 4/3 * r * r * r");
			Console.WriteLine("\tS = PÍ * 4/3 * " + GUĽA_R + " * " + GUĽA_R + " * " + GUĽA_R + " = " + Guľa_Výsledok_Objem);
			Console.WriteLine("\tS = " + Guľa_Výsledok_Objem);
			Console.WriteLine("┌─────────────────────┐");
			Console.WriteLine("│ Výpočet Povrchu     │");
			Console.WriteLine("└─────────────────────┘");
			Console.WriteLine("\tV = 4 * PÍ * r * r");
			Console.WriteLine("\tV = 4 * PÍ * " + GUĽA_R + " * " + GUĽA_R + " = " + Guľa_Výsledok_Povrch);
			Console.WriteLine("\tV = " + Guľa_Výsledok_Povrch);
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_HRANOL()
		{
			double PI = Math.PI;
			static double Cotan(double x)
			{ return 1 / Math.Tan(x); }
			double Hranol_Výsledok_Objem, Hranol_Výsledok_Povrch, Hranol_Výpočet_Sp, Hranol_Výpočet_Spl, Hranol_Výpočet_1časť, Hranol_Výpočet_2časť;
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj výšku hranola     │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double Hranol_v = double.Parse(Console.ReadLine());
			Console.WriteLine("┌─────────────────────────┐");
			Console.WriteLine("│ Zadaj počet strán       │");
			Console.WriteLine("└─────────────────────────┘");
			Console.Write("Zadaná hodnota -> ");
			double Hranol_n = double.Parse(Console.ReadLine());
			if (Hranol_n < 3) { Console.WriteLine("Hranol sa nedá vypočítať."); }
			else {
				Console.WriteLine("┌─────────────────────────┐");
				Console.WriteLine("│ Zadaj dĺžku strany -> A │");
				Console.WriteLine("└─────────────────────────┘");
				Console.Write("Zadaná hodnota -> ");
				double Hranol_a = double.Parse(Console.ReadLine());
				Console.Clear();
				Hranol_Výpočet_1časť = 1;
				Hranol_Výpočet_2časť = 4;
				Hranol_Výpočet_Spl = Hranol_n * Hranol_a * Hranol_v;
				Hranol_Výpočet_Sp = (Hranol_Výpočet_1časť / Hranol_Výpočet_2časť) * Hranol_n * Hranol_a * Hranol_a * Cotan(PI / Hranol_n);
				Hranol_Výsledok_Objem = Hranol_Výpočet_Sp * Hranol_v;
				Hranol_Výsledok_Povrch = 2 * Hranol_Výpočet_Sp + Hranol_Výpočet_Spl;
				Console.WriteLine("┌─────────────────────────┐");
				Console.WriteLine("│ Výpočet Objemu          │");
				Console.WriteLine("└─────────────────────────┘");
				Console.WriteLine("\tSp = 1/4 * n * a * a * cot( PÍ / n )");
				Console.WriteLine("\tSp = 1/4 * " + Hranol_n + " * " + Hranol_a + " * " + Hranol_a + " * cot( PÍ / " + Hranol_n + " ) = " + Hranol_Výpočet_Sp);
				Console.WriteLine("\tSpl = n * a * v");
				Console.WriteLine("\tSpl = " + Hranol_n + " * " + Hranol_a + " * " + Hranol_v + " = " + Hranol_Výpočet_Spl);
				Console.WriteLine("\tS = Sp * v");
				Console.WriteLine("\tS = " + Hranol_Výpočet_Sp + " * " + Hranol_v + " = " + Hranol_Výsledok_Objem);
				Console.WriteLine("\tS = " + Hranol_Výsledok_Objem);
				Console.WriteLine("┌─────────────────────────┐");
				Console.WriteLine("│ Výpočet Povrchu         │");
				Console.WriteLine("└─────────────────────────┘");
				Console.WriteLine("\tV = 2 * Sp + Spl");
				Console.WriteLine("\tV = 2 * " + Hranol_Výpočet_Sp + " + " + Hranol_Výpočet_Spl + " = " + Hranol_Výsledok_Povrch);
				Console.WriteLine("\tV = " + Hranol_Výsledok_Povrch); }
			Console.WriteLine("┌────────────────────────────────────────────────┐");
			Console.WriteLine("│          Pre pokračovanie stlač ENTER          │");
			Console.WriteLine("└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void VÝPOČET_REZISTOROV() {
			string Reset, Odpoveď;
			bool HlavnýCyklus = true, VedľajšíCyklus = true, PočítacíCyklus = true;
			double HlavnýRezistor = 0, Voľba = 0, VedľajšíRezistorKonvertovaný = 0;
			do {
				VedľajšíCyklus = true;
				Console.WriteLine("┌────────────────────────────────────────┐");
				Console.WriteLine("│       Vyber si jednu z možností.       │");
				Console.WriteLine("└────────────────────────────────────────┘");
				Console.WriteLine("┌────────────────────────────────────────┐");
				Console.WriteLine("│ Na výber sú ->                         │");
				Console.WriteLine("│ Sčítavanie rezistorov sériovo   -> (1) │");
				Console.WriteLine("│ Sčítavanie rezistorov paralélne -> (2) │");
				Console.WriteLine("│ Ukážka zapojenia rezistorov     -> (3) │");
				Console.WriteLine("│ sériovo a paralélne v schéme    -> (3) │");
				Console.WriteLine("│ Vynulovanie hodnôt rezistorov   -> (4) │");
				Console.WriteLine("│ Ukončiť program + Výsledok      -> (5) │");
				Console.WriteLine("└────────────────────────────────────────┘");
				Console.Write("Tvoj výber -> ");
				Voľba = double.Parse(Console.ReadLine());
				Console.Clear();
				switch (Voľba)
				{
					case 1: {
							Console.WriteLine("┌───────────────────────────────────────┐");
							Console.WriteLine("│ Výpočet rezistora v sériovom zapojení │");
							Console.WriteLine("└───────────────────────────────────────┘");
							if (HlavnýRezistor == 0) {
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu prvého rezistora        │");
								Console.WriteLine("└───────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								HlavnýRezistor = double.Parse(Console.ReadLine());
								Console.Clear(); }
							if (VedľajšíCyklus) {
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu druhého rezistora       │");
								Console.WriteLine("└───────────────────────────────────────┘");
								Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
								Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
								Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								Odpoveď = Console.ReadLine();
								Console.Clear();
								Odpoveď = Odpoveď.ToLower();
								if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear();
								else {
									do {
										VedľajšíRezistorKonvertovaný = double.Parse(Odpoveď);
										Console.WriteLine("┌────────────────────────────────────────┐");
										Console.WriteLine("│ Výpočet Rezistorov v sériovom zapojení │");
										Console.WriteLine("└────────────────────────────────────────┘");
										Console.WriteLine("R = R1 + R2");
										Console.WriteLine("R = " + HlavnýRezistor + " + " + VedľajšíRezistorKonvertovaný);
										HlavnýRezistor += VedľajšíRezistorKonvertovaný;
										Console.WriteLine("R = " + HlavnýRezistor);
										Console.WriteLine("┌────────────────────────────────────────┐");
										Console.WriteLine("│ Zadaj hodnotu ďaľšieho rezistora       │");
										Console.WriteLine("└────────────────────────────────────────┘");
										Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
										Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
										Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
										Console.Write("Zadaná hodnota -> ");
										Odpoveď = Console.ReadLine();
										Odpoveď = Odpoveď.ToLower();
										if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear(); PočítacíCyklus = false;
									} while (PočítacíCyklus);
								}
								VedľajšíCyklus = false; } } break;
					case 2: {
							Console.WriteLine("┌─────────────────────────────────────────┐");
							Console.WriteLine("│ Výpočet rezistora v paralélnom zapojení │");
							Console.WriteLine("└─────────────────────────────────────────┘");
							if (HlavnýRezistor == 0)
							{
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu prvého rezistora        │");
								Console.WriteLine("└───────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								HlavnýRezistor = double.Parse(Console.ReadLine());
								Console.Clear();
							}
							if (VedľajšíCyklus)
							{
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu druhého rezistora       │");
								Console.WriteLine("└───────────────────────────────────────┘");
								Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
								Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
								Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								Odpoveď = Console.ReadLine();
								Console.Clear();
								Odpoveď = Odpoveď.ToLower();
								if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear();
								else
								{
									do
									{
										VedľajšíRezistorKonvertovaný = double.Parse(Odpoveď);
										Console.WriteLine("┌─────────────────────────────────────────┐");
										Console.WriteLine("│ Výpočet rezistora v paralélnom zapojení │");
										Console.WriteLine("└─────────────────────────────────────────┘");
										Console.WriteLine("R = (R1 * R2)/(R1 + R2)");
										Console.WriteLine("R = (" + HlavnýRezistor + " * " + VedľajšíRezistorKonvertovaný + ")/(" + HlavnýRezistor + " + " + VedľajšíRezistorKonvertovaný + ")");
										HlavnýRezistor = (HlavnýRezistor * VedľajšíRezistorKonvertovaný) / (HlavnýRezistor + VedľajšíRezistorKonvertovaný);
										Console.WriteLine("R = " + HlavnýRezistor);
										Console.WriteLine("┌────────────────────────────────────────┐");
										Console.WriteLine("│ Zadaj hodnotu ďaľšieho rezistora       │");
										Console.WriteLine("└────────────────────────────────────────┘");
										Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
										Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
										Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
										Console.Write("Zadaná hodnota -> ");
										Odpoveď = Console.ReadLine();
										Odpoveď = Odpoveď.ToLower();
										if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear(); PočítacíCyklus = false;
									} while (PočítacíCyklus); }
								VedľajšíCyklus = false; } } break;
					case 3: {
							Console.WriteLine("\t\t┌─────────────────────────────────┐┌─────────────────────────────────┐");
							Console.WriteLine("\t\t│       Paralélne zapojenie       ││        Sériové zapojenie        │");
							Console.WriteLine("\t\t└─────────────────────────────────┘└─────────────────────────────────┘");
							Console.WriteLine("\t\t┌─────────────────────────────────┐┌─────────────────────────────────┐");
							Console.WriteLine("\t\t│                │                ││                                 │");
							Console.WriteLine("\t\t│           ┌────●────┐           ││                                 │");
							Console.WriteLine("\t\t│           │         │           ││                                 │");
							Console.WriteLine("\t\t│          ┌─┐       ┌─┐          ││    ┌─────────┐   ┌─────────┐    │");
							Console.WriteLine("\t\t│          │ │       │ │          ││ ───│         │───│         │─── │");
							Console.WriteLine("\t\t│          └─┘       └─┘          ││    └─────────┘   └─────────┘    │");
							Console.WriteLine("\t\t│           │         │           ││                                 │");
							Console.WriteLine("\t\t│           └────●────┘           ││                                 │");
							Console.WriteLine("\t\t│                │                ││                                 │");
							Console.WriteLine("\t\t└─────────────────────────────────┘└─────────────────────────────────┘");
							Console.WriteLine("\t\t┌────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine("\t\t│                    Pre pokračovanie stlač ENTER                    │");
							Console.WriteLine("\t\t└────────────────────────────────────────────────────────────────────┘");
							Console.ReadKey();
							Console.Clear(); } break;
					case 4: {
							Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine("│ Si si istý že chceš vynulovať hodnoty rezistorov?                        │");
							Console.WriteLine("└──────────────────────────────────────────────────────────────────────────┘");
							Console.WriteLine("┌────────────────────────────────────┐┌────────────────────────────────────┐");
							Console.WriteLine("│    Pre odpoveď áno napíš -> áno    ││    Pre odpoveď nie napíš -> nie    │");
							Console.WriteLine("└────────────────────────────────────┘└────────────────────────────────────┘");
							Console.Write("Tvoj výber -> ");
							Reset = Console.ReadLine();
							if (Reset == "áno" | Reset == "ano" | Reset == "Áno" | Reset == "Ano") {
								HlavnýRezistor = 0; VedľajšíRezistorKonvertovaný = 0;
								Console.WriteLine("┌────────────────────────────────────┐");
								Console.WriteLine("│ hodnoty rezistorov boli vynulované │");
								Console.WriteLine("└────────────────────────────────────┘");
							} else {
								Console.WriteLine("┌────────────────────────────────────┐");
								Console.WriteLine("│ hodnoty rezistorov boli zachované  │");
								Console.WriteLine("└────────────────────────────────────┘");
							}
							Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine("│ Pre vrátenie sa do Menu stlačte ENTER                                    │");
							Console.WriteLine("└──────────────────────────────────────────────────────────────────────────┘");
							Console.ReadKey();
							Console.Clear(); } break;
					case 5: { HlavnýCyklus = false; } break;

				}
			} while (HlavnýCyklus);
			if (HlavnýRezistor == 0) {
				Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────┐");
				Console.WriteLine("│ Výsledná hodnota rezistora - R = 0 (Nezadal si žiadne hodnoty)            │");
				Console.WriteLine("└───────────────────────────────────────────────────────────────────────────┘"); }
			else {
				Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────┐");
				Console.WriteLine("│ Výsledná hodnota rezistora                                                │");
				Console.WriteLine("└───────────────────────────────────────────────────────────────────────────┘");
				Console.WriteLine("R = " + HlavnýRezistor); }


		}
		static void VÝPOČET_KONDENZÁDOR()
		{
			string Reset, Odpoveď;
			bool HlavnýCyklus = true, VedľajšíCyklus = true, PočítacíCyklus = true;
			double HlavnýKondenzátor = 0, Voľba = 0, VedľajšíKondenzátorKonvertovaný = 0;
			do
			{
				VedľajšíCyklus = true;
				Console.WriteLine("┌───────────────────────────────────────────┐");
				Console.WriteLine("│         Vyber si jednu z možností.        │");
				Console.WriteLine("└───────────────────────────────────────────┘");
				Console.WriteLine("┌───────────────────────────────────────────┐");
				Console.WriteLine("│ Na výber sú ->                            │");
				Console.WriteLine("│ Sčítavanie kondenzátorov sériovo   -> (1) │");
				Console.WriteLine("│ Sčítavanie kondenzátorov paralélne -> (2) │");
				Console.WriteLine("│ Ukážka zapojenia kondenzátorov     -> (3) │");
				Console.WriteLine("│ sériovo a paralélne v schéme       -> (3) │");
				Console.WriteLine("│ Vynulovanie hodnôt kondenzátorov   -> (4) │");
				Console.WriteLine("│ Ukončiť program + Výsledok         -> (5) │");
				Console.WriteLine("└───────────────────────────────────────────┘");
				Console.Write("Tvoj výber -> ");
				Voľba = double.Parse(Console.ReadLine());
				Console.Clear();
				switch (Voľba)
				{
					case 1:
						{
							Console.WriteLine("┌───────────────────────────────────────────┐");
							Console.WriteLine("│ Výpočet kondenzátorov v sériovom zapojení │");
							Console.WriteLine("└───────────────────────────────────────────┘");
							if (HlavnýKondenzátor == 0)
							{
								Console.WriteLine("┌───────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu prvého kondenzátora │");
								Console.WriteLine("└───────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								HlavnýKondenzátor = double.Parse(Console.ReadLine());
								Console.Clear();
							}
							if (VedľajšíCyklus)
							{
								Console.WriteLine("┌────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu druhého kondenzátora │");
								Console.WriteLine("└────────────────────────────────────┘");
								Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
								Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
								Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								Odpoveď = Console.ReadLine();
								Console.Clear();
								Odpoveď = Odpoveď.ToLower();
								if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear();
								else
								{
									do
									{
										VedľajšíKondenzátorKonvertovaný = double.Parse(Odpoveď);
										Console.WriteLine("┌───────────────────────────────────────────┐");
										Console.WriteLine("│ Výpočet kondenzátorov v sériovom zapojení │");
										Console.WriteLine("└───────────────────────────────────────────┘");
										Console.WriteLine("C = C1 + C2");
										Console.WriteLine("C = " + HlavnýKondenzátor + " + " + VedľajšíKondenzátorKonvertovaný);
										HlavnýKondenzátor += VedľajšíKondenzátorKonvertovaný;
										Console.WriteLine("C = " + HlavnýKondenzátor);
										Console.WriteLine("┌─────────────────────────────────────┐");
										Console.WriteLine("│ Zadaj hodnotu ďaľšieho kondenzátora │");
										Console.WriteLine("└─────────────────────────────────────┘");
										Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
										Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
										Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
										Console.Write("Zadaná hodnota -> ");
										Odpoveď = Console.ReadLine();
										Odpoveď = Odpoveď.ToLower();
										if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear(); PočítacíCyklus = false;
									} while (PočítacíCyklus);
								}
								VedľajšíCyklus = false;
							}
						}
						break;
					case 2:
						{
							Console.WriteLine("┌─────────────────────────────────────────────┐");
							Console.WriteLine("│ Výpočet kondenzátorov v paralélnom zapojení │");
							Console.WriteLine("└─────────────────────────────────────────────┘");
							if (HlavnýKondenzátor == 0)
							{
								Console.WriteLine("┌───────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu prvého kondenzátora │");
								Console.WriteLine("└───────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								HlavnýKondenzátor = double.Parse(Console.ReadLine());
								Console.Clear();
							}
							if (VedľajšíCyklus)
							{
								Console.WriteLine("┌────────────────────────────────────┐");
								Console.WriteLine("│ Zadaj hodnotu druhého kondenzátora │");
								Console.WriteLine("└────────────────────────────────────┘");
								Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
								Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
								Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
								Console.Write("Zadaná hodnota -> ");
								Odpoveď = Console.ReadLine();
								Console.Clear();
								Odpoveď = Odpoveď.ToLower();
								if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear();
								else
								{
									do
									{
										VedľajšíKondenzátorKonvertovaný = double.Parse(Odpoveď);
										Console.WriteLine("┌─────────────────────────────────────────────┐");
										Console.WriteLine("│ Výpočet kondenzátorov v paralélnom zapojení │");
										Console.WriteLine("└─────────────────────────────────────────────┘");
										Console.WriteLine("C = (C1 * C2)/(C1 + C2)");
										Console.WriteLine("C = (" + HlavnýKondenzátor + " * " + VedľajšíKondenzátorKonvertovaný + ")/(" + HlavnýKondenzátor + " + " + VedľajšíKondenzátorKonvertovaný + ")");
										HlavnýKondenzátor = (HlavnýKondenzátor * VedľajšíKondenzátorKonvertovaný) / (HlavnýKondenzátor + VedľajšíKondenzátorKonvertovaný);
										Console.WriteLine("C = " + HlavnýKondenzátor);
										Console.WriteLine("┌─────────────────────────────────────┐");
										Console.WriteLine("│ Zadaj hodnotu ďaľšieho kondenzátora │");
										Console.WriteLine("└─────────────────────────────────────┘");
										Console.WriteLine("┌──────────────────────────────────────────────────────────────────────┐");
										Console.WriteLine("│ Na vrátenie sa do pôvodného menu napíš namiesto hodnoty slovo - Späť │");
										Console.WriteLine("└──────────────────────────────────────────────────────────────────────┘");
										Console.Write("Zadaná hodnota -> ");
										Odpoveď = Console.ReadLine();
										Odpoveď = Odpoveď.ToLower();
										if (Odpoveď == "späť" | Odpoveď == "spať" | Odpoveď == "spät" | Odpoveď == "spat") Console.Clear(); PočítacíCyklus = false;
									} while (PočítacíCyklus);
								}
								VedľajšíCyklus = false;
							}
						}
						break;
					case 3:
						{
							Console.WriteLine("\t\t┌─────────────────────────────────┐┌─────────────────────────────────┐");
							Console.WriteLine("\t\t│       Paralélne zapojenie       ││        Sériové zapojenie        │");
							Console.WriteLine("\t\t└─────────────────────────────────┘└─────────────────────────────────┘");
							Console.WriteLine("\t\t┌─────────────────────────────────┐┌─────────────────────────────────┐");
							Console.WriteLine("\t\t│                │                ││                                 │");
							Console.WriteLine("\t\t│           ┌────●────┐           ││                                 │");
							Console.WriteLine("\t\t│           │         │           ││           ││        ││          │");
							Console.WriteLine("\t\t│          ˭˭˭       ˭˭˭          ││      ─────││────────││─────     │");
							Console.WriteLine("\t\t│           │         │           ││           ││        ││          │");
							Console.WriteLine("\t\t│           └────●────┘           ││                                 │");
							Console.WriteLine("\t\t│                │                ││                                 │");
							Console.WriteLine("\t\t└─────────────────────────────────┘└─────────────────────────────────┘");
							Console.WriteLine("\t\t┌────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine("\t\t│                    Pre pokračovanie stlač ENTER                    │");
							Console.WriteLine("\t\t└────────────────────────────────────────────────────────────────────┘");
							Console.ReadKey();
							Console.Clear();
						}
						break;
					case 4:
						{
							Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine("│ Si si istý že chceš vynulovať hodnoty kondenzátorov?                     │");
							Console.WriteLine("└──────────────────────────────────────────────────────────────────────────┘");
							Console.WriteLine("┌────────────────────────────────────┐┌────────────────────────────────────┐");
							Console.WriteLine("│    Pre odpoveď áno napíš -> áno    ││    Pre odpoveď nie napíš -> nie    │");
							Console.WriteLine("└────────────────────────────────────┘└────────────────────────────────────┘");
							Console.Write("Tvoj výber -> ");
							Reset = Console.ReadLine();
							if (Reset == "áno" | Reset == "ano" | Reset == "Áno" | Reset == "Ano")
							{
								HlavnýKondenzátor = 0; VedľajšíKondenzátorKonvertovaný = 0;
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ hodnoty kondenzátorov boli vynulované │");
								Console.WriteLine("└───────────────────────────────────────┘");
							}
							else
							{
								Console.WriteLine("┌───────────────────────────────────────┐");
								Console.WriteLine("│ hodnoty kondenzátorov boli zachované  │");
								Console.WriteLine("└───────────────────────────────────────┘");
							}
							Console.WriteLine("┌───────────────────────────────────────┐");
							Console.WriteLine("│ Pre vrátenie sa do Menu stlačte ENTER │");
							Console.WriteLine("└───────────────────────────────────────┘");
							Console.ReadKey();
							Console.Clear();
						}
						break;
					case 5: { HlavnýCyklus = false; } break;

				}
			} while (HlavnýCyklus);
			if (HlavnýKondenzátor == 0)
			{
				Console.WriteLine("┌───────────────────────────────────────────────────────────────────┐");
				Console.WriteLine("│ Výsledná hodnota kondenzátorov - C = 0 (Nezadal si žiadne hodnoty)│");
				Console.WriteLine("└───────────────────────────────────────────────────────────────────┘");
			}
			else
			{
				Console.WriteLine("┌────────────────────────────────┐");
				Console.WriteLine("│ Výsledná hodnota kondenzátorov │");
				Console.WriteLine("└────────────────────────────────┘");
				Console.WriteLine("C = " + HlavnýKondenzátor);
			}

		}
		static string PONUKA_PREMENY_JEDNOTIEK()
		{
			string odpovedstring;
			Console.WriteLine("┌────────────────────────────────────────┐");
			Console.WriteLine("│       Vyber si jednu z možností.       │");
			Console.WriteLine("└────────────────────────────────────────┘");
			Console.WriteLine("┌────────────────────────────────────────┐");
			Console.WriteLine("│ Na výber sú ->                         │");
			Console.WriteLine("│ Ukázať tabuľku s násobičmi  -> (1)     │");
			Console.WriteLine("│ Ukázať podporované jednotky -> (2)     │");
			Console.WriteLine("│ Premena Jednotiek           -> (3)     │");
			Console.WriteLine("│ Ukončiť program             -> (4)     │");
			Console.WriteLine("└────────────────────────────────────────┘");
			Console.Write("Tvoj výber -> ");
			odpovedstring = Console.ReadLine();
			Console.Clear();
			odpovedstring = odpovedstring.ToUpper();
			return odpovedstring;
		}
		static void NÁSOBIČ_PREMENY_JEDNOTIEK()
		{
			Console.WriteLine("┌───────┐┌────────┐┌────────────────────────────────────────────────┐");
			Console.WriteLine("│ Názov ││ Symbol ││                     Násobič                    │");
			Console.WriteLine("└───────┘└────────┘└────────────────────────────────────────────────┘");
			Console.WriteLine("┌───────┐┌────────┐┌────────────────────────────────────────────────┐");
			/*Console.WriteLine("│ yotta ││   Y    ││ 1 000 000 000 000 000 000 000 000  = 10^24     │");
			Console.WriteLine("│ zetta ││   Z    ││ 1 000 000 000 000 000 000 000      = 10^21     │");
			Console.WriteLine("│ exa   ││   E    ││ 1 000 000 000 000 000 000          = 10^18     │");
			Console.WriteLine("│ peta  ││   P    ││ 1 000 000 000 000 000    = 10^15               │");*/
			Console.WriteLine("│ tera  ││   T    ││ 1 000 000 000 000        = 10^12               │");
			Console.WriteLine("│ giga  ││   G    ││ 1 000 000 000            = 10^ 9               │");
			Console.WriteLine("│ mega  ││   M    ││ 1 000 000    = 10^ 6                           │");
			Console.WriteLine("│ kilo  ││   k    ││ 1 000        = 10^ 3                           │");
			Console.WriteLine("│ hekto ││   h    ││ 100          = 10^ 2                           │");
			Console.WriteLine("│ deka  ││   da   ││ 10           = 10^ 1                           │");
			Console.WriteLine("│───────││────────││────────────────────────────────────────────────│");
			Console.WriteLine("│ deci  ││   d    ││ 0,1          = 10^-1                           │");
			Console.WriteLine("│ centi ││   c    ││ 0,01         = 10^-2                           │");
			Console.WriteLine("│ mili  ││   m    ││ 0,001        = 10^-3                           │");
			Console.WriteLine("│ mikro ││   u    ││ 0,000 001    = 10^-6                           │");
			Console.WriteLine("│ nano  ││   n    ││ 0,000 000 001            = 10^ -9              │");
			Console.WriteLine("│ piko  ││   p    ││ 0,000 000 000 001        = 10^-12              │");
			/*Console.WriteLine("│ femto ││   f    ││ 0,000 000 000 000 001    = 10^-15              │");
			Console.WriteLine("│ atto  ││   a    ││ 0,000 000 000 000 000 001         = 10^-18     │");
			Console.WriteLine("│ zepto ││   z    ││ 0,000 000 000 000 000 000 001     = 10^-21     │");
			Console.WriteLine("│ yocto ││   y    ││ 0,000 000 000 000 000 000 000 001 = 10^-24     │");*/
			Console.WriteLine("└───────┘└────────┘└────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void PODPOROVANÉ_JEDNOTKY_PREMENY_JEDNOTIEK() 
		{
			Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Podporované Jednotky (Voľba č.3) ->                         │");
			Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
			Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Fyzikálna veličina │ Základna jednotka │ Označenie Jednotky │");
			Console.WriteLine("│────────────────────│───────────────────│────────────────────│");
			Console.WriteLine("│ Dĺžka              │ meter             │ m       (!Malé M!) │");
			Console.WriteLine("│ Hmotnosť           │ Kilogram          │ kg                 │");
			Console.WriteLine("│ Elektrický prúd    │ Ampér             │ A                  │");
			Console.WriteLine("│ Frekvencia         │ Hertz             │ Hz                 │");
			Console.WriteLine("│ Sila               │ Newton            │ N                  │");
			Console.WriteLine("│ Tlak               │ Pascal            │ Pa                 │");
			Console.WriteLine("│ Práca, energia     │ Joule             │ J                  │");
			Console.WriteLine("│ Výkon              │ Watt              │ W                  │");
			Console.WriteLine("│ Ele. kapacita      │ Farad             │ N                  │");
			Console.WriteLine("│ Ele. Náboj         │ Coloumb           │ C                  │");
			Console.WriteLine("│ Ele. napätie       │ Volt              │ V                  │");
			Console.WriteLine("│ Ele. odpor         │ Ohm               │ Ohm                │");
			Console.WriteLine("│ Ele. vodivosť      │ Siemens           │ S                  │");
			Console.WriteLine("│ Indukčnosť         │ Henry             │ H                  │");
			Console.WriteLine("│ Mag. indukcia      │ Tesla             │ T                  │");
			Console.WriteLine("│ Mag. indukčný tok  │ Weber             │ Wb                 │");
			Console.WriteLine("│ Hladina hlasitosti │ Decibel           │ dB                 │");
			Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
			Console.ReadKey(); Console.Clear();
		}
		static void UKÁŽKA_PREMENY_JEDNOTIEK()
        {
			Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Zadaj hodnoty v tomto tvare -> [Hodnota] [Súčasná Jednotka] = [Výsledná Jednotka] │");
			Console.WriteLine("│───────────────────────────────────────────────────────────────────────────────────│");
			Console.WriteLine("│ Príklad -> 15 cm = m │ 150 GHz = MHz │ 68 kg = g │ 185 Ohm = kOhm                 │");
			Console.WriteLine("│───────────────────────────────────────────────────────────────────────────────────│");
			Console.WriteLine("│ Podporované Jednotky -> m, kg, A, Hz, N, Pa, J, W, N, C, V, Ohm, S, H, T, Wb, dB  │");
			Console.WriteLine("└───────────────────────────────────────────────────────────────────────────────────┘");
			Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Upozornenie -> Výsledky s jednotkami ktoré niesú podporované nemusia byť správne! │");
			Console.WriteLine("└───────────────────────────────────────────────────────────────────────────────────┘");
		}
		static BigInteger VYPOČTY_PREMENY_JEDNOTIEK_BETA(string Odpoveď)
		{
			string Číslo, Jednotka, JednotkaKonečná, AB, CD;
			BigInteger Konečnéčíslo = 0; BigInteger NegetívneKonečnéČíslo = 0;
			string[] OdpovedeVPoli = Odpoveď.Split(' ');
			Číslo = OdpovedeVPoli[0]; Jednotka = OdpovedeVPoli[1]; JednotkaKonečná = OdpovedeVPoli[3];
			Konečnéčíslo = BigInteger.Parse(Číslo);
			char[] PísmenáZnakov; PísmenáZnakov = Jednotka.ToCharArray();
			char[] JednotkaPole; JednotkaPole = JednotkaKonečná.ToCharArray();
			char A = PísmenáZnakov[0], B, C = JednotkaPole[0], D;
			// Odmocnovanie Čísla
			if (A == 'Y') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 24); }
			if (A == 'Z') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 21); }
			if (A == 'E') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 18); }
			if (A == 'P') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 15); }
			if (A == 'T' & Jednotka.Length >= 2) Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 12);
			if (A == 'G') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 9); }
			if (A == 'M') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 6); }
			if (A == 'k') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 3); }
			if (A == 'h') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 2); }
			// deka = da
			if (Jednotka.Length >= 2)
			{
				B = PísmenáZnakov[1]; AB = A.ToString() + B.ToString();
				if (AB == "da") Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 1);
			}
			NegetívneKonečnéČíslo = BigInteger.Negate(Konečnéčíslo);
			// Ukončenie + Pokračuje -
			if (A == 'd') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.1; }
			if (A == 'c') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.01; }
			if (A == 'm' & Jednotka.Length >= 2) Konečnéčíslo = Konečnéčíslo * (BigInteger)0.001;
			if (A == 'u') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000001; }
			if (A == 'n') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000001; }
			if (A == 'p') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000001; }
			if (A == 'f') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000001; }
			if (A == 'a') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000001; }
			if (A == 'z') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000000001; }
			if (A == 'y') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000000000001; }
			//
			// Konečné číslo
			//
			if (C == 'Y') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000000000001; }
			if (C == 'Z') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000000001; }
			if (C == 'E') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000000001; }
			if (C == 'P') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000000001; }
			if (C == 'T' & JednotkaKonečná.Length >= 2) { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000000001; }
			if (C == 'G') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000000001; }
			if (C == 'M') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.000001; }
			if (C == 'k') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.001; }
			if (C == 'h') { Konečnéčíslo = Konečnéčíslo * (BigInteger)0.01; }
			// deka = da
			if (JednotkaKonečná.Length >= 2)
			{
				D = JednotkaPole[1]; CD = C.ToString() + D.ToString();
				if (CD == "da") NegetívneKonečnéČíslo =  Konečnéčíslo = Konečnéčíslo * (BigInteger)0.1; 
			}
			//Konečnéčíslo = BigInteger.Negate(NegetívneKonečnéČíslo);
			// Ukončenie + Pokračuje -
			if (C == 'd') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 1); }
			if (C == 'c') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 2); }
			if (C == 'm' & JednotkaKonečná.Length >= 2) { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 3); }
			if (C == 'u') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 6); }
			if (C == 'n') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 9); }
			if (C == 'p') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 12); }
			if (C == 'f') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 15); }
			if (C == 'a') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 18); }
			if (C == 'z') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 21); }
			if (C == 'y') { Konečnéčíslo = Konečnéčíslo * BigInteger.Pow(10, 24); }
			return Konečnéčíslo;

		}
		static decimal VYPOČTY_PREMENY_JEDNOTIEK(string Odpoveď)
		{
			string Číslo, Jednotka, JednotkaKonečná, AB, CD;
			decimal Konečnéčíslo = 0; decimal NegetívneKonečnéČíslo = 0;
			string[] OdpovedeVPoli = Odpoveď.Split(' ');
			Číslo = OdpovedeVPoli[0]; Jednotka = OdpovedeVPoli[1]; JednotkaKonečná = OdpovedeVPoli[3];
			Konečnéčíslo = decimal.Parse(Číslo);
			char[] PísmenáZnakov; PísmenáZnakov = Jednotka.ToCharArray();
			char[] JednotkaPole; JednotkaPole = JednotkaKonečná.ToCharArray();
			char A = PísmenáZnakov[0], B, C = JednotkaPole[0], D;
			// Odmocnovanie Čísla
			/*if (A == 'Y') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 24); }
			if (A == 'Z') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 21); }
			if (A == 'E') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 18); }
			if (A == 'P') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 15); }*/
			if (A == 'T' & Jednotka.Length >= 2) Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 12);
			if (A == 'G') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 9); }
			if (A == 'M') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 6); }
			if (A == 'k') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 3); }
			if (A == 'h') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 2); }
			// deka = da
			if (Jednotka.Length >= 2)
			{
				B = PísmenáZnakov[1]; AB = A.ToString() + B.ToString();
				if (AB == "da") Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 1);
			}
			// Ukončenie + Pokračuje -
			if (A == 'd') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -1); }
			if (A == 'c') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -2); }
			if (A == 'm' & Jednotka.Length >= 2) Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -3);
			if (A == 'u') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -6); }
			if (A == 'n') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -9); }
			if (A == 'p') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -12); }
			/*if (A == 'f') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -15); }
			if (A == 'a') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -18); }
			if (A == 'z') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -21); }
			if (A == 'y') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -24); }*/
			//
			// Konečné číslo
			//
			/*if (C == 'Y') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -24); }
			if (C == 'Z') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -21); }
			if (C == 'E') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -18); }
			if (C == 'P') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -15); }*/
			if (C == 'T' & JednotkaKonečná.Length >= 2) { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -12); }
			if (C == 'G') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -9); }
			if (C == 'M') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -6); }
			if (C == 'k') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -3); }
			if (C == 'h') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -2); }
			// deka = da
			if (JednotkaKonečná.Length >= 2)
			{
				D = JednotkaPole[1]; CD = C.ToString() + D.ToString();
				if (CD == "da") NegetívneKonečnéČíslo = Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, -1);
			}
			//Konečnéčíslo = BigInteger.Negate(NegetívneKonečnéČíslo);
			// Ukončenie + Pokračuje -
			if (C == 'd') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 1); }
			if (C == 'c') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 2); }
			if (C == 'm' & JednotkaKonečná.Length >= 2) { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 3); }
			if (C == 'u') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 6); }
			if (C == 'n') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 9); }
			if (C == 'p') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 12); }
			/*if (C == 'f') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 15); }
			if (C == 'a') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 18); }
			if (C == 'z') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 21); }
			if (C == 'y') { Konečnéčíslo = Konečnéčíslo * (decimal)Math.Pow(10, 24); }*/
			return Konečnéčíslo;

		}
		static void PREMENY_JEDNOTIEK()
		{
			string Odpoveď, Číslo, JednotkaKonečná;
			decimal Konečnéčíslo = 0;
			UKÁŽKA_PREMENY_JEDNOTIEK();
			Odpoveď = Console.ReadLine();
			string[] OdpovedeVPoli = Odpoveď.Split(' '); 
			Číslo = OdpovedeVPoli[0]; JednotkaKonečná = OdpovedeVPoli[3];
			Konečnéčíslo = VYPOČTY_PREMENY_JEDNOTIEK(Odpoveď);
			bool DesatinnáČiarkaBool = true, DesatinnáČiarkaBoolPomôcka = false;
			string DesatinnáČiarkaString = Convert.ToString(Konečnéčíslo);
			int PozíciaDesatinnejČiarky = 0, PomôckaPriCykle = 0;
			char[] DesatinnáČiarkaPole; DesatinnáČiarkaPole = DesatinnáČiarkaString.ToCharArray();
			int ZaDesatinnouČiarkou = 0;
			do
            {
				PozíciaDesatinnejČiarky += 1;
				if (PomôckaPriCykle < DesatinnáČiarkaString.Length & DesatinnáČiarkaBoolPomôcka == false) break;
				if (DesatinnáČiarkaPole[PozíciaDesatinnejČiarky] == '.') DesatinnáČiarkaBoolPomôcka = true;
			}
			while (PomôckaPriCykle < DesatinnáČiarkaString.Length);

			if (DesatinnáČiarkaBoolPomôcka) {
				do
				{
					if (DesatinnáČiarkaPole[PozíciaDesatinnejČiarky] == '.') DesatinnáČiarkaBool = false;
					else PozíciaDesatinnejČiarky++;
				}
				while (DesatinnáČiarkaBool);
			}
			PozíciaDesatinnejČiarky += 1;
			if (DesatinnáČiarkaBoolPomôcka) ZaDesatinnouČiarkou = DesatinnáČiarkaString.Length - PozíciaDesatinnejČiarky;
			else ZaDesatinnouČiarkou = 0;
			PREMENY_JEDNOTIEK_VÝPIS(ZaDesatinnouČiarkou, Konečnéčíslo, JednotkaKonečná, PozíciaDesatinnejČiarky);

		}
		static void PREMENY_JEDNOTIEK_VÝPIS(int ZaDesatinnouČiarkou, decimal Konečnéčíslo, string JednotkaKonečná, int PozíciaDesatinnejČiarky)
        {
			int PredDesatinnouČiatkou = PozíciaDesatinnejČiarky -= 2;
			Console.Write("Výsledok -> ");
			if (ZaDesatinnouČiarkou <= 3 & PredDesatinnouČiatkou <= 3) Console.Write($"{Konečnéčíslo,3:N3}");
			if (ZaDesatinnouČiarkou > 3 & ZaDesatinnouČiarkou <= 6 & PredDesatinnouČiatkou <= 3) Console.Write($"{Konečnéčíslo,3:N6}");
			if (ZaDesatinnouČiarkou > 6 & ZaDesatinnouČiarkou <= 9 & PredDesatinnouČiatkou <= 3) Console.Write($"{Konečnéčíslo,3:N9}");
			if (ZaDesatinnouČiarkou > 9 & ZaDesatinnouČiarkou <= 12 & PredDesatinnouČiatkou <= 3) Console.Write($"{Konečnéčíslo,3:N12}");
			if (ZaDesatinnouČiarkou > 12 & ZaDesatinnouČiarkou <= 15 & PredDesatinnouČiatkou <= 3) Console.Write($"{Konečnéčíslo,3:N15}");

			if (ZaDesatinnouČiarkou <= 3 & PredDesatinnouČiatkou > 3 & PredDesatinnouČiatkou <= 6) Console.Write($"{Konečnéčíslo,6:N3}");
			if (ZaDesatinnouČiarkou > 3 & ZaDesatinnouČiarkou <= 6 & PredDesatinnouČiatkou > 3 & PredDesatinnouČiatkou <= 6) Console.Write($"{Konečnéčíslo,6:N6}");
			if (ZaDesatinnouČiarkou > 6 & ZaDesatinnouČiarkou <= 9 & PredDesatinnouČiatkou > 3 & PredDesatinnouČiatkou <= 6) Console.Write($"{Konečnéčíslo,6:N9}");
			if (ZaDesatinnouČiarkou > 9 & ZaDesatinnouČiarkou <= 12 & PredDesatinnouČiatkou > 3 & PredDesatinnouČiatkou <= 6) Console.Write($"{Konečnéčíslo,6:N12}");
			if (ZaDesatinnouČiarkou > 12 & ZaDesatinnouČiarkou <= 15 & PredDesatinnouČiatkou > 3 & PredDesatinnouČiatkou <= 6) Console.Write($"{Konečnéčíslo,6:N15}");

			if (ZaDesatinnouČiarkou <= 3 & PredDesatinnouČiatkou > 6 & PredDesatinnouČiatkou <= 9) Console.Write($"{Konečnéčíslo,9:N3}");
			if (ZaDesatinnouČiarkou > 3 & ZaDesatinnouČiarkou <= 6 & PredDesatinnouČiatkou > 6 & PredDesatinnouČiatkou <= 9) Console.Write($"{Konečnéčíslo,9:N6}");
			if (ZaDesatinnouČiarkou > 6 & ZaDesatinnouČiarkou <= 9 & PredDesatinnouČiatkou > 6 & PredDesatinnouČiatkou <= 9) Console.Write($"{Konečnéčíslo,9:N9}");
			if (ZaDesatinnouČiarkou > 9 & ZaDesatinnouČiarkou <= 12 & PredDesatinnouČiatkou > 6 & PredDesatinnouČiatkou <= 9) Console.Write($"{Konečnéčíslo,9:N12}");
			if (ZaDesatinnouČiarkou > 12 & ZaDesatinnouČiarkou <= 15 & PredDesatinnouČiatkou > 6 & PredDesatinnouČiatkou <= 9) Console.Write($"{Konečnéčíslo,9:N15}");

			if (ZaDesatinnouČiarkou <= 3 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou <= 12) Console.Write($"{Konečnéčíslo,12:N3}");
			if (ZaDesatinnouČiarkou > 3 & ZaDesatinnouČiarkou <= 6 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou <= 12) Console.Write($"{Konečnéčíslo,12:N6}");
			if (ZaDesatinnouČiarkou > 6 & ZaDesatinnouČiarkou <= 9 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou <= 12) Console.Write($"{Konečnéčíslo,12:N9}");
			if (ZaDesatinnouČiarkou > 9 & ZaDesatinnouČiarkou <= 12 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou <= 12) Console.Write($"{Konečnéčíslo,12:N12}");
			if (ZaDesatinnouČiarkou > 12 & ZaDesatinnouČiarkou <= 15 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou <= 12) Console.Write($"{Konečnéčíslo,12:N15}");

			if (ZaDesatinnouČiarkou <= 3 & PredDesatinnouČiatkou > 9 & PredDesatinnouČiatkou >= 12) Console.Write($"{Konečnéčíslo,15:N3}");
			if (ZaDesatinnouČiarkou > 3 & ZaDesatinnouČiarkou <= 6 & PredDesatinnouČiatkou >= 12) Console.Write($"{Konečnéčíslo,15:N6}");
			if (ZaDesatinnouČiarkou > 6 & ZaDesatinnouČiarkou <= 9 & PredDesatinnouČiatkou >= 12) Console.Write($"{Konečnéčíslo,15:N9}");
			if (ZaDesatinnouČiarkou > 9 & ZaDesatinnouČiarkou <= 12 & PredDesatinnouČiatkou >= 12) Console.Write($"{Konečnéčíslo,15:N12}");
			if (ZaDesatinnouČiarkou > 12 & ZaDesatinnouČiarkou <= 15 & PredDesatinnouČiatkou >= 12) Console.Write($"{Konečnéčíslo,15:N15}");

			Console.Write(" " + JednotkaKonečná);
			Console.ReadKey(); Console.Clear();
		}
		static void PREMENY_JEDNOTIEK_HLAVNÉ_TELO()
		{ 
            string odpovedstring;
            bool cyklus = true;
			do { odpovedstring = PONUKA_PREMENY_JEDNOTIEK();
                if (odpovedstring == "1") { NÁSOBIČ_PREMENY_JEDNOTIEK(); }
				if (odpovedstring == "2") { PODPOROVANÉ_JEDNOTKY_PREMENY_JEDNOTIEK(); }
				if (odpovedstring == "3") { PREMENY_JEDNOTIEK(); }
                if (odpovedstring == "4") { cyklus = false; }
            } while (cyklus);
        }
		static void HRA_TIK_TAK_TOU()
		{
			bool ZopakovanieHryCyklu = true;
			do
			{
				int ObtiažnosťHryFinalnáObtiažnosť = 0;
				string ObtiažnosťHry;
				char[,] HRACIE_POLE = new char[107, 21];
				bool KoniecHry = true, BoolAlwaysFalse = false, ObtiažnosťHryOverenieSprávnejOdpovede = true;
				char vítaz = 'E';
				if (BoolAlwaysFalse)
				{
					Console.WriteLine("       10│       20│       30│       40│       50│       60│       70│       80│       90│      100│123456");
					Console.WriteLine(" ┌────────────────────────────┐┌─────────────┐┌─────────────┐┌─────────────┐┌────────────────────────────┐"); // 0
					Console.WriteLine(" │                            ││             ││             ││             ││  ┌─────────────┐   ┌───┐   │"); // 1
					Console.WriteLine(" │   ┌────────────────────┐   ││             ││             ││             ││  │    *   *    │   │ P │   │"); // 2
					Console.WriteLine(" │   │ Zápis ťahu -> X Y  │   ││             ││             ││             ││  │     * *     │   │ O │   │"); // 3
					Console.WriteLine(" │   │ [ X (Medzera) Y ]  │   ││             ││             ││             ││  │      *      │   │ Č │   │"); // 4
					Console.WriteLine(" │   │ Príklad    -> 1 2  │   ││             ││             ││             ││  │     * *     │   │ Í │   │"); // 5
					Console.WriteLine(" │   └────────────────────┘   │└─────────────┘└─────────────┘└─────────────┘│  │    *   *    │   │ T │   │"); // 6
					Console.WriteLine(" │                            │┌─────────────┐┌─────────────┐┌─────────────┐│  └─────────────┘   │ A │   │"); // 7
					Console.WriteLine(" │────────────────────────────││    *   *    ││    *   *    ││    *   *    ││                    │ Č │   │"); // 8
					Console.WriteLine(" │   ┌────────────────────┐   ││     * *     ││     * *     ││     * *     ││                    └───┘   │"); // 9
					Console.WriteLine(" │   │   │  1 │ 2 │ 3 │ Y │   ││      *      ││      *      ││      *      ││────────────────────────────│"); // 10
					Console.WriteLine(" │   │───┌────────────────┘   ││     * *     ││     * *     ││     * *     ││                            │"); // 11
					Console.WriteLine(" │   │ 1 │ ┌─┐ ┌─┐ ┌─┐        ││    *   *    ││    *   *    ││    *   *    ││                            │"); // 12
					Console.WriteLine(" │   │───│ └─┘ └─┘ └─┘        │└─────────────┘└─────────────┘└─────────────┘│  ┌─────────────┐   ┌───┐   │"); // 13
					Console.WriteLine(" │   │ 2 │ ┌─┐ ┌─┐ ┌─┐        │┌─────────────┐┌─────────────┐┌─────────────┐│  │   * * * *   │   │ T │   │"); // 14
					Console.WriteLine(" │   │───│ └─┘ └─┘ └─┘        ││   * * * *   ││   * * * *   ││   * * * *   ││  │  *       *  │   │ V │   │"); // 15
					Console.WriteLine(" │   │ 3 │ ┌─┐ ┌─┐ ┌─┐        ││  *       *  ││  *       *  ││  *       *  ││  │  *       *  │   │ O │   │"); // 16
					Console.WriteLine(" │   │───│ └─┘ └─┘ └─┘        ││  *       *  ││  *       *  ││  *       *  ││  │  *       *  │   │ J │   │"); // 17
					Console.WriteLine(" │   │ X │                    ││  *       *  ││  *       *  ││  *       *  ││  │   * * * *   │   │ E │   │"); // 18
					Console.WriteLine(" │   └───┘                    ││   * * * *   ││   * * * *   ││   * * * *   ││  └─────────────┘   └───┘   │"); // 19
					Console.WriteLine(" └────────────────────────────┘└─────────────┘└─────────────┘└─────────────┘└────────────────────────────┘"); // 20
					Console.WriteLine("       10│       20│       30│       40│       50│       60│       70│       80│       90│      100│123456");
				} // Ukážka hracieho poľa ( Iba pre programátora :D )
				for (int X = 0; X < 20; X++)
				{
					for (int Y = 0; Y < 105; Y++)
					{
						HRACIE_POLE[Y, X] = ' ';
					}
				} // Zadávanie do celého poľa medzeru
				char[,] ZápisŤahov = new char[3, 3];
				do
				{
					Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
					Console.WriteLine(" │                                Vyber si jednu z obtiažností počítača.                                 │");
					Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
					Console.WriteLine("                                     ┌─────────────────────────────┐");
					Console.WriteLine("                                     │ Na výber sú -> Ľahká   -> 1 │");
					Console.WriteLine("                                     │ Na výber sú -> Stredná -> 2 │");
					Console.WriteLine("                                     │ Na výber sú -> Ťažká   -> 3 │");
					Console.WriteLine("                                     └─────────────────────────────┘");
					Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
					Console.WriteLine(" │                                   Po napísaní odpovede stlač ENTER                                    │");
					Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
					Console.Write(" Tvoja odpoveď -> ");
					ObtiažnosťHry = Console.ReadLine();
					ObtiažnosťHry = ObtiažnosťHry.ToLower();
					if (string.IsNullOrEmpty(ObtiažnosťHry) == false) {
						if (ObtiažnosťHry == "ľahká" | ObtiažnosťHry == "lahká" | ObtiažnosťHry == "ľahka" | ObtiažnosťHry == "lahka" | ObtiažnosťHry == "1")
						{
							ObtiažnosťHryFinalnáObtiažnosť = 1;
							ObtiažnosťHryOverenieSprávnejOdpovede = false;
						} // ľahká obtiažnosť 
						if (ObtiažnosťHry == "stredná" | ObtiažnosťHry == "stredna" | ObtiažnosťHry == "2")
						{
							ObtiažnosťHryFinalnáObtiažnosť = 2;
							ObtiažnosťHryOverenieSprávnejOdpovede = false;
						} // stredná obtiažnosť
						if (ObtiažnosťHry == "ťažká" | ObtiažnosťHry == "ťažka" | ObtiažnosťHry == "ťazka" | ObtiažnosťHry == "tažká" | ObtiažnosťHry == "tazká"
						  | ObtiažnosťHry == "tazka" | ObtiažnosťHry == "tažka" | ObtiažnosťHry == "3")
						{
							ObtiažnosťHryFinalnáObtiažnosť = 3;
							ObtiažnosťHryOverenieSprávnejOdpovede = false;
						} // Ťažká obtiažnosť
						if (ObtiažnosťHryOverenieSprávnejOdpovede)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                               Zadal si možnosť ktorá nie je na výber.                                 │");
							Console.WriteLine(" │                                    Pre pokračovanie stlač ENTER.                                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
							Console.ReadKey(); Console.Clear();
						} // Zlá odpoveď
						else Console.Clear();
					} // Overenie odpovede
					if (string.IsNullOrEmpty(ObtiažnosťHry))
					{
						Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
						Console.WriteLine(" │                                      Nezadal si žiadnu odpoveď.                                       │");
						Console.WriteLine(" │                                    Pre pokračovanie stlač ENTER.                                      │");
						Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						Console.ReadKey(); Console.Clear();
					} // Nezadal žiadnu odpoveďe
				} while (ObtiažnosťHryOverenieSprávnejOdpovede); // Zvolenie obtiažnosti počítača
				if (1 == 1)
				{
					ZápisŤahov[0, 0] = 'E'; ZápisŤahov[0, 1] = 'E'; ZápisŤahov[0, 2] = 'E';
					ZápisŤahov[1, 0] = 'E'; ZápisŤahov[1, 1] = 'E'; ZápisŤahov[1, 2] = 'E';
					ZápisŤahov[2, 0] = 'E'; ZápisŤahov[2, 1] = 'E'; ZápisŤahov[2, 2] = 'E';
				} // Zadávanie do Zápisov ťahov - E -> Empty
				if (1 == 1)
				{
					if (1 == 1)
					{
						for (int i = 0; i < 20; i++) { HRACIE_POLE[0, i] = ' '; } // Medzeri na začiatku
						HRACIE_POLE[1, 0] = '┌'; HRACIE_POLE[1, 20] = '└'; // Lava tabulka Rohy lavé
						HRACIE_POLE[30, 0] = '┐'; HRACIE_POLE[30, 20] = '┘'; // Lava tabulka Rohy pravé
						for (int i = 1; i < 20; i++) { HRACIE_POLE[1, i] = '│'; } // Lava tabulka lavá strana
						for (int i = 1; i < 20; i++) { HRACIE_POLE[30, i] = '│'; } // Lava tabulka pravá strana
						for (int i = 2; i < 30; i++) { HRACIE_POLE[i, 0] = '─'; } // Lava tabulka horná strana,
						for (int i = 2; i < 30; i++) { HRACIE_POLE[i, 20] = '─'; } // Lava tabulka dolná strana
						for (int i = 2; i < 30; i++) { HRACIE_POLE[i, 8] = '─'; } // Lava tabulka stredná ciara
						HRACIE_POLE[5, 2] = '┌'; HRACIE_POLE[5, 6] = '└'; // Lava tabulka horná tabulka Rohy lavé
						HRACIE_POLE[26, 2] = '┐'; HRACIE_POLE[26, 6] = '┘'; // Lava tabulka horná tabulka Rohy pravé
						for (int i = 6; i < 26; i++) { HRACIE_POLE[i, 2] = '─'; } // Lava tabulka horná tabulka horná strana
						for (int i = 6; i < 26; i++) { HRACIE_POLE[i, 6] = '─'; } // Lava tabulka horná tabulka dolná ciara
						for (int i = 3; i < 6; i++) { HRACIE_POLE[5, i] = '│'; } // Lava tabulka horná tabulka lava strana
						for (int i = 3; i < 6; i++) { HRACIE_POLE[26, i] = '│'; }// Lava tabulka horná tabulka prava strana
					} // Lava tabulka Rámčeky
					if (1 == 1)
					{
						HRACIE_POLE[7, 3] = 'Z'; HRACIE_POLE[8, 3] = 'á'; HRACIE_POLE[9, 3] = 'p'; HRACIE_POLE[10, 3] = 'i'; // Lava tabulka horná tabulka 1 riadok
						HRACIE_POLE[11, 3] = 's'; HRACIE_POLE[13, 3] = 'ť'; HRACIE_POLE[14, 3] = 'a'; HRACIE_POLE[15, 3] = 'h'; // Lava tabulka horná tabulka 1 riadok
						HRACIE_POLE[16, 3] = 'u'; HRACIE_POLE[18, 3] = '-'; HRACIE_POLE[19, 3] = '>'; HRACIE_POLE[21, 3] = 'X'; // Lava tabulka horná tabulka 1 riadok
						HRACIE_POLE[23, 3] = 'Y'; // Lava tabulka horná tabulka 1 riadok
						HRACIE_POLE[7, 4] = '['; HRACIE_POLE[9, 4] = 'X'; HRACIE_POLE[11, 4] = '('; HRACIE_POLE[12, 4] = 'M'; // Lava tabulka horná tabulka 2 riadok
						HRACIE_POLE[13, 4] = 'e'; HRACIE_POLE[14, 4] = 'd'; HRACIE_POLE[15, 4] = 'z'; HRACIE_POLE[16, 4] = 'e'; // Lava tabulka horná tabulka 2 riadok
						HRACIE_POLE[17, 4] = 'r'; HRACIE_POLE[18, 4] = 'a'; HRACIE_POLE[19, 4] = ')'; HRACIE_POLE[21, 4] = 'Y'; // Lava tabulka horná tabulka 2 riadok
						HRACIE_POLE[23, 4] = ']'; // Lava tabulka horná tabulka 1 riadok
						HRACIE_POLE[7, 5] = 'P'; HRACIE_POLE[8, 5] = 'r'; HRACIE_POLE[9, 5] = 'í'; HRACIE_POLE[10, 5] = 'k'; // Lava tabulka horná tabulka 3 riadok
						HRACIE_POLE[11, 5] = 'l'; HRACIE_POLE[12, 5] = 'a'; HRACIE_POLE[13, 5] = 'd'; HRACIE_POLE[18, 5] = '-'; // Lava tabulka horná tabulka 3 riadok
						HRACIE_POLE[19, 5] = '>'; HRACIE_POLE[21, 5] = '1'; HRACIE_POLE[23, 5] = '2'; // Lava tabulka horná tabulka 3 riadok
					} // Lava tabulka Text
					if (1 == 1)
					{
						HRACIE_POLE[5, 9] = '┌'; HRACIE_POLE[26, 9] = '┐'; HRACIE_POLE[26, 11] = '┘'; HRACIE_POLE[9, 11] = '┌'; // Lava tabulka dolná tabulka rohy
						HRACIE_POLE[9, 19] = '┘'; HRACIE_POLE[5, 19] = '└'; // Lava tabulka dolná tabulka rohy
						for (int i = 6; i < 26; i++) { HRACIE_POLE[i, 9] = '─'; } // Lava tabulka dolna tabulka horná strana
						for (int i = 10; i < 26; i++) { HRACIE_POLE[i, 11] = '─'; } // Lava tabulka dolna tabulka stredná strana
						for (int i = 10; i < 19; i++) { HRACIE_POLE[5, i] = '│'; } // Lava tabulka dolna tabulka lava strana
						for (int i = 12; i < 19; i++) { HRACIE_POLE[9, i] = '│'; } // Lava tabulka dolna tabulka prava strana
						HRACIE_POLE[9, 10] = '│'; HRACIE_POLE[14, 10] = '│'; HRACIE_POLE[18, 10] = '│'; // Lava tabulka dolná tabulka kolmé čiary X
						HRACIE_POLE[22, 10] = '│'; HRACIE_POLE[26, 10] = '│'; // Lava tabulka dolná tabulka kolmé čiary X
						for (int i = 11; i < 21; i += 2) { HRACIE_POLE[6, i] = '─'; HRACIE_POLE[7, i] = '─'; HRACIE_POLE[8, i] = '─'; } // Lava tabulka dolna tabulka vodorovné čiary Y
						HRACIE_POLE[12, 10] = '1'; HRACIE_POLE[16, 10] = '2'; // Lava tabulka dolná tabulka Y - 1,2
						HRACIE_POLE[20, 10] = '3'; HRACIE_POLE[24, 10] = 'Y'; // Lava tabulka dolná tabulka Y - 3,Y
						HRACIE_POLE[7, 12] = '1'; HRACIE_POLE[7, 14] = '2'; // Lava tabulka dolná tabulka X - 1,2
						HRACIE_POLE[7, 16] = '3'; HRACIE_POLE[7, 18] = 'X'; // Lava tabulka dolná tabulka X - 3,X
						for (int X = 12; X < 18; X += 2)
						{
							for (int Y = 11; Y < 22; Y += 4)
							{
								HRACIE_POLE[Y, X] = '┌'; HRACIE_POLE[Y + 1, X] = '─'; HRACIE_POLE[Y + 2, X] = '┐';
								HRACIE_POLE[Y, X + 1] = '└'; HRACIE_POLE[Y + 1, X + 1] = '─'; HRACIE_POLE[Y + 2, X + 1] = '┘';
							}
						} // Lava tabulka dolná tabulka Hracie pole

					} // Lava tabulka dolna rám
					if (1 == 1)
					{
						for (int X = 0; X < 21; X += 7)
						{
							for (int Y = 31; Y < 62; Y += 15)
							{
								HRACIE_POLE[Y, X] = '┌'; HRACIE_POLE[Y + 14, X] = '┐';
								HRACIE_POLE[Y, X + 6] = '└'; HRACIE_POLE[Y + 14, X + 6] = '┘';
								int P1 = Y + 14;
								int P2 = X + 6;
								int P3 = X + 1;
								for (int i = Y + 1; i < P1; i++) { HRACIE_POLE[i, X] = '─'; } // Hracie pole - horná strana
								for (int i = Y + 1; i < P1; i++) { HRACIE_POLE[i, P2] = '─'; } // Hracie pole - dolná strana
								for (int i = X + 1; i < P2; i++) { HRACIE_POLE[Y, i] = '│'; } // Hracie pole - ľavá strana
								for (int i = X + 1; i < P2; i++) { HRACIE_POLE[P1, i] = '│'; } // Hracie pole - Pravá strana
							}
						}

					} // Hracie Pole Rámy
					if (1 == 1)
					{

						HRACIE_POLE[76, 0] = '┌'; HRACIE_POLE[76, 20] = '└'; // pravá tabulka Rohy lavé
						HRACIE_POLE[105, 0] = '┐'; HRACIE_POLE[105, 20] = '┘'; // pravá tabulka Rohy pravé
						for (int i = 1; i < 20; i++) { HRACIE_POLE[76, i] = '│'; } // pravá tabulka lavá strana
						for (int i = 1; i < 20; i++) { HRACIE_POLE[105, i] = '│'; } // pravá tabulka pravá strana
						for (int i = 77; i < 105; i++) { HRACIE_POLE[i, 0] = '─'; } // pravá tabulka horná strana
						for (int i = 77; i < 105; i++) { HRACIE_POLE[i, 20] = '─'; } // pravá tabulka dolná strana
						for (int i = 77; i < 105; i++) { HRACIE_POLE[i, 10] = '─'; } // pravá tabulka stredná ciara
					} // pravá tabulka Rámčeky
					if (1 == 1)
					{
						HRACIE_POLE[79, 1] = '┌'; HRACIE_POLE[79, 7] = '└'; // pravá tabulka horná tabulka X Rohy lavé
						HRACIE_POLE[93, 1] = '┐'; HRACIE_POLE[93, 7] = '┘'; // pravá tabulka horná tabulka X Rohy pravé
						for (int i = 80; i < 93; i++) { HRACIE_POLE[i, 1] = '─'; } // pravá tabulka horná tabulka X horná strana
						for (int i = 80; i < 93; i++) { HRACIE_POLE[i, 7] = '─'; } // pravá tabulka horná tabulka X dolná ciara
						for (int i = 2; i < 7; i++) { HRACIE_POLE[79, i] = '│'; } // pravá tabulka horná tabulka X lava strana
						for (int i = 2; i < 7; i++) { HRACIE_POLE[93, i] = '│'; }// pravá tabulka horná tabulka X prava strana

						HRACIE_POLE[97, 1] = '┌'; HRACIE_POLE[97, 9] = '└'; // pravá tabulka horná tabulka PC Rohy lavé
						HRACIE_POLE[101, 1] = '┐'; HRACIE_POLE[101, 9] = '┘'; // pravá tabulka horná tabulka PC Rohy pravé
						for (int i = 98; i < 101; i++) { HRACIE_POLE[i, 1] = '─'; } // pravá tabulka horná tabulka PC horná strana
						for (int i = 98; i < 101; i++) { HRACIE_POLE[i, 9] = '─'; } // pravá tabulka horná tabulka PC dolná ciara
						for (int i = 2; i < 9; i++) { HRACIE_POLE[97, i] = '│'; } // pravá tabulka horná tabulka PC lava strana
						for (int i = 2; i < 9; i++) { HRACIE_POLE[101, i] = '│'; }// pravá tabulka horná tabulka PC prava strana


					} // pravá tabulka horná tabulka Rámčeky
					if (1 == 1)
					{
						HRACIE_POLE[99, 2] = 'P'; HRACIE_POLE[99, 3] = 'O'; // pravá tabulka horná tabulka Text
						HRACIE_POLE[99, 4] = 'Č'; HRACIE_POLE[99, 5] = 'Í'; // pravá tabulka horná tabulka Text
						HRACIE_POLE[99, 6] = 'T'; HRACIE_POLE[99, 7] = 'A'; // pravá tabulka horná tabulka Text
						HRACIE_POLE[99, 8] = 'Č';  // pravá tabulka horná tabulka Text

						HRACIE_POLE[84, 2] = '*'; HRACIE_POLE[88, 2] = '*'; // pravá tabulka horná tabulka Text X
						HRACIE_POLE[85, 3] = '*'; HRACIE_POLE[87, 3] = '*'; // pravá tabulka horná tabulka Text X
						HRACIE_POLE[86, 4] = '*';  // pravá tabulka horná tabulka Text X
						HRACIE_POLE[85, 5] = '*'; HRACIE_POLE[87, 5] = '*'; // pravá tabulka horná tabulka Text X
						HRACIE_POLE[84, 6] = '*'; HRACIE_POLE[88, 6] = '*'; // pravá tabulka horná tabulka Text X

					} // pravá tabulka horná tabulka Text
					if (1 == 1)
					{
						HRACIE_POLE[79, 13] = '┌'; HRACIE_POLE[79, 19] = '└'; // pravá tabulka horná tabulka X Rohy lavé
						HRACIE_POLE[93, 13] = '┐'; HRACIE_POLE[93, 19] = '┘'; // pravá tabulka horná tabulka X Rohy pravé
						for (int i = 80; i < 93; i++) { HRACIE_POLE[i, 13] = '─'; } // pravá tabulka horná tabulka X horná strana
						for (int i = 80; i < 93; i++) { HRACIE_POLE[i, 19] = '─'; } // pravá tabulka horná tabulka X dolná ciara
						for (int i = 14; i < 19; i++) { HRACIE_POLE[79, i] = '│'; } // pravá tabulka horná tabulka X lava strana
						for (int i = 14; i < 19; i++) { HRACIE_POLE[93, i] = '│'; }// pravá tabulka horná tabulka X prava strana

						HRACIE_POLE[97, 13] = '┌'; HRACIE_POLE[97, 19] = '└'; // pravá tabulka horná tabulka PC Rohy lavé
						HRACIE_POLE[101, 13] = '┐'; HRACIE_POLE[101, 19] = '┘'; // pravá tabulka horná tabulka PC Rohy pravé
						for (int i = 98; i < 101; i++) { HRACIE_POLE[i, 13] = '─'; } // pravá tabulka horná tabulka PC horná strana
						for (int i = 98; i < 101; i++) { HRACIE_POLE[i, 19] = '─'; } // pravá tabulka horná tabulka PC dolná ciara
						for (int i = 14; i < 19; i++) { HRACIE_POLE[97, i] = '│'; } // pravá tabulka horná tabulka PC lava strana
						for (int i = 14; i < 19; i++) { HRACIE_POLE[101, i] = '│'; }// pravá tabulka horná tabulka PC prava strana
					} // pravá tabulka dolná tabulka Rámčeky
					if (1 == 1)
					{
						HRACIE_POLE[99, 14] = 'T'; HRACIE_POLE[99, 15] = 'V'; // pravá tabulka dolná tabulka Text
						HRACIE_POLE[99, 16] = 'O'; HRACIE_POLE[99, 17] = 'J'; // pravá tabulka dolná tabulka Text
						HRACIE_POLE[99, 18] = 'E';  // pravá tabulka dolná tabulka Text

						for (int i = 83; i < 90; i += 2) { HRACIE_POLE[i, 14] = '*'; } // pravá tabulka dolná tabulka O horná strana
						for (int i = 83; i < 90; i += 2) { HRACIE_POLE[i, 18] = '*'; } // pravá tabulka dolná tabulka O dolná strana
						for (int i = 15; i < 18; i += 1) { HRACIE_POLE[82, i] = '*'; } // pravá tabulka dolná tabulka O ľavá strana
						for (int i = 15; i < 18; i += 1) { HRACIE_POLE[90, i] = '*'; } // pravá tabulka dolná tabulka O pravá strana

					} // pravá tabulka dolná tabulka Text
				} // Hracie pole Zápis Prvotný

				do
				{
					int Pozícia1 = 1, Pozícia2 = 1, Remíza = 0;
					Console.Clear();
					for (int X = 0; X < 21; X++)
					{
						for (int Y = 0; Y < 106; Y++)
						{
							string A = HRACIE_POLE[Y, X].ToString();
							FastConsole.Write(A);
						}
						if (X == 20) Console.Write(" ");
						FastConsole.Flush();
						Console.Write("\n");
					} // Výpis
					Console.Write("\n Tvoj ťah -> ");
					bool Overenie_Odpovede;
					if (1 == 1) {
						String Odpoveď = Console.ReadLine();
						char[] PozíciaVPoliChar = Odpoveď.ToCharArray();
						if (Odpoveď.Length == 3)
						{
							if (PozíciaVPoliChar[1] == ' ')
							{
								if (PozíciaVPoliChar[0] == '1' | PozíciaVPoliChar[0] == '2' | PozíciaVPoliChar[0] == '3' & PozíciaVPoliChar[2] == '1' | PozíciaVPoliChar[2] == '2' | PozíciaVPoliChar[2] == '3')
								{
									Pozícia1 = Convert.ToInt32(PozíciaVPoliChar[0] - 48); Pozícia2 = Convert.ToInt32(PozíciaVPoliChar[2] - 48);
									if (ZápisŤahov[Pozícia1-1,Pozícia2-1] == 'E') Overenie_Odpovede = true;
									else Overenie_Odpovede = false;
								}
								else Overenie_Odpovede = false;
							}
							else Overenie_Odpovede = false;
						} // Overenie Odpovede
						else Overenie_Odpovede = false;
						if (Overenie_Odpovede == false) {
							Console.Clear();
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                     Zadaj si nesprávnu odpoveď.                                       │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
							Console.WriteLine("                          ┌──────────────────────────────────────────────────┐");
							Console.WriteLine("                          │   ┌────────────────────┐ ┌────────────────────┐  │");
							Console.WriteLine("                          │   │ Zápis ťahu -> X Y  │ │   │  1 │ 2 │ 3 │ Y │  │");
							Console.WriteLine("                          │   │ [ X (Medzera) Y ]  │ │───┌────────────────┘  │");
							Console.WriteLine("                          │   │ Príklad    -> 1 2  │ │ 1 │ ┌─┐ ┌─┐ ┌─┐       │");
							Console.WriteLine("                          │   └────────────────────┘ │───│ └─┘ └─┘ └─┘       │");
							Console.WriteLine("                          │                          │ 2 │ ┌─┐ ┌─┐ ┌─┐       │");
							Console.WriteLine("                          │                          │───│ └─┘ └─┘ └─┘       │");
							Console.WriteLine("                          │                          │ 3 │ ┌─┐ ┌─┐ ┌─┐       │");
							Console.WriteLine("                          │                          │───│ └─┘ └─┘ └─┘       │");
							Console.WriteLine("                          │                          │ X │                   │");
							Console.WriteLine("                          │                          └───┘                   │");
							Console.WriteLine("                          └──────────────────────────────────────────────────┘");
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                        Najčastejšie chyby ->                                          │");
							Console.WriteLine(" │                            Stlačenie medzerníku bez zadávania odpovede                                │");
							Console.WriteLine(" │                 Zadávanie odpovede ktorá nie je rozmedzí 1 - 3, neobsahuje medzeru.                   │");
							Console.WriteLine(" │            Zadávanie odpovede ktorá už bola zahraná, na jednu pozíciu nevieš zahrať 2 ťahy.           │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                     Pre pokračovanie stlač ENTER                                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
							Console.WriteLine(" Tvoja odpoveď bola -> " + Odpoveď);


							Console.ReadKey(); Console.Clear();
						} // Výpis pri zlej odpovedi
					} // Zadanie a Overenie správnej odpovede
				   Random Náhoda = new Random();
					if (Overenie_Odpovede == true)
					{
						if (Pozícia1 == 1 & Pozícia2 == 1 & ZápisŤahov[0, 0] == 'E')
						{
							ZápisŤahov[0, 0] = 'O';
							int X = 31, Y = 0;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 1 & Pozícia2 == 2 & ZápisŤahov[0, 1] == 'E')
						{
							ZápisŤahov[0, 1] = 'O';
							int X = 46, Y = 0;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 1 & Pozícia2 == 3 & ZápisŤahov[0, 2] == 'E')
						{
							ZápisŤahov[0, 2] = 'O';
							int X = 61, Y = 0;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 2 & Pozícia2 == 1 & ZápisŤahov[1, 0] == 'E')
						{
							ZápisŤahov[1, 0] = 'O';
							int X = 31, Y = 7;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 2 & Pozícia2 == 2 & ZápisŤahov[1, 1] == 'E')
						{
							ZápisŤahov[1, 1] = 'O';
							int X = 46, Y = 7;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 2 & Pozícia2 == 3 & ZápisŤahov[1, 2] == 'E')
						{
							ZápisŤahov[1, 2] = 'O';
							int X = 61, Y = 7;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 3 & Pozícia2 == 1 & ZápisŤahov[2, 0] == 'E')
						{
							ZápisŤahov[2, 0] = 'O';
							int X = 31, Y = 14;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 3 & Pozícia2 == 2 & ZápisŤahov[2, 1] == 'E')
						{
							ZápisŤahov[2, 1] = 'O';
							int X = 46, Y = 14;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
						if (Pozícia1 == 3 & Pozícia2 == 3 & ZápisŤahov[2, 2] == 'E')
						{
							ZápisŤahov[2, 2] = 'O';
							int X = 61, Y = 14;
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 1] = '*'; }
							for (int i = X + 4; i < X + 12; i += 2) { HRACIE_POLE[i, Y + 5] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 3, i] = '*'; }
							for (int i = Y + 2; i < Y + 5; i += 1) { HRACIE_POLE[X + 11, i] = '*'; }
						}
					} // Zápis ťahu hráča
					if (Overenie_Odpovede == true)
					{
						if (ZápisŤahov[0, 0] == ZápisŤahov[0, 1] & ZápisŤahov[0, 1] == ZápisŤahov[0, 2] & ZápisŤahov[0, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[0, 0] == 'X') vítaz = 'X'; if (ZápisŤahov[0, 0] == 'O') vítaz = 'O'; } // Riadok 1
						if (ZápisŤahov[1, 0] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[1, 2] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Riadok 2
						if (ZápisŤahov[2, 0] == ZápisŤahov[2, 1] & ZápisŤahov[2, 1] == ZápisŤahov[2, 2] & ZápisŤahov[2, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[2, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[2, 1] == 'O') vítaz = 'O'; } // Riadok 3

						if (ZápisŤahov[0, 0] == ZápisŤahov[1, 0] & ZápisŤahov[1, 0] == ZápisŤahov[2, 0] & ZápisŤahov[1, 0] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 0] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 0] == 'O') vítaz = 'O'; } // Stĺpec 1
						if (ZápisŤahov[0, 1] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 1] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Stĺpec 2
						if (ZápisŤahov[0, 2] == ZápisŤahov[1, 2] & ZápisŤahov[1, 2] == ZápisŤahov[2, 2] & ZápisŤahov[1, 2] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 2] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 2] == 'O') vítaz = 'O'; } // Stĺpec 3

						if (ZápisŤahov[0, 0] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 2] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Do kríža
						if (ZápisŤahov[0, 2] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 0] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Do kríža
						Remíza = 0;
						if (vítaz == 'E')
						{
							for (int X = 0; X < 3; X++)
							{
								for (int Y = 0; Y < 3; Y++)
								{ if (ZápisŤahov[X, Y] != 'E') Remíza++; }
							}
						} // Overenie či sú všetky políčka plné
						if (vítaz == 'E' & Remíza == 9) { vítaz = 'R'; KoniecHry = false; }
					} // Overenie vítaza
					if (Overenie_Odpovede == true & vítaz == 'E')
					{
						bool OverenieŤahupočítača = true, ZahralPočítačŤah = false;
						if (ObtiažnosťHryFinalnáObtiažnosť == 1)
						{
							do
							{
								Random Náhoda2 = new Random();
								int Generáciaťahu = Náhoda2.Next(1, 10 + 1);
								if (ZahralPočítačŤah == false & Generáciaťahu != 1 & Generáciaťahu != 2 & Generáciaťahu != 3 & Generáciaťahu != 4)
								{
									do
									{
										OverenieŤahupočítača = true;
										int ŤahPočítača1 = Náhoda.Next(1, 3 + 1), ŤahPočítača2 = Náhoda.Next(1, 3 + 1); // Náhodné generovanie ťahou PC
										if (1 == 1)
										{
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 1 & ZápisŤahov[0, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 0] = 'X';
												int X = 31, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 2 & ZápisŤahov[0, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 1] = 'X';
												int X = 46, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 3 & ZápisŤahov[0, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 2] = 'X';
												int X = 61, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 1 & ZápisŤahov[1, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 0] = 'X';
												int X = 31, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 2 & ZápisŤahov[1, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 1] = 'X';
												int X = 46, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 3 & ZápisŤahov[1, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 2] = 'X';
												int X = 61, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 1 & ZápisŤahov[2, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 0] = 'X';
												int X = 31, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 2 & ZápisŤahov[2, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 1] = 'X';
												int X = 46, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 3 & ZápisŤahov[2, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 2] = 'X';
												int X = 61, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
										} // Overenie náhodného čísla a zápis ťahu
									} while (OverenieŤahupočítača); // Cyklus náhodného ťahu
									ZahralPočítačŤah = true;
								} // Náhodný ťah PC
								if (ZahralPočítačŤah == false & Generáciaťahu == 1 | Generáciaťahu == 2)
								{
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Výherný ťah pre počítač
								if (ZahralPočítačŤah == false & Generáciaťahu == 3 | Generáciaťahu == 4)
								{
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Blokovanie ťahu Hráča počítačom
							} while (ZahralPočítačŤah == false);
						} // Ľahká obtiažnosť
						if (ObtiažnosťHryFinalnáObtiažnosť == 2)
						{
							do
							{
								Random Náhoda2 = new Random();
								int Generáciaťahu = Náhoda2.Next(1, 11 + 1);
								if (ZahralPočítačŤah == false & Generáciaťahu == 1)
								{
									do
									{
										OverenieŤahupočítača = true;
										int ŤahPočítača1 = Náhoda.Next(1, 3 + 1), ŤahPočítača2 = Náhoda.Next(1, 3 + 1); // Náhodné generovanie ťahou PC
										if (1 == 1)
										{
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 1 & ZápisŤahov[0, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 0] = 'X';
												int X = 31, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 2 & ZápisŤahov[0, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 1] = 'X';
												int X = 46, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 3 & ZápisŤahov[0, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 2] = 'X';
												int X = 61, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 1 & ZápisŤahov[1, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 0] = 'X';
												int X = 31, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 2 & ZápisŤahov[1, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 1] = 'X';
												int X = 46, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 3 & ZápisŤahov[1, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 2] = 'X';
												int X = 61, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 1 & ZápisŤahov[2, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 0] = 'X';
												int X = 31, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 2 & ZápisŤahov[2, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 1] = 'X';
												int X = 46, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 3 & ZápisŤahov[2, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 2] = 'X';
												int X = 61, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
										} // Overenie náhodného čísla a zápis ťahu
									} while (OverenieŤahupočítača); // Cyklus náhodného ťahu
									ZahralPočítačŤah = true;
								} // Náhodný ťah PC
								if (ZahralPočítačŤah == false & Generáciaťahu == 2 | Generáciaťahu == 3 | Generáciaťahu == 4 | Generáciaťahu == 5 | Generáciaťahu == 6)
								{
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Výherný ťah pre počítač
								if (ZahralPočítačŤah == false & Generáciaťahu == 7 | Generáciaťahu == 8 | Generáciaťahu == 9 | Generáciaťahu == 10| Generáciaťahu == 11)
								{
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Blokovanie ťahu Hráča počítačom
							} while (ZahralPočítačŤah == false);
						} // Stredná obtiažnosť
						if (ObtiažnosťHryFinalnáObtiažnosť == 3)
						{
							do
							{
								if (ZahralPočítačŤah == false)
								{
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'X' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'X' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'X' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'X' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'X' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Výherný ťah pre počítač
								if (ZahralPočítačŤah == false)
								{
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 1
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[0, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 2
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 3
									if (ZápisŤahov[1, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 4
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 5
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[2, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 6

									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 7
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'O' & ZápisŤahov[2, 0] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 8
									if (ZápisŤahov[0, 1] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 9
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 1] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 1] = 'X';
										int X = 46, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 10
									if (ZápisŤahov[0, 2] == 'E' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 11
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 12

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 13
									if (ZápisŤahov[0, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 0] = 'X';
										int X = 31, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 14
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'E' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 2] = 'X';
										int X = 61, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 15
									if (ZápisŤahov[2, 0] == 'E' & ZápisŤahov[1, 1] == 'O' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[2, 0] = 'X';
										int X = 31, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 16

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[0, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[0, 1] = 'X';
										int X = 46, Y = 0;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 17
									if (ZápisŤahov[1, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[1, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 18
									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 14;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 19

									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 0] == 'E' & ZápisŤahov[2, 0] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 0] = 'X';
										int X = 31, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 20
									if (ZápisŤahov[0, 1] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 1] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 21
									if (ZápisŤahov[0, 2] == 'O' & ZápisŤahov[1, 2] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 2] = 'X';
										int X = 61, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 22

									if (ZápisŤahov[2, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[0, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 23
									if (ZápisŤahov[0, 0] == 'O' & ZápisŤahov[1, 1] == 'E' & ZápisŤahov[2, 2] == 'O' & ZahralPočítačŤah == false)
									{
										ZápisŤahov[1, 1] = 'X';
										int X = 46, Y = 7;
										HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
										HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
										HRACIE_POLE[X + 7, Y + 3] = '*';
										HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
										HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
										ZahralPočítačŤah = true;
									} // Ťah číslo 24
								} // Blokovanie ťahu Hráča počítačom
								if (ZahralPočítačŤah == false)
								{
									do
									{
										OverenieŤahupočítača = true;
										int ŤahPočítača1 = Náhoda.Next(1, 3 + 1), ŤahPočítača2 = Náhoda.Next(1, 3 + 1); // Náhodné generovanie ťahou PC
										if (1 == 1)
										{
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 1 & ZápisŤahov[0, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 0] = 'X';
												int X = 31, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 2 & ZápisŤahov[0, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 1] = 'X';
												int X = 46, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 1 & ŤahPočítača2 == 3 & ZápisŤahov[0, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[0, 2] = 'X';
												int X = 61, Y = 0;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 1 & ZápisŤahov[1, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 0] = 'X';
												int X = 31, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 2 & ZápisŤahov[1, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 1] = 'X';
												int X = 46, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 2 & ŤahPočítača2 == 3 & ZápisŤahov[1, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[1, 2] = 'X';
												int X = 61, Y = 7;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 1 & ZápisŤahov[2, 0] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 0] = 'X';
												int X = 31, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 2 & ZápisŤahov[2, 1] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 1] = 'X';
												int X = 46, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
											if (ŤahPočítača1 == 3 & ŤahPočítača2 == 3 & ZápisŤahov[2, 2] == 'E')
											{
												OverenieŤahupočítača = false;
												ZápisŤahov[2, 2] = 'X';
												int X = 61, Y = 14;
												HRACIE_POLE[X + 5, Y + 1] = '*'; HRACIE_POLE[X + 9, Y + 1] = '*';
												HRACIE_POLE[X + 6, Y + 2] = '*'; HRACIE_POLE[X + 8, Y + 2] = '*';
												HRACIE_POLE[X + 7, Y + 3] = '*';
												HRACIE_POLE[X + 6, Y + 4] = '*'; HRACIE_POLE[X + 8, Y + 4] = '*';
												HRACIE_POLE[X + 5, Y + 5] = '*'; HRACIE_POLE[X + 9, Y + 5] = '*';
											}
										} // Overenie náhodného čísla a zápis ťahu
									} while (OverenieŤahupočítača); // Cyklus náhodného ťahu
									ZahralPočítačŤah = true;
								} // Náhodný ťah PC
							} while (ZahralPočítačŤah == false);
						} // Ťažká obtiažnosť

					} // Ťah počítača
					if (1 == 1)
					{
						if (ZápisŤahov[0, 0] == ZápisŤahov[0, 1] & ZápisŤahov[0, 1] == ZápisŤahov[0, 2] & ZápisŤahov[0, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[0, 0] == 'X') vítaz = 'X'; if (ZápisŤahov[0, 0] == 'O') vítaz = 'O'; } // Riadok 1
						if (ZápisŤahov[1, 0] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[1, 2] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Riadok 2
						if (ZápisŤahov[2, 0] == ZápisŤahov[2, 1] & ZápisŤahov[2, 1] == ZápisŤahov[2, 2] & ZápisŤahov[2, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[2, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[2, 1] == 'O') vítaz = 'O'; } // Riadok 3

						if (ZápisŤahov[0, 0] == ZápisŤahov[1, 0] & ZápisŤahov[1, 0] == ZápisŤahov[2, 0] & ZápisŤahov[1, 0] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 0] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 0] == 'O') vítaz = 'O'; } // Stĺpec 1
						if (ZápisŤahov[0, 1] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 1] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Stĺpec 2
						if (ZápisŤahov[0, 2] == ZápisŤahov[1, 2] & ZápisŤahov[1, 2] == ZápisŤahov[2, 2] & ZápisŤahov[1, 2] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 2] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 2] == 'O') vítaz = 'O'; } // Stĺpec 3

						if (ZápisŤahov[0, 0] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 2] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Do kríža
						if (ZápisŤahov[0, 2] == ZápisŤahov[1, 1] & ZápisŤahov[1, 1] == ZápisŤahov[2, 0] & ZápisŤahov[1, 1] != 'E')
						{ KoniecHry = false; if (ZápisŤahov[1, 1] == 'X') vítaz = 'X'; if (ZápisŤahov[1, 1] == 'O') vítaz = 'O'; } // Do kríža

						if (vítaz == 'E') if (1 == 1)
							{
								for (int X = 0; X < 3; X++)
								{
									for (int Y = 0; Y < 3; Y++)
									{ if (ZápisŤahov[X, Y] != 'E') Remíza++; }
								}
							} // Overenie či sú všetky políčka plné
						if (vítaz == 'E' & Remíza == 9) { vítaz = 'R'; KoniecHry = false; }
					} // Overenie vítaza
				} while (KoniecHry); // Hlavné telo
				Console.Clear();
				for (int X = 0; X < 21; X++)
				{
					for (int Y = 0; Y < 106; Y++)
					{
						string A = HRACIE_POLE[Y, X].ToString();
						FastConsole.Write(A);
					}
					if (X == 20) Console.Write(" ");
					FastConsole.Flush();
					Console.Write("\n");
				} // Konečný výpis
				Console.WriteLine();
				if (1 == 1)
				{
					if (vítaz == 'O')
					{
						Random nahoda = new Random();
						int náhodnéčíslo = nahoda.Next(0, 2 + 1);
						if (náhodnéčíslo == 0)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                              Vyhral si! Gratulujem, porazil si počítač.                               │");
							Console.WriteLine(" │                         Veľa štatia do ďalšieho kola :), Budeš ho potrebovať.                         │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 1)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                        Tento krát si mal štatie :D, uvidíme či ti to vydrží.                          │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 2)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                      WOW, tak ty si to zahral ako profík. Či to bola len náhoda?                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
					} // Vítaz Hráč
					if (vítaz == 'X')
					{
						Random nahoda = new Random();
						int náhodnéčíslo = nahoda.Next(0, 2 + 1);
						if (náhodnéčíslo == 0)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                    Prehral si! Porazil ťa počítač.                                    │");
							Console.WriteLine(" │                              Musíš sa viacej snažiť, skús ho poraziť!                                 │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 1)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                          To čo sa práve stalo? Teba vážne porazil počítač?                            │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 2)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                     WOW, tak to som ešte nevidel... Ako ťa mohol poraziť počítač?                     │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
					} // Vítaz Počítač
					if (vítaz == 'R')
					{
						Random nahoda = new Random();
						int náhodnéčíslo = nahoda.Next(0, 2 + 1);
						if (náhodnéčíslo == 0)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                        Remíza! Nikto nevyhral.                                        │");
							Console.WriteLine(" │                           Bohužial si nevyhral, ale zase si ani neprehral.                            │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 1)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                   Nikto nevyhral, skús to znova. Ak si nepochopil, je to remíza :D.                   │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
						if (náhodnéčíslo == 2)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                          Skoro! Ani ty ani počítač nevyhral! Skús to znova.                           │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						}
					} // Vítaz Remíza 
				} // Výpis Vítaza
				if (1 == 1)
				{
					Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
					Console.WriteLine(" │                                 Pre pokračovanie stlač klávesu ENTER                                  │");
					Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
					Console.ReadKey(); Console.Clear();
				} // Pre pokračovanie stlačte ENTER
				string ZopakovanieHry;
				bool OverenieOdpovede = false, SprávnaOdpoved = false;
				if (1 == 1)
				{
					do
					{
						if (1 == 1)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                                   Chcel by si si zahrať ešte raz?                                     │");
							Console.WriteLine(" │                     Možné odpovede - Áno / Nie (Po zadaní odpovede stlačte ENTER)                     │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						} // Chces si zahrat znova?
						Console.Write(" Tvoja odpoveď -> ");
						ZopakovanieHry = Console.ReadLine();
						ZopakovanieHry = ZopakovanieHry.ToLower();
						if (ZopakovanieHry == "áno" | ZopakovanieHry == "ano") { ZopakovanieHryCyklu = true; OverenieOdpovede = true; SprávnaOdpoved = true; } // Overenie odpovede
						if (ZopakovanieHry == "nie") { ZopakovanieHryCyklu = false; OverenieOdpovede = true; SprávnaOdpoved = true; } // Overenie odpovede
						if (SprávnaOdpoved == false) OverenieOdpovede = true;
						Console.Clear();
						if (OverenieOdpovede == false)
                        {
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                               Zadal si odpoveď ktorá nebola na výber.                                 │");
							Console.WriteLine(" │                                     Pre pokračovanie stlač ENTER                                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						} // Odpoved ktorá nebola na výber
						if (ZopakovanieHryCyklu == true & OverenieOdpovede == true)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
							Console.WriteLine(" │                              To sa mi páči! Veľa štastia do ďalšej hry.                               │");
							Console.WriteLine(" │                                     Pre pokračovanie stlač ENTER                                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						} // Odpoved Zahrám znova
						if (ZopakovanieHryCyklu == false)
						{
							Console.WriteLine(" ┌───────────────────────────────────────────────────────────────────────────────────────────────────────┐");
                            Console.WriteLine(" │                   Škoda! Dúfam že sa niekedy ešte vrátiš, do tej doby sa maj krásne.                  │");
							Console.WriteLine(" │                                     Pre pokračovanie stlač ENTER                                      │");
							Console.WriteLine(" └───────────────────────────────────────────────────────────────────────────────────────────────────────┘");
						} // Odpoved Nezahrám znova
						Console.ReadKey(); Console.Clear();
					} while (OverenieOdpovede == false & ZopakovanieHry.Length > 0);
				} // Chceš si zahrať znova? 

			} while (ZopakovanieHryCyklu); // Celá hra
		}
		static void KONIEC_PROGRAMU()
		{
			Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ Koniec programu, Na ukončenie programu stlačte klávesu ENTER              │");
			Console.WriteLine("└───────────────────────────────────────────────────────────────────────────┘");
		}
		static void COPYRIGHT()
        {
            Console.WriteLine("┌───────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ Patrik Kozela © 2021                                                      │");
            Console.WriteLine("│ Kontaktné údaje ->                                                        │");
            Console.WriteLine("│ Email      -> Patrik.Kozela@Gmail.com                                     │");
            Console.WriteLine("│ Facebook   -> Patrik Kozela                                               │");
            Console.WriteLine("│ Instagram  -> Patrik Kozela                                               │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────────────────┘");
            Console.ReadKey();
        }
	}
}
