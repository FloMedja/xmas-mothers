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
    public class ChildBusiness : IChildBusiness
    {
        private readonly IMapper _mapper;
        private readonly IChildRepository _childRepository;
        private readonly Type _objectType;

        public ChildBusiness(IMapper mapper, IChildRepository childRepository)
        {
            _mapper = mapper;
            _childRepository = childRepository;
            _objectType = typeof(Child);
        }

        #region GET

        public async Task<int> GetNumberOfChild()
        {
            var count = await _childRepository.GetCountAsync();
            return count;
        }

        public async Task<IEnumerable<TResponse>> AllAsync<TResponse>()
        {
            var results = await _childRepository.AllAsync();
            return _mapper.Map<IEnumerable<TResponse>>(results);
        }

        public async Task<IEnumerable<TResponse>> AllWithChildrenAsync<TResponse>(bool includeAllRelativeEntitiesChildren = false)
        {
            var results = await _childRepository.AllWithChildrenAsync(includeAllRelativeEntitiesChildren);
            return _mapper.Map<IEnumerable<TResponse>>(results);
        }

        public async Task<TResponse> GetByIdAsync<TResponse>(Guid childId, bool includeEntityChildren, bool includeAllRelativeEntitiesChildren = false)
        {
            Child childEntity;
            if (includeEntityChildren)
            {
                childEntity = await _childRepository.GetByIdWithChildrenAsync(childId, includeAllRelativeEntitiesChildren);
            }
            else
            {
                childEntity = await _childRepository.GetByIdAsync(childId);
            }
            if (childEntity.IsNull())
            {
                throw new NotFoundException(childId, _objectType);
            }
            return _mapper.Map<TResponse>(childEntity);
        }

        public async Task<TResponse> GetByTrackingIdAsync<TResponse>(string trackingId, bool includeEntityChildren)
        {

            var childEntity = await _childRepository.GetByChildTrackingIdAsync(trackingId, includeEntityChildren);

            if (childEntity.IsNull())
            {
                throw new NotFoundException(trackingId, _objectType);
            }
            return _mapper.Map<TResponse>(childEntity);
        }

        public Task<TResponse> SearchAsync<TRequest, TResponse>(TRequest request, bool includeChildren = false,
            bool includeAllRelativeEntitiesChildren = false)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> SearchByAgeAsync<TRequest, TResponse>(TRequest request, bool includeChildren = false,
            bool includeAllRelativeEntitiesChildren = false)
        {
            
            throw new NotImplementedException();
        }

        #endregion



        #region CREATE_AND_UPDATE


        #endregion

        #region DELETE
        /// <summary>
        /// Delete child Async (soft-delete)
        /// </summary>
        /// <exception cref="NotFoundException">No matching child could be found.</exception>
        /// <param name="childId">Tenant ID.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid childId)
        {
            var childEntity = await _childRepository.GetByIdAsync(childId);

            if (childEntity.IsNull())
            {
                throw new NotFoundException(childId, _objectType);
            }

            await _childRepository.RemoveAsync(childEntity);
            await _childRepository.SaveChangesAsync();

        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Validates en entity content prior to creation or update.
        /// </summary>
        /// <param name="entity">Tenant entity to validate.</param>
        private void ValidateEntity(Child entity)
        {
            if (entity.Id == Guid.Empty)
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.Id), typeof(Child));
            }

        }



        public Task<TResponse> CreateAsync<TRequest, TResponse>(TRequest childPayload)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> UpdateAsync<TRequest, TResponse>(TRequest childPayload)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
