using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristmasMothers.Business.Interface
{
    //TODO : FM -- Add and implement advance search async method
    public interface IAddressBusiness
    {
        /// <summary>
        /// Gets a specific parent.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entity to.</typeparam>
        /// <param name="addressId">parent ID (guid).</param>
        /// <returns>The specific parent and optionnaly its child.</returns>
        Task<TResponse> GetByIdAsync<TResponse>(Guid addressId);
        /// <summary>
        /// Create a parent.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to parent)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> CreateOrUpdateAsync<TRequest, TResponse>(TRequest parentPayload);

        /// <summary>
        /// Deletes a parent by setting it's Deleted property (soft delete).
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Guid addressId);
    }
}
