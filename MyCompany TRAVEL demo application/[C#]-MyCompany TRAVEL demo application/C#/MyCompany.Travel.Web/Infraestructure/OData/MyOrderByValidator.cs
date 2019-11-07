

namespace MyCompany.Travel.Web.Infraestructure.OData
{
    using Microsoft.Data.OData;
    using Microsoft.Data.OData.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http.OData.Query;
    using System.Web.Http.OData.Query.Validators;

    /// <summary>
    /// The following validator class disables the 'desc' option for the $orderby option
    /// </summary>
    public class MyOrderByValidator : OrderByQueryValidator
    {
        /// <summary>
        /// Disallow the 'desc' parameter for $orderby option.
        /// </summary>
        /// <param name="orderByOption"></param>
        /// <param name="validationSettings"></param>
        public override void Validate(OrderByQueryOption orderByOption,
                                        ODataValidationSettings validationSettings)
        {
            if (orderByOption.OrderByNodes.Any(
                    node => node.Direction == OrderByDirection.Descending))
            {
                throw new ODataException("The 'desc' option is not supported.");
            }
            base.Validate(orderByOption, validationSettings);
        }
    }
}