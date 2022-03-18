namespace CarBuyRentSystem.Infrastructure.Data
{
    using System.Security.Claims;
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
         => user.IsInRole(WebConstants.AdministratorRoleName);
    }
}
