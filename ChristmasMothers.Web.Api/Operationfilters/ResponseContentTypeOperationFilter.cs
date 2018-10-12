using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;

namespace ChristmasMothers.Web.Api.Operationfilters
{
    public class ResponseContentTypeOperationFilter : IOperationFilter
        {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var requestAttributes = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<SwaggerResponseContentTypeAttribute>();

            foreach (var requestAttribute in requestAttributes)
            {
                if (requestAttribute.Exclusive)
                {
                    operation.Produces.Clear();
                }
                operation.Produces.Add(requestAttribute.ResponseType);
            }

        }
    }
}