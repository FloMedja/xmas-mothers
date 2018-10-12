using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    //TODO : FM -- Add and implement advance search async method

    public interface IParentRepository : IRepository<Parent, Guid>
    {
        Task<Parent> GetByIdAsync(Guid id, bool includeChildren);

        Task<Parent> GetByParentIdAsync(string parentId, bool includeChildren);

        /// <summary>
        /// return all parent async
        /// </summary>
        /// <returns></returns>
        new Task<IEnumerable<Parent>> AllAsync();

        // TODO : remove or not whether deleted is soft or hard
        //Task<int> GetCountAsync();

        /// <summary>
        ///search parent with given criterias
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<Parent>, int>> SearchAsync(int skip, int take, string criteria);

        //TODO : add function to verify if parent exist ( adapt to doft or hard deleted solution choose)
    }
}