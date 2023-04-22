using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class LimitExceededConfiguration : IEntityTypeConfiguration<LimitExceeded>
    {
        public void Configure(EntityTypeBuilder<LimitExceeded> builder)
        {
            builder.ToTable("LimitExceeded", "Parking")
                .HasComment("Tabela de controle de paradas que tiveram prazo excedido");

            #region PrimaryKey

            builder
                .HasKey(limitExceeded => limitExceeded.Id)
                .HasName("PK_LimitExceeded");

            #endregion

            #region ForeignKey

            builder
                .HasOne(limitExceeded => limitExceeded.Parked)
                .WithOne(parked => parked.LimitExceeded)
                .HasForeignKey<LimitExceeded>(limitExceeded => limitExceeded.ParkedId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Constrainsts

            builder.Property(limitExceeded => limitExceeded.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnType("INT")
                .HasComment("Chave Primária");

            builder.Property(limitExceeded => limitExceeded.ParkedId)
                .HasColumnType("DECIMAL(6,0)")
                .IsRequired()
                .HasComment("Chave da tabela de Parked");

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

            #endregion

            #region Indexes         

            builder.HasIndex(limitExceeded => limitExceeded.ParkedId, "IX_LimitExceeded_Parked")
                .IsUnique();

            #endregion

            #region PopulationData



            #endregion
        }
    }
}
