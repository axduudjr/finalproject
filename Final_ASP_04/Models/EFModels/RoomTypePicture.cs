namespace Final_ASP_04.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoomTypePicture
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomTypeName { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        public int DisplayOrder { get; set; }
    }
}
