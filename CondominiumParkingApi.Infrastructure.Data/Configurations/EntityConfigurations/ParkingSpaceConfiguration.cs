﻿using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> builder)
        {
            builder.ToTable("ParkingSpace", "Parking")
                .HasComment("Tabela de controle Paradas no estacionamento");

            #region PrimaryKey

            builder
                .HasKey(parkingSpace => parkingSpace.Id)
                .HasName("PK_ParkingSpace");

            #endregion

            #region ForeignKey

            #endregion

            #region Constrainsts

            builder.Property(parkingSpace => parkingSpace.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("INT")
                .HasComment("Chave Primária");

            builder.Property(parkingSpace => parkingSpace.Space)
                .IsRequired()
                .HasComment("Número da vaga de estacionamento");

            builder.Property(parked => parked.Handicap)
                .IsRequired()
                .HasColumnName("Handicap")
                .HasColumnType("BIT")
                .HasDefaultValueSql("0")
                .HasComment("Indica se a vaga é reservada para pessoas de necessidades especiais");

            builder.Property(parked => parked.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT")
                .HasDefaultValueSql("1")
                .HasComment("Indica se a vaga está ativa para uso");

            #endregion

            #region Indexes         

            builder.HasIndex(parkingSpace => parkingSpace.Space, "IX_Space")
                .IsUnique();

            #endregion

            #region PopulationData



            #endregion
        }
    }
}
