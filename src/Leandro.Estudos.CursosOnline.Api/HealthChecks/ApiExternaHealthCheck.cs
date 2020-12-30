using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Leandro.Estudos.CursosOnline.Api.HealthChecks
{
  public class ApiExternaHealthCheck : IHealthCheck
  {
    private readonly Uri _uri;
    private readonly HttpMethod _method;

    public ApiExternaHealthCheck(Uri uri, HttpMethod method)
    {
      _uri = uri;
      _method = method;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      try
      {
        using (var httpClient = new HttpClient())
        {
          using (var request = new HttpRequestMessage(_method, _uri))
          {
            var response = await httpClient.SendAsync(request, cancellationToken);
            return response.StatusCode == HttpStatusCode.OK
              ? HealthCheckResult.Healthy()
              : HealthCheckResult.Unhealthy();
          }
        }
      }
      catch (System.Exception)
      {
        return HealthCheckResult.Unhealthy();
      }
    }
  }
}