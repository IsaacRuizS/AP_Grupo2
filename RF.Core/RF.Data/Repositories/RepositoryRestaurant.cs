
namespace RF.Data.Repository
{
    public interface IRepositoryRestaurant : IRepositoryBase<Restaurant>
    {
    }

    public class RepositoryRestaurant : RepositoryBase<Restaurant>, IRepositoryRestaurant
    {
        public RepositoryRestaurant() : base()
        {
        }
    }
}