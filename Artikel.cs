public class Artikel
{
   
    public string Name { get; set; }
    public string Beschreibung { get; set; }
    public int Anzahl { get; set; }

    //Konstruktor
    public Artikel() { }

    public Artikel(string name, string beschreibung, int anzahl)
    {
        Name = name;
        Beschreibung = beschreibung;
        Anzahl = anzahl;
    }
}
