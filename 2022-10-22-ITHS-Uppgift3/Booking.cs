using System; 
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using System.Globalization;
using _2022_10_22_ITHS_Uppgift3;
using System.Windows.Controls;
using System.Windows;

namespace _2022_10_22_ITHS_Uppgift3
{
    public class Booking
    {
        public int table { get; set; }
        public string date { get; set; }
        public string times { get; set; }
        public string name { set; get; }

        public Booking(string date,string times, string name, int table)
        {
            this.table = table;
            this.date = date;
            this.times = times;           
            this.name = name;
        }
    }  
}
