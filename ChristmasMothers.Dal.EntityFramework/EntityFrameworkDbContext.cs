using System;

using Microsoft.EntityFrameworkCore;
using ChristmasMothers.Dal.Configurations;
using ChristmasMothers.Dal.EntityFramework.Extensions;

namespace ChristmasMothers.Dal.EntityFramework
{
    public class EntityFrameworkDbContext : DbContext
    {
        private readonly IDalConfiguration _dalConfiguration;
        
        public EntityFrameworkDbContext(IDalConfiguration dalConfiguration)
        {
            _dalConfiguration = dalConfiguration;
            DotConnectOracleConfigs();
        }

        public EntityFrameworkDbContext(IDalConfiguration dalConfiguration, DbContextOptions<EntityFrameworkDbContext> options) : base(options)
        {
            _dalConfiguration = dalConfiguration;
            DotConnectOracleConfigs();
        }

        private void DotConnectOracleConfigs()
        {
            var config = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            config.CodeFirstOptions.UseNonUnicodeStrings = true;
            config.CodeFirstOptions.UseNonLobStrings = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            switch (_dalConfiguration.DatabaseType)
            {
                case DatabaseType.Oracle:
                    optionsBuilder.UseOracle(_dalConfiguration.ConnectionString, options =>
                    {
                        options.MigrationsAssembly("ChristmasMothers.Dal.EntityFramework.Oracle");
                        options.MigrationsHistoryTable("__EFMigrationsHistory", _dalConfiguration.Schema);
                    });
                    break;
                case DatabaseType.SqlServer:
                    optionsBuilder.UseSqlServer(_dalConfiguration.ConnectionString, options =>
                    {
                        options.MigrationsAssembly("ChristmasMothers.Dal.EntityFramework.SqlServer");
                        options.MigrationsHistoryTable("__EFMigrationsHistory", _dalConfiguration.Schema);
                    });
                    break;
                case DatabaseType.InMemory:
                    optionsBuilder.UseInMemoryDatabase(_dalConfiguration.ConnectionString);
                    break;
                default:
                    throw new NotSupportedException($"The database type {Enum.GetName(typeof(DatabaseType), _dalConfiguration.DatabaseType)} is not supported.");
            }
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_dalConfiguration.Schema);
            modelBuilder.UseEntityTypeConfiguration();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Upper table names
                entity.Relational().TableName = entity.Relational().TableName.ToUpper();

                // Upper column names and set string properties
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToUpper();
                    if (property.ClrType == typeof(string))
                    {
                        property.IsUnicode(false);
                    }
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToUpper();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToUpper();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToUpper();
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}