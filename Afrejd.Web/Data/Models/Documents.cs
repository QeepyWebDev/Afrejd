using Microsoft.Identity.Client;

namespace Afrejd.Web.Data.Models
{
    public class Documents
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
