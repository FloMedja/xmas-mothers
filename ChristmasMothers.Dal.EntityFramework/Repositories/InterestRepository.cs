using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;
using ChristmasMothers.Entities.Constants;

namespace ChristmasMothers.Dal.EntityFramework.Repositories
{
    public class InterestRepository : Repository<Interest, Guid>, IInterestRepository
    {
        public InterestRepository(EntityFrameworkDbContext context) : base(context)
        {

        }

        public async Task<Interest> GetByLabelAsync(string label, bool includeEntityChildren)
        {
            var interestQuery = Query().Where(x => x.Label == label);

            if (includeEntityChildren)
            {
                interestQuery
                    .Include(x => x.InterestedChildren).ThenInclude(y => y.Child)
                    .Include(x => x.InterestedXMasMothers).ThenInclude(y => y.XMasMother);



            }

            return await interestQuery.SingleOrDefaultAsync();
        }

        public async Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, Guid interestId)
        {
            take = take <= 0 || take >= 1000 ? 100 : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Id == interestId)
                .Include(x => x.InterestedChildren).ThenInclude(y => y.Child);

            var interest = (await interestQuery.SingleOrDefaultAsync()).InterestedChildren;
            var childList = new Collection<Child>();
            foreach (var interestChild in interest)
            {
                childList.Add(interestChild.Child);
            }

            var childResult = childList.Skip(skip).Take(take).ToList();
            var count = childResult.Count;
            return new Tuple<IEnumerable<Child>, int>(childResult, count);

        }

        public async Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, string label)
        {
            take = take <= 0 || take >= 1000 ? 100 : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Label == label)
                .Include(x => x.InterestedChildren).ThenInclude(y => y.Child);

            var interest = (await interestQuery.SingleOrDefaultAsync()).InterestedChildren;
            var childList = new Collection<Child>();
            foreach (var interestChild in interest)
            {
                childList.Add(interestChild.Child);
            }

            var childResult = childList.Skip(skip).Take(take).ToList();
            var count = childResult.Count;
            return new Tuple<IEnumerable<Child>, int>(childResult, count);
        }

        public async Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, Guid interestId, bool match)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Id == interestId)
                .Include(x => x.InterestedChildren).ThenInclude(y => y.Child);

            var interestedChildren = (await interestQuery.SingleOrDefaultAsync()).InterestedChildren;
            var childList = new Collection<Child>();
            foreach (var interestChild in interestedChildren)
            {
                if (interestChild.Child.IsMatched == match)
                {
                    childList.Add(interestChild.Child);
                }
            }

            var childResult = childList.Skip(skip).Take(take).ToList();
            var count = childResult.Count;
            return new Tuple<IEnumerable<Child>, int>(childResult, count);
        }

        public async Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, string label, bool match)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Label == label)
                .Include(x => x.InterestedChildren).ThenInclude(y => y.Child);

            var interestedChildren = (await interestQuery.SingleOrDefaultAsync()).InterestedChildren;
            var childList = new Collection<Child>();
            foreach (var interestChild in interestedChildren)
            {
                if (interestChild.Child.IsMatched == match)
                {
                    childList.Add(interestChild.Child);
                }
            }

            var childResult = childList.Skip(skip).Take(take).ToList();
            var count = childResult.Count;
            return new Tuple<IEnumerable<Child>, int>(childResult, count);
        }

        public async Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, Guid interestId)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Id == interestId)
                .Include(x => x.InterestedXMasMothers).ThenInclude(y => y.XMasMother)
                .ThenInclude(xmax => xmax.MatchedChildren);

            var interestedXMasMothers = (await interestQuery.SingleOrDefaultAsync()).InterestedXMasMothers;
            var xMasMotherList = new Collection<XMasMother>();
            foreach (var interestXMasMother in interestedXMasMothers)
            {
                xMasMotherList.Add(interestXMasMother.XMasMother);
            }

            var xMasMotherResult = xMasMotherList.Skip(skip).Take(take).ToList();
            var count = xMasMotherResult.Count;
            return new Tuple<IEnumerable<XMasMother>, int>(xMasMotherResult, count);
        }

        public async Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, string label)
        {
            take = take <= 0 || take >= 1000 ? Constants.TakeDefaultValue : take;
            skip = skip < 0 ? 0 : skip;
            var interestQuery = Query().Where(x => x.Label == label)
                .Include(x => x.InterestedXMasMothers).ThenInclude(y => y.XMasMother)
                .ThenInclude(xmax => xmax.MatchedChildren);

            var interestedXMasMothers = (await interestQuery.SingleOrDefaultAsync()).InterestedXMasMothers;
            var xMasMotherList = new Collection<XMasMother>();
            foreach (var interestXMasMother in interestedXMasMothers)
            {
                xMasMotherList.Add(interestXMasMother.XMasMother);
            }

            var xMasMotherResult = xMasMotherList.Skip(skip).Take(take).ToList();
            var count = xMasMotherResult.Count;
            return new Tuple<IEnumerable<XMasMother>, int>(xMasMotherResult, count);
        }

        public Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, Guid interestId, bool match)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, string label, bool match)
        {
            throw new NotImplementedException();
        }
    }
}
