using System;
using Repository;
using Models;

namespace Service
{
    public class UserService
    {
        private readonly IUsersRepository _repository;

        public UserService(IUsersRepository repository){
            _repository = repository;
        }

        public void RegisterUser(string firstName, string lastName, string email, string userName, string password) => _repository.Add(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = userName,
            Password = password
        });

        public bool LogIn(string userName, string password){
            var foundUser = _repository.FindUserByUserName(userName);
            if (foundUser != null && foundUser.Password == password){
                _repository.SetLogin(foundUser, true);
                return true;
            }
            return false;
        }
    }
}
