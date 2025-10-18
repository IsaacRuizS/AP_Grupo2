
namespace RF.Data.Repository
{
    public interface IRepositorySchedule : IRepositoryBase<Schedule>
    {
    }

    public class RepositorySchedule : RepositoryBase<Schedule>, IRepositorySchedule
    {
        public RepositorySchedule() : base()
        {
        }
    }
}