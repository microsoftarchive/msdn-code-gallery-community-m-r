namespace MyCompany.Travel.Web.Infraestructure.OData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.OData.Query;

    /// <summary>
    /// Custom Queryable attribute to apply MyOrderByValidator
    /// </summary>
    public class MyQueryableAttribute : QueryableAttribute
    {
        /// <summary>
        /// override the ValidateQuery method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryOptions"></param>
        public override void ValidateQuery(HttpRequestMessage request,
            ODataQueryOptions queryOptions)
        {
            if (queryOptions.OrderBy != null)
            {
                queryOptions.OrderBy.Validator = new MyOrderByValidator();
            }
            base.ValidateQuery(request, queryOptions);
        }
    }
}