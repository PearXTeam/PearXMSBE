using System;
using System.Collections.Generic;
using System.IO;

namespace PearXMSBE
{
	public class FileUtils
	{
		public static string[] GetFilesInDir(string dir)
		{
			List<string> l = new List<string>();
			GetFilesInDir(dir, l);
			return l.ToArray();
		}

		static void GetFilesInDir(string dir, List<string> toAdd)
		{
			foreach (string d in Directory.GetDirectories(dir))
			{
				GetFilesInDir(d, toAdd);
			}
			foreach (string f in Directory.GetFiles(dir))
			{
				toAdd.Add(f);
			}
		}

		public static string GetRelativeString(string full, string relative, bool ignoreCase, char? dirSepChar = null)
		{
			StringComparison com = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (dirSepChar != null)
			{
				switch (dirSepChar.Value)
				{
					case '/':
						full = full.Replace('\\', '/');
						relative = relative.Replace('\\', '/');
						break;
					case '\\':
						full = full.Replace('/', '\\');
						relative = relative.Replace('/', '\\');
						break;
				}
			}
			if (full.StartsWith(relative, com))
			{
				return full.Substring(relative.Length);
			}
			return full;
		}

		public static string[] GetRelativeString(string[] full, string relative, bool ignoreCase, char? dirSepChar = null)
		{
			List<string> l = new List<string>();
			foreach (string s in full)
			{
				l.Add(GetRelativeString(s, relative, ignoreCase, dirSepChar));
			}
			return l.ToArray();
		}
	}
}
