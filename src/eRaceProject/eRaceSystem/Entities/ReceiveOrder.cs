namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class ReceiveOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceiveOrder()
        {
            ReceiveOrderItems = new HashSet<ReceiveOrderItem>();
            ReturnOrderItems = new HashSet<ReturnOrderItem>();
        }

        public int ReceiveOrderID { get; set; }

        public int OrderID { get; set; }

        public DateTime ReceiveDate { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Order Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiveOrderItem> ReceiveOrderItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnOrderItem> ReturnOrderItems { get; set; }
    }
}
