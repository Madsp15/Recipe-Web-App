using service;

namespace Recipe_Web_App;

public class TokenBearerHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenBearerHandler> _logger;
    
    public TokenBearerHandler(RequestDelegate next, ILogger<TokenBearerHandler> logger)
    {
    _next = next;
    _logger = logger;
    } 
    
    public async Task InvokeAsync(HttpContext http)
    {
        var jwtHelper = http.RequestServices.GetRequiredService<JWTTokenService>();
        
        try
        {
            var authHeader = http.Request.Headers.Authorization.FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Split(" ")[1];
                var data = jwtHelper.ValidateAndDecodeToken(token);
                http.SetSessionData(data);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error extracting user from bearer token in Authorization header");
        }
        
        await _next.Invoke(http);
    }
}