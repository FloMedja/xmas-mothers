using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    public interface IInterestRepository : IRepository<Interest, Guid>
    {
        Task<Interest> GetByLabelAsync(string label, bool includeEntityChildren);
        Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, Guid interestId);
        Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, string label);
        Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, Guid interestId, bool match);
        Task<Tuple<IEnumerable<Child>, int>> SearchChildWithInterestAsync(int skip, int take, string label, bool match);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, Guid interestId);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, string label);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, Guid interestId, bool match);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchXMasMotherWithInterestAsync(int skip, int take, string label, bool match);
    }
}
