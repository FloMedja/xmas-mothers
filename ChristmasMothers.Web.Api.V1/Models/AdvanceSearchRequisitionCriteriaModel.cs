using System;
using System.Collections.Generic;
using System.Text;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Web.Api.V1.Models
{
    //TODO : Refine according the comment on filtering values in fe
    public class AdvanceSearchRequisitionCriteriaModel
    {
        public string RequisitionId { get; set; }

        public bool Permanent { get; set; }

        public bool FullTime { get; set; }

        public Guid InstitutionId { get ; set; }

        public Guid JobId { get; set; }

        public Guid JobCategoryId { get; set; }
    }
}
