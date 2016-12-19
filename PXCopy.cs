using System;
using System.IO;
using Microsoft.Build.Utilities;

namespace PearXMSBE
{
	public class PXCopy : Task
	{
		public string StartMsg { get; set; } = "Copying files...";
		public string StopMsg { get; set; } = "Files are copied successfully!";
		public string From { get; set; }
		public string To { get; set; }

		public override bool Execute()
		{
			Log.LogMessage(StartMsg);
			From = From.Replace('\\', Path.DirectorySeparatorChar);
			To = To.Replace('\\', Path.DirectorySeparatorChar);
			if (!From.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				From = From + Path.DirectorySeparatorChar;
			if (!To.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				To = To + Path.DirectorySeparatorChar;
			var fls = FileUtils.GetFilesInDir(From);
			string[] relativePaths = FileUtils.GetRelativeString(fls, From, true);
			for (int i = 0; i < fls.Length; i++)
			{
				string fFrom = fls[i];
				string fTo = To + relativePaths[i];
				Directory.CreateDirectory(Path.GetDirectoryName(fTo));
				File.Copy(fFrom, fTo, true);
			}
			Log.LogMessage(StopMsg);
			return true;
		}
	}
}
