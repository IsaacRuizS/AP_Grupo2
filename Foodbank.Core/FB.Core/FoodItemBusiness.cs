using FB.Data;
using FB.Data.Entities;
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

        public static IEnumerable<FoodItem> GetFoodItemsByFilter(FoodItemsFilterViewModel filter)
        {
            var repository = new RepositoryFoodItem();
            var foodItems = repository.GetAllWithRole();

            if (string.IsNullOrEmpty(filter.OmitirRol))
            {
                if (filter.RoleName.Equals("Manager", StringComparison.OrdinalIgnoreCase))
                    foodItems = foodItems.Where(f => f.Role.RoleName == "Manager" || f.Role.RoleName == "Viewer");

                if (filter.RoleName.Equals("Viewer", StringComparison.OrdinalIgnoreCase))
                    foodItems = foodItems.Where(f => f.Role.RoleName == "Viewer");
            }

            if (!string.IsNullOrEmpty(filter.Name))
                foodItems = foodItems.Where(f => f.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Category))
                foodItems = foodItems.Where(f => f.Category.Contains(filter.Category));

            if (!string.IsNullOrEmpty(filter.Brand))
                foodItems = foodItems.Where(f => f.Brand.Contains(filter.Brand));

            if (!string.IsNullOrEmpty(filter.Description))
                foodItems = foodItems.Where(f => f.Description.Contains(filter.Description));

            if (filter.PrecioMin.HasValue)
                foodItems = foodItems.Where(f => f.Price >= filter.PrecioMin.Value);

            if (filter.PrecioMax.HasValue)
                foodItems = foodItems.Where(f => f.Price <= filter.PrecioMax.Value);

            if (!string.IsNullOrEmpty(filter.Unit))
                foodItems = foodItems.Where(f => f.Unit.Contains(filter.Unit));

            if (filter.Quantity.HasValue)
                foodItems = foodItems.Where(f => f.QuantityInStock == filter.Quantity.Value);

            if (filter.FechaDesde.HasValue)
                foodItems = foodItems.Where(f => f.ExpirationDate >= filter.FechaDesde.Value);

            if (filter.FechaHasta.HasValue)
                foodItems = foodItems.Where(f => f.ExpirationDate <= filter.FechaHasta.Value);

            if (filter.IsPerishable.HasValue)
                foodItems = foodItems.Where(f => f.IsPerishable == filter.IsPerishable.Value);

            if (filter.Calories.HasValue)
                foodItems = foodItems.Where(f => f.CaloriesPerServing == filter.Calories.Value);

            if (!string.IsNullOrEmpty(filter.Barcode))
                foodItems = foodItems.Where(f => f.Barcode.Contains(filter.Barcode));

            return foodItems;
        }
    }
}