namespace AP.Data.Repository
{
    public interface IRepositoryCategory : IRepositoryBase<Categories>
    {
    }

    public class RepositoryCategory : RepositoryBase<Categories>, IRepositoryCategory
    {
        public RepositoryCategory() : base()
        {
        }
    }
}