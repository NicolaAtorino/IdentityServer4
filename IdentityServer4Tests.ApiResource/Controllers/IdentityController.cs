using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Helpers;

namespace IdentityServer4Tests.ApiResource.Controllers
{
    [RoutePrefix("identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        [HttpGet]
        [Route("get")]
        public JsonResult Get()
        {
            var claimsList = from c in ((ClaimsPrincipal)User).Claims select new { c.Type, c.Value };
            return Json(claimsList,JsonRequestBehavior.AllowGet);
        }
    }
}