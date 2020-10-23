using System.Collections.Generic;

namespace Models.DataModels.Core
{
    public class Branch : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BookingLink { get; set; }
        public string SocialLink { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<News> Offers { get; set; }
        public virtual ICollection<BranchImage> BranchImages { get; set; }

    }
}
