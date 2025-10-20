using FB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace FB.Data.Repository
{
    public interface IRepositoryFoodItem : IRepositoryBase<FoodItem>
    {
        IEnumerable<FoodItem> GetAllWithRole();
    }

    public class RepositoryFoodItem : RepositoryBase<FoodItem>, IRepositoryFoodItem
    {
        public RepositoryFoodItem() : base()
        {

        }

        public IEnumerable<FoodItem> GetAllWithRole()
        {
            return _set.Include(f => f.Role).ToList();
        }
    }
}