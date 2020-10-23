using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DataModels;
using Models.Helpers;
using Repository.ExtendedRepositories;
using Repository.ExtendedRepositories.RoleSystem;
using Services.DTOs;

namespace BusinessLogic.Implementations
{
    public class JwtAuthorization : IAuth
    {
        private readonly IOptions<AppSettings> options;
        private readonly IPasswordManager passwordManager;
        private readonly IPermissionsRepository permissionsRepository;
        private readonly IRolesRepository rolesRepository;
        private readonly IUserRepository userRepository;

        public JwtAuthorization(IOptions<AppSettings> options,
            IUserRepository UserRepository, IPasswordManager PasswordManager,
            IRolesRepository RolesRepository, IPermissionsRepository PermissionsRepository)
        {
            this.options = options;
            passwordManager = PasswordManager;
            userRepository = UserRepository;
            rolesRepository = RolesRepository;
            permissionsRepository = PermissionsRepository;
        }

        public User GenerateToken(int UserId)
        {
            return GenerateToken(userRepository.Get(UserId));
        }

        public User Authenticate(UserAuthenticationRequest request)
        {
            User user = userRepository.GetUser(request.Email.ToLower());
            if (user == null || !passwordManager.ComparePassword(request.Password, user.Password)) return null;
            if (!user.LoggedIn)
            {
                user.LoggedIn = true;
                userRepository.Update(user);
            }

            return GenerateToken(user);
        }

        public void Logout(int UserId)
        {
            User user = userRepository.Get(UserId);
            user.LoggedIn = false;
            user.LastLogOut = DateTime.UtcNow;
            userRepository.Update(user);
        }

        public bool Validate(int UserId, DateTime TokenIssuedDate)
        {
            User user = userRepository.Get(UserId);
            if (user.LoggedIn && (user.LastLogOut == null || TokenIssuedDate > user.LastLogOut)) return true;
            return false;
        }

        public User GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Value.Secret);
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email.ToLower()),
                new Claim("Id", user.Id.ToString()),
                new Claim("DateIssued", DateTime.UtcNow.ToString())
            };
            if (options.Value.ValidateRolesFromToken)
            {
                List<Claim> RoleClaims = null, PermissionClaims = null;
                Task.WaitAll(
                    Task.Run(() =>
                    {
                        RoleClaims = new List<Claim>(from role in rolesRepository.GetRolesOfUser(user.Id)
                            select new Claim(ClaimTypes.Role, role.Name));
                    }),
                    Task.Run(() =>
                    {
                        PermissionClaims = new List<Claim>(
                            from permission in permissionsRepository.GetPermissionsOfUser(user.Id)
                            select new Claim("Permission", permission.Name));
                    })
                );
                Claims.AddRange(RoleClaims);
                Claims.AddRange(PermissionClaims);
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(options.Value.TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }
    }
}