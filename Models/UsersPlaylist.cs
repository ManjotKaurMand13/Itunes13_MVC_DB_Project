using System;
using System.Collections.Generic;

namespace Itunes_13.Models
{
    public partial class UsersPlaylist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
        
        public UsersPlaylist()
        {
        }
        
        public UsersPlaylist(int userId, int songId)
        {
            UserId = userId;
            SongId = songId;
        }
    }
}
