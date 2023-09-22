namespace Final_ASP_04.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BranchId { get; set; }

        public int RoomId { get; set; }

        public int Price { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? PaymentTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Member Member { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual Room Room { get; set; }
    }
}
