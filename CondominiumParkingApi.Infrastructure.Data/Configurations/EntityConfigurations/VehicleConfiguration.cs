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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle", "Condominium")
                .HasComment("Tabela de veiculos do condomínio");

            #region PrimaryKey

            builder
                .HasKey(vehicle => vehicle.Id)
                .HasName("PK_Vehicle");

            #endregion

            #region ForeignKey
                       
            builder
                .HasOne(vehicle => vehicle.VehicleModel)
                .WithMany(model => model.Vehicles)
                .HasForeignKey(vehicle => vehicle.VehicleModelId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Constrainsts

            builder.Property(vehicle => vehicle.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("DECIMAL(6,0)")
                .HasComment("Chave Primária");

            builder.Property(vehicle => vehicle.Plate)
                .IsRequired()
                .HasColumnName("Plate")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(7)
                .HasComment("Placa do veículo");

            builder.Property(apart => apart.VehicleModelId)
                .IsRequired()
                .HasComment("Chave da tabela de Model");

            builder.Property(apart => apart.Handicap)
                .IsRequired()
                .HasColumnName("Handicap")
                .HasColumnType("BIT")
                .HasDefaultValueSql("0")
                .HasComment("Indica se o veículo é adpatado para PcD");

            builder.Property(apart => apart.Vehicle_Type)
                .IsRequired()
                .HasColumnName("Vehicle_Type")
                .HasColumnType("INT")
                .HasComment("Indica tipo de veiculo. 1 == Automóvel, 2 == Motoclicleta, 3 == Caminhão");

            builder.Property(vehicle => vehicle.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data de cadastramento do veículo");

            builder.Property(vehicle => vehicle.Last_Update_Date)
                .IsRequired()
                .HasColumnName("Last_Update_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Ultima atualização do cadastro do veículo");        

            #endregion

            #region Indexes          

            builder.HasIndex(vehicle => vehicle.Plate, "IX_Vehicle_Plate")
                .IsUnique();

            #endregion

            #region PopulationData

            builder.HasData(
                new Vehicle
                {
                    Id = 1,
                    Plate = "QNH3936",
                    VehicleModelId = 1,
                    Vehicle_Type = 1,
                    Handicap = false
                },
                new Vehicle
                {
                    Id = 2,
                    Plate = "ABC5566",
                    VehicleModelId = 3,
                    Vehicle_Type = 1,
                    Handicap = false
                },
                new Vehicle
                {
                    Id = 3,
                    Plate = "EGC4355",
                    VehicleModelId = 5,
                    Vehicle_Type = 1,
                    Handicap = false
                });

            #endregion
        }
    }
}
