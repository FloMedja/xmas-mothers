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
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly Type _objectType;

        public AddressBusiness(IMapper mapper, IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _objectType = typeof(Address);
        }

        #region GET

        /// <summary>
        /// Get By ID Async
        /// </summary>
        /// <exception cref="NotFoundException">No matching address could be found.</exception>
        /// <typeparam name="TResponse">The type to map the resulting entity to</typeparam>
        /// <param name="addressId">address ID.</param>
        /// <returns>The address entity (mapped to <typeparamref name="TResponse"/>).</returns>
        public async Task<TResponse> GetByIdAsync<TResponse>(Guid addressId)
        {

            var addressEntity = await _addressRepository.GetByIdAsync(addressId);

            if (addressEntity.IsNull())
            {
                throw new NotFoundException(addressId, typeof(Address));
            }
            return _mapper.Map<TResponse>(addressEntity);
        }

        public async Task<int> GetNumberOfAddress()
        {
            var count = await _addressRepository.GetCountAsync();
            return count;
        }

        #endregion



        #region CREATE_OR_UPDATE

        /// <summary>
        /// Update address Async
        /// </summary>
        /// <exception cref="NotFoundException">No matching address could be found.</exception>
        /// <exception>Tenant with same name already exist.</exception>
        /// <typeparam name="TRequest">Payload type (must be map-able to Address)</typeparam>
        /// <typeparam name="TResponse">Desired return type.</typeparam>
        /// <returns></returns>
        public async Task<TResponse> CreateOrUpdateAsync<TRequest, TResponse>(TRequest addressPayload)
        {
            // Map payload to entity.
            var newAddressEntity = _mapper.Map<Address>(addressPayload);
            if (newAddressEntity.Id == Guid.Empty)
            {
                newAddressEntity.Id = Guid.NewGuid();
            }
            ValidateEntity(newAddressEntity);

            var oldAddressEntity = await _addressRepository.GetByIdAsync(newAddressEntity.Id);
            if (oldAddressEntity.IsNull())
            {
                await _addressRepository.AddAsync(newAddressEntity);
            }

            await _addressRepository.UpdateEntryAsync(newAddressEntity);
            await _addressRepository.SaveChangesAsync();
            return _mapper.Map<TResponse>(newAddressEntity);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete address Async (soft-delete)
        /// </summary>
        /// <exception cref="NotFoundException">No matching address could be found.</exception>
        /// <param name="addressId">Tenant ID.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid addressId)
        {
            var addressEntity = await _addressRepository.GetByIdAsync(addressId);

            if (addressEntity.IsNull())
            {
                throw new NotFoundException(addressId, _objectType);
            }

            await _addressRepository.RemoveAsync(addressEntity);
            await _addressRepository.SaveChangesAsync();

        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Validates en entity content prior to creation or update.
        /// </summary>
        /// <param name="entity">Tenant entity to validate.</param>
        private void ValidateEntity(Address entity)
        {
            if (entity.Id == Guid.Empty)
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.Id), typeof(Address));
            }
            if (entity.City.IsNullOrEmptyOrWhiteSpace())
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.City), typeof(Address));
            }
            if (entity.PostalCode.IsNullOrEmptyOrWhiteSpace())
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.PostalCode), typeof(Address));
            }
            if (entity.StreetName.IsNullOrEmptyOrWhiteSpace())
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.StreetName), typeof(Address));
            }
            if (entity.Province.IsNullOrEmptyOrWhiteSpace())
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.Province), typeof(Address));
            }
            if (entity.Country.IsNullOrEmptyOrWhiteSpace())
            {
                throw new MissingOrEmptyPropertyException(nameof(entity.Country), typeof(Address));
            }
        }



        #endregion
    }
}
