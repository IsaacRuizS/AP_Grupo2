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
    public interface IRepositoryProduct : IRepositoryBase<Products>
    {
    }

    public class RepositoryProduct : RepositoryBase<Products>, IRepositoryProduct
    {
        public RepositoryProduct() : base()
        {
        }
    }
}