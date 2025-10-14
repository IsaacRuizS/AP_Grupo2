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
    public class ComponentBusiness
    {

        private readonly IRepositoryComponent _repositoryComponent;

        public ComponentBusiness()
        {
            _repositoryComponent = new RepositoryComponent();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Components component)
        {

            if (component.ID <= 0)
                _repositoryComponent.Add(component);
            else
                _repositoryComponent.Update(component);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryComponent.Delete(id);
            return true;
        }

        public IEnumerable<Components> GetComponents(int id)
        {
            return id <= 0
                ? _repositoryComponent.GetAll()
                : new List<Components>() { _repositoryComponent.GetById(id) };
        }

        public IEnumerable<Components> FilterByString(string value)
        {

            var valueToLower = value.ToLower();

            var filtered = GetComponents(0).Where(x => x.name.ToLower().Contains(valueToLower) || x.content.ToLower().Contains(valueToLower) || x.ID.ToString().ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
