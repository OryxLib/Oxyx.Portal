using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace Oryx.Utilities
{
    public class RarTool
    {
        public static string Exact(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            var outputDirectory = Path.GetDirectoryName(path);
            var targetDir = outputDirectory + "\\" + Path.GetFileNameWithoutExtension(path);
  
            using (var archive = RarArchive.Open(path))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(outputDirectory, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                }
            }

            return targetDir;
        }
    }
}
