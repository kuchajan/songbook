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
using System.Collections;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using SongBook.Data;
using SongBook.Entity;
using SongBook.Constant;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace SongBook.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var template = new FuncDataTemplate<Song>((value, namescope) => new TextBlock
        {
            [!TextBlock.TextProperty] = new Binding("Title")
        });
        ListBoxSongs.ItemTemplate = template;
        UpdateSongList();
        UpdateButtonAvailability();
    }

    private Song? GetSelectedSong()
    {
        return (Song?)ListBoxSongs.SelectedItem;
    }
    private void UpdateSongList()
    {
        using (var context = new AppDbContext())
        {
            var songs = context.Songs.ToList();
            ListBoxSongs.ItemsSource = new ObservableCollection<Song>(songs);
        }
    }

    private void UpdateButtonAvailability()
    {
        IList? list = ListBoxSongs.SelectedItems;
        bool selected = list != null && list.Count == 1;
        ButtonEditSong.IsEnabled = selected;
        ButtonOpenSong.IsEnabled = selected;
        ButtonRemoveSong.IsEnabled = selected;
    }
    public async void ShowInfo(object sender, RoutedEventArgs args)
    {
        var messagebox = MessageBoxManager.GetMessageBoxStandard("Informace o programu", AppInfo.Info, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.None, WindowStartupLocation.CenterScreen);
        await messagebox.ShowAsync();
    }
    public void ShowSourceCode(object sender, RoutedEventArgs args)
    {
        Process.Start(new ProcessStartInfo(AppInfo.SourceCodeURL) { UseShellExecute = true });
    }
    public void ListBoxSongsSelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        UpdateButtonAvailability();
    }
    public async void ButtonNewSongClick(object sender, RoutedEventArgs args)
    {
        var window = new NewSongWindow();
        var result = await window.ShowDialog<SongDTO?>(this);
        if(result == null)
        {
            return;
        }
        using (var context = new AppDbContext())
        {
            context.Songs.Add(new Song
            {
                Title = result.Title,
                AudioPath = result.AudioPath,
                Genres = context.Genres.AsQueryable().Where(g => result.GenresIds.Contains(g.Id)).ToList(),
                Lyrics = result.Lyrics,
                Comments = result.Comments
            });
            context.SaveChanges();
        }
        UpdateSongList();
    }
    public void ButtonOpenSongClick(object sender, RoutedEventArgs args)
    {
        var song = GetSelectedSong() ?? throw new Exception("Song can't be null");
        var window = new OpenSongWindow(song.createDTO());
        window.Show();
    }
    public async void ButtonEditSongClick(object sender, RoutedEventArgs args)
    {
        var song = GetSelectedSong() ?? throw new Exception("Song can't be null");
        var window = new NewSongWindow(song.createDTO());
        var result = await window.ShowDialog<SongDTO?>(this);
        if(result == null)
        {
            return;
        }
        using (var context = new AppDbContext())
        {
            var dbSong = context.Songs.Find(song.Id) ?? throw new Exception($"Song with id {song.Id} was not found");
            dbSong.update(result);
            context.SaveChanges();
        }
        UpdateSongList();
    }
    public async void ButtonDeleteSongClick(object sender, RoutedEventArgs args)
    {
        var song = GetSelectedSong() ?? throw new Exception("Song can't be null");
        var warningbox = MessageBoxManager.GetMessageBoxStandard("Upozornění", $"Jste si jistí, že chcete smazat písničku {song.Title}?", MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Warning, WindowStartupLocation.CenterScreen);
        var result = await warningbox.ShowAsync();
        if (result != MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            return;
        }
        using (var context = new AppDbContext())
        {
            context.Songs.Remove(song);
            context.SaveChanges();
        }
        UpdateSongList();
        UpdateButtonAvailability();
    }
}