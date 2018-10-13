using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChristmasMothers.Business.Interface;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;
using ChristmasMothers.Entities.@interface;
using ChristmasMothers.Exceptions;
using ChristmasMothers.Extensions;

namespace ChristmasMothers.Business
{
    public class ParentBusiness : IParentBusiness
    {
        private readonly IMapper _mapper;
        private readonly IParentRepository _parentRepository;
        private readonly IAddressBusiness _addressBusiness;
        private readonly Type _objectType;

        public ParentBusiness(IMapper mapper, IParentRepository parentRepository, IAddressRepository addressRepository, IAddressBusiness addressBusiness)
        {
            _mapper = mapper;
            _parentRepository = parentRepository;
            _addressBusiness = addressBusiness;
            _objectType = typeof(Parent);
        }

        #region GET
        /// <summary>
        /// Get AllAsync
        /// </summary>
        /// <typeparam name="TResponse">The type to map the resulting entity to</typeparam>
        /// <returns>The Tenant entities (mapped to <typeparamref name="TResponse"/>).</returns>
        public async Task<IEnumerable<TResponse>> AllAsync<TResponse>()
        {
            var results = await _parentRepository.AllAsync();
            return _mapper.Map<IEnumerable<TResponse>>(results);
        }
        public async Task<IEnumerable<TResponse>> AllWithChildrenAsync<TResponse>(bool includeAllRelativeEntitiesChildren = false)
        {
            var results = await _parentRepository.AllWithChildrenAsync(includeAllRelativeEntitiesChildren);
            return _mapper.Map<IEnumerable<TResponse>>(results);
        }

        /// <summary>
        /// Get By ID Async
        /// </summary>
        /// <exception cref="NotFoundException">No matching parent could be found.</exception>
        /// <typeparam name="TResponse">The type to map the resulting entity to</typeparam>
        /// <param name="parentId">parent ID.</param>
        /// <param name="includeChildren">Include children collections: true|False</param>
        /// <param name="includeAllRelativeEntitiesChildren"></param>
        /// <returns>The parent entity (mapped to <typeparamref name="TResponse"/>).</returns>
        public async Task<TResponse> GetByIdAsync<TResponse>(Guid parentId, bool includeChildren, bool includeAllRelativeEntitiesChildren = false)
        {
            Parent parentEntity;
            if (includeChildren)
            {
                parentEntity = await _parentRepository.GetByIdWithChildrenAsync(parentId,includeAllRelativeEntitiesChildren);
            }
            else
            {
                parentEntity = await _parentRepository.GetByIdAsync(parentId);
            }
            if (parentEntity.IsNull())
            {
                throw new NotFoundException(parentId, _objectType);
            }
            return _mapper.Map<TResponse>(parentEntity);
        }

        /// <summary>
        /// Get parent by parent Id
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="includeChildren">Include children collections: true|False</param>
        /// <param name="includeAllRelativeEntitiesChildren"></param>
        /// <returns>The parent entity (mapped to <typeparamref name="TResponse"/>)</returns>
        public async Task<TResponse> SearchAsync<TRequest, TResponse>(TRequest request, bool includeChildren = false, bool includeAllRelativeEntitiesChildren = false)
        {
            var search = _mapper.Map<ISearchChildByAgeRequest>(request);
           
            if (includeChildren)
            {
                var parentsWithchildren = await _parentRepository.AllWithChildrenAsync(search.Skip, search.Take, includeAllRelativeEntitiesChildren);
                return _mapper.Map<TResponse>(parentsWithchildren);
            }

            var parentsWithoutchildren = await _parentRepository.AllAsync(search.Skip, search.Take);
            return _mapper.Map<TResponse>(parentsWithoutchildren);

        }

        public async Task<int> GetNumberOfParent()
        {
            var count = await _parentRepository.GetCountAsync();
            return count;
        }

        #endregion

        #region CREATE
        /// <summary>
        /// Add or update parent Async
        /// </summary>
        /// <typeparam name="TRequest">Payload type (must be map-able to Tenant)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        public async Task<TResponse> CreateAsync<TRequest, TResponse>(TRequest parentPayload)
        {
            // Map payload to entity.
            var payloadEntity = _mapper.Map<Parent>(parentPayload);
            ValidateEntity(payloadEntity);

            await _addressBusiness.CreateOrUpdateAsync<Address,Address>(payloadEntity.Address);
            await _parentRepository.AddAsync(payloadEntity);
            await _parentRepository.SaveChangesAsync();
            return _mapper.Map<TResponse>(payloadEntity);
        }
        #endregion

        #region UPDATE

        /// <summary>
        /// Update parent Async
        /// </summary>
        /// <exception cref="NotFoundException">No matching parent could be found.</exception>
        /// <exception>Tenant with same name already exist.</exception>
        /// <typeparam name="TRequest">Payload type (must be map-able to Parent)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        public async Task<TResponse> UpdateAsync<TRequest, TResponse>(TRequest parentPayload)
        {
            // Map payload to entity.
            var updateEntity = _mapper.Map<Parent>(parentPayload);
            ValidateEntity(updateEntity);

            var oldEntity = await _parentRepository.GetByIdAsync(updateEntity.Id);
            if (oldEntity.IsNull())
            {
                throw new NotFoundException(updateEntity.Id, typeof(Parent));
            }

            await _parentRepository.UpdateEntryAsync(updateEntity);
            await _parentRepository.SaveChangesAsync();

            return _mapper.Map<TResponse>(updateEntity);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete parent Async (soft-delete)
        /// </summary>
        /// <exception cref="NotFoundException">No matching parent could be found.</exception>
        /// <param name="parentId">Tenant ID.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid parentId)
        {
            var parentEntity = await _parentRepository.GetByIdAsync(parentId);

            if (parentEntity.IsNull())
            {
                throw new NotFoundException(parentId, _objectType);
            }
            await _addressBusiness.DeleteAsync(parentEntity.Address.Id);
            await _parentRepository.RemoveAsync(parentEntity);
            await _parentRepository.SaveChangesAsync();
        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Validates en entity content prior to creation or update.
        /// </summary>
        /// <param name="entity">Tenant entity to validate.</param>
        private void ValidateEntity(Parent entity)
        {
            if (entity.Id == Guid.Empty)
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.Id), typeof(Parent));
            }
        }


        #endregion
    }
}
