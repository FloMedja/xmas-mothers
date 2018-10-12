using AutoMapper;
using ChristmasMothers.Entities;
using ChristmasMothers.Web.Api.V1.Models;

namespace ChristmasMothers.Web.Api.V1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// The add view model mapper configuration.
        /// </summary>
        /// <param name="cfg">
        /// The cfg.
        /// </param>
        public static void AddViewModelMapperConfigurationV1(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ParentRequestModel, Parent>();
            cfg.CreateMap<Parent, ParentRequestModel>();
        }
    }
}