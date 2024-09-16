using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            var username = HttpContext.Session.GetString("sessionLogin-username");
            if (string.IsNullOrEmpty(username) )
            {
                ctx.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(ctx);
        }
    }
}
