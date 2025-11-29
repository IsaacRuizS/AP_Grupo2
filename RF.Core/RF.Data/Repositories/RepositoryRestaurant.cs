
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RF.Data.Repository
{
    public interface IRepositoryRestaurant : IRepositoryBase<Restaurant>
    {
        IEnumerable<Restaurant> GetRestaurantsByUser(int userId);
    }

    public class RepositoryRestaurant : RepositoryBase<Restaurant>, IRepositoryRestaurant
    {
        public RepositoryRestaurant() : base()
        {
        }

        public IEnumerable<Restaurant> GetRestaurantsByUser(int userId)
        { 

            return _set.Where(x => x.UserID == userId);
        }
    }
}