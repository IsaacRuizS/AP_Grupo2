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

    //TODO: Principio utilizados: S-O-I
    //S: (Single Responsibility Principle)
    // Por qué? Acá la clase se encarga únicamente de hacer el CRUD mediante las clases de SaveOrUpdate, Delete,
    // GetComponents. Además del filtro de datos de la tarea 2 parte 1
    // O:(Open/Closed Principle)
    // Por qué? Porque la clase está abierta a que se agreguen nuevas funcioanlidades de ser neceasrio, pero sus metodos
    //principales del CRUD no necesitan ser modificados
    // I: (Interface Segregation Principle)
    // Por qué? La clase trae a la interfaz IRepositorySupplier, que igual a las demás interfaces, traen metodos de RepositoryBase.

    public class SupplierBusiness
    {

        private readonly IRepositorySupplier _repositorySupplier;

        public SupplierBusiness()
        {
            _repositorySupplier = new RepositorySupplier();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Suppliers supplier)
        {

            if (supplier.SupplierID <= 0)
                _repositorySupplier.Add(supplier);
            else
                _repositorySupplier.Update(supplier);

            return true;
        }

        public bool Delete(int id)
        {
            _repositorySupplier.Delete(id);
            return true;
        }

        public IEnumerable<Suppliers> GetSuppliers(int id)
        {
            return id <= 0
                ? _repositorySupplier.GetAll()
                : new List<Suppliers>() { _repositorySupplier.GetById(id) };
        }

        public IEnumerable<Suppliers> FilterByString(string value)
        {
            var valueToLower = value.ToLower();

            var filtered = GetSuppliers(0).Where(x => x.SupplierName.ToLower().Contains(valueToLower) || x.ContactName.ToLower().Contains(valueToLower) || x.ContactTitle.ToString().ToLower().Contains(valueToLower)
            || x.Phone.ToString().ToLower().Contains(valueToLower) || x.Address.ToLower().Contains(valueToLower) || x.City.ToLower().Contains(valueToLower) || x.Country.ToLower().Contains(valueToLower) || x.LastModified.ToString().ToLower().Contains(valueToLower) || x.ModifiedBy.ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
