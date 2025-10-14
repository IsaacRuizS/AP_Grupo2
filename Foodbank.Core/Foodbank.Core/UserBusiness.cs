using AP.Data;
using AP.Data.Entities;
using AP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Core
{
    public class UserBusiness
    {

        private readonly IRepositoryUser _repositoryUser;

        public UserBusiness()
        {
            _repositoryUser = new RepositoryUser();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Users user)
        {

            if (user.UserID <= 0)
                _repositoryUser.Add(user);
            else
                _repositoryUser.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryUser.Delete(id);
            return true;
        }

        public IEnumerable<Users> GetUsers(int id)
        {
            return id <= 0
                ? _repositoryUser.GetAll()
                : new List<Users>() { _repositoryUser.GetById(id) };
        }
    }
}
