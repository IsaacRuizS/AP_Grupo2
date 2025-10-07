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
    public class CategoryBusiness
    {

        private readonly IRepositoryCategory _repositoryCategory;

        public CategoryBusiness()
        {
            _repositoryCategory = new RepositoryCategory();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Categories category)
        {

            if (category.CategoryID <= 0)
                _repositoryCategory.Add(category);
            else
                _repositoryCategory.Update(category);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryCategory.Delete(id);
            return true;
        }

        public IEnumerable<Categories> GetCategories(int id)
        {
            return id <= 0
                ? _repositoryCategory.GetAll()
                : new List<Categories>() { _repositoryCategory.GetById(id) };
        }
    }
}
