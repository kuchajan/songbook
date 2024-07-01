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

namespace SongBook.Constant;

class AppInfo
{
	public const string Version = "0.0.1";
    public const string Copyright = $"SongBook verze {Version} Copyright (C) 2024 Jan Kuchař <jan.kuchar.2003@gmail.com>";
    public const string FreeSoftware = "Tento program je svobodný software: můžete jej šířit a upravovat podle ustanovení\nObecné veřejné licence GNU (GNU General Public Licence), vydávané Free Software\nFoundation a to buď podle 3. verze této Licence, nebo (podle vašeho uvážení) kterékoli\npozdější verze.";
    public const string Warranty = "Tento program je rozšiřován v naději, že bude užitečný, avšak BEZ JAKÉKOLIV ZÁRUKY.\nNeposkytují se ani odvozené záruky PRODEJNOSTI anebo VHODNOSTI PRO URČITÝ ÚČEL.\nDalší podrobnosti hledejte v Obecné veřejné licenci GNU.";
    public const string GPLLink = "Kopii Obecné veřejné licence GNU jste měli obdržet spolu s tímto programem.\nPokud se tak nestalo, najdete ji zde: <http://www.gnu.org/licenses/>.";
    public const string Info = $"{Copyright}\n\n{FreeSoftware}\n\n{Warranty}\n\n{GPLLink}";
    public const string SourceCodeURL = "https://github.com/kuchajan/songbook";
}