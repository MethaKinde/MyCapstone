namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sermon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string Photo { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime? FullDate { get; set; }

        [StringLength(255)]
        public string Preacher { get; set; }
    }
}
