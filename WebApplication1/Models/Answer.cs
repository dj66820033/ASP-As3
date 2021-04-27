namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int AnswerID { get; set; }

        [Required]
        [StringLength(5000)]
        public string AnswerText { get; set; }

        [Required]
        public DateTime? AnswerTime { get; set; }

        [Required]
        public int? QuestionID { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }
    }
}
