namespace Final_ASP_04_back.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrafficInfos")]
    public partial class TrafficInfo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
