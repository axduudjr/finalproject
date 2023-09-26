using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Final_ASP_04.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Branch> Branches { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<FAQ> FAQs { get; set; }
		public virtual DbSet<GuestNumber> GuestNumbers { get; set; }
		public virtual DbSet<Mail> Mails { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<PaymentType> PaymentTypes { get; set; }
		public virtual DbSet<Room> Rooms { get; set; }
		public virtual DbSet<RoomTypePicture> RoomTypePictures { get; set; }
		public virtual DbSet<RoomType> RoomTypes { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<BranchPicture> BranchPictures { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Branch>()
				.Property(e => e.PhoneNumber)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Branch>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Branch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Branch>()
				.HasMany(e => e.Discounts)
				.WithRequired(e => e.Branch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Branch>()
				.HasMany(e => e.News)
				.WithRequired(e => e.Branch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Branch>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Branch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Branch>()
				.HasMany(e => e.RoomTypes)
				.WithRequired(e => e.Branch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<City>()
				.HasMany(e => e.Branches)
				.WithRequired(e => e.City)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<GuestNumber>()
				.HasMany(e => e.Rooms)
				.WithRequired(e => e.GuestNumber)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Account)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.EncryptedPassword)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.PhoneNumber)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Mails)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.Comments)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PaymentType>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.PaymentType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<RoomType>()
				.HasMany(e => e.Rooms)
				.WithRequired(e => e.RoomType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Account)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);
		}
	}
}
