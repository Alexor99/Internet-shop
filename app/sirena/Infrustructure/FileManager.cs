using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Infrustructure
{
    public class FileManager
    {
        public async Task<string> UploadPhoto(string rootDirectory, string recordFolderName, IFormFile file)
        {
            // путь к папке Files
            string directory = $"/Files/{recordFolderName}";
            string path = $"{directory}/{file.FileName}";

            if (!Directory.Exists(rootDirectory + directory))
                Directory.CreateDirectory(rootDirectory + directory);

            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(rootDirectory + path, FileMode.Create))
                await file.CopyToAsync(fileStream);

            return path;
        }


        public void DeleteFile(string rootPath, string filePath)
        {
            DeleteFile($"{rootPath}/{filePath}");
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public void DeleteFolder(string rootPath, string filePath)
        {
            if(!string.IsNullOrEmpty(filePath))
                DeleteFolder($"{rootPath}/{filePath}");
        }

        public void DeleteFolder(string path)
        {
            int idx = path.LastIndexOf('/');

            if (idx != -1)
            {
                var dir = new DirectoryInfo(path.Substring(0, idx));
                dir.Delete(true);
            }
        }
    }
}
