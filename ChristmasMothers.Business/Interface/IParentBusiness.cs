using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristmasMothers.Business.Interface
{
    //TODO : FM -- Add and implement advance search async method
    public interface IParentBusiness
    {
        /// <summary>
        /// Gets all parents.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entities to.</typeparam>
        /// <returns>The collection of parent.</returns>
        Task<IEnumerable<TResponse>> AllAsync<TResponse>();

        /// <summary>
        /// Gets a specific parent.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entity to.</typeparam>
        /// <param name="parentId">parent ID (guid).</param>
        /// <param name="includeChildren">includeChildren (bool).</param>
        /// <returns>The specific parent and optionnaly its child.</returns>
        Task<TResponse> GetByIdAsync<TResponse>(Guid parentId, bool includeChildren);

        Task<TResponse> GetByRequisitionIdAsync<TResponse>(string parentId, bool includeChildren);

        Task<TResponse> GetAllAsync<TResponse>();

        Task<int> GetNumberOfRequisition();
        Task<TResponse> SearchAsync<TRequest,TResponse>(TRequest request);

        /// <summary>
        /// Create a parent.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to parent)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> CreateAsync<TRequest, TResponse>(TRequest parentPayload);

        /// <summary>
        /// Update if not existing a parent.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to parent)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> UpdateAsync<TRequest, TResponse>( TRequest parentPayload);

        /// <summary>
        /// Deletes a parent by setting it's Deleted property (soft delete).
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Guid parentId);
    }
}
