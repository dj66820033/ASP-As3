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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }

        [StringLength(5000)]
        public string AnswerText { get; set; }

        public DateTime? AnswerTime { get; set; }

        public int? QuestionID { get; set; }

        [StringLength(50)]
        public string username { get; set; }
    }
}
