using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class RestaurantBusiness
    {

        private readonly IRepositoryRestaurant _repositoryRestaurant;

        public RestaurantBusiness()
        {
            _repositoryRestaurant = new RepositoryRestaurant();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Restaurant user)
        {

            if (user.RestaurantID <= 0)
                _repositoryRestaurant.Add(user);
            else
                _repositoryRestaurant.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryRestaurant.Delete(id);
            return true;
        }

        public IEnumerable<Restaurant> GetRestaurants(int id)
        {
            return id <= 0
                ? _repositoryRestaurant.GetAll()
                : new List<Restaurant>() { _repositoryRestaurant.GetById(id) };
        } 
    }
}
