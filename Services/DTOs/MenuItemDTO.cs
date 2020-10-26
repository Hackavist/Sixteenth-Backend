using System;
namespace Services.DTOs
{
    public class MenuItemResponseDTO:ResponseBaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public double Price { get; set; }

        public int BranchId { get; set; }
    }
    public class MenuItemRequestDTO 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public double Price { get; set; }

        public int BranchId { get; set; }
    }
}
