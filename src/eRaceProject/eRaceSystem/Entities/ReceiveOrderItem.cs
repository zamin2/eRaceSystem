namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class ReceiveOrderItem
    {
        public int ReceiveOrderItemID { get; set; }

        public int ReceiveOrderID { get; set; }

        public int OrderDetailID { get; set; }

        public int ItemQuantity { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}
