using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttpMiddleware.Models.DTOs.Requests
{
    public class ImageProp
    {
        public int BatchId { get; set; }
        public string folderName { get; set; }
        public string fileOrPhoto { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public string orgName { get; set; }
        public int StudentId { get; set; }
        public int StudentClassId { get; set; }
        public int DocTypeId { get; set; }
        public int PageId { get; set; }
        public int ParentId { get; set; }


    }
}
