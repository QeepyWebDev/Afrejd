namespace Afrejd.Web.Data
{
    public class FileChunkDto
    {
        public string FileName { get; set; } = "";
        public long Offset { get; set; }
        public byte[]? Data { get; set; }
        public bool FirstChunk = false;
    }
}
