/*
    Copyright (C) 2024  Jan Kuchař <jan.kuchar.2003@gmail.com>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
