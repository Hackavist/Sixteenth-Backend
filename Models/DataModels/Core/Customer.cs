using Models.Enums;

namespace Models.DataModels.Core
{
    public class Customer : User
    {
        public string Name { get; set; }
        public Districts Residence { get; set; }
    }
}