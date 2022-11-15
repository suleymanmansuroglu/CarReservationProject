using Car.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DataAccess.Concrete.EntityFramework
{
    public class CarReservationContext : DbContext
    {
        public CarReservationContext()
        {
            
        }
        public DbSet<CarModel> Cars { get; set; }




        public static void SetConnectionString(string connectionString)
        {
            if (ConnectionString == null)
            {
                ConnectionString = connectionString;
            }
            else
            {
                throw new ArgumentNullException("c", "ConnectionString");
            }
        }

        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        private static string ConnectionString { get; set; }


        public static void SetLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }
        private static LogLevel _logLevel;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionString)
                    .EnableSensitiveDataLogging(true)
                    .EnableSensitiveDataLogging(true);
            }

        }

        }
    
    }

