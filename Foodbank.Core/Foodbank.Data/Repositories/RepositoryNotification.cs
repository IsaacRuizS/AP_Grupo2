using AP.Data.Entities;
using AP.Data;
using AP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Data.Repository
{
    public interface IRepositoryNotification : IRepositoryBase<Notifications>
    {
    }

    public class RepositoryNotification : RepositoryBase<Notifications>, IRepositoryNotification
    {
        public RepositoryNotification() : base()
        {
        }
    }
}