using Microsoft.AspNetCore.Antiforgery;
using Y.Controllers;

namespace Y.Web.Host.Controllers
{
    public class AntiForgeryController : YControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
