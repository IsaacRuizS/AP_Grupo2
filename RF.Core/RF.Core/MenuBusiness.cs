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
        public bool SaveOrUpdate(Menu menu)
        {

            if (menu.MenuID <= 0)
                _repositoryMenu.Add(menu);
            else
                _repositoryMenu.Update(menu);

            return true;
        }

        public bool Delete(int id)
        {
            return _repositoryMenu.DeleteMenuAndFk(id);
        }

        public IEnumerable<Menu> GetMenus(int id)
        {
            return id <= 0
                ? _repositoryMenu.GetAll()
                : new List<Menu>() { _repositoryMenu.GetById(id) };
        }

        public IEnumerable<Menu> GetMenusByRestaurant(int restuarantId)
        {
            return _repositoryMenu.GetMenusByRestaurant(restuarantId);
        }
    }
}
