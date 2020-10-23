using System;

namespace Services.DTOs
{
    public class BranchResponseDTO : BaseDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BookingLink { get; set; }
        public string SocialLink { get; set; }
        public string AddressText { get; set; }
        public string AddressLink { get; set; }
        public int MainPhotoId { get; set; }
        public DateTime AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
    public class BranchRequestDTO : BaseDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BookingLink { get; set; }
        public string SocialLink { get; set; }
    }
}