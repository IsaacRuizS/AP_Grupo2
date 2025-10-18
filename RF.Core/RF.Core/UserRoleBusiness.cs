using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class UserRoleBusiness
    {

        private readonly IRepositoryUserRole _repositoryUserRole;

        public UserRoleBusiness()
        {
            _repositoryUserRole = new RepositoryUserRole();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(UserRole user)
        {

            if (user.RoleID <= 0)
                _repositoryUserRole.Add(user);
            else
                _repositoryUserRole.Update(user);

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
