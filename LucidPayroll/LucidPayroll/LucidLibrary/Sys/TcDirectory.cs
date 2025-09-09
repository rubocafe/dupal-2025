using System.Diagnostics;
using System.IO;

// Harshan Nishantha
// 2013-09-23

namespace LucidLibrary.Sys
{
    public class TcDirectory
    {
        public static void Open(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Process.Start(directoryPath);
            }
        }

        public static void Create(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void Delete(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }

        public static void Clear(string directory)
        {
            Delete(directory);
            Create(directory);
        }

        public static void Copy(string sourceDirectory, string destinationDirectory, bool copySubDirectories)
        {
            DirectoryInfo directory = new DirectoryInfo(sourceDirectory);
            DirectoryInfo[] subDirectories = directory.GetDirectories();

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                string destinationFile = Path.Combine(destinationDirectory, file.Name);
                file.CopyTo(destinationFile, true);
            }

            if (copySubDirectories)
            {
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    string subDestinationDirectory = Path.Combine(destinationDirectory, subDirectory.Name);
                    Copy(subDirectory.FullName, subDestinationDirectory, copySubDirectories);
                }
            }
        }

        public static void CreateDirectoryOfFilePath(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            Create(info.Directory.FullName);
        }
    }
}
