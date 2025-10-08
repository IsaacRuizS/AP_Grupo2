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

        public IEnumerable<Notifications> FilterByString(string value)
        {

            var valueToLower = value.ToLower();

            var filtered = GetNotifications(0).Where(x => x.message.ToLower().Contains(valueToLower) || x.id.ToString().ToLower().Contains(valueToLower)
            || x.user_id.ToString().ToLower().Contains(valueToLower) || x.created_at.ToString().ToLower().Contains(valueToLower)).ToList();

            return filtered;
        }
    }
}
