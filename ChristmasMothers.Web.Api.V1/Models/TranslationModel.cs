using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Web.Api.V1.Models
{
    public class TranslationModel
    {
        /// <summary>
        /// Gets or sets the fr-CA.
        /// </summary>
        [JsonProperty("fr-CA")]
        public string FrCa { get; set; }

        /// <summary>
        /// Gets or sets the fr-CA.
        /// </summary>
        [JsonProperty("en-CA")]
        public string EnCa { get; set; }
    }
}
