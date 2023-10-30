using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("QuestionBankNExam")]
    public class RawQuestionBankNExam
    {
        [Key]
        public int QuestionBankNExamId { get; set; }
        public int QuestionBankId { get; set; }
        public short ExamId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
