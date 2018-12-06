using System;
using Xunit;
using Service;
using Repository;
using Models;

namespace Tests
{
    public class UserServiceTests
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

        public UserServiceTests(){
            testRepository = new UsersRepository();
            sut = new UserService(testRepository);
        }

        [Fact]
        public void Test_User_Registration_Successful()
        {
            Assert.Null(testRepository.FindUserByUserName(testUser.UserName));
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            
            var foundUser = testRepository.FindUserByUserName(testUser.UserName);
            Assert.NotNull(foundUser);
            Assert.Equal(testUser.FirstName, foundUser.FirstName);
            Assert.Equal(testUser.LastName, foundUser.LastName);
            Assert.Equal(testUser.Email, foundUser.Email);
            Assert.Equal(testUser.UserName, foundUser.UserName);
            Assert.Equal(testUser.Password, foundUser.Password);
        }

        [Fact]
        public void Test_User_LogIn_Successful()
        {            
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            Assert.False(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
            sut.LogIn(testUser.UserName, testUser.Password);

            Assert.True(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
        }

        [Fact]
        public void Test_User_LogIn_Failed_Bad_Password()
        {            
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            Assert.False(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
            
            Assert.False(sut.LogIn(testUser.UserName, "I am a bad password"));
            Assert.False(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
        }

        [Fact]
        public void Test_User_LogIn_Failed_User_Not_Registered()
        {                
            Assert.Null(testRepository.FindUserByUserName(testUser.UserName));
            Assert.False(sut.LogIn(testUser.UserName, testUser.Password));
        }

        [Fact]
        public void Test_User_LogOut_Successful()
        {            
            sut.RegisterUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.UserName, testUser.Password);
            sut.LogIn(testUser.UserName, testUser.Password);
            Assert.True(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
            sut.LogOut(testUser.UserName);

            Assert.False(testRepository.FindUserByUserName(testUser.UserName).IsLoggedIn);
        }
    }
}
