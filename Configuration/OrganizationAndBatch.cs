using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttpMiddleware.Configuration
{
    public class OrganizationAndBatch
    {
        public int OrgId { get; set; }
        public int SubOrgId { get; set; }
        public int BatchId { get; set; }
        public int StudentClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
    }
}
