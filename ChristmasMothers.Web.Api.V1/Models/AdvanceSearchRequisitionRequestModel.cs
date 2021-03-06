﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Web.Api.V1.Models
{
    public class SearchRequisitionRequestModel
    {

        public SearchRequisitionRequestModel()
        {
            Q = string.Empty;
            Skip = 0;
            Take = 100;
        }

        /// <summary>
        /// Gets or sets Q, search criterias.
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// Gets or sets Skip.
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// Gets or sets Take.
        /// </summary>
        public int Take { get; set; }
    }
}
