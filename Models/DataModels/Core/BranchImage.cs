namespace Models.DataModels.Core
{
    public class BranchImage : BaseModel
    {
        public string Base64Content { get; set; }
        public string Extension { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}