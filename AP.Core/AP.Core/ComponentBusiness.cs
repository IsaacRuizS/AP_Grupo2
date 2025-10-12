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
    // Por qué? La clase trae a la interfaz IRepositoryComponent, que igual a las demás interfaces, traen metodos de RepositoryBase.
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
