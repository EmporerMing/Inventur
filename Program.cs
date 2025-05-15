using System;
using System.Collections.Generic;
using Inventur_MiLuv1.ArtikelVerwaltung;

namespace Inventur_MiLuv1
{
    class Program
    {
        static List<Benutzer> benutzerListe = new List<Benutzer>();
        static List<Artikel> artikelListe = new List<Artikel>();
        static List<Vorschlag> vorschlagsListe = new List<Vorschlag>();

        static void Main(string[] args)
        {
            // Standard-Admin anlegen
            benutzerListe.Add(new Benutzer("admin", "admin", "Administrator"));

            bool programmBeendet = false;

            while (!programmBeendet)
            {
                Console.WriteLine("Benutzername:");
                string benutzername = Console.ReadLine();
                Console.WriteLine("Passwort:");
                string passwort = Console.ReadLine();

                Benutzer benutzer = null;
                foreach (var b in benutzerListe)
                {
                    if (b.Benutzername == benutzername && b.Passwort == passwort)
                    {
                        benutzer = b;
                        break;
                    }
                }

                if (benutzer != null)
                {
                    bool abmelden = false;
                    while (!abmelden)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Menü:");
                        Console.WriteLine("1. Vorschlag erfassen");
                        Console.WriteLine("2. Vorschläge bestätigen");
                        if (benutzer.Gruppe == "Administrator")
                        {
                            Console.WriteLine("3. Benutzer anlegen");
                            Console.WriteLine("4. Neuen Admin anlegen");
                            Console.WriteLine("5. Artikel anzeigen");
                            Console.WriteLine("6. Artikel suchen");
                            Console.WriteLine("7. Beenden");
                        }
                        else
                        {
                            Console.WriteLine("3. Artikel anzeigen");
                            Console.WriteLine("4. Artikel suchen");
                            Console.WriteLine("5. Beenden");
                        }

                        string auswahl = Console.ReadLine();

                        if (benutzer.Gruppe == "Administrator")
                        {
                            switch (auswahl)
                            {
                                case "1":
                                    VorschlagErfassen();
                                    break;
                                case "2":
                                    VorschlaegeBestaetigen();
                                    break;
                                case "3":
                                    BenutzerAnlegen();
                                    break;
                                case "4":
                                    AdminAnlegen();
                                    break;
                                case "5":
                                    ArtikelAnzeigen();
                                    break;
                                case "6":
                                    ArtikelSuchen();
                                    break;
                                case "7":
                                    abmelden = true;
                                    programmBeendet = true;
                                    break;
                                default:
                                    Console.WriteLine("Ungültige Auswahl.");
                                    break;
                            }
                        }
                        else
                        {
                            switch (auswahl)
                            {
                                case "1":
                                    VorschlagErfassen();
                                    break;
                                case "2":
                                    VorschlaegeBestaetigen();
                                    break;
                                case "3":
                                    ArtikelAnzeigen();
                                    break;
                                case "4":
                                    ArtikelSuchen();
                                    break;
                                case "5":
                                    abmelden = true;
                                    programmBeendet = true;
                                    break;
                                default:
                                    Console.WriteLine("Ungültige Auswahl.");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Anmeldung fehlgeschlagen!");
                }
            }

            Console.WriteLine("Programm beendet.");
        }

        static void VorschlagErfassen()
        {
            Console.WriteLine("Neuen Artikelsvorschlag erfassen:");
            Console.Write("Artikelnummer (ganzzahlig): ");
            int artikelnummer;
            while (!int.TryParse(Console.ReadLine(), out artikelnummer))
            {
                Console.WriteLine("Bitte eine gültige Artikelnummer eingeben.");
                Console.Write("Artikelnummer: ");
            }

            Console.Write("Menge: ");
            int menge;
            while (!int.TryParse(Console.ReadLine(), out menge))
            {
                Console.WriteLine("Bitte eine gültige Menge eingeben.");
                Console.Write("Menge: ");
            }

            var vorschlag = new Vorschlag { Artikelnummer = artikelnummer, Menge = menge };
            vorschlagsListe.Add(vorschlag);
            Console.WriteLine("Vorschlag gespeichert!");
        }

        static void VorschlaegeBestaetigen()
        {
            if (vorschlagsListe.Count == 0)
            {
                Console.WriteLine("Keine Vorschläge vorhanden.");
                return;
            }

            Console.WriteLine("Offene Vorschläge:");
            for (int i = 0; i < vorschlagsListe.Count; i++)
            {
                var v = vorschlagsListe[i];
                Console.WriteLine($"{i + 1}. Artikelnummer: {v.Artikelnummer}, Menge: {v.Menge}");
            }

            Console.Write("Vorschlagnummer zum Bestätigen (0 zum Abbrechen): ");
            if (int.TryParse(Console.ReadLine(), out int auswahl) && auswahl > 0 && auswahl <= vorschlagsListe.Count)
            {
                var bestaetigterVorschlag = vorschlagsListe[auswahl - 1];

                // Artikel anlegen (Name/Beschreibung als Platzhalter)
                artikelListe.Add(new Artikel(
                    $"Artikel {bestaetigterVorschlag.Artikelnummer}",
                    "Beschreibung folgt",
                    bestaetigterVorschlag.Menge
                ));

                vorschlagsListe.RemoveAt(auswahl - 1);
                Console.WriteLine("Vorschlag wurde als Artikel übernommen!");
            }
            else if (auswahl != 0)
            {
                Console.WriteLine("Ungültige Eingabe.");
            }
        }

        static void ArtikelAnzeigen()
        {
            if (artikelListe.Count == 0)
            {
                Console.WriteLine("Keine Artikel vorhanden.");
                return;
            }

            Console.WriteLine("Name\t\tBeschreibung\t\tAnzahl");
            foreach (var artikel in artikelListe)
            {
                Console.WriteLine($"{artikel.Name}\t{artikel.Beschreibung}\t{artikel.Anzahl}");
            }
        }

        static void ArtikelSuchen()
        {
            Console.Write("Artikelname oder Beschreibung suchen: ");
            string suche = Console.ReadLine();

            var gefunden = artikelListe.FindAll(a =>
                (a.Name != null && a.Name.Contains(suche, StringComparison.OrdinalIgnoreCase)) ||
                (a.Beschreibung != null && a.Beschreibung.Contains(suche, StringComparison.OrdinalIgnoreCase)));

            if (gefunden.Count == 0)
            {
                Console.WriteLine("Kein Artikel gefunden.");
            }
            else
            {
                Console.WriteLine("Gefundene Artikel:");
                foreach (var artikel in gefunden)
                {
                    Console.WriteLine($"Name: {artikel.Name}, Beschreibung: {artikel.Beschreibung}, Anzahl: {artikel.Anzahl}");
                }
            }
        }

        static void BenutzerAnlegen()
        {
            Console.WriteLine("Neuen Benutzer anlegen:");
            string benutzername;
            do
            {
                Console.Write("Benutzername: ");
                benutzername = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(benutzername))
                {
                    Console.WriteLine("Benutzername darf nicht leer sein.");
                    continue;
                }
                bool existiert = benutzerListe.Exists(b => b.Benutzername == benutzername);
                if (existiert)
                {
                    Console.WriteLine("Benutzername existiert bereits. Bitte anderen wählen.");
                    benutzername = "";
                }
            } while (string.IsNullOrWhiteSpace(benutzername));

            string passwort;
            do
            {
                Console.Write("Passwort: ");
                passwort = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(passwort))
                    Console.WriteLine("Passwort darf nicht leer sein.");
            } while (string.IsNullOrWhiteSpace(passwort));

            string gruppe = "";
            do
            {
                Console.Write("Gruppe wählen (1 = Aushilfe, 2 = Mitarbeiter, 3 = Administrator): ");
                string eingabe = Console.ReadLine();
                switch (eingabe)
                {
                    case "1":
                        gruppe = "Aushilfe";
                        break;
                    case "2":
                        gruppe = "Mitarbeiter";
                        break;
                    case "3":
                        gruppe = "Administrator";
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe! Bitte 1, 2 oder 3 eingeben.");
                        break;
                }
            } while (string.IsNullOrWhiteSpace(gruppe));

            benutzerListe.Add(new Benutzer(benutzername, passwort, gruppe));
            Console.WriteLine("Neuer Benutzer wurde angelegt.");
        }

        static void AdminAnlegen()
        {
            Console.WriteLine("Neuen Administrator anlegen:");
            string benutzername;
            do
            {
                Console.Write("Benutzername: ");
                benutzername = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(benutzername))
                {
                    Console.WriteLine("Benutzername darf nicht leer sein.");
                    continue;
                }
                bool existiert = benutzerListe.Exists(b => b.Benutzername == benutzername);
                if (existiert)
                {
                    Console.WriteLine("Benutzername existiert bereits. Bitte anderen wählen.");
                    benutzername = "";
                }
            } while (string.IsNullOrWhiteSpace(benutzername));

            string passwort;
            do
            {
                Console.Write("Passwort: ");
                passwort = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(passwort))
                    Console.WriteLine("Passwort darf nicht leer sein.");
            } while (string.IsNullOrWhiteSpace(passwort),
            benutzerListe.Add(new Benutzer(benutzername, passwort, "Administrator"));
            Console.WriteLine("Neuer Administrator wurde angelegt.");
        }
    }
}
