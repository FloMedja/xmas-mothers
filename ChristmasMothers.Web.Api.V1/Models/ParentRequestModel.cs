using System;
using System.ComponentModel.DataAnnotations;

namespace ChristmasMothers.Web.Api.V1.Models
{
    public class ParentRequestModel
    {
        /// <summary>
        /// Gets or sets the internal id of the requisition.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the requisition.
        /// </summary>
        [Required(ErrorMessage = "The requisition id is mandatory")]
        public string RequisitionId { get; set; }
    }
}
