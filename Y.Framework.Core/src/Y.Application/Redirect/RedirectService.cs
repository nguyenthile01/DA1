using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Y.Dto;

namespace Y.Services
{
    public class RedirectService : IRedirectService
    {
        private const string RedirectFilename = "redirects.xml";
        private readonly IHostingEnvironment hostingEnvironment;

        public RedirectService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        private IEnumerable<Redirect> LoadRedirects()
        {
            //var redirectsPath = hostingEnvironment.MapPath(RedirectFilename);

            if (!File.Exists(RedirectFilename))
            {
                return Enumerable.Empty<Redirect>();
            }

            var doc = XDocument.Load(RedirectFilename);
            return GetRedirectsFromXml(doc);
        }
        private IEnumerable<Redirect> GetRedirectsFromXml(XDocument doc)
        {
            var redirects = (from e in doc.Descendants("redirect")
                             select new Redirect
                             {
                                 From = e.Attribute("from").Value,
                                 To = e.Attribute("to").Value
                             }).ToList();
            return redirects;
        }
        public Redirect GetRedirect301(string url)
        {
            return LoadRedirects()
                    .FirstOrDefault(x => x.From == url);
        }
    }
}

