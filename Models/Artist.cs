using System;

namespace Itunes_13.Models
{
    public class Artist
    {
        public int Id { get;set; }

       public string Name { get;set; }
       
       public int Sales { get; set; }
       public int Rating { get; set; }

       public Artist()
       {
       }
       
       public Artist(string name, int sales)
       {
           Name = name;
           Sales = sales;
       }
    }
}
