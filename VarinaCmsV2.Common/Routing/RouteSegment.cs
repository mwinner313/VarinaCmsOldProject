using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace VarinaCmsV2.Common.Routing
{
    /// <summary>
    /// Represents a generated route url with route data
    /// </summary>
    public class RouteUrl
    {
        /// <summary>
        /// Gets or sets the route URL.
        /// </summary>
        /// <value>Route URL.</value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets route values.
        /// </summary>
        /// <value>Route values.</value>
        public RouteValueDictionary Values { get; set; }
    }
}
