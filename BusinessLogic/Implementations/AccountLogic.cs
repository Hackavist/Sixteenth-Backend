using System;

using BusinessLogic.Interfaces;

using Models.DataModels;
using Models.DataModels.Core;
using Models.Enums;

using Repository;
using Repository.ExtendedRepositories;
using Repository.ExtendedRepositories.RoleSystem;

using Services.DTOs;

namespace BusinessLogic.Implementations
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IPasswordManager passwordManager;
        private readonly IRolesRepository rolesRepository;
        private readonly IUserRepository userRepository;

        public AccountLogic(IUserRepository UserRepository, IRolesRepository RolesRepository,
            IPasswordManager PasswordManager, IRepository<Customer> customerRepository)
        {
            userRepository = UserRepository;
            rolesRepository = RolesRepository;
            passwordManager = PasswordManager;
            this.customerRepository = customerRepository;
        }

        public bool Register(UserRegistrationDTO request, string Role)
        {
            if (userRepository.CheckEmailExists(request.Email.ToLower())) return false;
            User u = new User
            {
                Email = request.Email.ToLower(),
                Password = passwordManager.HashPassword(request.Password)
            };
            userRepository.Insert(u).Wait();
            rolesRepository.AssignRoleToUser(Role, u.Id);
            Customer customer = new Customer { Name = request.Name };

            if (Enum.TryParse(typeof(Districts), request.District, false, out object? district) && district != null)
                customer.Residence = (Districts)district;
            else
                customer.Residence = Districts.Ryad;

            customer.UserId = u.Id;
            customerRepository.Insert(customer);
            return true;
        }
    }
}