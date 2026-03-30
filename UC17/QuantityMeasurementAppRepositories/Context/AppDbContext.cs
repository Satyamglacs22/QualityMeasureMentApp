using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements => Set<QuantityMeasurementEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureMeasurementTable(builder);
        }

        private static void ConfigureMeasurementTable(ModelBuilder builder)
        {
            var entity = builder.Entity<QuantityMeasurementEntity>();

            entity.ToTable("quantity_measurements");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.Operation)
                  .HasColumnName("operation")
                  .IsRequired()
                  .HasMaxLength(50);

            MapNumericColumn(entity, e => e.FirstValue,  "first_value");
            MapNumericColumn(entity, e => e.SecondValue, "second_value");
            MapNumericColumn(entity, e => e.ResultValue, "result_value");

            entity.Property(e => e.FirstUnit)
                  .HasColumnName("first_unit").HasMaxLength(50);

            entity.Property(e => e.SecondUnit)
                  .HasColumnName("second_unit").HasMaxLength(50);

            entity.Property(e => e.MeasurementType)
                  .HasColumnName("measurement_type").HasMaxLength(50);

            entity.Property(e => e.IsError)
                  .HasColumnName("is_error");

            entity.Property(e => e.ErrorMessage)
                  .HasColumnName("error_message").HasMaxLength(500);

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(e => e.Operation)
                  .HasDatabaseName("IX_quantity_measurements_operation");

            entity.HasIndex(e => e.MeasurementType)
                  .HasDatabaseName("IX_quantity_measurements_measurement_type");

            entity.HasIndex(e => e.IsError)
                  .HasDatabaseName("IX_quantity_measurements_is_error");
        }

        private static void MapNumericColumn(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuantityMeasurementEntity> e,
            System.Linq.Expressions.Expression<System.Func<QuantityMeasurementEntity, double>> prop,
            string columnName)
        {
            e.Property(prop).HasColumnName(columnName);
        }
    }
}