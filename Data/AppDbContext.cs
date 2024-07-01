using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SongBook.Constant;
using SongBook.Entity;
using System.IO;

namespace SongBook.Data;

public class AppDbContext : DbContext
{
    public DbSet<Song> Songs { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public string DbPath { get; }
    public AppDbContext()
    {
        DbPath = Path.Join(AppPath.AppRootPath, "songdatabase.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}
