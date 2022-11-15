using System.Globalization;

namespace CarReservationApi.Middleware
{
    public class LocalizationMessageMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMessageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private readonly List<string> _supportedLanguages = new List<string>() { "tr-TR", "en-US", "ru-RU", "ro-RO" };

        public async Task Invoke(HttpContext httpContext)
        {

            SetCulture(httpContext.Request);

            await _next(httpContext);
        }

        private void SetCulture(HttpRequest request)
        {
            request.Headers.TryGetValue("Language", out var traceValue);

            // Desteklediğimiz dillerden biri var ise culture bilgisini o dile göre güncelliyoruz.
            if (_supportedLanguages.Contains(traceValue))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(traceValue);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(traceValue);
            }
        }
    }
}
