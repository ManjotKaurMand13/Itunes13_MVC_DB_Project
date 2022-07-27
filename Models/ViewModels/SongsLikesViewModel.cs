using Microsoft.AspNetCore.Mvc.Rendering;

namespace Itunes_13.Models.ViewModels {
    public class SongsLikesViewModel {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public List<SelectListItem> Artists { get; set; }
        public Dictionary<string, int> SongsLikes { get; set; }


        public SongsLikesViewModel() {
            Artist = new Artist();
            Artists = new List<SelectListItem>();
            SongsLikes = new Dictionary<string, int>();
        }
    }
}
