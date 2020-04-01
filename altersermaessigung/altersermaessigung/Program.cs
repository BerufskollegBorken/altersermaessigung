using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace altersermaessigung
{
    class Program
    {
        static void Main(string[] args)
        {
            var prds = new Periodes();
            var anre = new Anrechnungs();
            var lehs = new Lehrers(prds, anre);

            Console.ReadKey();
        }
    }
}
