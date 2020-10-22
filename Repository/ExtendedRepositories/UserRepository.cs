using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels;

namespace Repository.ExtendedRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string email);
        bool CheckEmailExists(string email);
        bool CheckUserExists(int UserId);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context, logger)
        {
        }

        public User GetUser(string email)
        {
            User result = GetAll().Where(e => e.Email == email).AsQueryable().FirstOrDefault();
            return result != null && result.Id > 0
                ? result
                : throw new KeyNotFoundException(
                    $"{nameof(email)} {email} Doesn't exist in {nameof(User)} Table");
        }

        public bool CheckEmailExists(string Email)
        {
            return (from user in GetAll()
                where user.Email == Email
                select user).Any();
        }

        public bool CheckUserExists(int UserId)
        {
            return (from user in GetAll()
                where user.Id == UserId
                select user).Any();
        }
    }
}