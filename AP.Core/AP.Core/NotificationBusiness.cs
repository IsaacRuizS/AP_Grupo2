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
    }
}
