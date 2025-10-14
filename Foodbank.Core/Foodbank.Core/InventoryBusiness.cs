using AP.Data;
using AP.Data.Entities;
using AP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Core
{
    public class InventoryBusiness
    {

        private readonly IRepositoryInventory _repositoryInventory;

        public InventoryBusiness()
        {
            _repositoryInventory = new RepositoryInventory();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Inventory inventory)
        {

            if (inventory.InventoryID <= 0)
                _repositoryInventory.Add(inventory);
            else
                _repositoryInventory.Update(inventory);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryInventory.Delete(id);
            return true;
        }

        public IEnumerable<Inventory> GetInventory(int id)
        {
            return id <= 0
                ? _repositoryInventory.GetAll()
                : new List<Inventory>() { _repositoryInventory.GetById(id) };
        }

        public IEnumerable<Inventory> FilterByString(string value)
        {

            var valueToLower = value.ToLower();

            var filtered = GetInventory(0).Where(x => x.InventoryID.ToString().ToLower().Contains(valueToLower) || x.UnitPrice.ToString().ToLower().Contains(valueToLower)
            || x.UnitsInStock.ToString().ToLower().Contains(valueToLower) || x.LastUpdated.ToString().ToLower().Contains(valueToLower) || x.DateAdded.ToString().ToLower().Contains(valueToLower)
            || x.ModifiedBy.ToString().ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
