using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace CondominiumParkingApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Condominium")
                .HasComment("Tabela de Pessoas Cadastradas");

            #region PrimaryKey
            builder
                .HasKey(person => person.Id)
                .HasName("PK_Person");

            builder
                .Property(person => person.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Chave Primária");
            #endregion

            #region Constrainsts
            builder.Property(person => person.First_Name)
                .IsRequired()
                .HasColumnName("First_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .HasComment("Nome");

            builder.Property(person => person.Last_Name)
                .IsRequired()
                .HasColumnName("Last_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .HasComment("Sobrenome");

            builder.Property(person => person.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(13)
                .HasComment("Telefone para contato");

            builder.Property(person => person.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .HasComment("Email");

            builder.Property(person => person.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data de Criação do Cadastro");

            builder.Property(person => person.Last_Update_Date)
                .IsRequired()
                .HasColumnName("Last_Update_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Ultima atualização do cadastro");
            #endregion

            #region Indexes

            builder.HasIndex(x => x.Email, "IX_Person_Email")
                .IsUnique();

            builder.HasIndex(x => x.Cpf, "IX_Person_Cpf")
                .IsUnique();

            #endregion

            #region PopulationData
            builder.HasData(
                new Person
                {
                    Id = Guid.Parse("6fd401e3-fb52-4eed-b7df-28c99753ae55"),
                    First_Name = "Admin",
                    Last_Name = "System",
                    Cpf = "0124567890",
                    Phone = "11987654321",
                    Email = "admin@admin.com"
                },
            new Person
            {
                Id = Guid.Parse("5a7a5658-ccbc-4451-b4bc-5a0264bd0a81"),
                First_Name = "Garen",
                Last_Name = "Stemmaguarda",
                Cpf = "11122233344",
                Phone = "11987654322",
                Email = "garen@stemmaguarda.com"
            },
            new Person
            {
                Id = Guid.Parse("495dadfc-add1-4826-bde8-828c9b0c0134"),
                First_Name = "Lux",
                Last_Name = "Stemmaguarda",
                Cpf = "22233344455",
                Phone = "11987654322",
                Email = "lux@stemmaguarda.com"
            },
            new Person
            {
                Id = Guid.Parse("59de6d3b-2002-42fa-80e3-057f2cfc5cae"),
                First_Name = "Annie",
                Last_Name = "Hastur",
                Cpf = "22233344456",
                Phone = "11987654322",
                Email = "annie@hastur.com"
            },
            new Person
            {
                Id = Guid.Parse("e69cb7b8-164c-41ed-a670-7b40480c3887"),
                First_Name = "Ashe",
                Last_Name = "Avarosa",
                Cpf = "33344455566",
                Phone = "11987654323",
                Email = "ashe@avarosa.com"
            });
            #endregion

        }
    }
}
