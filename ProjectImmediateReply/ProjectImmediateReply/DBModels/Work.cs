namespace ProjectImmediateReply.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Work
    {
        public int ProjectID { get; set; }

        public int TeamID { get; set; }

        public int WorkID { get; set; }

        [Column("Work")]
        [Required]
        [StringLength(255)]
        public string Work1 { get; set; }

        public string WorkDescription { get; set; }

        public DateTime DeadLine { get; set; }

        public string FilePath { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? SpendTime { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string WhoCreate { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(50)]
        public string WhoDelete { get; set; }

        public virtual Project Project { get; set; }
    }
}
