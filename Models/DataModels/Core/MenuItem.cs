namespace Models.DataModels.Core
{
    public class MenuItem : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public double Price { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}