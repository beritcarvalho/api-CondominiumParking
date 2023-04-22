using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondominiumApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.ToTable("VehicleModel", "Condominium")
            .HasComment("Tabela de modelos de veículo");

            #region PrimaryKey

            builder
                .HasKey(vModel => vModel.Id)
                .HasName("PK_Vehicle_Model");          

            #endregion

            #region ForeignKey

             builder
                .HasOne(VehicleModel => VehicleModel.Brand)
                .WithMany(brand => brand.Models)
                .HasForeignKey(VehicleModel => VehicleModel.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Constrainsts

            builder.Property(vModel => vModel.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("INT")
                .HasComment("Chave Primária");

            builder.Property(vModel => vModel.Model_Name)
                .IsRequired()
                .HasColumnName("Model_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .HasComment("Modelo");

            builder.Property(vModel => vModel.BrandId)
                .IsRequired()
                .HasColumnName("BrandId")
                .HasColumnType("INT")
                .HasComment("PK da tabela de marca");

            #endregion

            #region Indexes          

            builder.HasIndex(vehicle => vehicle.Model_Name, "IX_Model_Name")
                .IsUnique();

            builder.HasIndex(vehicle => vehicle.BrandId, "IX_Brand_Id");

            #endregion

            #region PopulationData

            builder.HasData(
                new VehicleModel
                {
                    Id = 1,
                    Model_Name = "Onix",
                    BrandId = 1
                },
                new VehicleModel
                {
                    Id = 2,
                    Model_Name = "Cruze",
                    BrandId = 1
                },
                new VehicleModel
                {
                    Id = 3,
                    Model_Name = "Celta",
                    BrandId = 1
                },
                new VehicleModel
                {
                    Id = 4,
                    Model_Name = "HB20",
                    BrandId = 2
                },
                new VehicleModel
                {
                    Id = 5,
                    Model_Name = "Fox",
                    BrandId = 3
                },
                new VehicleModel
                {
                    Id = 6,
                    Model_Name = "Gol",
                    BrandId = 3
                },
                new VehicleModel
                {
                    Id = 7,
                    Model_Name = "Civic",
                    BrandId = 4
                });

            #endregion
        }
    }
}
