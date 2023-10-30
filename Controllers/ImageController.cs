
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using ttpMiddleware.Models.DTOs;
using ttpMiddleware.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ttpMiddleware.Controllers
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class ImageController : ControllerBase
    {
        public ImageController(
            IRequestObject requestobject,
            ttpauthContext db,
            IWebHostEnvironment hostEnvironment)
        {
            this.requestobject = requestobject;
            this.db = db;
            this.hostEnvironment = hostEnvironment;
        }
        string errorPath = ConfigurationManager.AppSettings["dev"];
        private readonly IRequestObject requestobject;
        private readonly ttpauthContext db;
        private readonly IWebHostEnvironment hostEnvironment;

        [HttpPost]
        [Route("uploadimage")]
        public async Task<ActionResult> UploadImage()
        {
            short orgId = 0;
            int subOrgId = 0;
            int parentId = 0;
            string response = "";
            string imageName = null;
            var _StudentId = 0;
            var StudentClassId = 0;
            var DocTypeId = 0;
            var PageId = 0;
            var _categoryId = 0;
            StringBuilder sb = new StringBuilder();
            var httpRequest = requestobject.getobject().HttpContext.Request;// HttpContext.Current.Request;
            var batch = httpRequest.Form["BatchId"].ToString() == "" ? "0" : httpRequest.Form["BatchId"].ToString();
            var folderName = httpRequest.Form["folderName"];
            var fileOrPhoto = httpRequest.Form["fileOrPhoto"];
            var description = httpRequest.Form["description"];
            var postedFile = httpRequest.Form.Files[0];

            if (!string.IsNullOrEmpty(httpRequest.Form["orgId"]))
                orgId = Convert.ToInt16(httpRequest.Form["orgId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["subOrgId"]))
                subOrgId = Convert.ToInt32(httpRequest.Form["subOrgId"]);
            if (httpRequest.Form["StudentId"].ToString() != "")
                _StudentId = Convert.ToInt32(httpRequest.Form["StudentId"]);
            if (string.IsNullOrEmpty(httpRequest.Form["StudentClassId"]))
                StudentClassId = Convert.ToInt32(httpRequest.Form["StudentClassId"]);
            if (string.IsNullOrEmpty(httpRequest.Form["DocTypeId"]))
                DocTypeId = Convert.ToInt32(httpRequest.Form["DocTypeId"]);
            if (string.IsNullOrEmpty(httpRequest.Form["PageId"]))
                PageId = Convert.ToInt32(httpRequest.Form["PageId"]);

            if (string.IsNullOrEmpty(httpRequest.Form["parentId"]))
                parentId = Convert.ToInt16(httpRequest.Form["parentId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["categoryId"]))
                _categoryId = Convert.ToInt32(httpRequest.Form["categoryId"]);
            //FilesNPhoto fileNPhoto = null;
            //using (TTPEntities db = new TTPEntities())
            //{
            if (parentId == 0)
            {
                var storageFnP = new StorageFnP()
                {
                    UpdatedFileFolderName = folderName,
                    FileOrFolder = 1,//Convert.ToByte(fileOrFolder),
                    FileOrPhoto = Convert.ToByte(fileOrPhoto),
                    FileName = folderName,
                    Active = 1,
                    OrgId = orgId,
                    SubOrgId = subOrgId,
                    ParentId = 0
                };
                db.StorageFnPs.Add(storageFnP);
                db.SaveChanges();
                parentId = storageFnP.FileId;
            }
            //}
            var fileDir = HttpContext.Request.PathBase + folderName;
            var photoPath = "Image/" + folderName;

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            try
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(20).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                response = imageName;
                var filepath = fileDir + "/" + imageName;
                //foreach (var formFile in postedFile)
                //{
                //    if (formFile.Length > 0)
                //    {
                var serverFilePath = Path.Combine(hostEnvironment.ContentRootPath, filepath);

                using (var stream = System.IO.File.Create(serverFilePath))
                {
                    await postedFile.CopyToAsync(stream);
                }
                //    }
                //}
                if (_StudentId > 0)
                {
                    Student student = db.Students.First(s => s.StudentId == _StudentId);
                    student.Photo = imageName;
                    db.SaveChanges();
                }
                else if (PageId > 0)
                {
                    Page page = db.Pages.First(s => s.PageId == PageId);
                    page.PhotoPath = imageName;
                    db.SaveChanges();
                }
                else
                {
                    StorageFnP file = new StorageFnP()
                    {
                        ParentId = parentId,
                        Description = description,
                        FileName = imageName,
                        UpdatedFileFolderName = imageName,
                        FileOrPhoto = Convert.ToByte(fileOrPhoto),
                        FileOrFolder = 0,
                        BatchId = Convert.ToInt16(batch),
                        StudentId = _StudentId,
                        CategoryId = _categoryId,
                        StudentClassId = Convert.ToInt32(StudentClassId),
                        DocTypeId = Convert.ToInt16(DocTypeId),
                        Active = 1,
                        OrgId=orgId,
                        SubOrgId=subOrgId,
                        UploadDate = DateTime.Now,
                        CreatedDate = DateTime.Now
                    };
                    if (folderName == "organization logo")
                    {
                        var _org = db.Organizations.Where(x => x.OrganizationId == orgId).ToList();
                        foreach (var item in _org)
                        {
                            item.LogoPath = imageName;
                            db.Organizations.Update(item);
                        }
                    }
                    db.StorageFnPs.Add(file);
                    db.SaveChanges();
                }
                //}
            }
            catch (Exception e)
            {

                System.IO.File.AppendAllText(errorPath, e.StackTrace);
                //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);

                throw;
            }
            //}
            return Ok();
        }

        [HttpPost]
        [Route("uploadimages")]
        public async Task<IActionResult> UploadImages()
        {
            short orgId = 0;
            int subOrgId = 0;
            var studentId = 0;
            var employeeId = 0;
            var studentClassId = 0;
            var docTypeId = 0;
            var pageId = 0;
            var parentId = 0;
            var _categoryId = 0;
            var questionId = 0;
            string imageName = null;
            StringBuilder sb = new StringBuilder();
            var httpRequest = requestobject.getobject().HttpContext.Request;
            var folderName = httpRequest.Form["folderName"].ToString();
            var fileOrPhoto = httpRequest.Form["fileOrPhoto"].ToString();
            var fileName = httpRequest.Form["fileName"].ToString();
            var orgName = httpRequest.Form["orgName"].ToString();
            var batchId = httpRequest.Form["batchId"].ToString() == "" ? "0" : httpRequest.Form["batchId"].ToString();
            var description = httpRequest.Form["description"];
            if (!string.IsNullOrEmpty(httpRequest.Form["studentId"]))
                studentId = Convert.ToInt32(httpRequest.Form["studentId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["orgId"]))
                orgId = Convert.ToInt16(httpRequest.Form["orgId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["subOrgId"]))
                subOrgId = Convert.ToInt32(httpRequest.Form["subOrgId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["employeeId"]))
                employeeId = Convert.ToInt32(httpRequest.Form["employeeId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["studentClassId"]))
                studentClassId = Convert.ToInt32(httpRequest.Form["studentClassId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["docTypeId"]))
                docTypeId = Convert.ToInt32(httpRequest.Form["docTypeId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["pageId"]))
                pageId = Convert.ToInt32(httpRequest.Form["pageId"]);
            if (!string.IsNullOrEmpty(httpRequest.Form["questionId"]))
                questionId = Convert.ToInt32(httpRequest.Form["questionId"]);

            if (!string.IsNullOrEmpty(httpRequest.Form["parentId"]))
                parentId = Convert.ToInt16(httpRequest.Form["parentId"]);
            
            if (!string.IsNullOrEmpty(httpRequest.Form["categoryId"]))
                _categoryId = Convert.ToInt32(httpRequest.Form["categoryId"]);
            
            using var tran = db.Database.BeginTransaction();
            try
            {
                var fileDir = Path.Combine(hostEnvironment.ContentRootPath, "Uploads\\" + orgName + "\\" + folderName);
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                if (parentId == 0)
                {
                    StorageFnP storageFnP = new StorageFnP()
                    {
                        UpdatedFileFolderName = folderName,
                        FileOrFolder = 1,
                        FileOrPhoto = Convert.ToByte(fileOrPhoto),
                        FileName = folderName,
                        Active = 1,
                        OrgId = orgId,
                        SubOrgId = subOrgId,
                        ParentId = 0,

                    };
                    db.StorageFnPs.Add(storageFnP);
                    db.SaveChanges();
                    parentId = storageFnP.FileId;
                }

                foreach (var fName in httpRequest.Form.Files)
                {
                    var postedFile = fName;// httpRequest.Form.Files[fName];
                    imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-") + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

                    imageName = imageName + Path.GetExtension(postedFile.FileName);
                    var filepath = fileDir + "\\" + imageName;

                    try
                    {
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await fName.CopyToAsync(stream);
                        }

                        StorageFnP file = new StorageFnP()
                        {
                            ParentId = parentId,
                            Description = description,
                            FileName = imageName,
                            UpdatedFileFolderName = imageName,
                            FileOrFolder = 0,
                            FileOrPhoto = Convert.ToByte(fileOrPhoto),
                            Active = 1,
                            QuestionId = questionId,
                            CategoryId = _categoryId,
                            StudentId = studentId,
                            StudentClassId = studentClassId,
                            EmployeeId = employeeId,
                            PageId = pageId,
                            OrgId = orgId,
                            SubOrgId = subOrgId,
                            BatchId = Convert.ToInt16(batchId),
                            DocTypeId = docTypeId,
                            UploadDate = DateTime.Now,
                            CreatedDate = DateTime.Now
                        };
                        //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", "\n" + albumId.ToString()+ ":" + DateTime.Now);
                        db.StorageFnPs.Add(file);
                        if (folderName == "organization logo")
                        {
                            var _org = db.Organizations.Where(x => x.OrganizationId == orgId).ToList();
                            foreach (var item in _org)
                            {
                                item.LogoPath = imageName;
                                db.Organizations.Update(item);
                            }
                        }
                        db.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        System.IO.File.AppendAllText(errorPath, e.StackTrace);
                        //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);
                        throw;
                    }
                }//foreach file save
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                System.IO.File.AppendAllText(errorPath, e.StackTrace);
                //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);
                throw;
            }
            return Ok();
        }
    }
}
