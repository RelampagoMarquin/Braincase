﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(APIDbContext))]
    partial class APIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api.Models.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Justify")
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Api.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Api.Models.Favorites", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Own")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Api.Models.Institution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Institution");
                });

            modelBuilder.Entity("Api.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte>("Dificult")
                        .HasColumnType("tinyint unsigned");

                    b.Property<Guid?>("InstitutionId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Api.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Api.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Api.Models.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUse")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("char(36)");

                    b.HasKey("QuestionId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("QuestionTag");
                });

            modelBuilder.Entity("QuestionTest", b =>
                {
                    b.Property<Guid>("QuestionsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TestsId")
                        .HasColumnType("char(36)");

                    b.HasKey("QuestionsId", "TestsId");

                    b.HasIndex("TestsId");

                    b.ToTable("QuestionTest");
                });

            modelBuilder.Entity("Api.Models.Answer", b =>
                {
                    b.HasOne("Api.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Api.Models.Comment", b =>
                {
                    b.HasOne("Api.Models.Question", "Question")
                        .WithMany("Comment")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Models.Favorites", b =>
                {
                    b.HasOne("Api.Models.Question", "Question")
                        .WithMany("Favorites")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Models.Question", b =>
                {
                    b.HasOne("Api.Models.Institution", "Institution")
                        .WithMany("questions")
                        .HasForeignKey("InstitutionId");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("Api.Models.Tag", b =>
                {
                    b.HasOne("Api.Models.Subject", "Subject")
                        .WithMany("Tags")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Api.Models.Test", b =>
                {
                    b.HasOne("Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.HasOne("Api.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuestionTest", b =>
                {
                    b.HasOne("Api.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Institution", b =>
                {
                    b.Navigation("questions");
                });

            modelBuilder.Entity("Api.Models.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Comment");

                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("Api.Models.Subject", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favorites");
                });
#pragma warning restore 612, 618
        }
    }
}
