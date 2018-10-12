using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ChristmasMothers.Web.Api.V2.Models
{
    public class TranslationModel
    {
        /// <summary>
        /// Gets or sets the fr-CA.
        /// </summary>
        [JsonProperty("fr-CA")]
        [Required(ErrorMessage = "The french translation is mandatory."), MaxLength(255)]
        public string FrCa { get; set; }

        /// <summary>
        /// Gets or sets the en-CA.
        /// </summary>
        [JsonProperty("en-CA")]
        [Required(ErrorMessage = "The english translation is mandatory."), MaxLength(255)]
        public string EnCa { get; set; }
    }
}
