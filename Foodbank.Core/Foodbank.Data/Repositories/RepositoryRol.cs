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
    public interface IRepositoryRol : IRepositoryBase<Roles>
    {
    }

    public class RepositoryRol : RepositoryBase<Roles>, IRepositoryRol
    {
        public RepositoryRol() : base()
        {
        }
    }
}