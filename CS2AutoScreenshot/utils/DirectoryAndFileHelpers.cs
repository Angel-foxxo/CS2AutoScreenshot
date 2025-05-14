using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace RadGenCore.Components
{
    public class DirectoryAndFileHelpers
    {
        public static bool GetIfAllDirectoriesExist(List<string> filepaths)
        {
            return filepaths.All(x => Directory.Exists(x));
        }

        public static bool GetIfAllFilesExist(List<string> filepaths)
        {
            return filepaths.All(x => File.Exists(x));
        }

        public static void CreateDirectoryIfDoesntExist(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
        }

        public static void DeleteDirectoryIfExists(string filepath, bool recursive = true)
        {
            if (Directory.Exists(filepath))
            {
                Directory.Delete(filepath, recursive);
            }
        }

        public static void CreateFileIfDoesntExist(string filepath)
        {
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }
        }

        public static void DeleteFileIfExists(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        public static void CopyFileAndCreateDestinationDirectoryIfDoesntExist(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }

            CreateDirectoryIfDoesntExist(FixupDirectoryPath(Directory.GetParent(destFileName).FullName));

            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public static bool CheckFileIsNotLocked(string filepath, bool checkRead = true, bool checkWrite = true, int maxRetries = 20, int waitTimeSeconds = 1)
        {
            CreateFileIfDoesntExist(filepath);

            var fileReadable = false;
            var fileWriteable = false;

            var retries = 0;
            while (retries < maxRetries)
            {
                try
                {
                    if (checkRead)
                    {
                        using (var fs = File.OpenRead(filepath))
                        {
                            if (fs.CanRead)
                            {
                                fileReadable = true;
                            }
                        }
                    }
                    if (checkWrite)
                    {
                        using (var fs = File.OpenWrite(filepath))
                        {
                            if (fs.CanWrite)
                            {
                                fileWriteable = true;
                            }
                        }
                    }

                    if ((!checkRead || fileReadable) && (!checkWrite || fileWriteable))
                    {
                        return true;
                    }
                }
                catch { }

                retries++;

                if (retries < maxRetries)
                {

                    Thread.Sleep(waitTimeSeconds * 1000);
                    continue;
                }
            }


            return false;
        }

        // Filters are optional for both folders & files, and are only used in the top level
        // Takes wildcards into account for the filters
        public static bool CopyFilesRecursively(
            string sourcePath,
            string targetPath,
            List<string>? topLevelFolderNameFilter = null,
            List<string>? topLevelFilenameFilter = null,
            List<string>? topLevelFolderNameExcludeFilter = null,
            List<string>? topLevelFilenameExcludeFilter = null,
            List<string>? subLevelFolderNameFilter = null,
            List<string>? subLevelFilenameFilter = null,
            List<string>? subLevelFolderNameExcludeFilter = null,
            List<string>? subLevelFilenameExcludeFilter = null)
        {
            if (sourcePath.Trim() == targetPath.Trim())
            {
                return false;
            }


            // Copy all folders/directories (takes into account the filter if it exists)
            foreach (var folderPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Select(x => FixupDirectoryPath(x)))
            {
                // filters on top level only
                if (Directory.GetParent(folderPath).FullName == sourcePath)
                {
                    if (topLevelFolderNameFilter != null &&
                        topLevelFolderNameFilter.Any() &&
                        !topLevelFolderNameFilter.Any(x => string.Concat(sourcePath, @"\", x).ToLower() == folderPath.ToLower())
                    )
                    {
                        continue;
                    }

                    if (topLevelFolderNameExcludeFilter != null &&
                        topLevelFolderNameExcludeFilter.Any() &&
                        topLevelFolderNameExcludeFilter.Any(x => string.Concat(sourcePath, @"\", x).ToLower() == folderPath.ToLower())
                    )
                    {
                        continue;
                    }
                }
                else // NOTE: matching on directory name could have false positives, as it doesn't check the full path
                {
                    if (subLevelFolderNameFilter != null &&
                        subLevelFolderNameFilter.Any() &&
                        !subLevelFolderNameFilter.Any(x => Path.GetDirectoryName(string.Concat(sourcePath, @"\", x)).ToLower() == folderPath.ToLower())
                    )
                    {
                        continue;
                    }

                    if (subLevelFolderNameExcludeFilter != null &&
                        subLevelFolderNameExcludeFilter.Any() &&
                        subLevelFolderNameExcludeFilter.Any(x => Path.GetDirectoryName(string.Concat(sourcePath, @"\", x)).ToLower() == folderPath.ToLower())
                    )
                    {
                        continue;
                    }
                }

                Directory.CreateDirectory(folderPath.Replace(sourcePath, targetPath));
            }

            // Copy all files (takes into account the filter if it exists) & overwrites if files already exist
            foreach (var filepath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                // filters on top level only
                if (Directory.GetParent(filepath).FullName == sourcePath)
                {
                    if (topLevelFilenameFilter != null &&
                        topLevelFilenameFilter.Any() &&
                        !topLevelFilenameFilter.Any(x => string.Concat(sourcePath, @"\", x).ToLower() == filepath.ToLower())
                    )
                    {
                        continue;
                    }

                    if (topLevelFilenameExcludeFilter != null &&
                        topLevelFilenameExcludeFilter.Any() &&
                        topLevelFilenameExcludeFilter.Any(x => string.Concat(sourcePath, @"\", x).ToLower() == filepath.ToLower())
                    )
                    {
                        continue;
                    }
                }
                else // NOTE: matching on filename could have false positives, as it doesn't check the full path
                {
                    if (subLevelFilenameFilter != null &&
                        subLevelFilenameFilter.Any() &&
                        !subLevelFilenameFilter.Any(x => Path.GetFileName(string.Concat(sourcePath, @"\", x)).ToLower() == Path.GetFileName(filepath).ToLower())
                    )
                    {
                        continue;
                    }

                    if (subLevelFilenameExcludeFilter != null &&
                        subLevelFilenameExcludeFilter.Any() &&
                        subLevelFilenameExcludeFilter.Any(x => Path.GetFileName(string.Concat(sourcePath, @"\", x)).ToLower() == Path.GetFileName(filepath).ToLower())
                    )
                    {
                        continue;
                    }
                }

                File.Copy(filepath, filepath.Replace(sourcePath, targetPath), true);
            }

            return true;
        }

        public static string FixupDirectoryPath(string path)
        {
            path = path.Replace("\"", string.Empty);
            path = path.Replace("//", "/");
            path = path.Replace("/", @"\");
            path = path.Replace(@"\\", @"\");
            if (path.EndsWith(@"\"))
                path = Directory.GetParent(path).FullName ?? path;

            var pathSplit = path.Split(@"\");
            if (path.EndsWith(@"\.."))
                path = Directory.GetParent(path).FullName + @"\" + pathSplit.SkipLast(2).Last();

            return path;
        }
    }
}
