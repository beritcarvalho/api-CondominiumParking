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
    public class ApartmentVehicleConfiguration : IEntityTypeConfiguration<ApartmentVehicle>
    {
        public void Configure(EntityTypeBuilder<ApartmentVehicle> builder)
        {
            builder.ToTable("ApartmentsVehicles", "Condominium")
                .HasComment("Tabela Associtativa de Apartamento e Veiculos");

            #region PrimaryKey

            builder
                .HasKey(apartVehi => apartVehi.Id)
                .HasName("PK_ApartmentsVehicles");

            #endregion

            #region ForeignKey

            builder
                .HasOne(apartVehi => apartVehi.Apartment)
                .WithMany(apartment => apartment.ApartmentsVehicles)
                .HasForeignKey(apartVehi => apartVehi.ApartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(apartVehi => apartVehi.Vehicle)
                .WithMany(vehicle => vehicle.ApartmentsVehicles)
                .HasForeignKey(apartVehi => apartVehi.VehicleId)
                .OnDelete(DeleteBehavior.NoAction); 

            #endregion

            #region Constrainsts

            builder.Property(apartVehi => apartVehi.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("DECIMAL(6,0)")
                .HasComment("Chave Primária");

            builder.Property(apartVehi => apartVehi.Id)
                .HasColumnType("INT")
                .HasComment("Chave da tabela Apartment")
                .IsRequired();

            builder.Property(apartVehi => apartVehi.Id)
                .HasColumnType("DECIMAL(6,0)")
                .HasComment("Chave da tabela Vehicle")
                .IsRequired();

            builder.Property(apart => apart.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT")
                .HasDefaultValueSql("0")
                .HasComment("Indica se o veiculo está ativo");

           
            builder.Property(apartVehi => apartVehi.Active_Date)
                .HasColumnName("Active_Date")
                .HasColumnType("DATETIME")
                .HasComment("Data de Ativação");

            builder.Property(apartVehi => apartVehi.Inactive_Date)
                .HasColumnName("Inactive_Date")
                .HasColumnType("DATETIME")
                .HasComment("Data de Desativação");

            #endregion

            #region Indexes          

            builder.HasIndex(apartVehi => apartVehi.ApartmentId, "IX_Apartment_Active")
                .HasFilter("[Active] = 1")
                .IsUnique();

            builder.HasIndex(apartVehi => apartVehi.VehicleId, "IX_Vehicle_Active")
                .HasFilter("[Active] = 1")
                .IsUnique();

            builder.HasIndex(apartVehi => new { apartVehi.VehicleId, apartVehi.ApartmentId }, "IX_Vehicle_Apartment")
                .IsUnique();

            #endregion


            #region PopulationData

            builder.HasData(
                new ApartmentVehicle
                {
                    Id = 1,
                    ApartmentId = 1,
                    VehicleId = 1,
                    Active = true
                }, new ApartmentVehicle
                {
                    Id = 2,
                    ApartmentId = 2,
                    VehicleId = 2,
                    Active = false
                });

            #endregion
        }
    }
}
