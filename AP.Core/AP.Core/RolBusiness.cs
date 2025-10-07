using AP.Data;
using AP.Data.Entities;
using AP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Core
{
    public class RolBusiness
    {

        private readonly IRepositoryRol _repositoryRol;

        public RolBusiness()
        {
            _repositoryRol = new RepositoryRol();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Roles rol)
        {

            if (rol.RoleID <= 0)
                _repositoryRol.Add(rol);
            else
                _repositoryRol.Update(rol);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryRol.Delete(id);
            return true;
        }

        public IEnumerable<Roles> GetRols(int id)
        {
            return id <= 0
                ? _repositoryRol.GetAll()
                : new List<Roles>() { _repositoryRol.GetById(id) };
        }
    }
}
