
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace RF.Data.Repository
{
    public interface IRepositoryMenuItem : IRepositoryBase<MenuItem>
    {
        IEnumerable<MenuItem> GetItemsByMenu(int menuId);
    }

    public class RepositoryMenuItem : RepositoryBase<MenuItem>, IRepositoryMenuItem
    {
        public RepositoryMenuItem() : base()
        {
        }

        public IEnumerable<MenuItem> GetItemsByMenu(int menuId)
        {

            return _set.Include("Menu").Where(x => x.MenuID == menuId).ToList();
        } 
    }
}