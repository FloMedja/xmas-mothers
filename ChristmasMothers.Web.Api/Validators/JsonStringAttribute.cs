using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChristmasMothers.Web.Api.Validators
{
    /// <summary>
    /// Validates that the property contains a valid JSON string IFF it is
    /// not null or empty or white space.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonStringAttribute : ValidationAttribute
    {
        private static readonly string DefaultErrorMessage = "Field `{0}` must contain a valid JSON string";

        public JsonStringAttribute() : base(DefaultErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            var stringValue = value as string;
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                try
                {
                    JsonConvert.DeserializeObject(stringValue);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
