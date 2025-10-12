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
    // Por qué? La clase trae a la interfaz IRepositoryTask, que igual a las demás interfaces, traen metodos de RepositoryBase.

    public class TaskBusiness
    {

        private readonly IRepositoryTask _repositoryTask;

        public TaskBusiness()
        {
            _repositoryTask = new RepositoryTask();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Tasks Task)
        {

            if (Task.Id <= 0)
                _repositoryTask.Add(Task);
            else
                _repositoryTask.Update(Task);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryTask.Delete(id);
            return true;
        }

        public IEnumerable<Tasks> GetTasks(int id)
        {
            return id <= 0
                ? _repositoryTask.GetAll()
                : new List<Tasks>() { _repositoryTask.GetById(id) };
        }

        public IEnumerable<Tasks> FilterByString(string value)
        {
            var valueToLower = value.ToLower();

            var filtered = GetTasks(0).Where(x => x.Name.ToLower().Contains(valueToLower) || x.Description.ToLower().Contains(valueToLower)
            || x.Status.ToString().ToLower().Contains(valueToLower) || x.ModifiedBy.ToLower().Contains(valueToLower)
            || x.LastModified.ToString().ToLower().Contains(valueToLower) || x.DueDate.ToString().ToLower().Contains(valueToLower)
            || x.CreatedAt.ToString().ToLower().Contains(valueToLower)
            ).ToList();

            return filtered;
        }
    }
}
