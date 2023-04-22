using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class BlockConfiguration : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.ToTable("Block", "Condominium")
                .HasComment("Tabela de Bloco de apartamentos");

            #region PrimaryKey

            builder
                .HasKey(block => block.Id)
                .HasName("PK_Block");

            builder
                .Property(block => block.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasComment("Chave Primária");

            #endregion

            #region Constrainsts

            builder.Property(block => block.Block_Name)
                .IsRequired()
                .HasColumnName("Block_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2)
                .HasComment("Bloco por Apartamento");

            #endregion

            #region Indexes

            builder
                .HasIndex(block => block.Block_Name, "IX_Block")
                .IsUnique();

            #endregion

            #region PopulationData

            builder.HasData(
            new Block
            {
                Id = 1,
                Block_Name = "A"
            },
            new Block
            {
                Id = 2,
                Block_Name = "B"
            },
            new Block
            {
                Id = 3,
                Block_Name = "C"
            });

            #endregion
        }
    }
}
