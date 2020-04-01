using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace altersermaessigung
{
    public class Anrechnungs : List<Anrechnung>
    {
        public Anrechnungs()
        {
            Anrechnungsgrunds anrechnungsgrunds = new Anrechnungsgrunds();
            
            using (OleDbConnection oleDbConnection = new OleDbConnection(Global.ConU))
            {
                try
                {
                    string queryString = @"SELECT CountValue.Deleted, CountValue.Text, CountValue.Value, CountValue.CV_REASON_ID, CountValue.TEACHER_ID, CountValue.DateFrom, CountValue.DateTo
FROM CountValue
WHERE (((CountValue.SCHOOLYEAR_ID)= " + Global.AktSjUnt + ") AND ((CountValue.Value)<>0) AND ((CountValue.TEACHER_ID)<>0)) ORDER BY CountValue.TEACHER_ID;";

                    OleDbCommand oleDbCommand = new OleDbCommand(queryString, oleDbConnection);
                    oleDbConnection.Open();
                    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

                    double gesamtwert = 0;

                    while (oleDbDataReader.Read())
                    {
                        double wert;

                        try
                        {
                            wert = Convert.ToDouble(oleDbDataReader.GetInt32(2)) / 100000;
                        }
                        catch (Exception)
                        {
                            wert = 0;
                        }

                        Anrechnung anrechnung = new Anrechnung()
                        {
                            Gelöscht = oleDbDataReader.GetBoolean(0),
                            Beschreibung = Global.SafeGetString(oleDbDataReader, 1),
                            Wert = wert,
                            Grund = (from a in anrechnungsgrunds where a.UntisId == oleDbDataReader.GetInt32(3) select a).FirstOrDefault(),
                            LehrerIdUntis = oleDbDataReader.GetInt32(4),
                            Von = oleDbDataReader.GetInt32(5) == 0 ? Global.ErsterTagDesSchuljahres : DateTime.ParseExact((oleDbDataReader.GetInt32(5)).ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                            Bis = oleDbDataReader.GetInt32(6) == 0 ? Global.ErsterTagDesSchuljahres : DateTime.ParseExact((oleDbDataReader.GetInt32(6)).ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        };

                        // Wenn eine Anrechnung mit dem selben Lehrer und dem selben Grund schon existiert, dann wird der Wert addiert, ...

                        var anr = (from an in this where an.LehrerIdUntis == anrechnung.LehrerIdUntis where an.Grund == anrechnung.Grund select an).FirstOrDefault();

                        if (anr != null)
                        {
                            anr.Wert = anr.Wert + anrechnung.Wert;
                        }
                        else
                        {
                            this.Add(anrechnung);
                    
                        }
                        gesamtwert = gesamtwert + anrechnung.Wert;
                    };
                    oleDbDataReader.Close();
                    
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    oleDbConnection.Close();
                }
            }
        }
        public string SafeGetString(OleDbDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}