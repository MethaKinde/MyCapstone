namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}
