using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System;

namespace SongBook.Constant;

class Path
{
	public static readonly string AppPath;
	public static readonly string SoundfilesPath;

	static Path()
	{
		var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		AppPath = $"{appDataPath}/kuchajan/songbook";
		if (!Directory.Exists(AppPath))
		{
			Directory.CreateDirectory(AppPath);
		}
		SoundfilesPath = $@"{AppPath}/soundfiles";
		if (!Directory.Exists(SoundfilesPath))
		{
			Directory.CreateDirectory(SoundfilesPath);
		}
	}
}