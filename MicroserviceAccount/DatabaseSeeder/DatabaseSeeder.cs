using MicroserviceAccount.Constants;
using MicroserviceAccount.Data;
using MicroserviceAccount.Entities;
using Microsoft.AspNetCore.Identity;

namespace MicroserviceAccount.DatabaseSeeder
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly AccountDataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DatabaseSeeder(ILogger<DatabaseSeeder> logger, AccountDataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            AddAdministrator();
            _context.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                var adminRole = new Role()
                {
                    Name = RoleConstants.AdministratorRole,
                    Description = "Administrator role with full permission"
                };
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    _logger.LogInformation("Seeded Administrator Role.");
                }

                var customerRole = new Role()
                {
                    Name = RoleConstants.CustomerRole,
                    Description = "Customer role with custom permission"
                };
                var employeeRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.CustomerRole);
                if (employeeRoleInDb == null)
                {
                    await _roleManager.CreateAsync(customerRole);
                    _logger.LogInformation("Seeded Employee Role.");
                }

                //Check if User Exists
                var superUser = new User()
                {
                    FullName = "Super Admin",
                    Email = "nguyenkhanhlinh2752001@gmail.com",
                    UserName = "superadmin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    PhoneNumber="12345"
                };
                var superUserInDb = await _userManager.FindByNameAsync(superUser.UserName);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default SuperAdmin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}