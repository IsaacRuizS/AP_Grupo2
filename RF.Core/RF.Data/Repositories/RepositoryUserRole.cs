
namespace RF.Data.Repository
{
    public interface IRepositoryUserRole : IRepositoryBase<UserRole>
    {
    }

    public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
    {
        public RepositoryUserRole() : base()
        {
        }
    }
}