
using System.Collections.Generic;
using System.Linq;

namespace RF.Data.Repository
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        User GetUserByNameAndEmail(string email);
    }

    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser() : base()
        { 
             
        }

        public User GetUserByNameAndEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) )
                return null;

            var normalizedEmail = email.Trim().ToLower();

            return _set.FirstOrDefault(x => x.Email.Trim().ToLower() == normalizedEmail);
        }
    }
}


