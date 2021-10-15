﻿using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication
{
    public class WeatherForecastContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherForecastContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("MySql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WeatherForecast>(entity =>
            {
                entity.HasKey(e => e.Date);
                entity.Property(e => e.Summary).IsRequired();
                entity.Property(e => e.TemperatureC);
                entity.Ignore(e => e.TemperatureF);
            });
        }
    }
}
