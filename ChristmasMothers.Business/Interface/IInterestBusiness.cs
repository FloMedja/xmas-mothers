using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasMothers.Business.Interface
{
    public interface IInterestBusiness
    {
        /// <summary>
        /// Gets a specific interest.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entity to.</typeparam>
        /// <param name="interestId">interest ID (guid).</param>
        /// <returns>The specific interest and optionnaly its child.</returns>
        Task<TResponse> GetByIdAsync<TResponse>(Guid interestId);
        /// <summary>
        /// Create a interest.
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to interest)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        Task<TResponse> CreateOrUpdateByLabelAsync<TRequest, TResponse>(TRequest interestPayload);

        /// <summary>
        /// Deletes a interest by setting it's Deleted property (soft delete).
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Guid interestId);
    }
}
