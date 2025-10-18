
namespace RF.Data.Repository
{
    public interface IRepositoryMenuItem : IRepositoryBase<MenuItem>
    {
    }

    public class RepositoryMenuItem : RepositoryBase<MenuItem>, IRepositoryMenuItem
    {
        public RepositoryMenuItem() : base()
        {
        }
    }
}