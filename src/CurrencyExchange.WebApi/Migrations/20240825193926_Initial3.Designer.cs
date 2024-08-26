﻿// <auto-generated />
using CurrencyExchange.Persistanse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CurrencyExchange.WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240825193926_Initial3")]
    partial class Initial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CurrencyExchange.Core.Currencies.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CurrencyExchange.Core.Currencies.CurrencyExchangeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("ExchangedCurrencyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ExchangedCurrencyId");

                    b.ToTable("CurrencyExchangeValue");
                });

            modelBuilder.Entity("CurrencyExchange.Core.Wallets.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Capacity")
                        .HasColumnType("numeric");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("CurrencyExchange.Core.Currencies.CurrencyExchangeValue", b =>
                {
                    b.HasOne("CurrencyExchange.Core.Currencies.Currency", null)
                        .WithMany("AllowedToConvert")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CurrencyExchange.Core.Currencies.Currency", "ExchangedCurrency")
                        .WithMany()
                        .HasForeignKey("ExchangedCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExchangedCurrency");
                });

            modelBuilder.Entity("CurrencyExchange.Core.Wallets.Wallet", b =>
                {
                    b.HasOne("CurrencyExchange.Core.Currencies.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("CurrencyExchange.Core.Currencies.Currency", b =>
                {
                    b.Navigation("AllowedToConvert");
                });
#pragma warning restore 612, 618
        }
    }
}
