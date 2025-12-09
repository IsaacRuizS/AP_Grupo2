
using System.Collections.Generic;
using System.Linq;

namespace RF.Data.Repository
{
    public interface IRepositorySchedule : IRepositoryBase<Schedule>
    {
        IEnumerable<Schedule> GetSchedulesByRestaurant(int restaurantId);

    }

    public class RepositorySchedule : RepositoryBase<Schedule>, IRepositorySchedule
    {
        public RepositorySchedule() : base()
        {
        }

        public IEnumerable<Schedule> GetSchedulesByRestaurant(int restaurantId)
        {

            return _set.Include("Restaurant").Where(x => x.RestaurantID == restaurantId).ToList();
        }
    }
}
