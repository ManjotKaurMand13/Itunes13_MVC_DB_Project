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
    public class UserController : Controller
    {
        private readonly I_tunes13Context _context;

        public UserController(I_tunes13Context context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'I_tunes13Context.Users'  is null.");
        }

        public IActionResult AddMoney()
        {
            ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public IActionResult AddMoney(int? userId, float? amount)
        {
            User user = _context.Users.First(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            
            user.Wallet += (float)amount;
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        
        public IActionResult ViewSongs(int? id)
        {
            var vm = new ViewSongsViewModel();
            var user = _context.Users.FirstOrDefault(u => u.Id==id);
            vm.User = user;
            var songs = new Dictionary<string, List<Song>>();
            if(user != null)
            {
                vm.Id = user.Id;
                var usersPlaylist = _context.UsersPlaylists.Where(up => up.UserId ==id).Include(up => up.Song).ThenInclude(s=>s.Artist).Include(up => up.User).ToList();
                foreach (var up in usersPlaylist)
                {
                    if (songs.ContainsKey(up.Song.Artist.Name))
                    {
                        songs[up.Song.Artist.Name].Add(up.Song);
                    }
                    else
                    {
                        songs[up.Song.Artist.Name] = new List<Song>();
                        songs[up.Song.Artist.Name].Add(up.Song);
                    }
                }
                //songs = songs.OrderBy(s => s.Key);
                vm.ArtistSongs = songs;
            }
            foreach (var u in _context.Users.ToList())
            {
                vm.Users.Add(new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            }
            return View(vm);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'I_tunes13Context.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
