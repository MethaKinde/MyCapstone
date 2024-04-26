namespace MyChurch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDEvent { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? EventTypeID { get; set; }

        public string FlyerEvent { get; set; }

        public virtual EventType EventType { get; set; }

        //// Proprietà per la data di inizio formattata
        //public string DateStartFormatted { get; set; }

        //// Proprietà per la data di fine formattata, se necessario
        //public string DateEndFormatted { get; set; }
    }
}
