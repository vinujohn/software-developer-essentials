using System;
using Xunit;
using Service;
using Repository;
using Models;

namespace Tests
{
    public class UserRegistrationTests
    {
        User testUser = new User{
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@mail.com",
            UserName = "jdoe",
            Password = "p@ssw0rd"
        };

        IUsersRepository testRepository;
        UserService sut;

        public UserRegistrationTests(){
            testRepository = new UsersRepository();
            sut = new UserService(testRepository);
        }

        [Fact]
        public void Test_User_Registration_Successful()
        {
            Assert.Null(testRepository.FindUserByUserName(testUser.UserName));
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            
            Assert.NotNull(testRepository.FindUserByUserName(testUser.UserName));
        }

        [Fact]
        public void Test_User_LogIn_Successful()
        {            
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            Assert.False(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
            sut.LogIn(testUser.UserName, testUser.Password);

            Assert.True(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
        }
    }
}
