using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class RestaurantRegistrationRequestBusiness
    {

        private readonly IRepositoryRestaurantRegistrationRequest _repositoryRestaurantRegistrationRequest;

        public RestaurantRegistrationRequestBusiness()
        {
            _repositoryRestaurantRegistrationRequest = new RepositoryRestaurantRegistrationRequest();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(RestaurantRegistrationRequest restaurantRegistrationRequest)
        {

            if (restaurantRegistrationRequest.Id <= 0)
                _repositoryRestaurantRegistrationRequest.Add(restaurantRegistrationRequest);
            else
                _repositoryRestaurantRegistrationRequest.Update(restaurantRegistrationRequest);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryRestaurantRegistrationRequest.Delete(id);
            return true;
        }

        public IEnumerable<RestaurantRegistrationRequest> GetRestaurantRegistrationRequests(int id)
        {
            return id <= 0
                ? _repositoryRestaurantRegistrationRequest.GetAll()
                : new List<RestaurantRegistrationRequest>() { _repositoryRestaurantRegistrationRequest.GetById(id) };
        } 
    }
}
