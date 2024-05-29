using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data;

namespace Afrejd.Web.Components.Pages.UserPages
{
    public partial class LaddaUppDokument
    {
        [Inject] IFilesUploadService? FilesManager { get; set; }
        private bool isUploading = false;
        private string ErrorMessage = string.Empty;
        private string dropClass = string.Empty;
        private int maxAllowedFiles = 3;
        List<FileUploadProgress> filesQueue = new();


        private void AddFilesToQueue(InputFileChangeEventArgs e)
        {
            dropClass = string.Empty;
            ErrorMessage = string.Empty;

            var allowedExtensions = new List<string> { ".pptx", ".xlsx", ".docx", ".pdf" };

            if (e.FileCount > maxAllowedFiles)
            {
                ErrorMessage = $"Det högsta antalet tillåtna filer är {maxAllowedFiles}, du har valt {e.FileCount} filer!";
            }
            else
            {
                var files = e.GetMultipleFiles(maxAllowedFiles);
                var fileCount = filesQueue.Count;

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.Name).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ErrorMessage = $"Ogiltig filtyp: {file.Name}. Tillåtna filtyper är: {string.Join(", ", allowedExtensions)}";
                        return;
                    }

                    var progress = new FileUploadProgress(file, file.Name, file.Size, fileCount);
                    filesQueue.Add(progress);
                    fileCount++;
                }
            }
        }

        private async Task UploadFileQueue(string userCompanyName)
        {
            isUploading = true;
            await InvokeAsync(StateHasChanged);

            foreach (var file in filesQueue.OrderByDescending(x => x.FileId))
            {
                if (!file.HasBeenUploaded)
                {
                    await UploadChunks(file, userCompanyName);
                    file.HasBeenUploaded = true;
                }
            }

            isUploading = false;
        }

        private async Task UploadChunks(FileUploadProgress file, string userCompanyName)
        {
            var totalBytes = file.Size;
            long chunkSize = 400000;
            long numChunks = totalBytes / chunkSize;
            long remainder = totalBytes % chunkSize;

            string nameOnly = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            string newFileNameWithoutPath = $"{userCompanyName}-{nameOnly}{extension}";

            bool firstChunk = true;
            using (var inStream = file.FileData.OpenReadStream(long.MaxValue))
            {
                for (int i = 0; i < numChunks; i++)
                {
                    var buffer = new byte[chunkSize];
                    await inStream.ReadAsync(buffer, 0, buffer.Length);

                    var chunk = new FileChunkDto
                    {
                        Data = buffer,
                        FileName = newFileNameWithoutPath,
                        Offset = filesQueue[file.FileId].UploadedBytes,
                        FirstChunk = firstChunk
                    };

                    await FilesManager.UploadFileChunk(chunk, userCompanyName);
                    firstChunk = false;

                    filesQueue[file.FileId].UploadedBytes += chunkSize;
                    await InvokeAsync(StateHasChanged);
                }

                if (remainder > 0)
                {
                    var buffer = new byte[remainder];
                    await inStream.ReadAsync(buffer, 0, buffer.Length);

                    var chunk = new FileChunkDto
                    {
                        Data = buffer,
                        FileName = newFileNameWithoutPath,
                        Offset = filesQueue[file.FileId].UploadedBytes,
                        FirstChunk = firstChunk
                    };

                    await FilesManager.UploadFileChunk(chunk, userCompanyName);

                    filesQueue[file.FileId].UploadedBytes += remainder;

                    await InvokeAsync(StateHasChanged);
                }
            }
        }


        private void RemoveFromQueue(int fileId)
        {
            var itemToRemove = filesQueue.SingleOrDefault(x => x.FileId == fileId);
            if (itemToRemove != null)
                filesQueue.Remove(itemToRemove);
        }


        private void ClearFileQueue()
        {
            filesQueue.Clear();
        }


        record FileUploadProgress(IBrowserFile File, string FileName, long Size, int FileId)
        {
            public IBrowserFile FileData { get; set; } = File;
            public int FileId { get; set; } = FileId;
            public long UploadedBytes { get; set; }
            public double UploadedPercentage => (double)UploadedBytes / (double)Size * 100d;
            public bool HasBeenUploaded { get; set; } = false;
        }


        void HandleDragEnter()
        {
            dropClass = "dropzone-active";
        }
        void HandleDragLeave()
        {
            dropClass = string.Empty;
        }
    }
}
