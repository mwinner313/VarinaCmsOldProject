using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace VarinaCmsV2.Common
{
    public class FeedResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }

        private readonly SyndicationFeedFormatter _feed;
        public SyndicationFeedFormatter Feed => _feed;

        public FeedResult(SyndicationFeedFormatter feed)
        {
            this._feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (_feed != null)
                using (var xmlWriter = new XmlTextWriter(response.Output))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    _feed.WriteTo(xmlWriter);
                }
        }
    }
}
