using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    //TODO : FM -- Add and implement advance search async method

    public interface IParentRepository : IRepository<Parent, Guid>
    {
        //Task<Parent> GetByIdAsync(Guid id, bool includeChildren);

        // TODO : remove or not whether deleted is soft or hard
        //Task<int> GetCountAsync();

        Task<Tuple<IEnumerable<Parent>, int>> AllAsync(int skip, int take);
        Task<Tuple<IEnumerable<Parent>, int>> AllWithChildrenAsync(int skip, int take, bool includeAllchildrenEntitiesRelatives = false);

        /// <summary>
        ///search parent with given criterias
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<Parent>, int>> SearchAsync(int skip, int take, string criteria);

    }
}