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

using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System;

namespace SongBook.Constant;

class AppPath
{
	public static readonly string AppRootPath;
	public static readonly string SoundfilesPath;

	static AppPath()
	{
		var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		AppRootPath = $"{appDataPath}/kuchajan/songbook";
		if (!Directory.Exists(AppRootPath))
		{
			Directory.CreateDirectory(AppRootPath);
		}
		SoundfilesPath = $@"{AppRootPath}/soundfiles";
		if (!Directory.Exists(SoundfilesPath))
		{
			Directory.CreateDirectory(SoundfilesPath);
		}
	}
}