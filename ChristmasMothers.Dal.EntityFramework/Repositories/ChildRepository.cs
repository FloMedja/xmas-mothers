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
    public class ChildRepository : Repository<Child, Guid>, IChildRepository
    {
        public ChildRepository(EntityFrameworkDbContext context) : base(context)
        {

        }

        public async Task<Child> GetByChildTrackingIdAsync(string childTrackingId, bool includeEntityChildren)
        {
            var childQuery = Query().Where(x => x.ChildTrackingId == childTrackingId).Include(x => x.Address);

            if (includeEntityChildren)
            {
                childQuery
                    .Include(x => x.ChildInterests)
                    .Include(x => x.GiftGiver)
                    .Include(x => x.Parent);
            }

            return await childQuery.SingleOrDefaultAsync();
        }
        public Task<Tuple<IEnumerable<Child>, int>> SearchAsync(int skip, int take, string criteria)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<Child>, int>> SearchByAgeAsync(int skip, int take, int minAge, int maxAge)
        {
            throw new NotImplementedException();
        }
    }
}
