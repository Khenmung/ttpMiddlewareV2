using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttpMiddleware.Models.DTOs.Requests
{
    public class Promote
    {
        public string Condition { get; set; }
        public int ExamId { get; set; }
        public string FailGradeId { get; set; }
        public short CurrentBatchId { get; set; }
        public short NextBatchId { get; set; }
    }
}
