using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongBook.Data;

namespace SongBook.Entity;

public class Song : IDTOable<SongDTO>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? AudioPath { get; set; }
    public required List<Genre> Genres { get; set; }
    public required string Lyrics { get; set; }
    public string? Comments { get; set; }

    public SongDTO createDTO()
    {
        return new SongDTO
        {
            Title = Title,
            AudioPath = AudioPath,
            GenresIds = Genres.AsEnumerable().Select(g => g.Id).ToList(),
            Lyrics = Lyrics,
            Comments = Comments
        };
    }

    public void update(SongDTO updateFrom)
    {
        Title = updateFrom.Title;
        AudioPath = updateFrom.AudioPath;
        using (var context = new AppDbContext())
        {
            Genres = context.Genres.AsQueryable().Where(g => updateFrom.GenresIds.Contains(g.Id)).ToList();
        }
        Lyrics = updateFrom.Lyrics;
        Comments = updateFrom.Comments;
    }
}
