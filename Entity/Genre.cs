using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Entity;

public class Genre : IDTOable<GenreDTO>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required List<Song> Songs { get; set; }

    public GenreDTO createDTO()
    {
        return new GenreDTO
        {
            Name = Name
        };
    }

    public void update(GenreDTO updateFrom)
    {
        Name = updateFrom.Name;
    }
}
