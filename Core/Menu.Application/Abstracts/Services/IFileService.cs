using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Absrtacts.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file, string endFolderPath);

        // Bir neçə faylı yükləyir və onların GUID-lərinin siyahısını qaytarır.
        Task<List<string>> UploadFiles(List<IFormFile> files, string endFolderPath);

        // Məhsul ID və endFolderPath-ə görə faylı əldə edir.
        Task<IFormFile> GetFileById(string productId, string endFolderPath);

        // Fayl adları siyahısına və endFolderPath-ə əsasən faylları əldə edir.
        Task<List<IFormFile>> GetAllFiles(List<string> filenames, string endFolderPath);

        Task DeleteFile(string endFolderPath, string imageName);
    }
}
