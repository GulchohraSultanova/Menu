using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NReco.VideoConverter;
using Menu.Application.Absrtacts.Services;
using Menu.Application.GlobalAppException;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Menu.Infrastructure.Concreters.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file, string endFolderPath)
        {
            if (file == null || file.Length == 0)
                throw new GlobalAppException("Düzgün Olmayan Fayl");

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", endFolderPath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileGuid = Guid.NewGuid();
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            string fileName;
            var filePath = Path.Combine(uploadPath, fileGuid.ToString());

            if (IsImage(file.FileName))
            {
                fileName = fileGuid + ".webp";
                using var image = await Image.LoadAsync(file.OpenReadStream());
                var outputStream = new MemoryStream();
                image.Mutate(x => x.AutoOrient());
                await image.SaveAsync(outputStream, new WebpEncoder());
                await File.WriteAllBytesAsync(Path.Combine(uploadPath, fileName), outputStream.ToArray());
            }
            else if (IsVideo(file.FileName))
            {
                fileName = fileGuid + ".webm";
                string tempInputPath = Path.GetTempFileName();
                string tempOutputPath = Path.ChangeExtension(tempInputPath, ".webm");
                using (var memoryStream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(memoryStream);
                    await File.WriteAllBytesAsync(tempInputPath, memoryStream.ToArray());
                }

                var ffmpeg = new FFMpegConverter();
                ffmpeg.ConvertMedia(tempInputPath, tempOutputPath, Format.webm);
                await File.WriteAllBytesAsync(Path.Combine(uploadPath, fileName), await File.ReadAllBytesAsync(tempOutputPath));
                File.Delete(tempInputPath);
                File.Delete(tempOutputPath);
            }
            else
            {
                fileName = fileGuid + fileExtension;
                filePath = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<List<string>> UploadFiles(List<IFormFile> files, string endFolderPath)
        {
            if (files == null || !files.Any())
                throw new GlobalAppException("Fayl Siyahısı Boşdur!");

            var fileNames = new List<string>();
            foreach (var file in files)
            {
                var fileName = await UploadFile(file, endFolderPath);
                fileNames.Add(fileName);
            }
            return fileNames;
        }

        public async Task<IFormFile> GetFileById(string fileId, string endFolderPath)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", endFolderPath);
            var filePath = Path.Combine(uploadPath, fileId);

            if (!File.Exists(filePath))
                throw new GlobalAppException("Fayl Tapılmadı");

            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await fileStream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, memoryStream.Length, "file", fileId);
        }

        public async Task<List<IFormFile>> GetAllFiles(List<string> filenames, string endFolderPath)
        {
            var files = new List<IFormFile>();

            foreach (var fileName in filenames)
            {
                var file = await GetFileById(fileName, endFolderPath);
                files.Add(file);
            }

            return files;
        }

        public async Task DeleteFile(string endFolderPath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new GlobalAppException("Fayl Adı Null və ya Boş Ola Bilməz!");

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", endFolderPath, fileName);
            try
            {
                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                }
                else
                {
                    throw new GlobalAppException("Fayl Tapılmadı!");
                }
            }
            catch (Exception)
            {
            
            }
        }

        private bool IsImage(string fileName)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };
            return imageExtensions.Contains(Path.GetExtension(fileName).ToLower());
        }

        private bool IsVideo(string fileName)
        {
            string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".flv", ".wmv" };
            return videoExtensions.Contains(Path.GetExtension(fileName).ToLower());
        }
    }
}
