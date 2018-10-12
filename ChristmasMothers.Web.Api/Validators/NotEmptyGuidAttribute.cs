using System;
using System.ComponentModel.DataAnnotations;

namespace ChristmasMothers.Web.Api.Validators
{
    /// <summary>
    /// Validates that the property contains a non-empty GUID. Use on "Guid" properties
    /// but not "Guid?" (nullable) properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        private static readonly string DefaultErrorMessage = "Field `{0}` must contain a non-empty GUID";

        public NotEmptyGuidAttribute() : base(DefaultErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            return !((Guid)value).Equals(Guid.Empty);
        }
    }
}
