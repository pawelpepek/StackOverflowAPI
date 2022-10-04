﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflowAPI.Entities;

#nullable disable

namespace StackOverflowAPI.Migrations
{
    [DbContext(typeof(StackOverflowDbContext))]
    partial class StackOverflowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.Property<long>("QuestionsId")
                        .HasColumnType("bigint");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("QuestionTag");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Edited")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Message");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Message");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UsersDislikedPosts", b =>
                {
                    b.Property<long>("DislikedPostsId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserDislikesId")
                        .HasColumnType("int");

                    b.HasKey("DislikedPostsId", "UserDislikesId");

                    b.HasIndex("UserDislikesId");

                    b.ToTable("UsersDislikedPosts");
                });

            modelBuilder.Entity("UsersLikedPosts", b =>
                {
                    b.Property<long>("LikedPostsId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserLikesId")
                        .HasColumnType("int");

                    b.HasKey("LikedPostsId", "UserLikesId");

                    b.HasIndex("UserLikesId");

                    b.ToTable("UsersLikedPosts");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Comment", b =>
                {
                    b.HasBaseType("StackOverflowAPI.Entities.Message");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Post", b =>
                {
                    b.HasBaseType("StackOverflowAPI.Entities.Message");

                    b.HasDiscriminator().HasValue("Post");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Answer", b =>
                {
                    b.HasBaseType("StackOverflowAPI.Entities.Post");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.HasIndex("AuthorId");

                    b.HasIndex("QuestionId");

                    b.HasDiscriminator().HasValue("Answer");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Question", b =>
                {
                    b.HasBaseType("StackOverflowAPI.Entities.Post");

                    b.HasIndex("AuthorId");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowAPI.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UsersDislikedPosts", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("DislikedPostsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("StackOverflowAPI.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserDislikesId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UsersLikedPosts", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("LikedPostsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("StackOverflowAPI.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserLikesId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Comment", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowAPI.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Answer", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.User", "Author")
                        .WithMany("Answers")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowAPI.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Question", b =>
                {
                    b.HasOne("StackOverflowAPI.Entities.User", "Author")
                        .WithMany("Questions")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.User", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Comments");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("StackOverflowAPI.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
