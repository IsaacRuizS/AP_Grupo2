
using System.Collections.Generic;
using System.Linq;

namespace RF.Data.Repository
{
    public interface IRepositoryMenu : IRepositoryBase<Menu>
    {
        IEnumerable<Menu> GetMenusByRestaurant(int restaurantId);
        bool DeleteMenuAndFk(int menuId);
    }

    public class RepositoryMenu : RepositoryBase<Menu>, IRepositoryMenu
    {
        public RepositoryMenu() : base()
        {
        }

        public IEnumerable<Menu> GetMenusByRestaurant(int restaurantId)
        {

            return _set.Include("Restaurant").Where(x => x.RestaurantID == restaurantId).ToList();
        }

        public bool DeleteMenuAndFk(int menuId)
        {

            var menuItems = _context.MenuItems.Where(x => x.MenuID == menuId).ToList<MenuItem>();

            if (menuItems.Count() > 0)
            {
                _context.MenuItems.RemoveRange(menuItems);
            }

            var menu = _set.Find(menuId);
            if (menu != null)
            {
                _set.Remove(menu);
                Save();

                return true;
            }

            return false;
        }
    } 
}