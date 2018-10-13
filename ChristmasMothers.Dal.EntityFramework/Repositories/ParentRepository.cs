using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework.Repositories
{
    public class ParentRepository : Repository<Parent, Guid>, IParentRepository
    {
        public ParentRepository(EntityFrameworkDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Return not Deleted
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Gets a parent by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeChildren">Indicates if children collection should be included.</param>
        /// <returns>parent entity or null if not found.</returns>
        //public async Task<Parent> GetByIdAsync(Guid id, bool includeChildren)
        //{


        //    //TODO: FM -- if soft deleted is used we must Filters sub-collections deleted items (filtering in Include not possible yet)
        //    //TODO : that can be doing with global query filter

        //    var parentQuery = Query().Where(x => x.Id == id).Include(x => x.Address);

        //    if (includeChildren)
        //    {
        //        parentQuery
        //            .Include(x => x.Children);

        //    }

        //    return await parentQuery.SingleOrDefaultAsync();
        //}      

        public Task<Tuple<IEnumerable<Parent>, int>> SearchAsync(int skip, int take, string criteria)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<IEnumerable<Parent>, int>> AllAsync(int skip, int take)
        {
            take = take <= 0 || take >= 1000 ? 100 : take;
            skip = skip < 0 ? 0 : skip;
            var parentResult =  await Query().OrderBy(x => x.FamilyName).ThenBy(x => x.GivenName).Skip(skip).Take(take).ToArrayAsync();
            var count = parentResult.Length;
            return new Tuple<IEnumerable<Parent>, int>(parentResult, count);
        }

        public async Task<Tuple<IEnumerable<Parent>, int>> AllWithChildrenAsync(int skip, int take, bool includeAllchildrenEntitiesRelatives = false)
        {
            take = take <= 0 || take >= 1000 ? 100 : take;
            skip = skip < 0 ? 0 : skip;
            var allParents = await AllWithChildrenAsync(includeAllchildrenEntitiesRelatives);
            var parentResult = allParents.OrderBy(x => x.FamilyName).ThenBy(x => x.GivenName).Skip(skip).Take(take).ToArray();
            var count = parentResult.Length;
            return new Tuple<IEnumerable<Parent>, int>(parentResult, count);

        }
    }
}
