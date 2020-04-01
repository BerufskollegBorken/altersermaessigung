using System;

namespace altersermaessigung
{
    public class Anrechnung
    {
        public int LehrerIdUntis { get; set; }
        public bool Gelöscht { get; set; }
        public double Wert { get; set; }
        public string Beschreibung { get; set; }
        public Anrechnungsgrund Grund { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public string Zeitart { get; internal set; }
    }
}