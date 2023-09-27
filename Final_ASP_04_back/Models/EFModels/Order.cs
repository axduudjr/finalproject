namespace Final_ASP_04_back.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BranchId { get; set; }

        public int RoomId { get; set; }

        public int Price { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int PaymentTypeId { get; set; }

        public DateTime OrderTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual Branch Branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Member Member { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual Room Room { get; set; }
    }
}
