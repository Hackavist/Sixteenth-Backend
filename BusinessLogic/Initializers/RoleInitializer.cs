using Repository.ExtendedRepositories;
using System.Linq;
using Models.DataModels;
using System.Threading.Tasks;
using Models.DataModels.RoleSystem;
using Repository.ExtendedRepositories.RoleSystem;

namespace BusinessLogic.Initializers
{
    public class RoleInitializer : BaseInitializer
    {
        private readonly IRolesRepository roleRepository;
        private readonly IPermissionsRepository permissionsRepository;
        public RoleInitializer(IRolesRepository RoleRepository, IPermissionsRepository PermissionsRepository)
        {
            this.roleRepository = RoleRepository;
            this.permissionsRepository = PermissionsRepository;
        }
        public override void Initialize()
        {
            if (!roleRepository.GetAll().Any())
            {
                roleRepository.Insert(new Role { Name = "User" });
                Role admin = new Role { Name = "Admin" };
                roleRepository.Insert(admin).Wait();
                if (!permissionsRepository.GetAll().Any(u => u.Name == "CanManageRoles"))
                {
                    permissionsRepository.Insert(new Permission { Name = "CanManageRoles" }).Wait();
                }
                foreach (int permissionId in permissionsRepository.GetAll().Select(u => u.Id).ToList())
                {
                    permissionsRepository.AssignPermissionToRole(permissionId, admin.Id);
                }
            }
        }
    }
}
