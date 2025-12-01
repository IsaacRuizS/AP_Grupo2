using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CASO2.Data;
using CASO2.Data.Repository;

namespace CASO2.Data.Repositories
{
    public interface IRepositoryUserDetail : IRepositoryBase<UserDetail>
    {
        int DeleteByCountry(string country);
    }

    public class RepositoryUserDetail : RepositoryBase<UserDetail>, IRepositoryUserDetail
    {
        public RepositoryUserDetail() : base()
        {
        }

        public int DeleteByCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return 0;

            var normalized = country.Trim().ToLower();
            var toDelete = _set
                .Where(u => u.Country != null && u.Country.ToLower() == normalized)
                .ToList();

            if (!toDelete.Any())
                return 0;

            _set.RemoveRange(toDelete);
            Save();

            return toDelete.Count;
        }
    }
}
