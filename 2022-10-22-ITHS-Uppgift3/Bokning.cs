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
    public class Bokning
    {
        public int table { get; set; }
        public string datum { get; set; }
        public string tider { get; set; }
        
        public string name { set; get; }

        public Bokning(string datum,string tider, string name, int table)
        {
            this.table = table;
            this.datum = datum;
            this.tider = tider;           
            this.name = name;
        }

      public bool controlBooking(string datum,string tider, string name, int bord)
        {

            if(String.IsNullOrEmpty(datum) || String.IsNullOrEmpty(tider) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(bord.ToString())) {
                return true;
            }
            else
            {
                return false;
            }

        }


            

        //public string getBookings()
        //{
        //    return datum.ToString() + " " + tider.ToString() + " " + name;
            
        //}




        //DateTime orderDateTime = dateOnly.ToDateTime(timeOnly);

    }
    
}
