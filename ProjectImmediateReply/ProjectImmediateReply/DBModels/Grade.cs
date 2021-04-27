namespace ProjectImmediateReply.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grade
    {
        public int GradeID { get; set; }

        public int UserID { get; set; }

        public byte PresidentProjectGrade { get; set; }

        public byte PresidentInterviewGrade { get; set; }

        public string PresidentComments { get; set; }

        public byte PMProjectGrade { get; set; }

        public byte PMInterviewGrade { get; set; }

        public string PMComments { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string WhoCreate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(50)]
        public string WhoDelete { get; set; }

        public virtual User User { get; set; }
    }
}
