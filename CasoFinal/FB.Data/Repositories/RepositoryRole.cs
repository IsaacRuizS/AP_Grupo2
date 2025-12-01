namespace FB.Data.Repository
{
    public interface IRepositoryRole : IRepositoryBase<Role>
    {
    }

    public class RepositoryRole : RepositoryBase<Role>, IRepositoryRole
    {
        public RepositoryRole() : base()
        {
        }
    }
}