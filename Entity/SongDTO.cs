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
