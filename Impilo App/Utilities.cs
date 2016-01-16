using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App
{
    class Utilities
    {
        public static Random randomGenerator = new Random();
        public static string GenerateScreeningID(string Name, string Surname)
        {
            return DateTime.Now.Year.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + Name + Surname + randomGenerator.Next(1000, 10000);
        }
    }
}
