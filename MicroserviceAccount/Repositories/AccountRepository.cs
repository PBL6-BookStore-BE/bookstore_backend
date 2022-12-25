﻿using MicroserviceAccount.Constants;
using MicroserviceAccount.Data;
using MicroserviceAccount.DTOs;
using MicroserviceAccount.Entities;
using MicroserviceAccount.Interfaces;
using MicroserviceAccount.Services;
using MicroserviceAccount.Settings;
using MicroserviceAccount.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly AccountDataContext _context;

        public AccountRepository(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JWT> jwt, IMailService mailService, AccountDataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mailService = mailService;
            _context = context;
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
            if (!user.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Account with {model.Email} is disabled";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.Message = "Login successfully";
                authenticationModel.Id = user.Id;
                authenticationModel.IsSuccess = true;
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
            authenticationModel.IsSuccess = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<AuthenticationVM> RegisterAsync(RegisterDTO model)
        {
            var authenticationModel = new AuthenticationVM();
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
                    authenticationModel.Message = "Register successfully";
                    authenticationModel.IsSuccess = true;
                    return authenticationModel;
                }
                else
                {
                    authenticationModel.Message = "Register failed";
                    authenticationModel.IsSuccess = false;
                    authenticationModel.Errors = result.Errors.Select(e => e.Description);
                    return authenticationModel;
                }
            }
            authenticationModel.Message = $"User with email {userWithSameEmail.Email} already registered";
            return authenticationModel;
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
            token = token.Replace(@"/", @"%2F");
            if (await _mailService.ForgetPasswordSendMail(user.Email, user.UserName, token))
            {
                authenticationModel.Message = $"Check your email at {model.Email} to reset password";
                authenticationModel.IsSuccess = true;
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.IsSuccess = false;
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
                authenticationModel.IsSuccess = true;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            else
            {
                authenticationModel.IsSuccess = false;
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
                authenticationModel.IsSuccess = false;
                authenticationModel.Message = $"No Accounts Registered with {email}.";
                return authenticationModel;
            }
            var rs = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (rs.Succeeded)
            {
                authenticationModel.Message = $"{user.UserName} changes password successfully";
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email;
                authenticationModel.IsSuccess = true;
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
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id)
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

        public async Task<AuthenticationVM> CreateUser(CreateUserDTO model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                PhoneNumber = model.Phone,
                CreatedOn = DateTime.Now.Date,
                EmailConfirmed = true,
                IsActive = true,
                PhoneNumberConfirmed = true,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, UserConstants.DefaultPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.AdministratorRole.ToString());
                    await _userManager.AddToRoleAsync(user, RoleConstants.CustomerRole.ToString());
                    return new AuthenticationVM
                    {
                        Message = $"User created successfully with username {user.UserName}",
                        IsSuccess = true
                    };
                }
            }
            return new AuthenticationVM
            {
                Message = $"Email {user.Email} is already registered.",
                IsSuccess = false
            };
        }

        public async Task<IEnumerable<GetListCustomersVM>> GetListCustomers(string? email, string? phone)
        {
            var list = await (from c in _context.Users
                              where c.PhoneNumberConfirmed == false
                              && (string.IsNullOrEmpty(email) || c.Email.ToLower().Contains(email!))
                              && (string.IsNullOrEmpty(phone) || c.PhoneNumber.ToLower().Contains(phone!))
                              select new GetListCustomersVM
                              {
                                  Id = c.Id,
                                  Username = c.UserName,
                                  FullName = c.FullName,
                                  Email = c.Email,
                                  Address = c.Address,
                                  PhoneNumber = c.PhoneNumber,
                                  CreatdOn = c.CreatedOn,
                                  IsActive = c.IsActive,
                              }).ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<IEnumerable<GetListAdminsVM>> GetListAdmins(string? email, string? phone)
        {
            var list = await (from c in _context.Users
                              where c.PhoneNumberConfirmed == true
                              && (string.IsNullOrEmpty(email) || c.Email.ToLower().Contains(email!))
                              && (string.IsNullOrEmpty(phone) || c.PhoneNumber.ToLower().Contains(phone!))
                              select new GetListAdminsVM
                              {
                                  Id = c.Id,
                                  Username = c.UserName,
                                  FullName = c.FullName,
                                  Email = c.Email,
                                  Address = c.Address,
                                  PhoneNumber = c.PhoneNumber,
                                  CreatedOn = c.CreatedOn,
                                  IsActive = c.IsActive,
                              }).ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<GetUserVM> GetUserByEmail(string email)
        {
            var user = await (from u in _context.Users
                              where u.Email == email
                              select new GetUserVM
                              {
                                  Id = u.Id,
                                  FullName = u.FullName,
                                  UserName=u.UserName,
                                  Email = u.Email,
                                  Address = u.Address,
                                  PhoneNumber = u.PhoneNumber,
                              }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<AuthenticationVM> UpdateStateUser(string id, bool state)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = state;
            await _context.SaveChangesAsync();
            var msg = (state == true) ? "Activated" : "Disabled";
            return new AuthenticationVM
            {
                Message = $"{msg} successfully user {user.UserName}",
                IsSuccess = true
            };
        }

        public async Task<AuthenticationVM> UpdateUser(string id, UpdateUserDTO model)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.PhoneNumber = model.Phone;
            user.UserName = model.Username;
            user.Address = model.Address;
            user.FullName = model.FullName;
            await _context.SaveChangesAsync();
            return new AuthenticationVM
            {
                Message = $"Updated information successfully user {user.Email}",
                IsSuccess = true
            };
        }

        public async Task<int> GetTotalAccount()
        {
            var total = (from c in _context.Users
                         where !c.PhoneNumberConfirmed
                         select c).Count();
            return total;
        }
    }
}