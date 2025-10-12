using AP.Data;
using AP.Data.Entities;
using AP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Core

    //TODO: Principio utilizados: S-O-I
    //S: (Single Responsibility Principle)
    // Por qué? Acá la clase se encarga únicamente de hacer el CRUD mediante las clases de SaveOrUpdate, Delete,
    // GetComponents. Además del filtro de datos de la tarea 2 parte 1
    // O:(Open/Closed Principle)
    // Por qué? Porque la clase está abierta a que se agreguen nuevas funcioanlidades de ser neceasrio (por medio de la interfaz para mantener el principio I), pero sus metodos
    //principales del CRUD no necesitan ser modificados
    // I: (Interface Segregation Principle)
    // Por qué? La clase trae a la interfaz IRepositoryInventory, que igual a las demás interfaces, traen metodos de RepositoryBase.
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
