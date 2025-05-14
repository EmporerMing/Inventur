public class Benutzer
{
  
    public string Benutzername { get; set; }
    public string Passwort { get; set; }
    public string Gruppe { get; set; }

    //Konstruktor
    public Benutzer() { }

    public Benutzer(string benutzername, string passwort, string gruppe)
    {
        Benutzername = benutzername;
        Passwort = passwort;
        Gruppe = gruppe;
    }
}



