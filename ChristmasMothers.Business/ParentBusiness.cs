using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChristmasMothers.Business.Interface;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;
using ChristmasMothers.Exceptions;

namespace ChristmasMothers.Business
{
    public class ParentBusiness : IParentBusiness
    {
        private readonly IMapper _mapper;
        private readonly IParentRepository _parentRepository;

        public ParentBusiness(IMapper mapper, IParentRepository parentRepository)
        {
            _mapper = mapper;
            _parentRepository = parentRepository;
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

        /// <summary>
        /// Get By ID Async
        /// </summary>
        /// <exception cref="NotFoundException">No matching parent could be found.</exception>
        /// <typeparam name="TResponse">The type to map the resulting entity to</typeparam>
        /// <param name="parentId">parent ID.</param>
        /// <param name="includeChildren">Include children collections: true|False</param>
        /// <returns>The parent entity (mapped to <typeparamref name="TResponse"/>).</returns>
        public async Task<TResponse> GetByIdAsync<TResponse>(Guid parentId, bool includeChildren)
        {
            var parentEntity = await _parentRepository.GetByIdAsync(parentId, includeChildren);
            if (parentEntity.IsNull())
            {
                throw new NotFoundException(parentId, typeof(Parent));
            }
            return _mapper.Map<TResponse>(parentEntity);
        }

        /// <summary>
        /// Get parent by parent Id
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="parentId">parent ID.</param>
        /// <param name="includeChildren">Include children collections: true|False</param>
        /// <returns>The parent entity (mapped to <typeparamref name="TResponse"/>)</returns>
        public async Task<TResponse> GetByParentIdAsync<TResponse>(string parentId, bool includeChildren)
        {
            var parentEntity = await _parentRepository.GetByParentIdAsync(parentId, includeChildren);
            if (parentEntity.IsNull())
            {
                throw new NotFoundException(parentId, typeof(Parent));
            }
            return _mapper.Map<TResponse>(parentEntity);
        }

        public async Task<TResponse> GetAllAsync<TResponse>()
        {
            var result = await _parentRepository.AllAsync();
            return Mapper.Map<TResponse>(result);
        }

        public Task<TResponse> SearchAsync<TRequest, TResponse>(TRequest request)
        {
            throw new NotImplementedException();
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

            // TODO Control if parent exist and is deleted  or parent with same id exist

            // Set "not deleted".
            //payloadEntity.Deleted = false;

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
            var payloadEntity = _mapper.Map<Parent>(parentPayload);
            ValidateEntity(payloadEntity);

            var parentEntity = await _parentRepository.GetByIdAsync(payloadEntity.Id, false);
            if (parentEntity.IsNull())
            {
                throw new NotFoundException(payloadEntity.Id, typeof(Parent));
            }
            //TODO : check if parent with same parent Id already exist

            //TODO: update parent entity


            await _parentRepository.UpdateAsync(parentEntity);
            await _parentRepository.SaveChangesAsync();

            return _mapper.Map<TResponse>(parentEntity);
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
            // TODO : implement details dependent that the delete is soft or hard :)

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
