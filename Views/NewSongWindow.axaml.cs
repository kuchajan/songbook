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
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SongBook.Entity;

namespace SongBook.Views;

public partial class NewSongWindow : Window
{
    private SongDTO _song;
    private static readonly string NO_FILE = "Žádný soubor nevybrán";
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
        var isNull = _song.AudioPath == null;
        LabelFilePath.Content = isNull ? NO_FILE : _song.AudioPath;
        ButtonRemoveFile.IsEnabled = !isNull;
    }

    public void ButtonGetFileClick(object sender, RoutedEventArgs args)
    {
        UpdateFileStuff();
    }
    public void ButtonRemoveFileClick(object sender, RoutedEventArgs args)
    {
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