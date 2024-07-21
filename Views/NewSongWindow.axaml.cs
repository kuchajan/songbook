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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using SongBook.Constant;
using SongBook.Entity;

namespace SongBook.Views;

public partial class NewSongWindow : Window
{
    private SongDTO _song;
    private const string NO_FILE = "Žádný soubor nevybrán";
    public NewSongWindow()
    {
        _song = new SongDTO
        {
            Title = "",
            GenresIds = [],
            Lyrics = ""
        };
        Title = "Nová písnička";
        CommonInit();
    }
    public NewSongWindow(SongDTO song)
    {
        ArgumentNullException.ThrowIfNull(song);
        _song = song;
        Title = $"Úprava písničky: {song.Title}";
        CommonInit();
    }

    public void CommonInit()
    {
        InitializeComponent();
        TextBoxTitle.Text = _song.Title;
        UpdateFileStuff();
        //todo: CheckedListBoxGenres
        TextBoxLyrics.Text = _song.Lyrics;
        TextBoxComments.Text = _song.Comments;
    }
    private void UpdateFileStuff()
    {
        var isNotNull = _song.AudioPath != null;
        LabelFilePath.Content = isNotNull ? _song.AudioPath : NO_FILE;
        ButtonRemoveFile.IsEnabled = isNotNull;
    }

    private async Task<IStorageFile?> GetFile()
    {
        var toplevel = GetTopLevel(this) ?? throw new Exception("Top level cannot be null");
        var audioFileFilter = new FilePickerFileType("Audio")
        {
            Patterns = ["*.mp3;*.wav;*.ogg;*.m4a;*.flac;*.wma;*.aac;*.midi"], // there may be more
            AppleUniformTypeIdentifiers = ["public.audio"],
            MimeTypes = ["audio/*"]
        };
        var files = await toplevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Vyber písničku",
            FileTypeFilter = [audioFileFilter, FilePickerFileTypes.All],
            AllowMultiple = false
        });
        if (files == null || files.Count == 0)
        {
            return null;
        }
        if (files.Count != 1) 
        {
            throw new Exception("Unexpected count of files");
        }
        return files[0];
    }

    private void DeleteAudioFile()
    {
        if (_song.AudioPath == null)
        {
            return;
        }
        File.Delete(Path.Join(AppPath.SoundfilesPath, _song.AudioPath));
        _song.AudioPath = null;
    }

    public async void ButtonGetFileClick(object sender, RoutedEventArgs args)
    {
        var file = await GetFile();
        if (file == null)
        {
            return;
        }
        if(_song.AudioPath != null)
        {
            DeleteAudioFile();
        }

        var guid = Guid.NewGuid();
        var newFileName = string.Concat(guid.ToString("N"), ".", file.Name.Split('.').Last());
        using (var source = await file.OpenReadAsync())
        using (var target = File.Create(Path.Join(AppPath.SoundfilesPath, newFileName)))
        {
            source.CopyTo(target);
        }

        _song.AudioPath = newFileName;
        UpdateFileStuff();
    }
    public void ButtonRemoveFileClick(object sender, RoutedEventArgs args)
    {
        DeleteAudioFile();
        UpdateFileStuff();
    }

    public void ButtonSaveSongClick(object sender, RoutedEventArgs args)
    {
        _song.Title = TextBoxTitle.Text ?? _song.Title;
        // File was already updated
        //todo: CheckedListBoxGenres
        _song.Lyrics = TextBoxLyrics.Text ?? _song.Lyrics;
        _song.Comments = TextBoxComments.Text;
        Close(_song);
    }
}