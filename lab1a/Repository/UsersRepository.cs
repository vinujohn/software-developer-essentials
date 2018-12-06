using System;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private ICollection<User> _registeredUsers;

        public UsersRepository(){
            _registeredUsers = new List<User>();
        }

        public void Add(User user){
            _registeredUsers.Add(user);
        }

        public User FindUserByUserName(string userName){
            return _registeredUsers.FirstOrDefault(x => x.UserName == userName);
        }

        public void SetLogin(User foundUser, bool value)
        {
            foundUser.IsLoggedIn = value;
        }
    }
}
