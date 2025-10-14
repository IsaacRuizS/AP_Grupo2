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
    public interface IRepositoryTask : IRepositoryBase<Tasks>
    {
    }

    public class RepositoryTask : RepositoryBase<Tasks>, IRepositoryTask
    {
        public RepositoryTask() : base()
        {
        }
    }
}