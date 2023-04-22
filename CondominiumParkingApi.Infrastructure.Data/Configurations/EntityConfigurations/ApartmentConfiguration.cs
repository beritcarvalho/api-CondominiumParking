using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Apartment", "Condominium")
                .HasComment("Tabela de Apartamentos");

            #region PrimaryKey

            builder
                .HasKey(apart => apart.Id)
                .HasName("PK_Apartment");

            #endregion

            #region ForeignKey

            builder
                .HasOne(apart => apart.Owner)
                .WithOne(person => person.ApartmentOwner)
                .HasForeignKey<Apartment>(apart => apart.OwnerId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Apartment_Owner_PersonId");

            builder.HasOne(apart => apart.Resident)
                .WithOne(person => person.ApartmentResident)
                .HasForeignKey<Apartment>(apart => apart.ResidentId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Apartment_Resident_PersonId");

            builder
                .HasOne(apart => apart.Block)
                .WithMany(block => block.Apartments)
                .HasForeignKey(apart => apart.BlockId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Constrainsts

            builder
                .Property(apart => apart.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasComment("Chave Primária");

            builder.Property(apart => apart.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("INT")
                .HasComment("Numero do Apartamento");

            builder.Property(apart => apart.OwnerId)
                .HasComment("Chave da tabela de Pessoa");

            builder.Property(apart => apart.ResidentId)
                .HasComment("Chave da tabela de Pessoa");

            builder.Property(apart => apart.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data de Criação do Cadastro do apartamento");

            builder.Property(apart => apart.Last_Update_Date)
                .IsRequired()
                .HasColumnName("Last_Update_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Ultima atualização dos dados do apartamento");

            #endregion

            #region Indexes

            builder.HasIndex(apart => apart.OwnerId, "IX_Apartment_OwnerId")
                .IsUnique();

            builder.HasIndex(apart => apart.ResidentId, "IX_Apartment_ResidentId")
                .IsUnique();


            builder.HasIndex(apart => new { apart.Number, apart.BlockId }, "IX_Apartment_Block")
                .IsUnique();

            #endregion

            #region PopulationData

            builder.HasData(
                new Apartment
                {
                    Id = 1,
                    Number = 1,
                    BlockId = 1,
                    OwnerId = Guid.Parse("5a7a5658-ccbc-4451-b4bc-5a0264bd0a81")
                },
                new Apartment
                {
                    Id = 2,
                    Number = 1,
                    BlockId = 2,
                    OwnerId = Guid.Parse("495dadfc-add1-4826-bde8-828c9b0c0134"),
                    ResidentId = Guid.Parse("495dadfc-add1-4826-bde8-828c9b0c0134")
                },
                new Apartment
                {
                    Id = 3,
                    Number = 2,
                    BlockId = 1,
                    OwnerId = Guid.Parse("59de6d3b-2002-42fa-80e3-057f2cfc5cae"),
                    ResidentId = Guid.Parse("e69cb7b8-164c-41ed-a670-7b40480c3887")
                });

            #endregion

        }
    }
}
