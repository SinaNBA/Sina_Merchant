using System.Security.Claims;

namespace SinaMerchant.Web.HttpExtentions
{
    public static class IdentityExtentions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
