namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ReceiveOrders = new HashSet<ReceiveOrder>();
        }

        public int OrderID { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int EmployeeID { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal TaxGST { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public int VendorID { get; set; }

        public bool Closed { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Vendor Vendor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiveOrder> ReceiveOrders { get; set; }
    }
}
