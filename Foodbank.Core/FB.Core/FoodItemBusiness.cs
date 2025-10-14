using FB.Data;
using FB.Data.Repository;
using System.Collections.Generic;

namespace FB.Core
{
    public class FoodItemBusiness
    {

        private readonly IRepositoryFoodItem _repositoryFoodItem;

        public FoodItemBusiness()
        {
            _repositoryFoodItem = new RepositoryFoodItem();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(FoodItem user)
        {

            if (user.FoodItemID <= 0)
                _repositoryFoodItem.Add(user);
            else
                _repositoryFoodItem.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryFoodItem.Delete(id);
            return true;
        }

        public IEnumerable<FoodItem> GetFoodItems(int id)
        {
            return id <= 0
                ? _repositoryFoodItem.GetAll()
                : new List<FoodItem>() { _repositoryFoodItem.GetById(id) };
        }
    }
}
