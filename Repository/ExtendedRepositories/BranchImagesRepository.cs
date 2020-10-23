using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.Core;

namespace Repository.ExtendedRepositories
{
    public interface IBranchPhotosRepository : IRepository<BranchImage>
    {
        IQueryable<BranchImage> GetAllImagesOfBranch(int BranchId);
        bool CheckIfImageForBranchExist(int imageId, int branchId);
        BranchImage GetBranchImage(int imageId, int branchId);
    }

    public class BranchImagesRepository : Repository<BranchImage>, IBranchPhotosRepository
    {
        public BranchImagesRepository(ApplicationDbContext context, ILogger<Repository<BranchImage>> logger) : base(
            context, logger)
        {
        }

        public IQueryable<BranchImage> GetAllImagesOfBranch(int BranchId)
        {
            return GetAll().Where(b => b.BranchId == BranchId);
        }

        public bool CheckIfImageForBranchExist(int imageId, int branchId)
        {
            return GetAll().Where(i => i.Id == imageId && i.BranchId == branchId).FirstOrDefault().Id > 0;
        }

        public BranchImage GetBranchImage(int imageId, int branchId)
        {
            BranchImage img = GetAll().Where(i => i.Id == imageId && i.BranchId == branchId).FirstOrDefault();
            return img != null && img.Id > 0
                ? img
                : throw new KeyNotFoundException($"Image with ImageId{imageId} and BranchId {branchId} doesn't exist");
        }
    }
}