
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RF.Data.Repository
{
    public interface IRepositoryRestaurant : IRepositoryBase<Restaurant>
    {
        IEnumerable<Restaurant> GetRestaurantsByUser(int userId);
        IEnumerable<Restaurant> GetRestaurantsComplete(int userId);
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

        public IEnumerable<Restaurant> GetRestaurantsComplete(int restaurantId)
        {

            if(restaurantId > 0)
            {
                return _set.Include("Menus").Include("Menus.MenuItems").Include("Schedules").Where(x => x.RestaurantID == restaurantId).ToList();
            }
            else
            {
                return _set.Include("Menus").Include("Menus.MenuItems").Include("Schedules").ToList();

            }

        }
    }
}