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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Entity;

public class SongDTO
{
    public int Id {get; set;}
    public required string Title { get; set; }
    public string? AudioPath { get; set; }
    public required List<int> GenresIds { get; set; }
    public required string Lyrics { get; set; }
    public string? Comments { get; set; }
}
