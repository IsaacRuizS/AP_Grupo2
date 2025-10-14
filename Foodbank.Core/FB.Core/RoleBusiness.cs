using FB.Data;
using FB.Data.Repository;
using System.Collections.Generic;

namespace FB.Core
{
    public class RoleBusiness
    {

        private readonly IRepositoryRole _repositoryRole;

        public RoleBusiness()
        {
            _repositoryRole = new RepositoryRole();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Role user)
        {

            if (user.RoleId <= 0)
                _repositoryRole.Add(user);
            else
                _repositoryRole.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryRole.Delete(id);
            return true;
        }

        public IEnumerable<Role> GetRoles(int id)
        {
            return id <= 0
                ? _repositoryRole.GetAll()
                : new List<Role>() { _repositoryRole.GetById(id) };
        }
    }
}
