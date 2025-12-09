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
        public bool SaveOrUpdate(Schedule schedule)
        {

            if (schedule.ScheduleID <= 0)
                _repositorySchedule.Add(schedule);
            else
                _repositorySchedule.Update(schedule);

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

        public IEnumerable<Schedule> GetSchedulesByRestaurant(int restuarantId)
        {
            return _repositorySchedule.GetSchedulesByRestaurant(restuarantId);
        }
    }
}
