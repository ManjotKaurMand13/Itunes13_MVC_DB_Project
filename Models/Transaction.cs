using System;
using System.Collections.Generic;

namespace Itunes_13.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
        
        public Transaction()
        {
            Date = DateTime.Now;
        }
        
        public Transaction(int songId, int userId)
        {
            SongId = songId;
            UserId = userId;
            Date = DateTime.Now;
        }
    }
}