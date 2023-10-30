using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawEmpEmployee
    {
        public RawEmpEmployee()
        {
            
        }

        [Key]
        public int EmpEmployeeId { get; set; }
        [StringLength(10)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(30)]
        public string FatherName { get; set; }
        [StringLength(30)]
        public string MotherName { get; set; }
        public int GenderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DOB { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DOJ { get; set; }
        public int? BloodgroupId { get; set; }
        public int? CategoryId { get; set; }
        [StringLength(20)]
        public string BankAccountNo { get; set; }
        [StringLength(15)]
        public string IFSCcode { get; set; }
        [StringLength(20)]
        public string MICRNo { get; set; }
        [StringLength(15)]
        public string AdhaarNo { get; set; }
        [StringLength(50)]
        public string PhotoPath { get; set; }
        public int? ReligionId { get; set; }
        [StringLength(50)]
        public string ContactNo { get; set; }
        [StringLength(30)]
        public string WhatsappNo { get; set; }
        [StringLength(30)]
        public string AlternateContactNo { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(30)]
        public string EmergencyContactNo { get; set; }
        public int? EmploymentStatusId { get; set; }
        public int? EmploymentTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
        public short? NoticePeriodDays { get; set; }
        public short? ProbationPeriodDays { get; set; }
        [StringLength(12)]
        public string PAN { get; set; }
        [StringLength(12)]
        public string PassportNo { get; set; }
        public int? MaritalStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MarriedDate { get; set; }
        [StringLength(20)]
        public string PFAccountNo { get; set; }
        public short OrgId { get; set; }
        public int? SubOrgId { get; set; }
        public int? NatureId { get; set; }
        [StringLength(30)]
        public string EmployeeCode { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(256)]
        public string PresentAddress { get; set; }
        public int? PresentAddressCityId { get; set; }
        public int? PresentAddressStateId { get; set; }
        public int? PresentAddressCountryId { get; set; }
        public int? PermanentAddressCityId { get; set; }
        public int? PermanentAddressStateId { get; set; }
        public int? PermanentAddressCountryId { get; set; }
        [StringLength(256)]
        public string PermanentAddress { get; set; }
        [StringLength(10)]
        public string PresentAddressPincode { get; set; }
        [StringLength(10)]
        public string PermanentAddressPincode { get; set; }
        public int? DepartmentId { get; set; }
        public int? EmpGradeId { get; set; }
        public int? DesignationId { get; set; }
        public int? WorkAccountId { get; set; }
        public bool Deleted { get; set; }
        [StringLength(100)]
        public string IDMark { get; set; }
        public int? Assistant1Id { get; set; }
        public int? Assistant2Id { get; set; }

       
    }
}
