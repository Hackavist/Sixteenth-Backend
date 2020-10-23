using Models.Enums;

namespace Models.DataModels.Core
{
    public class News : BaseModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string MainImageBase64 { get; set; }
        public string MainImageExtension { get; set; }
        public string PreviewImageBase64 { get; set; }
        public string PreviewImageExtension { get; set; }
        public NewsType NewsType { get; set; }

        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}