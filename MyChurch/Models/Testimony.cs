namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Testimony
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ID { get; set; }

        public int? Member { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfTestimony { get; set; }

        public string Text { get; set; }

        public virtual Member Member1 { get; set; }
    }
}
