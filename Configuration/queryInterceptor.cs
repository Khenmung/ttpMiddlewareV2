using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

namespace ttpMiddleware.Configuration
{

    public class ExtendedEnableQueryAttribute : EnableQueryAttribute
    {
        public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
        {
            //var identity = HttpContext
            //    .User.Identity as ClaimsIdentity;
            //if (identity != null)
            //{
            //    IEnumerable<Claim> claims = identity.Claims;
            //    // or
            //    identity.FindFirst("ClaimName").Value;

            //}

            ////Filter specific claim    
            //var orgId = claims?.FirstOrDefault(x => x.Type.Equals("Sid", StringComparison.OrdinalIgnoreCase))?.Value;
            var orgId = 2;
            // ... second check in the if statement might be overkill - abundance of caution?
            if (queryOptions.Filter != null && queryOptions.Request.Query.ContainsKey("$filter"))
            {
                var stringValuesDict = new Dictionary<string, StringValues>();

                foreach (var kvPair in queryOptions.Request.Query.Where(d => !d.Key.Equals("$filter")))
                {
                    // This way the new StringValues instances are owned exclusively by substitute query collection
                    var values = new List<string>();
                    foreach (var value in kvPair.Value)
                    {
                        values.Add(value);
                    }
                    stringValuesDict.Add(kvPair.Key, new StringValues(values.ToArray()));
                }
                // Substitute the $filter option
                stringValuesDict.Add("$filter", new StringValues($"OrgId eq {orgId}"));
                // Substitute the request query collection
                queryOptions.Request.Query = new QueryCollection(stringValuesDict);

                queryOptions = new ODataQueryOptions(queryOptions.Context, queryOptions.Request);
            }

            return base.ApplyQuery(queryable, queryOptions);
        }
    }
}


