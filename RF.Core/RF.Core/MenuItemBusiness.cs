using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class MenuItemBusiness
    {

        private readonly IRepositoryMenuItem _repositoryMenuItem;

        public MenuItemBusiness()
        {
            _repositoryMenuItem = new RepositoryMenuItem();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(MenuItem user)
        {

            if (user.ItemID <= 0)
                _repositoryMenuItem.Add(user);
            else
                _repositoryMenuItem.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryMenuItem.Delete(id);
            return true;
        }

        public IEnumerable<MenuItem> GetMenuItems(int id)
        {
            return id <= 0
                ? _repositoryMenuItem.GetAll()
                : new List<MenuItem>() { _repositoryMenuItem.GetById(id) };
        } 
    }
}
