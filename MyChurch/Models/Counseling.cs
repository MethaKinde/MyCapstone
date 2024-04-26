namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counseling
    {
        [key]
        public int ID { get; set; }

        public int? Member { get; set; }

        [StringLength(100)]
        public string Service { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        public DateTime? DateEndTime { get; set; }

        public string Description { get; set; }

        public virtual Member Member1 { get; set; }
    }
}
