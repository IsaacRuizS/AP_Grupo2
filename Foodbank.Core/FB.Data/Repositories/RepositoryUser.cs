using System.Linq;

namespace FB.Data.Repository
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        void DeleteUserWithRoles(int id);
        User GetByUsername(string username);
    }

    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser() : base() { }

        public void DeleteUserWithRoles(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            var userRoles = _context.UserRoles.Where(x => x.UserId == id).ToList();

            if (userRoles != null && userRoles.Any())
            {
                _context.UserRoles.RemoveRange(userRoles);
                Save();
            } 

            if (user != null)
            {
                Delete(id);
            } 
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}