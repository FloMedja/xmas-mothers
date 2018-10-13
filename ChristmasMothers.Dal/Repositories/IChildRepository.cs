using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    public interface IChildRepository : IRepository<Child, Guid>
    {
        Task<Child> GetByChildTrackingIdAsync(string childTrackingId, bool includeEntityChildren);
        /// <summary>
        ///search parent with given criterias
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<Child>, int>> SearchAsync(int skip, int take, string criteria);
        Task<Tuple<IEnumerable<Child>, int>> SearchByAgeAsync(int skip, int take, int minAge , int maxAge);
        Task<Tuple<IEnumerable<Child>, int>> SearchByMatchAsync(int skip, int take, bool match);

    }
}
