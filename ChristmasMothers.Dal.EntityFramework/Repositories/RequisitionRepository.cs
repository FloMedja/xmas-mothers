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
    public class RequisitionRepository : Repository<Requisition, Guid>, IRequisitionRepository
    {
        public RequisitionRepository(EntityFrameworkDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Return not Deleted
        /// </summary>
        /// <returns></returns>
        //public override IQueryable<Requisition> Query()
        //{
        //    return base.Query().Where(x => x.Deleted == false);
        //}

        /// <summary>
        /// Gets a requisition by its id.
        /// </summary>
        /// <param name="includeChildren">Indicates if child collections (questions
        /// and sections descriptions) should be fetched and included in the result.</param>
        /// <returns>requisition entity or null if not found.</returns>
        public async Task<Requisition> GetByIdAsync(Guid id, bool includeChildren)
        {


            //TODO: FM -- if soft deleted is used we must Filters sub-collections deleted items (filtering in Include not possible yet)
            //TODO : that can be doing with global query filter

            var requisitionQuery = Query().Where(x => x.Id == id);

            if (includeChildren)
            {
                requisitionQuery.Include(x => x.SectionDescriptions)
                    .Include(x => x.Questions);


            }

            return await requisitionQuery.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets a requisition by its requisition id.
        /// </summary>
        /// <param name="includeChildren">Indicates if child collections (questions
        /// and sections descriptions) should be fetched and included in the result.</param>
        /// <returns>requisition entity or null if not found.</returns>
        public async Task<Requisition> GetByRequisitionIdAsync(string requisitionId, bool includeChildren)
        {

            //TODO: FM -- if soft deleted is used we must Filters sub-collections deleted items (filtering in Include not possible yet)
            //TODO : that can be doing with global query filter
            var requisitionQuery = Query().Where(x => x.RequisitionId == requisitionId);

            if (includeChildren)
            {
                requisitionQuery.Include(x => x.SectionDescriptions)
                    .Include(x => x.Questions);
            }

            return await requisitionQuery.SingleOrDefaultAsync();
        }

       

        //TODO : if soft deleted is not used or global query filter is used
        //TODO : for implement deletion remove this
        /// <summary>
        /// Gets all requisition.
        /// </summary>
        /// <returns>The collection of requisitions.</returns>
        public new async Task<IEnumerable<Requisition>> AllAsync()
        {
            return await Query().ToListAsync();
        }

        public Task<Tuple<IEnumerable<Requisition>, int>> SearchAsync(int skip, int take, string criteria)
        {
            throw new NotImplementedException();
        }

      
    }
}
