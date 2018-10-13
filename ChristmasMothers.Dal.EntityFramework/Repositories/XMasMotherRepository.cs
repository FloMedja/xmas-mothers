using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;
using ChristmasMothers.Entities.Constants;
using ChristmasMothers.Extensions;

namespace ChristmasMothers.Dal.EntityFramework.Repositories
{
    public class XMasMotherRepository : Repository<XMasMother, Guid>, IXMasMotherRepository
    {
        public XMasMotherRepository(EntityFrameworkDbContext context) : base(context)
        {

        }

        public async Task<XMasMother> GetByXMasMotherTrackingIdAsync(string xMasMotherTrackingId, bool includeEntityXMasMotherren)
        {
            var xMasMotherQuery = Query().Where(x => x.XMasMotherTrackingId == xMasMotherTrackingId).Include(x => x.Address);

            if (includeEntityXMasMotherren)
            {
                xMasMotherQuery
                    .Include(x => x.MatchedChildren)
                    .Include(x => x.XMasMotherInterests)
                        .ThenInclude(interest => interest.Interest);
            }

            return await xMasMotherQuery.SingleOrDefaultAsync();
        }
        public Task<Tuple<IEnumerable<XMasMother>, int>> SearchAsync(int skip, int take, string criteria)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<IEnumerable<XMasMother>, int>> SearchMatchedXMaxMother(int skip, int take)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var xMasMothers = await AllWithChildrenAsync();
            var xMasMotherResult = xMasMothers.Where(x => x.MatchedChildren.Any()).Skip(skip).Take(take).ToArray();
            var count = xMasMotherResult.Length;
            return new Tuple<IEnumerable<XMasMother>, int>(xMasMotherResult, count);

        }

        public async  Task<Tuple<IEnumerable<XMasMother>, int>> SearchMatchedXMaxMother(int skip, int take,bool giftDeliver)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var xMasMothers = await AllWithChildrenAsync();
            var xMasMotherResult = xMasMothers.Where(x => x.MatchedChildren.Any() && x.GiftDeliver == giftDeliver)
                .Skip(skip).Take(take).ToArray();
            var count = xMasMotherResult.Length;
            return new Tuple<IEnumerable<XMasMother>, int>(xMasMotherResult, count);
        }

        public async Task<Tuple<IEnumerable<XMasMother>, int>> SearchUnMatchedXMaxMotherWithChildAndDelivery(int skip, int take)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var xMasMothers = await AllWithChildrenAsync();
            var xMasMotherResult = xMasMothers.Where(x => !x.MatchedChildren.Any()).Skip(skip).Take(take).ToArray();
            var count = xMasMotherResult.Length;
            return new Tuple<IEnumerable<XMasMother>, int>(xMasMotherResult, count);
        }
    }
}
