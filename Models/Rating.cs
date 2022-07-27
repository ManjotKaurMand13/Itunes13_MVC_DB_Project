using System;
using System.Collections.Generic;

namespace Itunes_13.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public Rating()
        {
            RatingValue = 0;
            CreatedAt = DateTime.Now;
        }
        
        public Rating(int ratingValue, int songId, int userId)
        {
            RatingValue = ratingValue;
            SongId = songId;
            UserId = userId;
            CreatedAt = DateTime.Now;
        }
        
    }
}