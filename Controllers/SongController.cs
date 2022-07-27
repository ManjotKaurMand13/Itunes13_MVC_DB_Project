using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Itunes_13.Models;

namespace Itunes_13.Controllers
{
    public class SongController : Controller
    {
        private readonly I_tunes13Context _context;

        public SongController(I_tunes13Context context)
        {
            _context = context;
        }

        // GET: Song
        public async Task<IActionResult> Index()
        {
              return _context.Songs != null ? 
                          View(await _context.Songs.ToListAsync()) :
                          Problem("Entity set 'I_tunes13Context.Songs'  is null.");
        }

        // GET: Song/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public IActionResult TopSellingSong() {
            Song song = _context.Songs.OrderByDescending(s => s.Sales).First();
            return View(song);
        }

        // GET: Song/Bug
        public IActionResult Buy() {
            ViewBag.Songs = new SelectList(_context.Songs.ToList(), "Id", "Name");
            ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "Name");

            return View();
        }

        // POST: Song/Bug
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int songId, int userId) {
            
            User user = _context.Users.Find(userId);
            Song song = _context.Songs.Find(songId);
            Artist artist = _context.Artists.Find(song.ArtistId);

            Transaction transaction = _context.Transactions.FirstOrDefault(t => t.UserId == userId && t.SongId == songId);

            if (user == null || song == null || artist == null) {
                return NotFound();
            }

            Transaction newTransaction = new Transaction(songId, userId);

            if (transaction == null) {
                song.Sales++;
                artist.Sales++;

                _context.Transactions.Add(newTransaction);
                _context.Songs.Update(song);
                _context.Artists.Update(artist);


                await _context.SaveChangesAsync();
               
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Song/Create
        public IActionResult Create()
        {
            ViewBag.Artists = new SelectList(_context.Artists.ToList(), "Id", "Name");
            return View();
        }

        // POST: Song/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ArtistId")] Song song)
        {
            _context.Add(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Song/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Song/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ArtistId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            return View(song);
        }

        // GET: Song/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Songs == null)
            {
                return Problem("Entity set 'I_tunes13Context.Songs'  is null.");
            }
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
          return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
