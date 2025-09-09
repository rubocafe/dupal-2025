using Ionic.Zip;
using System;
using System.IO;

// Harshan Nishatha
// 2013-09-12

namespace Payroll.Library.Zip
{
    public class TcZip
    {
        public static void ZipFolder(string folderPath, string zipFileName)
        {
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            string ZipFileToCreate = zipFileName;

            if (folder.Exists)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(folderPath);
                    zip.Comment = string.Format("This zip was created at {0}", System.DateTime.Now.ToString("G"));
                    zip.Save(ZipFileToCreate);
                }
            }
            else
            {
                throw new Exception(string.Format("Folder [{0}] does not exist", folderPath));
            }
        }

        public static void ZipFolder(string folderPath)
        {
            string ZipFileToCreate = folderPath + ".zip";

            ZipFolder(folderPath, ZipFileToCreate);
        }

        public static void UnzipFile(string zipFile, string destination)
        {
            if (!File.Exists(zipFile))
                throw new Exception(string.Format("Zip file [{0}] does not exist", zipFile));

            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            using (ZipFile zip = ZipFile.Read(zipFile))
            {
                foreach (ZipEntry zipEntry in zip)
                {
                    zipEntry.Extract(destination, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
