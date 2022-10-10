using MicroserviceAccount.Constants;
using MicroserviceAccount.DTOs;
using MicroserviceAccount.Entities;
using MicroserviceAccount.Interfaces;
using MicroserviceAccount.Services;
using MicroserviceAccount.Settings;
using MicroserviceAccount.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroserviceAccount.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JWT _jwt;
        private readonly IMailService _mailService;

        public AccountRepository(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JWT> jwt, IMailService mailService )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mailService = mailService;
        }

        public async Task<AuthenticationVM> LoginAsync(LoginDTO model)
        {
            var authenticationModel = new AuthenticationVM();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.Message = "Login successfully";
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<string> RegisterAsync(RegisterDTO model)
        {
            var user = new User
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                CreatedOn = DateTime.Now,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                IsActive = true
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.CustomerRole.ToString());
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} is already registered.";
            }
        }

        public async Task<string> AddRoleAsync(AddRoleDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }

            var roleExists = Enum.GetNames(typeof(RoleConstants.Roles)).Any(x => x.ToLower() == model.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(RoleConstants.Roles)).Cast<RoleConstants.Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Added {model.Role} to user {model.Email}.";
            }
            return $"Role {model.Role} not found.";
        }

        public async Task<AuthenticationVM> ForgetPassword(ForgetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var authenticationModel = new AuthenticationVM();
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (await _mailService.ForgetPasswordSendMail(user.Email, user.UserName, token))
            {
                authenticationModel.Message = $"Check your email at {model.Email} to reset password";
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<AuthenticationVM> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var authenticationModel = new AuthenticationVM();
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {user.Email}.";
                return authenticationModel;
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (resetPassResult.Succeeded)
            {
                authenticationModel.Message = $"{user.UserName} reset password successfully";
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            else
            {
                authenticationModel.Errors = resetPassResult.Errors.Select(e => e.Description);
                return authenticationModel;
            }
        }

        public async Task<AuthenticationVM> ChangePassword(string email, ChangePasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var authenticationModel = new AuthenticationVM();
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {email}.";
                return authenticationModel;
            }
            var rs = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (rs.Succeeded)
            {
                authenticationModel.Message = $"{user.UserName} changes password successfully";
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            else
            {
                authenticationModel.Errors = rs.Errors.Select(e => e.Description);
                return authenticationModel;
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
            audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}