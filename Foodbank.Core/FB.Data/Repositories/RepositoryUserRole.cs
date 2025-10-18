using System.Linq;
using FB.Data;

namespace FB.Data.Repository
{
    public interface IRepositoryUserRole : IRepositoryBase<UserRole>
    {
        UserRole GetByKey(int userId, int roleId);
        void DeleteByKey(int userId, int roleId);
    }

    public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
    {
        public RepositoryUserRole() : base()
        {
        }

        public UserRole GetByKey(int userId, int roleId)
        {
            return _set.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public void DeleteByKey(int userId, int roleId)
        {
            var entity = GetByKey(userId, roleId);
            if (entity != null)
            {
                _set.Remove(entity);
                Save();
            }
        }
    }
}