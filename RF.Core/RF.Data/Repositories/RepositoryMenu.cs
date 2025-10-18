
namespace RF.Data.Repository
{
    public interface IRepositoryMenu : IRepositoryBase<Menu>
    {
    }

    public class RepositoryMenu : RepositoryBase<Menu>, IRepositoryMenu
    {
        public RepositoryMenu() : base()
        {
        }
    }
}