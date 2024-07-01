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