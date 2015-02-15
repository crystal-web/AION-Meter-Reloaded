using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AIONMeter
{
	/// <summary>
	/// Create a New INI file to store or load data
	/// </summary>
	public class IniFile : IOException
	{
		private string path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		/// <summary>
		/// INIFile Constructor.
		/// </summary>
		/// <param name="INIPath"></param>
		public void IniFilePath(string INIPath)
		{
			path = Path.GetFullPath(INIPath);
			if (!File.Exists (path)) {
				throw new FileNotFoundException ("File not found");
			}
		}
		/// <summary>
		/// Write Data to the INI File
		/// </summary>
		/// <param name="Section"></param>
		/// Section name
		/// <param name="Key"></param>
		/// Key Name
		/// <param name="Value"></param>
		/// Value Name
		public void IniWriteValue(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, this.path);
		}

		/// <summary>
		/// Read Data Value From the Ini File
		/// </summary>
		/// <param name="Section"></param>
		/// <param name="Key"></param>
		/// <returns></returns>
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
			return (temp.ToString().Length > 0) ? temp.ToString() : Key;
		}
	}

}