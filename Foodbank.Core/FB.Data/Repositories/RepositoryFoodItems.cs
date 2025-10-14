using FB.Data;

namespace FB.Data.Repository
{
    public interface IRepositoryFoodItem : IRepositoryBase<FoodItem>
    {
    }

    public class RepositoryFoodItem : RepositoryBase<FoodItem>, IRepositoryFoodItem
    {
        public RepositoryFoodItem() : base()
        {
        }
    }
}