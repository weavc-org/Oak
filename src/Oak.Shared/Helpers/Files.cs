using System;
using System.IO;

namespace Oak.Shared.Helpers
{
    /// <summary>
    /// Misc file helpers
    /// </summary>
    public static class Files
    {
        /// <summary>
        /// Generate a random filename
        /// </summary>
        public static string RandomFilename(string ext)
        {
            if (!ext.StartsWith("."))
            {
                ext = $".{ext}";
            }
            
            return $"{Guid.NewGuid().ToString()}{ext}";
        }

        /// <summary>
        /// Find the extension from a filename string
        /// </summary>
        public static string FileExtention(string filename) 
        {
            return Path.GetExtension(filename);
        }
    }
}