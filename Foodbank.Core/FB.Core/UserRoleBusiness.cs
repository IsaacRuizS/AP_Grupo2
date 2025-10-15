using FB.Data;
using FB.Data.Repository;
using System.Collections.Generic;

namespace FB.Core
{
    public class UserRoleBusiness
    {

        private readonly IRepositoryUserRole _repositoryUserRole;

        public UserRoleBusiness()
        {
            _repositoryUserRole = new RepositoryUserRole();
        }

        public bool Update(UserRole user)
        {

            _repositoryUserRole.Update(user);

            return true;
        }

        public bool Save(UserRole user)
        {
            _repositoryUserRole.Add(user);
         
            return true;
        }

        public bool Delete(int id)
        {
            _repositoryUserRole.Delete(id);
            return true;
        }

        public IEnumerable<UserRole> GetUserRoles(int id)
        {
            return id <= 0
                ? _repositoryUserRole.GetAll()
                : new List<UserRole>() { _repositoryUserRole.GetById(id) };
        }
    }
}
