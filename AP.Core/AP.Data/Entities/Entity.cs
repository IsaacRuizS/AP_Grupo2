using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Data.Entities
{
    public class Entity : IEntity
    {
        public string Description { get; set; }
    }

    public interface IEntity
    {
        string Description { get; set; }
    }
}
