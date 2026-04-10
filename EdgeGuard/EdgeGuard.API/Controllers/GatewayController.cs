//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace EdgeGuard.API.Controllers
//{
//    [ApiController]
//    [Route("gateway/{**path}")]
//    public class GatewayController : ControllerBase
//    {
//        private readonly HttpClient _httpClient;

//        public GatewayController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//        }

//        [HttpGet, HttpPost, HttpPut, HttpDelete]
//        public async Task<IActionResult> Forward()
//        {
//            // 1️⃣ Get incoming request details
//            var method = HttpContext.Request.Method;
//            var path = HttpContext.Request.Path.Value;          // /gateway/appointments/book/10
//            var query = HttpContext.Request.QueryString.Value;  // ?date=...

//            // 2️⃣ Convert path → remove /gateway
//            var targetPath = path.Replace("/gateway", "");

//            // 3️⃣ Build target URL (SmartAppointmentScheduler)
//            var targetUrl = $"https://localhost:44371/api{targetPath}{query}";
//            targetUrl = HttpUtility.UrlDecode(targetUrl);
//            // 4️⃣ Create request message
//            var requestMessage = new HttpRequestMessage(new HttpMethod(method), targetUrl);

//            // 5️⃣ Copy body (for POST/PUT)
//            if (HttpContext.Request.ContentLength > 0)
//            {
//                using var reader = new StreamReader(HttpContext.Request.Body);
//                var body = await reader.ReadToEndAsync();
//                requestMessage.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
//            }

//            // 6️⃣ Send request
//            var response = await _httpClient.SendAsync(requestMessage);

//            // 7️⃣ Read response
//            var responseContent = await response.Content.ReadAsStringAsync();

//            // 8️⃣ Return response back to client
//            return StatusCode((int)response.StatusCode, responseContent);
//        }
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EdgeGuard.API.Controllers
{
    [ApiController]
    [Route("gateway/{**path}")]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GatewayController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public async Task<IActionResult> Forward([FromBody] object body = null)
        {
            // 1️⃣ Read request details
            var method = HttpContext.Request.Method;
            var path = HttpContext.Request.Path.Value;          // /gateway/appointments/...
            var query = HttpContext.Request.QueryString.Value;  // ?...

            // 2️⃣ Clean path
            var targetPath = path.Replace("/gateway", "");      // /appointments/...

            // 3️⃣ Build target URL
            var targetUrl = $"https://localhost:44371/api{targetPath}{query}";
            targetUrl = HttpUtility.UrlDecode(targetUrl);
            // 4️⃣ Create request
            var requestMessage = new HttpRequestMessage(new HttpMethod(method), targetUrl);

            // 5️⃣ Forward body (if exists)
            if (body != null)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(body);
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            // 6️⃣ Send request
            var response = await _httpClient.SendAsync(requestMessage);

            // 7️⃣ Read response
            var responseContent = await response.Content.ReadAsStringAsync();

            // 8️⃣ Return response
            return StatusCode((int)response.StatusCode, responseContent);
        }
    }
}
