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
    // Por qué? La clase trae a la interfaz IRepositoryNotification, que igual a las demás interfaces, traen metodos de RepositoryBase.
    public class NotificationBusiness
    {

        private readonly IRepositoryNotification _repositoryNotification;

        public NotificationBusiness()
        {
            _repositoryNotification = new RepositoryNotification();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Notifications notification)
        {

            if (notification.id <= 0)
                _repositoryNotification.Add(notification);
            else
                _repositoryNotification.Update(notification);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryNotification.Delete(id);
            return true;
        }

        public IEnumerable<Notifications> GetNotifications(int id)
        {
            return id <= 0
                ? _repositoryNotification.GetAll()
                : new List<Notifications>() { _repositoryNotification.GetById(id) };
        }

        public IEnumerable<Notifications> FilterByString(string value)
        {

            var valueToLower = value.ToLower();

            var filtered = GetNotifications(0).Where(x => x.message.ToLower().Contains(valueToLower) || x.id.ToString().ToLower().Contains(valueToLower)
            || x.user_id.ToString().ToLower().Contains(valueToLower) || x.created_at.ToString().ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
