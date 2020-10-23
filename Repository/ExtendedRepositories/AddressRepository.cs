using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.Core;

namespace Repository.ExtendedRepositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Address GetAddressOfBranch(int BranchId);
        string GetAddressStringOfBranch(int BranchId);
    }

    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context, ILogger<Repository<Address>> logger) : base(context,
            logger)
        {
        }

        public Address GetAddressOfBranch(int BranchId)
        {
            Address address = GetAll().Where(x => x.BranchId == BranchId).FirstOrDefault();
            return address != null && address.Id > 0
                ? address
                : throw new KeyNotFoundException($"Address for branch id {BranchId} is not found");
        }

        public string GetAddressStringOfBranch(int BranchId)
        {
            Address address = GetAll().Where(x => x.BranchId == BranchId).FirstOrDefault();
            return address != null && address.Id > 0
                ? address.ToString()
                : throw new KeyNotFoundException($"Address for branch id {BranchId} is not found");
        }
    }
}