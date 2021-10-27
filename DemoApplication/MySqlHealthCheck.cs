using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace DemoApplication
{
    public class MySqlHealthCheck
        : IHealthCheck
    {
        private readonly string _connectionString;
        public MySqlHealthCheck(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql");
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                if (connection.State != ConnectionState.Open)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, $"The {nameof(MySqlHealthCheck)} check fail.");
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
