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

    //TODO: Principio utilizados: S-O-I
    //S: (Single Responsibility Principle)
    // Por qué? Acá la clase se encarga únicamente de hacer el CRUD mediante las clases de SaveOrUpdate, Delete,
    // GetComponents. Además del filtro de datos de la tarea 2 parte 1
    // O:(Open/Closed Principle)
    // Por qué? Porque la clase está abierta a que se agreguen nuevas funcioanlidades de ser neceasrio, pero sus metodos
    //principales del CRUD no necesitan ser modificados
    // I: (Interface Segregation Principle)
    // Por qué? La clase trae a la interfaz IRepositoryRol, que igual a las demás interfaces, traen metodos de RepositoryBase.

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

        public IEnumerable<Roles> FilterByString(string value)
        {
            var valueToLower = value.ToLower();

            var filtered = GetRols(0).Where(x => x.RoleName.ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
