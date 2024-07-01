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