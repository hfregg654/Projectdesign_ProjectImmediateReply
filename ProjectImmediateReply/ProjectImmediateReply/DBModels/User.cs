namespace ProjectImmediateReply.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Grades = new HashSet<Grade>();
        }

        public int UserID { get; set; }

        [StringLength(255)]
        public string Account { get; set; }

        [StringLength(255)]
        public string PassWord { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        [StringLength(255)]
        public string LineID { get; set; }

        [StringLength(10)]
        public string ClassNumber { get; set; }

        [Required]
        [StringLength(16)]
        public string License { get; set; }

        public int? ProjectID { get; set; }

        public int? TeamID { get; set; }

        [StringLength(50)]
        public string TeamName { get; set; }

        [StringLength(255)]
        public string WorkID { get; set; }

        [Required]
        [StringLength(16)]
        public string Privilege { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string WhoCreate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(50)]
        public string WhoDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }

        public virtual Project Project { get; set; }
    }
}
