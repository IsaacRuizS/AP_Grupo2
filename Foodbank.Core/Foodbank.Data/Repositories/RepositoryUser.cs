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
    public interface IRepositoryUser : IRepositoryBase<Users>
    {
    }

    public class RepositoryUser : RepositoryBase<Users>, IRepositoryUser
    {
        public RepositoryUser() : base()
        {
        }
    }
}