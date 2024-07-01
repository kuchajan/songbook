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

public partial class OpenSongWindow : Window
{
    private readonly SongDTO _song;
    public OpenSongWindow() => throw new InvalidOperationException();
    public OpenSongWindow(SongDTO song)
    {
        ArgumentNullException.ThrowIfNull(song);
        InitializeComponent();
        _song = song;
        Title = _song.Title;
        LabelSongName.Content = _song.Title;
        ButtonPlay.IsEnabled = _song.AudioPath != null;
        TextBlockLyrics.Text = _song.Lyrics;
    }

    public void ButtonPlayClick(object sender, RoutedEventArgs args)
    {
        if (_song.AudioPath == null)
        {
            return;
        }
        // ...
    }
}