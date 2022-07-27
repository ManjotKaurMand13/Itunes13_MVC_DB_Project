using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Itunes_13.Models;
using Itunes_13.Models.ViewModels;

namespace Itunes_13.Controllers
{
    public class UsersPlaylistController : Controller
    {
        private readonly I_tunes13Context _context;
        

        public UsersPlaylistController(I_tunes13Context context)
        {
            _context = context;
        }

        // GET: UsersPlaylist
        public async Task<IActionResult> Index()
        {
              return _context.UsersPlaylists != null ? 
                          View(await _context.UsersPlaylists.Include(up => up.Song).Include(up => up.User).ToListAsync()) :
                          Problem("Entity set 'I_tunes13Context.UsersPlaylists'  is null.");
        }

        public async Task<IActionResult> SongsLikes(int? id) {
            var vm = new SongsLikesViewModel();
            var artist = _context.Artists.FirstOrDefault(a => a.Id == id);
            vm.Artist = artist;
            var songsLIkes = new Dictionary<string, int>();

            if (artist != null) {
                vm.Id = artist.Id;
                foreach (var song in _context.Songs.ToList()) {
                    foreach (var up in _context.UsersPlaylists.Include(up => up.Song).ToList()) {
                        if (song.Name == up.Song.Name) {
                            if (songsLIkes.ContainsKey(song.Name)) {
                                songsLIkes[song.Name]++;
                            } else {
                                songsLIkes[song.Name] = 1;
                            }
                        }
                    }
                }
                vm.SongsLikes = songsLIkes;
            }

            foreach (var u in _context.Artists.ToList()) {
                vm.Artists.Add(new SelectListItem {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            }

            return View(vm);
        }

        // GET: UsersPlaylist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsersPlaylists == null)
            {
                return NotFound();
            }

            var usersPlaylist = await _context.UsersPlaylists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersPlaylist == null)
            {
                return NotFound();
            }

            return View(usersPlaylist);
        }

        // GET: UsersPlaylist/Create
        public IActionResult Create()

        {

            ViewBag.Users = new SelectList(_context.Users.ToList(),"Id","Name");
            ViewBag.Songs = new SelectList(_context.Songs.ToList(), "Id", "Name");


            return View();
        }

        // POST: UsersPlaylist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,SongId")] UsersPlaylist usersPlaylist)
        {
            _context.Add(usersPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(usersPlaylist);
        }

        // GET: UsersPlaylist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsersPlaylists == null)
            {
                return NotFound();
            }

            var usersPlaylist = await _context.UsersPlaylists.FindAsync(id);
            if (usersPlaylist == null)
            {
                return NotFound();
            }
            return View(usersPlaylist);
        }

        // POST: UsersPlaylist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SongId")] UsersPlaylist usersPlaylist)
        {
            if (id != usersPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersPlaylistExists(usersPlaylist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usersPlaylist);
        }

        // GET: UsersPlaylist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsersPlaylists == null)
            {
                return NotFound();
            }

            var usersPlaylist = await _context.UsersPlaylists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersPlaylist == null)
            {
                return NotFound();
            }

            return View(usersPlaylist);
        }

        // POST: UsersPlaylist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsersPlaylists == null)
            {
                return Problem("Entity set 'I_tunes13Context.UsersPlaylists'  is null.");
            }
            var usersPlaylist = await _context.UsersPlaylists.FindAsync(id);
            if (usersPlaylist != null)
            {
                _context.UsersPlaylists.Remove(usersPlaylist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersPlaylistExists(int id)
        {
          return (_context.UsersPlaylists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
