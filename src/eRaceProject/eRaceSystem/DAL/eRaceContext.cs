using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using eRaceSystem.Entities;

namespace eRaceSystem.DAL
{
    internal partial class eRaceContext : DbContext
    {
        public eRaceContext()
            : base("name=eRaceDB")
        {
        }

        public virtual DbSet<CarClass> CarClasses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<DatabaseVersion> DatabaseVersions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RaceDetail> RaceDetails { get; set; }
        public virtual DbSet<RaceFee> RaceFees { get; set; }
        public virtual DbSet<RacePenalty> RacePenalties { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<ReceiveOrderItem> ReceiveOrderItems { get; set; }
        public virtual DbSet<ReceiveOrder> ReceiveOrders { get; set; }
        public virtual DbSet<ReturnOrderItem> ReturnOrderItems { get; set; }
        public virtual DbSet<StoreRefund> StoreRefunds { get; set; }
        public virtual DbSet<UnOrderedItem> UnOrderedItems { get; set; }
        public virtual DbSet<VendorCatalog> VendorCatalogs { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarClass>()
                .Property(e => e.CarClassName)
                .IsUnicode(false);

            modelBuilder.Entity<CarClass>()
                .Property(e => e.MaxEngineSize)
                .HasPrecision(4, 1);

            modelBuilder.Entity<CarClass>()
                .Property(e => e.CertificationLevel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CarClass>()
                .Property(e => e.RaceRentalFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CarClass>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CarClass>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.CarClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Ownership)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Certification>()
                .Property(e => e.CertificationLevel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Certification>()
                .HasMany(e => e.CarClasses)
                .WithRequired(e => e.Certification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Certification>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Certification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Certification>()
                .HasMany(e => e.Races)
                .WithRequired(e => e.Certification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LoginId)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SocialInsuranceNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ReceiveOrders)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.GST)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.StoreRefunds)
                .WithRequired(e => e.RefundInvoice)
                .HasForeignKey(e => e.InvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.ReturnedItems)
                .WithRequired(e => e.OriginalInvoice)
                .HasForeignKey(e => e.OriginalInvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.CertificationLevel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.RaceDetails)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Cost)
                .HasPrecision(10, 4);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(e => e.ReceiveOrderItems)
                .WithRequired(e => e.OrderDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.TaxGST)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ReceiveOrders)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Wage)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ItemPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ReStockCharge)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.StoreRefunds)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.VendorCatalogs)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RaceDetail>()
                .Property(e => e.RaceFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RaceDetail>()
                .Property(e => e.RentalFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RaceFee>()
                .Property(e => e.Fee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Race>()
                .Property(e => e.Run)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Race>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Race>()
                .Property(e => e.CertificationLevel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Race>()
                .HasMany(e => e.RaceDetails)
                .WithRequired(e => e.Race)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReceiveOrder>()
                .HasMany(e => e.ReceiveOrderItems)
                .WithRequired(e => e.ReceiveOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReceiveOrder>()
                .HasMany(e => e.ReturnOrderItems)
                .WithRequired(e => e.ReceiveOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VendorCatalog>()
                .Property(e => e.OrderUnitType)
                .IsUnicode(false);

            modelBuilder.Entity<VendorCatalog>()
                .Property(e => e.OrderUnitCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Vendor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendor>()
                .HasMany(e => e.VendorCatalogs)
                .WithRequired(e => e.Vendor)
                .WillCascadeOnDelete(false);
        }
    }
}
