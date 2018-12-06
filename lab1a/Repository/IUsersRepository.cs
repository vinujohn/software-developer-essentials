using System;
using Models;

namespace Repository
{
    public interface IUsersRepository
    {
        void Add(User user);

        User FindUserByUserName(string userName);
        void SetLogin(User foundUser, bool value);
    }
}
