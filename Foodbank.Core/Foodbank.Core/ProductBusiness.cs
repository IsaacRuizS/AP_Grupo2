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
    public class ProductBusiness
    {

        private readonly IRepositoryProduct _repositoryProduct;

        public ProductBusiness()
        {
            _repositoryProduct = new RepositoryProduct();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Products product)
        {

            if (product.ProductID <= 0)
                _repositoryProduct.Add(product);
            else
                _repositoryProduct.Update(product);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryProduct.Delete(id);
            return true;
        }

        public IEnumerable<Products> GetProducts(int id)
        {
            return id <= 0
                ? _repositoryProduct.GetAll()
                : new List<Products>() { _repositoryProduct.GetById(id) };
        }

        public IEnumerable<Products> FilterByString(string value)
        {
            // LINQ Objects - entities SQL

            //var filtered = from x in GetProducts(0) where x.ProductName.Contains(value) select x;

            var valueToLower = value.ToLower();

            var filtered = GetProducts(0).Where(x => x.ProductName.ToLower().Contains(valueToLower) || x.Description.ToLower().Contains(valueToLower) || x.Rating.ToString().ToLower().Contains(valueToLower) 
            || x.LastModified.ToString().ToLower().Contains(valueToLower) || x.ModifiedBy.ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
