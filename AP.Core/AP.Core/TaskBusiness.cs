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
