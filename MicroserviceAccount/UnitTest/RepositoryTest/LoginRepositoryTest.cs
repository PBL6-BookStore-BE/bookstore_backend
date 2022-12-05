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

        [TestInitialize]
        public void Initialize()
        {
            repo = new AccountRepository(userManager, roleManager, jwt, mailService, context);
        }

        [TestMethod]
        public void lele()
        {
            int a = 1;
            int b = 2;
            var sum = a + b;
            Assert.Equals(3, sum);
        }

        [TestMethod]
        public void Register_Repository()
        {
            LoginDTO model = new LoginDTO();
            model.Email = "nguyenkhanhlinh2752001@gmail.com";
            model.Password = "admin";
            var result = repo.LoginAsync(model);
            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }
    }
}