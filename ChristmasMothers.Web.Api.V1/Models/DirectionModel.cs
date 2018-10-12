using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace ChristmasMothers.Web.Api.V1.Models
{
    public class DirectionModel
    {
        public Guid? Id { get; set; }
        
        [Required(ErrorMessage = "The direction Gch Id is mandatory")]
        public string DirectionId { get; set; }
        public string NameFr { get; set; }
        public string NameEn { get; set; }

    }
}
