using service;

namespace Recipe_Web_App;

public static class HttpContextExtensions
{
    public static void SetSessionData(this HttpContext httpContext, SessionData data)
    {
        httpContext.Items["data"] = data;
    }

    public static SessionData? GetSessionData(this HttpContext httpContext)
    {
        return httpContext.Items["data"] as SessionData;
    }
}