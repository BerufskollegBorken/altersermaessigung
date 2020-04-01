using System;

namespace altersermaessigung
{
    public class Lehrer
    {
        public int IdAtlantis { get; internal set; }
        public string Kürzel { get; internal set; }
        public string Mail { get; internal set; }
        public string Nachname { get; internal set; }
        public string Vorname { get; internal set; }
        public string Anrede { get; internal set; }
        public string Titel { get; internal set; }
        public string Raum { get; internal set; }
        public string Funktion { get; internal set; }
        public string Dienstgrad { get; internal set; }
        public DateTime Geburtsdatum { get; internal set; }
        public string Typ { get; internal set; }
        public string Personalnummer { get; internal set; }
        public string Geschlecht { get; internal set; }
        public string Staatsang { get; internal set; }
        public string PLZ { get; internal set; }
        public string Dienstbezeichnung { get; internal set; }
        public DateTime Autrittsdatum { get; internal set; }
        public string Beschäftigungsverhältnis { get; internal set; }
        public string Beschaeftigungsart { get; internal set; }
        public string Rechtsverhaeltnis { get; internal set; }
        public string Besoldungsgruppe { get; internal set; }
        public string Ausweisnummer { get; internal set; }
        public string Geburtsort { get; internal set; }
        public string Aktiv { get; internal set; }
        public string MitgliedSchulleitung { get; internal set; }
        public DateTime Eintrittsdatum { get; internal set; }
        public string Mutterschutz { get; internal set; }
        public string Strasse { get; internal set; }
        public string Ort { get; internal set; }
        public string Telefon { get; internal set; }
        public string Lehramt { get; internal set; }
        public Anrechnungs Anrechnungs { get; internal set; }
        
        public Lehrer()
        {
        }
    }
}