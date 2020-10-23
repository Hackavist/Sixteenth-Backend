using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.RoleSystem;

namespace Repository.ExtendedRepositories.RoleSystem
{
    public interface IRolesRepository : IRepository<Role>
    {
        Role GetRole(string Name);
        bool CheckRoleExists(string Name);
        IQueryable<Role> GetRolesOfUser(int UserId);
        IQueryable<Role> GetRolesOfUser(string Username);
        void AssignRoleToUser(string Role, string Username);
        void AssignRoleToUser(string Role, int UserId);
        bool UserHasRole(string Username, string Role);
        bool UserHasRole(int UserId, string Role);
        void RemoveRoleFormUser(string Role, int UserId);
    }

    public class RolesRepository : Repository<Role>, IRolesRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IRepository<UserRole> userRoleRepository;

        public RolesRepository(ApplicationDbContext db, ILogger<RolesRepository> logger,
            IUserRepository UserRepository, IRepository<UserRole> UserRoleRepository) : base(db, logger)
        {
            userRepository = UserRepository;
            userRoleRepository = UserRoleRepository;
        }

        public void AssignRoleToUser(string Role, string Username)
        {
            AssignRoleToUser(Role, userRepository.GetUser(Username).Id);
        }

        public void AssignRoleToUser(string Role, int UserId)
        {
            userRoleRepository.Insert(new UserRole
            {
                UserId = UserId,
                RoleId = GetRole(Role).Id
            });
        }

        public void RemoveRoleFormUser(string Role, int UserId)
        {
            UserRole userRole = userRoleRepository.GetAll().Where(x => x.UserId == UserId && x.Role.Name == Role)
                .FirstOrDefault();
            userRoleRepository.SoftDelete(userRole);
        }

        public Role GetRole(string Name)
        {
            return (from role in GetAll()
                where role.Name == Name
                select role).SingleOrDefault();
        }

        public IQueryable<Role> GetRolesOfUser(int UserId)
        {
            return from userRole in userRoleRepository.GetAll()
                where userRole.UserId == UserId
                select userRole.Role;
        }

        public IQueryable<Role> GetRolesOfUser(string Email)
        {
            return from userRole in userRoleRepository.GetAll()
                where userRole.User.Email == Email
                select userRole.Role;
        }

        public bool UserHasRole(string Email, string Role)
        {
            return (from userRole in userRoleRepository.GetAll()
                where userRole.User.Email == Email && userRole.Role.Name == Role
                select userRole).Any();
        }

        public bool UserHasRole(int UserId, string Role)
        {
            return (from userRole in userRoleRepository.GetAll()
                where userRole.UserId == UserId && userRole.Role.Name == Role
                select userRole).Any();
        }

        public bool CheckRoleExists(string Name)
        {
            return (from role in GetAll()
                where role.Name == Name
                select role).Any();
        }
    }
}