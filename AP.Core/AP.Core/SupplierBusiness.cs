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
    }
}
