using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristmasMothers.Business.Interface
{
    //TODO : FM -- Add and implement advance search async method
    public interface IChildBusiness
    {
        /// <summary>
        /// Gets all childs.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entities to.</typeparam>
        /// <returns>The collection of child.</returns>
        Task<IEnumerable<TResponse>> AllAsync<TResponse>();
        Task<IEnumerable<TResponse>> AllWithChildrenAsync<TResponse>(bool includeAllRelativeEntitiesChildren = false);

        /// <summary>
        /// Gets a specific child.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entity to.</typeparam>
        /// <param name="childId">child ID (guid).</param>
        /// <param name="includeEntityChildren"></param>
        /// <param name="includeAllRelativeEntitiesChildren"></param>
        /// <returns>The specific child and optionnaly its child.</returns>
        Task<TResponse> GetByIdAsync<TResponse>(Guid childId, bool includeEntityChildren, bool includeAllRelativeEntitiesChildren = false);
        Task<TResponse> GetByTrackingIdAsync<TResponse>(string trackingId, bool includeEntityChildren);

        Task<TResponse> SearchAsync<TRequest,TResponse>(TRequest request, bool includeChildren = false ,bool includeAllRelativeEntitiesChildren = false);

        Task<TResponse> SearchByAgeAsync<TRequest, TResponse>(TRequest request, bool includeChildren = false, bool includeAllRelativeEntitiesChildren = false);

        /// <summary>
        /// Create a child.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to child)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> CreateAsync<TRequest, TResponse>(TRequest childPayload);

        /// <summary>
        /// Update if not existing a child.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to child)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> UpdateAsync<TRequest, TResponse>( TRequest childPayload);
        /// <summary>
        /// Deletes a child by setting it's Deleted property (soft delete).
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Guid childId);

        Task<int> GetNumberOfChild();
    }
}
