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
    }
}