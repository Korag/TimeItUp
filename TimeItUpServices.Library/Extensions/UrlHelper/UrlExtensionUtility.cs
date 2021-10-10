namespace TimeItUpServices.Extensions.UrlHelper
{
    public static class UrlExtensionUtility
    {
        public static string GenerateResetUserPasswordActionURL(this Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper, string userId, string authorizationToken)
        {
            return urlHelper.Link("ResetUserPasswordAction", new { Controller = "Accounts", Action = "ResetUserPasswordAction", id = userId, token = authorizationToken });
        }
    }
}