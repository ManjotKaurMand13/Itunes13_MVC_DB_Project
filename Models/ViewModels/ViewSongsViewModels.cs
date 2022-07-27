using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Itunes_13.Models.ViewModels
{
    public class ViewSongsViewModel
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public List<SelectListItem> Users{ get; set; }
        public Dictionary<string, List<Song>> ArtistSongs { get; set; }

        public ViewSongsViewModel()
        {
            User = new User();
            Users = new List<SelectListItem>();
            ArtistSongs = new Dictionary<string, List<Song>>();
        }
    }
}

