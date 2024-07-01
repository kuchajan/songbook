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

namespace SongBook.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private Song? GetSelectedSong()
    {
        // todo: replace with actual get
        return new Song
        {
            Id = -1,
            Title = "Moje úžasná písnička",
            Genres = [],
            Lyrics = "Můj úžasnej text písničky"
        };
    }
    private void UpdateSongList()
    {
        ListBoxSongList.Items.Clear();
        using (var context = new AppDbContext())
        {
            foreach (var song in context.Songs.ToList())
            {
                ListBoxSongList.Items.Add(new ListBoxItem
                {
                    Content = song.Title
                });
            }
        }
        
        // todo: get songs from db
        // todo: add to listbox
    }

    private void UpdateButtonAvailability()
    {
        IList? list = ListBoxSongList.SelectedItems;
        bool show = list != null && list.Count == 1;
        ButtonEditSong.IsEnabled = show;
        ButtonOpenSong.IsEnabled = show;
        ButtonRemoveSong.IsEnabled = show;
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
    public void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
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
        // save new song
    }
    public void ButtonOpenSongClick(object sender, RoutedEventArgs args)
    {
        var song = GetSelectedSong();
        if (song == null)
        {
            throw new Exception("Song can't be null");
        }
        var window = new OpenSongWindow(song.createDTO());
        window.Show();
    }
    public async void ButtonEditSongClick(object sender, RoutedEventArgs args)
    {
        var song = GetSelectedSong();
        if (song == null)
        {
            throw new Exception("Song can't be null");
        }
        var window = new NewSongWindow(song.createDTO());
        var result = await window.ShowDialog<SongDTO?>(this);
        if(result == null)
        {
            return;
        }
        // save the edit
    }
    public async void ButtonDeleteSongClick(object sender, RoutedEventArgs args)
    {
        Song? song = GetSelectedSong();
        if (song == null)
        {
            return;
        }
        var warningbox = MessageBoxManager.GetMessageBoxStandard("Upozornění", $"Jste si jistí, že chcete smazat písničku {song.Title}", MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Warning, WindowStartupLocation.CenterScreen);
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