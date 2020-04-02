using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;

namespace altersermaessigung
{
    public class Lehrers : List<Lehrer>
    {        
        public Lehrers(Periodes periodes, Anrechnungs anrechungs)
        {
            using (OdbcConnection connection = new OdbcConnection(Global.ConAtl))
            {
                DataSet dataSet = new DataSet();

                OdbcDataAdapter lehrerAdapter = new OdbcDataAdapter(@"SELECT DBA.lehrer.le_id AS IdAtlantis,
DBA.lehrer.le_kuerzel AS Kürzel,
DBA.lehrer.s_typ_le AS Typ,
DBA.lehrer.personalnr AS Personalnummer,
DBA.lehrer.s_geschl AS Geschlecht,
DBA.lehrer.s_anrede AS Anrede,
DBA.lehrer.s_staat AS Staatsang,
DBA.lehrer.name_1 AS Nachname,
DBA.lehrer.name_2 AS Vorname,
DBA.lehrer.s_titel_le AS Titel,
DBA.lehrer.s_lehramt AS Lehramt,
DBA.lehrer.dienstbezeichnung AS Dienstbezeichnung,
DBA.lehrer.dat_geburt AS Geburtsdatum,
DBA.lehrer.s_besch_verhaeltnis AS Beschäftigungsverhältnis,
DBA.lehrer.s_beschaftigungsart_le AS Beschäftigungsart,
DBA.lehrer.s_rechtsverhaeltnis AS Rechtsverhältnis,
DBA.lehrer.s_besoldungsgruppe AS Besoldungsgruppe,
DBA.lehrer.personenkennzahl_kumi AS Personenkennzahlt,
DBA.lehrer.personenpruefziffer_kumi AS Personenprüfziffer,
DBA.lehr_sc.dat_austritt AS Austrittsdatum,
DBA.lehrer.gebort_lkrs AS Geburtsort,
DBA.lehrer.ausweis_nr AS Ausweisnummer,
DBA.lehrer.aktiv_jn AS Aktiv,
DBA.lehr_sc.mitgl_schulleitung_jn AS Schulleitung,
DBA.lehr_sc.dat_eintritt AS Eintrittsdatum,
DBA.lehr_sc.mutterschutz AS Mutterschutz,
DBA.lehr_sc.s_funktion AS MitgliedSchulleitung,
DBA.adresse.strasse AS Strasse,
DBA.adresse.plz AS PLZ,
DBA.adresse.ort AS Ort,
DBA.adresse.tel_1 AS Telefon,
DBA.adresse.email AS Mail,
DBA.le_fa.fa_id,
DBA.le_fa.s_typ_le_fa AS Fach,
DBA.le_fa.s_erlaubnistyp AS Erlaubnis,
DBA.fach.kuerzel AS Fachrichtung,
DBA.fach.kurztext AS FachKurztext,
DBA.fach.zeugnistext
FROM ( ( ( DBA.lehr_sc JOIN DBA.lehrer ON DBA.lehr_sc.le_id = DBA.lehrer.le_id ) JOIN DBA.adresse ON DBA.lehrer.le_id = DBA.adresse.le_id ) LEFT OUTER JOIN DBA.le_fa ON DBA.lehrer.le_id = DBA.le_fa.le_id ) LEFT OUTER JOIN DBA.fach ON DBA.le_fa.fa_id = DBA.fach.fa_id
WHERE vorgang_schuljahr = '" + Global.AktSjAtl + @"'ORDER BY DBA.lehr_sc.ls_kuerzel ASC", connection);

                try
                {
                    connection.Open();
                    lehrerAdapter.Fill(dataSet, "DBA.lehrer");

                    foreach (DataRow theRow in dataSet.Tables["DBA.lehrer"].Rows)
                    {
                        Lehrer lehrer = (from l in this where l.IdAtlantis == Convert.ToInt32(theRow["IdAtlantis"]) select l).FirstOrDefault();
                        if (lehrer == null)
                        {
                            lehrer = new Lehrer()
                            {
                                Kürzel = theRow["Kürzel"] == null ? "" : theRow["Kürzel"].ToString(),
                                Typ = theRow["Typ"] == null ? "" : theRow["Typ"].ToString(),
                                Personalnummer = theRow["Personalnummer"] == null ? "" : theRow["Personalnummer"].ToString(),
                                IdAtlantis = theRow["IdAtlantis"] == null ? -99 : Convert.ToInt32(theRow["IdAtlantis"]),
                                Geschlecht = theRow["Geschlecht"] == null ? "" : theRow["Geschlecht"].ToString(),
                                Anrede = theRow["Anrede"] == null ? "" : theRow["Anrede"].ToString(),
                                Nachname = theRow["Nachname"] == null ? "" : theRow["Nachname"].ToString(),
                                Staatsang = theRow["Staatsang"] == null ? "" : theRow["Staatsang"].ToString(),
                                PLZ = theRow["PLZ"] == null ? "" : theRow["PLZ"].ToString(),
                                Vorname = theRow["Vorname"] == null ? "" : theRow["Vorname"].ToString(),
                                Titel = theRow["Titel"] == null ? "" : theRow["Titel"].ToString(),
                                Dienstbezeichnung = theRow["Dienstbezeichnung"] == null ? "" : theRow["Dienstbezeichnung"].ToString(),
                                Geburtsdatum = theRow["Geburtsdatum"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["Geburtsdatum"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                                Autrittsdatum = theRow["Austrittsdatum"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["Austrittsdatum"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                                Beschäftigungsverhältnis = theRow["Beschäftigungsverhältnis"] == null ? "" : theRow["Beschäftigungsverhältnis"].ToString(),
                                Beschaeftigungsart = theRow["Beschäftigungsart"] == null ? "" : theRow["Beschäftigungsart"].ToString(),
                                Rechtsverhaeltnis = theRow["Rechtsverhältnis"] == null ? "" : theRow["Rechtsverhältnis"].ToString(),
                                Besoldungsgruppe = theRow["Besoldungsgruppe"] == null ? "" : theRow["Besoldungsgruppe"].ToString(),
                                Ausweisnummer = theRow["Ausweisnummer"] == null ? "" : theRow["Ausweisnummer"].ToString(),
                                Geburtsort = theRow["Geburtsort"] == null ? "" : theRow["Geburtsort"].ToString(),
                                Aktiv = theRow["Aktiv"] == null ? "" : theRow["Aktiv"].ToString(),
                                MitgliedSchulleitung = theRow["MitgliedSchulleitung"].ToString().Contains("DV") ? "Vertr" : theRow["MitgliedSchulleitung"].ToString().Contains("DS") ? "Schulleit" : "",
                                Eintrittsdatum = theRow["Eintrittsdatum"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["Eintrittsdatum"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                                Mutterschutz = theRow["Mutterschutz"] == null ? "" : theRow["Mutterschutz"].ToString(),
                                Strasse = theRow["Strasse"] == null ? "" : theRow["Strasse"].ToString(),
                                Ort = theRow["Ort"] == null ? "" : theRow["Ort"].ToString(),
                                Telefon = theRow["Telefon"] == null ? "" : theRow["Telefon"].ToString(),
                                Mail = theRow["Mail"] == null ? "" : theRow["Mail"].ToString(),
                                Lehramt = theRow["Lehramt"] == null ? "" : theRow["Lehramt"].ToString()
                            };
                            lehrer.Anrechnungs = new List<Anrechnung>();

                            lehrer.DeputatSoll = (from a in anrechungs where a.LehrerKürzel == lehrer.Kürzel select a.DeputatSoll).FirstOrDefault();
                            lehrer.Anrechnungs.AddRange((from a in anrechungs where a.LehrerKürzel == lehrer.Kürzel select a).ToList());
                            this.Add(lehrer);                            
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        internal void PrüfeAltersermäßigung()
        {
            Console.WriteLine("§ 2 der Verordnung zur Ausführung des § 93 Abs. 2 Schulgesetz");

            Console.WriteLine("(2) Die Zahl der wöchentlichen Pflichtstunden nach Absatz 1 wird aus Altersgründen ermäßigt vom Beginn des Schuljahres an, das auf die Vollendung des 55. Lebensjahres folgt.");

            Console.WriteLine("25,5 Stunden entspricht 100% => 3 Stunden.");
            Console.WriteLine("19.5 Stunden entspricht 75%% => 2 Stunden.");
            Console.WriteLine("12,25 Stunden entspricht 50% => 1,5 Stunden.");

            foreach (var lehrer in this)
            {
                lehrer.PrüfeAltersermäßigung();
            }
        }
    }
}