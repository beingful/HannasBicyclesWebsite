using Microsoft.AspNetCore.Http;

namespace BicycleShop.Extensions
{
    public static class RequestHelper
    {
        public static string CurrentUrl(this HttpRequest request)
        {
            return request.Path + request.QueryString;
        }
    }
}
