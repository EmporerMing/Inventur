using Inventur_MiLuv1;
using System;
using System.Collections.Generic;

namespace ArtikelVerwaltung
{
    class Program
    {
        static List<Benutzer> benutzerListe = new List<Benutzer>();
        static List<Artikel> artikelListe = new List<Artikel>();

        static void Main(string[] args)
        {
            // Standard-Admin anlegen
            benutzerListe.Add(new Benutzer { Benutzername = "admin", Passwort = "admin", Gruppe = "Administrator" });

            bool programmBeendet = false;

            while (!programmBeendet)
            {
                // Login
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
                        // Menü
                        Console.WriteLine();
                        Console.WriteLine("Menü:");
                        Console.WriteLine("1. Vorschlag erfassen");
                        Console.WriteLine("2. Vorschläge bestätigen");
                        Console.WriteLine("3. Benutzer anlegen");
                        Console.WriteLine("4. Artikel anzeigen");
                        Console.WriteLine("5. Artikel suchen");
                        Console.WriteLine("6. Beenden");
                        // Nur für Admins ersichtlich: Einen Admin mit Benutzernamen anlegen
                        if (benutzer.Gruppe == "Administrator")
                        {
                            Console.WriteLine("7. Neuen Admin anlegen");
                        }

                        string auswahl = Console.ReadLine();

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
                                ArtikelAnzeigen();
                                break;
                            case "5":
                                ArtikelSuchen();
                                break;
                            case "6":
                                abmelden = true;
                                programmBeendet = true;
                                break;
                            case "7":
                                if (benutzer.Gruppe == "Administrator")
                                    AdminAnlegen();
                                else
                                    Console.WriteLine("Keine Berechtigung.");
                                break;
                            default:
                                Console.WriteLine("Ungültige Auswahl.");
                                break;
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

        // Platzhalter-Methoden
        static void VorschlagErfassen()
        {
            Console.WriteLine("[Platzhalter] Vorschlag erfassen");
        }

        static void VorschlaegeBestaetigen()
        {
            Console.WriteLine("[Platzhalter] Vorschläge bestätigen");
        }

        static void BenutzerAnlegen()
        {
            Console.WriteLine("[Platzhalter] Benutzer anlegen");
        }

        static void ArtikelAnzeigen()
        {
            Console.WriteLine("[Platzhalter] Artikel anzeigen");
        }

        static void ArtikelSuchen()
        {
            Console.WriteLine("[Platzhalter] Artikel suchen");
        }

        // Methode: Admin anlegen
        static void AdminAnlegen()
        {
            Console.WriteLine("Neuen Administrator anlegen:");

            // Benutzername
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
                // Prüfen, ob Benutzername schon existiert
                bool existiert = false;
                foreach (var b in benutzerListe)
                {
                    if (b.Benutzername == benutzername)
                    {
                        existiert = true;
                        break;
                    }
                }
                if (existiert)
                {
                    Console.WriteLine("Benutzername existiert bereits. Bitte anderen wählen.");
                    benutzername = "";
                }
            } while (string.IsNullOrWhiteSpace(benutzername));

            // Passwort
            string passwort;
            do
            {
                Console.Write("Passwort: ");
                passwort = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(passwort))
                    Console.WriteLine("Passwort darf nicht leer sein.");
            } while (string.IsNullOrWhiteSpace(passwort));

            // Administratorengruppe
            benutzerListe.Add(new Benutzer { Benutzername = benutzername, Passwort = passwort, Gruppe = "Administrator" });
            Console.WriteLine("Neuer Administrator wurde angelegt.");
        }
    }
}
