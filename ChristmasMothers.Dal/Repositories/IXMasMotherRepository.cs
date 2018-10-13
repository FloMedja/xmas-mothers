using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    public interface IXMasMotherRepository : IRepository<XMasMother, Guid>
    {
        Task<XMasMother> GetByXMasMotherTrackingIdAsync(string xMasMotherTrackingId, bool includeChildren);


        /// <summary>
        ///search parent with given criterias
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchAsync(int skip, int take, string criteria);

        Task<Tuple<IEnumerable<XMasMother>, int>> SearchMatchedXMaxMotherWith(int skip, int take);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchMatchedXMaxMotherWith(int skip, int take, bool giftDeliver);
        Task<Tuple<IEnumerable<XMasMother>, int>> SearchUnMatchedXMaxMotherWithChildAndDelivery(int skip, int take);
    }
}
