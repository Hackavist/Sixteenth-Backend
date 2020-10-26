namespace Services.DTOs
{
    public class BranchImageDTO
    {
        public string Base64Content { get; set; }
        public string Extension { get; set; }

        public int BranchId { get; set; }
    }
}
