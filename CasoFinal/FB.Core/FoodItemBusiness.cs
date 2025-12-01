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

        public static IEnumerable<FoodItem> GetFoodItemsByFilters(FilterFoodItemDto filtersInfo)
        {
            var repository = new RepositoryFoodItem();

            IEnumerable<FoodItem> allItems = repository.GetAll();

            if (filtersInfo != null) {

                //NAME
                if (!string.IsNullOrEmpty(filtersInfo.Name) && filtersInfo.Name.Length > 1)
                {
                    allItems = allItems.Where(x => x.Name.Length > 0 && x.Name.ToLower().Contains(filtersInfo.Name.ToLower()));
                }

                //CATEGORY
                if (!string.IsNullOrEmpty(filtersInfo.Category) && filtersInfo.Category.Length > 1)
                {
                    allItems = allItems.Where(x => x.Category.Length > 0 && x.Category.ToLower().Contains(filtersInfo.Category.ToLower()));
                }

                //BRAND
                if (!string.IsNullOrEmpty(filtersInfo.Brand) && filtersInfo.Brand.Length > 1)
                {
                    allItems = allItems.Where(x => x.Brand.Length > 0 && x.Brand.ToLower().Contains(filtersInfo.Brand.ToLower()));
                }

                //DESC
                if (!string.IsNullOrEmpty(filtersInfo.Description) && filtersInfo.Description.Length > 1)
                {
                    allItems = allItems.Where(x => x.Description.Length > 0 && x.Description.ToLower().Contains(filtersInfo.Description.ToLower()));
                }

                //MIN PRICE
                if (filtersInfo.MinPrice > 0)
                {
                    allItems = allItems.Where(x => x.Price >= filtersInfo.MinPrice);
                }

                //MAX PRICE
                if (filtersInfo.MaxPrice > 0)
                {
                    allItems = allItems.Where(x => x.Price <= filtersInfo.MaxPrice);
                }

                //UNIT
                if (!string.IsNullOrEmpty(filtersInfo.Unit) && filtersInfo.Unit.Length > 1)
                {
                    allItems = allItems.Where(x => x.Unit.Length > 0 && x.Unit.ToLower().Contains(filtersInfo.Unit.ToLower()));
                }

                //QUANTITY
                if (filtersInfo.QuantityInStock > 0)
                {
                    allItems = allItems.Where(x => x.QuantityInStock == filtersInfo.QuantityInStock);
                }

                //START
                if (filtersInfo.StartExpirationDate != null)
                {
                    allItems = allItems.Where(x => x.ExpirationDate != null && x.ExpirationDate >= filtersInfo.StartExpirationDate);
                }

                //END
                if (filtersInfo.EndExpirationDate != null)
                {
                    allItems = allItems.Where(x => x.ExpirationDate != null && x.ExpirationDate <= filtersInfo.EndExpirationDate);
                }

                //PERISABLE
                if (filtersInfo.IsPerishable != null)
                {
                    allItems = allItems.Where(x => x.IsPerishable == filtersInfo.IsPerishable);
                }

                //CALORIES
                if (filtersInfo.CaloriesPerServing > 0)
                {
                    allItems = allItems.Where(x => x.CaloriesPerServing == filtersInfo.CaloriesPerServing);
                }

                //BARCODE
                if (!string.IsNullOrEmpty(filtersInfo.Barcode) && filtersInfo.Barcode.Length > 1)
                {
                    allItems = allItems.Where(x => x.Barcode.Length > 0 && x.Barcode.ToLower().Contains(filtersInfo.Barcode.ToLower()));
                }

                //si el checkbox esta activa
                if(filtersInfo.FilterByRole == true)
                {
                    //ROLE
                    if (filtersInfo.RoleId.HasValue && filtersInfo.RoleId > 0)
                    {
                        if(filtersInfo.RoleId == 3)
                        allItems = allItems.Where(x => x.RoleId == filtersInfo.RoleId.Value); //mostrar solo viewer
                        if(filtersInfo.RoleId == 2)
                        allItems = allItems.Where(x => x.RoleId == filtersInfo.RoleId.Value || x.RoleId == 3);// que el manager pueda ver manager y viewer que es rol 3
                    }
                } 
            }

            return allItems;
        }
    }
}