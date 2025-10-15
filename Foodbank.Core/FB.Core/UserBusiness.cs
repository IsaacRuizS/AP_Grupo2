using FB.Data;
using FB.Data.Repository;
using System.Collections.Generic;

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
    }
}
