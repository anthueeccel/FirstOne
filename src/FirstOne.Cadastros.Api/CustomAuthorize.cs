using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Security.Claims;

namespace FirstOne.Cadastros.Api
{
    public class CustomAuthorize
    {
        public static bool ValidateUserClaims(HttpContext httpContext, string claimName, string claimValue)
        {
            return httpContext.User.Identity.IsAuthenticated &&
                httpContext.User.Claims.Any(y => y.Type == claimName && y.Value.Contains(claimValue));
        }
    }
    public class ClaimsAuthorizationAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizationAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequirementClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequirementClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorize.ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new JsonResult("Usuário sem permissão") { StatusCode = 403 };
                return;
            }
        }
    }
}
