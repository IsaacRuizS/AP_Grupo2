
namespace RF.Data.Repository
{
    public interface IRepositoryRestaurantRegistrationRequest : IRepositoryBase<RestaurantRegistrationRequest>
    {
    }

    public class RepositoryRestaurantRegistrationRequest : RepositoryBase<RestaurantRegistrationRequest>, IRepositoryRestaurantRegistrationRequest
    {
        public RepositoryRestaurantRegistrationRequest() : base()
        {
        }
    }
}