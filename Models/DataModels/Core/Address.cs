using Models.Enums;

namespace Models.DataModels.Core
{
    public class Address : BaseModel
    {
        public string AddressLink { get; set; }
        public string StreetName { get; set; }
        public Districts District { get; set; }

        public override string ToString()
        {
            return $"{StreetName},{District}";
        }
    }
}