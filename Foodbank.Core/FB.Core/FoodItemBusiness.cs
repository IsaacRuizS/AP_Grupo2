using FB.Data;
using FB.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Core
{
    public class FoodItemBusiness
    {
        private readonly IRepositoryFoodItem _repositoryFoodItem;

        public FoodItemBusiness()
        {
            _repositoryFoodItem = new RepositoryFoodItem();
        }

        // Upsert (Update / Insert)
        public static bool SaveOrUpdate(FoodItem foodItem)
        {
            var repository = new RepositoryFoodItem();

            if (foodItem.FoodItemID <= 0)
                repository.Add(foodItem);
            else
                repository.Update(foodItem);

            return true;
        }

        public static bool Delete(int id)
        {
            var repository = new RepositoryFoodItem();
            repository.Delete(id);
            return true;
        }

        public static IEnumerable<FoodItem> GetFoodItems(int id)
        {
            var repository = new RepositoryFoodItem();

            return id <= 0
                ? repository.GetAll()
                : new List<FoodItem>() { repository.GetById(id) };
        }

        public static IEnumerable<FoodItem> GetFoodItemsByRole(string roleName)
        {
            var repository = new RepositoryFoodItem();
            var foodItems = repository.GetAllWithRole();

            if (string.IsNullOrEmpty(roleName))
            {
                return foodItems;
            }

            if (roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return foodItems;
            }
            else if (roleName.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                return foodItems.Where(f => f.Role.RoleName == "Manager" || f.Role.RoleName == "Viewer");
            }
            else if (roleName.Equals("Viewer", StringComparison.OrdinalIgnoreCase))
            {
                return foodItems.Where(f => f.Role.RoleName == "Viewer");
            }

            return foodItems;
        }
    }
}