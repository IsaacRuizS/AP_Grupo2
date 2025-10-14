using AP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Data.Repository
{
    //TODO: Principios utilizados: S - O - L - I
    // S: (Single Responsibility Principle)
    // Por qué? Cada clase de repositorio (como RepositoryUser, RepositoryTask, etc.) tiene una sola responsabilidad: 
    // gestionar las operaciones CRUD de su entidad correspondiente (Users, Tasks, etc.). 
    // No mezclan otras lógicas de negocio, cumpliendo con la responsabilidad única.
    // O: (Open/Closed Principle)
    // Por qué? El patrón genérico RepositoryBase<T> permite extender el comportamiento 
    // creando nuevas clases específicas (RepositoryUser, RepositoryProduct, etc.) 
    // sin modificar el código existente del repositorio base. 
    // Está abierta a extensión pero cerrada a modificación.
    // L: (Liskov Substitution Principle)
    // Por qué? Las clases derivadas (por ejemplo, RepositoryUser) pueden sustituir al tipo base (RepositoryBase<T>) 
    // sin alterar el correcto funcionamiento del programa, ya que heredan y mantienen el mismo contrato 
    // de comportamiento definido por la interfaz IRepositoryBase<T>.
    // I: (Interface Segregation Principle)
    // Por qué? Cada interfaz (IRepositoryUser, IRepositoryTask, IRepositoryProduct, etc.) 
    // está especializada en una entidad concreta y hereda solo los métodos necesarios del repositorio base, 
    // evitando que las clases implementen métodos que no necesitan. 
    // Esto divide las interfaces grandes en interfaces más pequeñas y enfocadas.
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ProductDBEntities _context;
        protected readonly DbSet<T> _set;

        public RepositoryBase()
        {
            _context = new ProductDBEntities();
            _set = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.ToList();
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public void Add(T entity)
        {
            _set.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _set.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            T entityToDelete = _set.Find(id);
            if (entityToDelete != null)
            {
                _set.Remove(entityToDelete);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
