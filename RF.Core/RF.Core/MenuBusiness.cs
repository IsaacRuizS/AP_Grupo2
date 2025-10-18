using RF.Data;
using RF.Data.Repository;
using System.Collections.Generic;

namespace RF.Core
{
    public class MenuBusiness
    {

        private readonly IRepositoryMenu _repositoryMenu;

        public MenuBusiness()
        {
            _repositoryMenu = new RepositoryMenu();
        }

        //Upsert (Update / Insert)
        public bool SaveOrUpdate(Menu user)
        {

            if (user.MenuID <= 0)
                _repositoryMenu.Add(user);
            else
                _repositoryMenu.Update(user);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryMenu.Delete(id);
            return true;
        }

        public IEnumerable<Menu> GetMenus(int id)
        {
            return id <= 0
                ? _repositoryMenu.GetAll()
                : new List<Menu>() { _repositoryMenu.GetById(id) };
        } 
    }
}
