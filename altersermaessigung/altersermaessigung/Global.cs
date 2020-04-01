using System;
using System.Data.OleDb;

namespace altersermaessigung
{
    public static class Global
    {

        public static string ConAtl = @"Dsn=Atlantis9;uid=DBA";

        public static string ConU = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source=M:\\Data\\gpUntis.mdb;";

        public static string AdminMail { get; internal set; }

        public static string AktSjAtl
        {
            get
            {
                int sj = (DateTime.Now.Month >= 8 ? DateTime.Now.Year + 1 : DateTime.Now.Year);
                return sj.ToString() + "/" + (sj + 1 - 2000);
            }
        }

        public static string AktSjUnt
        {
            get
            {
                int sj = (DateTime.Now.Month >= 8 ? DateTime.Now.Year + 1 : DateTime.Now.Year);
                return sj.ToString() + (sj + 1);
            }
        }

        public static string Titel
        {
            get
            {
                return @" Altersermäßigung | Published under the terms of GPLv3 | Stefan Bäumer 2020 | Version 20200330\n".PadRight(50, '=');
            }
        }

        public static DateTime ErsterTagDesSchuljahres {
            get
            {
                int sj = (DateTime.Now.Month >= 8 ? DateTime.Now.Year + 1 : DateTime.Now.Year);
                return new DateTime(sj,8,1);
            }
        }

        public static string SafeGetString(OleDbDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }        
}