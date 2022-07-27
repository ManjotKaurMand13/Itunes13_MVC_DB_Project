using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Itunes_13.Models;

namespace Itunes_13.Models
{
    public partial class I_tunes13Context : DbContext {
        public I_tunes13Context() {
        }

        public I_tunes13Context(DbContextOptions<I_tunes13Context> options)
            : base(options) {
        }

        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersPlaylist> UsersPlaylists { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; }

    }
}
