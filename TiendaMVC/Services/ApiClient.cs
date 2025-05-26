namespace TiendaMVC.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _context;

        public ApiClient(HttpClient http, IHttpContextAccessor context)
        {
            _http = http;
            _context = context;

            //Si es que hay un token de sesión, lo incluimos en cada petición
            var token = _context.HttpContext!.Session.GetString("JWToken");

        }
    }

    public class TokenResponse { public string Token { get; set; } = ""; }

}