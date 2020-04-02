using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<Anrechnung> Anrechnungs { get; internal set; }
        public double DeputatSoll { get; internal set; }

        public Lehrer()
        {
        }

        internal void PrüfeAltersermäßigung()
        {            
            bool imVorherigenSchuljahrOderDavor55Geworden = this.Geburtsdatum.AddYears(55) < Global.ErsterTagDesSchuljahres ? true : false;

            bool innerhalbDiesesSchuljahres55sterGeburtstag = Global.ErsterTagDesSchuljahres <= this.Geburtsdatum.AddYears(55) && this.Geburtsdatum.AddYears(55) <= Global.LetzterTagDesSchuljahres ? true : false;

            bool imVorherigenSchuljahrOderDavor60Geworden = this.Geburtsdatum.AddYears(60) < Global.ErsterTagDesSchuljahres ? true : false;

            bool innerhalbDiesesSchuljahres60sterGeburtstag = Global.ErsterTagDesSchuljahres <= this.Geburtsdatum.AddYears(60) && this.Geburtsdatum.AddYears(60) <= Global.LetzterTagDesSchuljahres ? true : false;

            bool vollzeit = this.DeputatSoll == 25.5 ? true : false;

            bool mindestens75Prozent = !vollzeit && this.DeputatSoll > 25.5 * 0.75 ? true : false;

            bool mindestens50Prozent = !vollzeit && !mindestens75Prozent && this.DeputatSoll > 25.5 * 0.5 ? true : false;

            if (imVorherigenSchuljahrOderDavor60Geworden)
            {
                if (vollzeit)
                {
                    // Altersermäßigung 3 Stunde

                    if (!(from a in this.Anrechnungs where a.Grund.Nummer == "200" where a.Wert == 3 select a).Any())
                    {
                        double ist = (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a).Any() ? (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a.Wert).FirstOrDefault() : 0;

                        Console.WriteLine("Der Lehrer " + this.Kürzel.PadRight(4) + " (" + this.Geburtsdatum.ToShortDateString() + ") muss im kommenden Schuljahr eine Altersermäßigung von 3.0 statt " + ist + " bekommen. ");
                    }
                }
                if (mindestens75Prozent)
                {
                    // Altersermäßigung 2 Stunden

                    if (!(from a in this.Anrechnungs where a.Grund.Nummer == "200" where a.Wert == 2 select a).Any())
                    {
                        double ist = (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a).Any() ? (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a.Wert).FirstOrDefault() : 0;

                        Console.WriteLine("[WARNUNG] Der Lehrer " + this.Kürzel + " (" + this.Geburtsdatum.ToShortDateString() + "," + DeputatSoll + ") muss im kommenden Schuljahr eine Altersermäßigung von 2.0 statt " + ist + " bekommen. ");
                    }
                }
                if (mindestens50Prozent)
                {
                    // Altersermäßigung 1,5 Stunden
                    if (!(from a in this.Anrechnungs where a.Grund.Nummer == "200" where a.Wert == 1.5 select a).Any())
                    {
                        double ist = (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a).Any() ? (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a.Wert).FirstOrDefault() : 0;

                        Console.WriteLine("[WARNUNG] Der Lehrer " + this.Kürzel + " (" + this.Geburtsdatum.ToShortDateString() + "," + DeputatSoll+ ") muss im kommenden Schuljahr eine Altersermäßigung von 1.5 statt " + ist + " bekommen. ");
                    }
                }
            }
            else
            {
                if (imVorherigenSchuljahrOderDavor55Geworden)
                {
                    if (vollzeit)
                    {
                        // Altersermäßigung 1 Stunde
                        if (!(from a in this.Anrechnungs where a.Grund.Nummer == "200" where a.Wert == 1 select a).Any())
                        {
                            double ist = (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a).Any() ? (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a.Wert).FirstOrDefault() : 0;

                            Console.WriteLine("[WARNUNG] Der Lehrer " + this.Kürzel + " (" + this.Geburtsdatum.ToShortDateString() + "," + DeputatSoll + ") muss im kommenden Schuljahr eine Altersermäßigung von 1.0 statt " + ist + " bekommen. ");
                        }
                    }
                    if (mindestens50Prozent || mindestens75Prozent)
                    {
                        // Altersermäßigung 0,5 Stunden
                        if ((from a in this.Anrechnungs where a.Grund.Nummer == null select a).Any())
                        {
                            Console.WriteLine("[FEHLER] Der Lehrer " + this.Kürzel + " (" + this.Geburtsdatum.ToShortDateString() + "," + DeputatSoll + ") hat eine Anrechnung ohne Grund. ENTER");
                            Console.ReadKey();
                        }
                        if (!(from a in this.Anrechnungs where a.Grund.Nummer == "200" where a.Wert == 0.5 select a).Any())
                        {
                            double ist = (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a).Any() ? (from a in this.Anrechnungs where a.Grund.Nummer == "200" select a.Wert).FirstOrDefault() : 0;

                            Console.WriteLine("[WARNUNG] Der Lehrer " + this.Kürzel + " (" + this.Geburtsdatum.ToShortDateString() + ";" + DeputatSoll + ") muss im kommenden Schuljahr eine Altersermäßigung von 0.5 statt " + ist + " bekommen. ");
                        }
                    }
                }
            }

            /*
             (3) Die Zahl der wöchentlichen Pflichtstunden wird für schwerbehinderte Lehrerinnen und Lehrer im Sinne des Schwerbehindertenrechts (Sozialgesetzbuch IX) ermäßigt, bei einem Grad der Behinderung von

 

1. 50 oder mehr
	

 

a) bei Vollzeitbeschäftigung nach Absatz 1
	

um 2 Stunden,

b) bei einer Beschäftigung im Umfang von mindestens 50 v. H.
	

um 1 Stunde,

2. 70 oder mehr
	

 

a) bei Vollzeitbeschäftigung nach Absatz 1
	

um 3 Stunden,

b) bei einer Beschäftigung im Umfang von mindestens 75 v. H.
	

um 2 Stunden,

c) bei einer Beschäftigung im Umfang von mindestens 50 v. H.
	

um 1,5 Stunden,

3. 90 oder mehr
	

 

a) bei Vollzeitbeschäftigung nach Absatz 1
	

um 4 Stunden

b) bei einer Beschäftigung im Umfang von mindestens 75 v. H.
	

um 3 Stunden

c) bei einer Beschäftigung im Umfang von mindestens 50 v. H.
	

um 2 Stunden

 

Über die Regelermäßigung nach Satz 1 hinaus kann auf Antrag die oder der zuständige Dienstvorgesetzte in besonderen Fällen die Zahl der wöchentlichen Pflichtstunden befristet ermäßigen, soweit die Art der Behinderung dies im Hinblick auf die Unterrichtserteilung erfordert, höchstens aber um vier weitere Stunden.
             
             */

        }
    }
}