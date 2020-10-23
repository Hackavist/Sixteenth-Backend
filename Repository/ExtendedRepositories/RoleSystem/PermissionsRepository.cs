using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.RoleSystem;

namespace Repository.ExtendedRepositories.RoleSystem
{
    public interface IPermissionsRepository : IRepository<Permission>
    {
        Permission GetPermission(string Name);
        bool CheckPermissionExists(string Name);

        IQueryable<Permission> GetPermissionsOfRole(string Role);
        IQueryable<Permission> GetPermissionsOfRole(int RoleId);

        IQueryable<Permission> GetPermissionsOfUser(int UserId);
        IQueryable<Permission> GetPermissionsOfUser(string Email);

        void AssignPermissionToRole(string Permission, string Role);
        void AssignPermissionToRole(string Permission, int RoleId);

        void RemovePermissionFromRole(string Permission, int RoleId);
        void RemovePermissionFromRole(string Permission, string roleName);
        void AssignPermissionToRole(int PermisionId, int RoleId);

        bool UserHasPermission(string Email, string Permission);
        bool UserHasPermission(int UserId, string Permission);
        bool UserHasPermission(int UserId, int PermissionId);
    }
    public class PermissionsRepository : Repository<Permission>, IPermissionsRepository
    {
        private readonly IRepository<RolePermission> rolePermissionRepository;
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly IRolesRepository rolesRepository;
        public PermissionsRepository(ApplicationDbContext db, ILogger<PermissionsRepository> logger,
            IRepository<RolePermission> RolePermissionRepository, IRolesRepository RolesRepository, IRepository<UserRole> UserRoleRepository)
            : base(db, logger)
        {
            this.rolePermissionRepository = RolePermissionRepository;
            this.rolesRepository = RolesRepository;
            this.userRoleRepository = UserRoleRepository;
        }
        public void AssignPermissionToRole(string Permission, string Role)
        {
            AssignPermissionToRole(Permission, rolesRepository.GetRole(Role).Id);
        }
        public void AssignPermissionToRole(string Permission, int RoleId)
        {
            rolePermissionRepository.Insert(new RolePermission
            {
                RoleId = RoleId,
                PermissionId = GetPermission(Permission).Id
            });
        }

        public void RemovePermissionFromRole(string Permission, int RoleId)
        {
            var userRole = rolePermissionRepository.GetAll().Where(x => x.Permission.Name == Permission && x.Role.Id == RoleId).FirstOrDefault();
            rolePermissionRepository.SoftDelete(userRole);
        }
        public void AssignPermissionToRole(int PermisionId, int RoleId)
        {
            rolePermissionRepository.Insert(new RolePermission
            {
                RoleId = RoleId,
                PermissionId = PermisionId
            });
        }

        public Permission GetPermission(string Name)
        {
            return (from permission in GetAll()
                    where permission.Name == Name
                    select permission).SingleOrDefault();
        }
        public IQueryable<Permission> GetPermissionsOfRole(string Role)
        {
            return from rolePermission in rolePermissionRepository.GetAll()
                   where rolePermission.Role.Name == Role
                   select rolePermission.Permission;
        }
        public IQueryable<Permission> GetPermissionsOfRole(int RoleId)
        {
            return from rolePermission in rolePermissionRepository.GetAll()
                   where rolePermission.RoleId == RoleId
                   select rolePermission.Permission;
        }
        public IQueryable<Permission> GetPermissionsOfUser(int UserId)
        {
            return (from roleUser in userRoleRepository.GetAll()
                    join permissionRole in rolePermissionRepository.GetAll()
                    on roleUser.RoleId equals permissionRole.RoleId
                    where roleUser.UserId == UserId
                    select permissionRole.Permission).Distinct();
        }
        public IQueryable<Permission> GetPermissionsOfUser(string Email)
        {
            return (from roleUser in userRoleRepository.GetAll()
                    join permissionRole in rolePermissionRepository.GetAll()
                    on roleUser.RoleId equals permissionRole.RoleId
                    where roleUser.User.Email == Email
                    select permissionRole.Permission).Distinct();
        }
        public bool UserHasPermission(string Email, string Permission)
        {
            return (from roleUser in userRoleRepository.GetAll()
                    join permissionRole in rolePermissionRepository.GetAll()
                    on roleUser.RoleId equals permissionRole.RoleId
                    where roleUser.User.Email == Email && permissionRole.Permission.Name == Permission
                    select permissionRole).Any();
        }
        public bool UserHasPermission(int UserId, string Permission)
        {
            return (from roleUser in userRoleRepository.GetAll()
                    join permissionRole in rolePermissionRepository.GetAll()
                    on roleUser.RoleId equals permissionRole.RoleId
                    where roleUser.UserId == UserId && permissionRole.Permission.Name == Permission
                    select permissionRole.Permission).Any();
        }

        public void RemovePermissionFromRole(string Permission, string roleName)
        {
            var userRole = rolePermissionRepository.GetAll().Where(x => x.Permission.Name == Permission && x.Role.Name == roleName).FirstOrDefault();
            rolePermissionRepository.SoftDelete(userRole);
        }

        public bool UserHasPermission(int UserId, int PermissionId)
        {
            return (from roleUser in userRoleRepository.GetAll()
                    join permissionRole in rolePermissionRepository.GetAll()
                    on roleUser.RoleId equals permissionRole.RoleId
                    where roleUser.UserId == UserId && permissionRole.PermissionId == PermissionId
                    select permissionRole).Any();
        }

        public bool CheckPermissionExists(string Name)
        {
            return (from permission in GetAll()
                    where permission.Name == Name
                    select permission).Any();
        }
    }
}
