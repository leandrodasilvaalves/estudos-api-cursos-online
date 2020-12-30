using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Leandro.Estudos.CursosOnline.Api.HealthChecks
{
  public class SqlServerCustomHealthCheck : IHealthCheck
  {
    private readonly string _connection;

    public SqlServerCustomHealthCheck(string connection)
    {
      _connection = connection;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      try
      {
        using (var connection = new SqlConnection(_connection))
        {
          await connection.OpenAsync(cancellationToken);
          var command = connection.CreateCommand();
          command.CommandText = "SELECT COUNT(1) FROM Cursos";
          return
            Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy();
        }
      }
      catch (System.Exception)
      {

        return HealthCheckResult.Unhealthy();
      }
    }
  }
}