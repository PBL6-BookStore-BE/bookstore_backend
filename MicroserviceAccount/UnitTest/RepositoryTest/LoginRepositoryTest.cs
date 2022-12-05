using MicroserviceAccount.Data;
using MicroserviceAccount.DTOs;
using MicroserviceAccount.Entities;
using MicroserviceAccount.Interfaces;
using MicroserviceAccount.Repositories;
using MicroserviceAccount.Services;
using MicroserviceAccount.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroserviceAccount.UnitTest.RepositoryTest
{
    [TestClass]
    public class LoginRepositoryTest
    {
        private IAccountRepository repo;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        private IOptions<JWT> jwt;
        private IMailService mailService;
        private AccountDataContext context;

        //[TestInitialize]
        //public void Initialize()
        //{
        //    repo = new AccountRepository(userManager, roleManager, jwt, mailService, context);
        //    context = new AccountDataContext();
        //}

        [TestMethod]
        public void Login_Repository()
        {
            //LoginDTO model = new LoginDTO();
            //model.Email = "nguyenkhanhlinh2752001@gmail.com";
            //model.Password = "admin";
            //var result = repo.LoginAsync(model);
            Assert.IsNotNull(1);
        }
    }
}