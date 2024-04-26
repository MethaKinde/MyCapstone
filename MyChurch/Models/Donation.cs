namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string FullNameDonator { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? DonationDate { get; set; }

        public string Description { get; set; }
    }
}
