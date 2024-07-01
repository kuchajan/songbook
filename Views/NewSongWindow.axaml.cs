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
        Close(_song);
    }
}