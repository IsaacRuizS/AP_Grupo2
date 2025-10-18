using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class ScheduleBusiness
    {

        private readonly IRepositorySchedule _repositorySchedule;

        public ScheduleBusiness()
        {
            _repositorySchedule = new RepositorySchedule();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Schedule user)
        {

            if (user.ScheduleID <= 0)
                _repositorySchedule.Add(user);
            else
                _repositorySchedule.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositorySchedule.Delete(id);
            return true;
        }

        public IEnumerable<Schedule> GetSchedules(int id)
        {
            return id <= 0
                ? _repositorySchedule.GetAll()
                : new List<Schedule>() { _repositorySchedule.GetById(id) };
        } 
    }
}
