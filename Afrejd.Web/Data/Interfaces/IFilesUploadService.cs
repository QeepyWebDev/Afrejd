namespace Afrejd.Web.Data.Interfaces
{
    public interface IFilesUploadService
    {
        Task<bool> UploadFileChunk(FileChunkDto fileChunkDto, string userCompanyName);
        Task<List<string>> GetFileNames(string userCompanyName);
    }
}
