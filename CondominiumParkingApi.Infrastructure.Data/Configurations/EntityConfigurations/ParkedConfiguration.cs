using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class ParkedConfiguration : IEntityTypeConfiguration<Parked>
    {
        public void Configure(EntityTypeBuilder<Parked> builder)
        {
            builder.ToTable("Parked", "Parking")
                .HasComment("Tabela de controle Paradas no estacionamento");

            #region PrimaryKey

            builder
                .HasKey(parked => parked.Id)
                .HasName("PK_Parked");

            #endregion

            #region ForeignKey

            builder
                .HasOne(parked => parked.ApartmentVehicle)
                .WithMany(apartmentVehicle => apartmentVehicle.Parkeds)
                .HasForeignKey(parked => parked.ApartmentVehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(parked => parked.ParkingSpace)
                .WithMany(parkingSpace => parkingSpace.Parkeds)
                .HasForeignKey(parked => parked.ParkingSpaceId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Constrainsts

            builder.Property(parked => parked.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("DECIMAL(6,0)")
                .HasComment("Chave Primária");

            builder.Property(parked => parked.ParkingSpaceId)
                .IsRequired()
                .HasColumnType("INT")
                .HasComment("Chave da tabela de ParkingSpace");

            builder.Property(parked => parked.ApartmentVehicleId)
                .IsRequired()
                .HasColumnType("DECIMAL(6,0)")
                .HasComment("Chave da tabela de ApartmentVehicle");

            builder.Property(parked => parked.In_Date)
                .IsRequired()
                .HasColumnName("In_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data e hora da entrada do Veículo");

            builder.Property(parked => parked.Out_Date)
                .HasColumnName("Out_Date")
                .HasColumnType("DATETIME")
                .HasComment("Data e hora da saída do Veículo");

            builder.Property(person => person.Deadline)
                .IsRequired()
                .HasColumnName("Deadline")
                .HasColumnType("DATETIME")
                .HasComment("Prazo para retirada do veículo");

            builder.Property(limitExceeded => limitExceeded.Time_Exceeded)
                .IsRequired()
                .HasColumnName("Time_Exceeded")
                .HasColumnType("TIME")
                .HasComment("Total de tempo excedido na vaga");

            builder.Property(parked => parked.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT")
                .HasComment("Indica se a parada do veículo está em ativa");


            #endregion

            #region Indexes         

            builder.HasIndex(parked => parked.ApartmentVehicleId, "IX_Vehicle_Active")
                .HasFilter("[Active] = 1")
                .IsUnique();

            builder.HasIndex(parked => parked.ParkingSpaceId, "IX_ParkingSpace_Active")
                .HasFilter("[Active] = 1")
                .IsUnique();

            #endregion

            #region PopulationData



            #endregion
        }
    }
}
