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
        [StringLength(100)]
        public string QuestionName { get; set; }

        [Required]
        public DateTime? QuestionTime { get; set; }

        [Required]
        public int? vote { get; set; }

        [Required]
        public int? viewnum { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public int ans_ed { get; set; }

        [NotMapped]
        public List<Answer> answers { get; set; }

        [NotMapped]
        public Answer answer { get; set; }
    }
}
