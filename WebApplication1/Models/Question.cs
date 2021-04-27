namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionName { get; set; }

        [Required]
        public DateTime? QuestionTime { get; set; }

        [Required]
        public int? vote { get; set; }

        [Required]
        public int? viewnum { get; set; }

        [Required]
        public int? CategoryID { get; set; }
    }
}
