using Models.Enums;

namespace Models.DataModels.Core
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public Districts Residence { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}