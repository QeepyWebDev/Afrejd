using Afrejd.Web.Data.Interfaces;

namespace Afrejd.Web.Data.Services
{
    public class FilesUploadService : IFilesUploadService
    {
        private readonly string _baseDirectory;

        public FilesUploadService()
        {
            _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }

        public async Task<bool> UploadFileChunk(FileChunkDto fileChunkDto, string userCompanyName)
        {
            try
            {
                string companyDirectory = Path.Combine(_baseDirectory, userCompanyName);
                if (!Directory.Exists(companyDirectory))
                {
                    Directory.CreateDirectory(companyDirectory);
                }

                string filePath = Path.Combine(companyDirectory, fileChunkDto.FileName);

                if (fileChunkDto.FirstChunk && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    await stream.WriteAsync(fileChunkDto.Data, 0, fileChunkDto.Data.Length);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<string>> GetFileNames(string userCompanyName)
        {
            try
            {
                string companyDirectory = Path.Combine(_baseDirectory, userCompanyName);
                if (!Directory.Exists(companyDirectory))
                {
                    return new List<string>();
                }

                var files = Directory.GetFiles(companyDirectory);
                var fileUrls = new List<string>();
                foreach (var file in files)
                {
                    var relativePath = Path.Combine("UploadedFiles", userCompanyName, Path.GetFileName(file));
                    fileUrls.Add(relativePath.Replace("\\", "/"));
                }

                return await Task.FromResult(fileUrls);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}