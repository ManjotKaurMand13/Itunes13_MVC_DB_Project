using System;
using System.Collections.Generic;

namespace Itunes_13.Models
{
    public partial class Song
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public float Price { get; set; }

        public int Sales { get; set; }
        public int Rating { get; set; }

        public Song()
        {
        }
        
        public Song(string name, Artist artist, float price)
        {
            Name = name;
            Artist = artist;
            Price = price;
        }
        
    }
}
