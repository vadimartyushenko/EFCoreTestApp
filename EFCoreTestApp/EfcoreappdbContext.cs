using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFCoreTestApp
{
	public partial class EfcoreappdbContext : DbContext
	{
		private readonly StreamWriter _logStream = new ("log.txt", true);
		
		public virtual DbSet<BlogUser> BlogUsers { get; set; }

		public EfcoreappdbContext(DbContextOptions<EfcoreappdbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
				if (!optionsBuilder.IsConfigured)
				{
					optionsBuilder.UseSqlServer("Server=VM0129-ORACLE\\SQLEXPRESS;Database=efcoreappdb;Trusted_Connection=True;");
				}

				optionsBuilder.LogTo(_logStream.WriteLine);
		}

		public override void Dispose()
		{
			base.Dispose();
			_logStream.Dispose();
		}

		public override async ValueTask DisposeAsync()
		{
			await base.DisposeAsync();
			await _logStream.DisposeAsync();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

			modelBuilder.Entity<BlogUser>(entity =>
			{
				entity.HasKey(e => e.UserLogin)
									.HasName("PK_BlogUser_UserLogin");

				entity.ToTable("BlogUser");

				entity.Property(e => e.UserLogin)
									.HasMaxLength(50)
									.IsUnicode(false);

				entity.Property(e => e.Email)
									.IsRequired()
									.HasMaxLength(50)
									.IsUnicode(false);

				entity.Property(e => e.Name)
									.IsRequired()
									.HasMaxLength(50);

				entity.Property(e => e.Password)
									.IsRequired()
									.HasMaxLength(50)
									.IsUnicode(false);

				entity.Property(e => e.Patronymic).HasMaxLength(50);

				entity.Property(e => e.RegistrationDate)
									.HasColumnType("datetime")
									.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Surname)
									.IsRequired()
									.HasMaxLength(50);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
