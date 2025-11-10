using FB.Data;
using FB.Data.Repository;
using System.Collections.Generic;
using System;

namespace FB.Core
{
    public class UserBusiness
    {

        private readonly IRepositoryUser _repositoryUser;

        public UserBusiness()
        {
            _repositoryUser = new RepositoryUser();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(User user)
        {

            if (user.UserId <= 0)
                _repositoryUser.Add(user);
            else
                _repositoryUser.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryUser.DeleteUserWithRoles(id);// call the modified method to delete the roles firts
            return true;
        }

        public IEnumerable<User> GetUsers(int id)
        {
            return id <= 0
                ? _repositoryUser.GetAll()
                : new List<User>() { _repositoryUser.GetById(id) };
        }

        // Get user by username/email
        public User GetUserByUsername(string username)
        {
            return _repositoryUser.GetByUsername(username);
        }

        // Create user record if not exists or update last login if exists
        public bool CreateOrUpdateLastLogin(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var existing = _repositoryUser.GetByUsername(username);
            if (existing != null)
            {
                existing.LastLogin = DateTime.Now;
                _repositoryUser.Update(existing);
            }
            else
            {
                var newUser = new User
                {
                    Username = username,
                    Email = username,
                    FullName = string.Empty,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now
                };
                _repositoryUser.Add(newUser);
            }

            return true;
        }
    }
}
