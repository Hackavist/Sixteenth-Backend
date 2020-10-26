using System.Collections.Generic;
using System.Linq;
using Services.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IBranchLogic
    {
        IEnumerable<BranchResponseDTO> GetAll();
        BranchResponseDTO Get(int id);
        int Insert(BranchRequestDTO entity);
        IEnumerable<int> InsertRange(IEnumerable<BranchRequestDTO> entities);
        void Update(int branchId,BranchRequestDTO entity);
        void SoftDelete(int id);
        void UpdateAddressOfBranch(int branchId, AddressRequestDTO newAddress);

        IQueryable<BranchImageDTO> GetAllImagesOfBranch(int BranchId);
        bool CheckIfImageForBranchExist(int imageId, int branchId);
        BranchImageDTO GetBranchMainImage(int branchId);
    }
}