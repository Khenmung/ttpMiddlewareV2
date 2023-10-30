using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Index(nameof(HouseId), Name = "idx_HouseId")]
    public class RawStudent
    {
      

        [Key]
        public int StudentId { get; set; }
        public int PID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FatherName { get; set; }
        [StringLength(50)]
        public string MotherName { get; set; }
        public int? GenderId { get; set; }
        [StringLength(250)]
        public string PermanentAddress { get; set; }
        [StringLength(250)]
        public string PresentAddress { get; set; }
        [StringLength(15)]
        public string WhatsAppNumber { get; set; }
        public int? PermanentAddressCityId { get; set; }
        [StringLength(10)]
        public string PermanentAddressPincode { get; set; }
        public int? PermanentAddressStateId { get; set; }
        public int? PermanentAddressCountryId { get; set; }
        public int? PresentAddressCityId { get; set; }
        public int? PresentAddressStateId { get; set; }
        public int? PresentAddressCountryId { get; set; }
        [StringLength(10)]
        public string PresentAddressPincode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DOB { get; set; }
        public int? BloodgroupId { get; set; }
        public int? CategoryId { get; set; }
        [StringLength(30)]
        public string AccountHolderName { get; set; }
        [StringLength(30)]
        public string BankAccountNo { get; set; }
        [StringLength(30)]
        public string IFSCCode { get; set; }
        [StringLength(30)]
        public string MICRNo { get; set; }
        [StringLength(30)]
        public string AdhaarNo { get; set; }
        [StringLength(50)]
        public string Photo { get; set; }
        public int? ReligionId { get; set; }
        [StringLength(32)]
        public string PersonalNo { get; set; }
        [StringLength(32)]
        public string AlternateContact { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public short? ClassAdmissionSought { get; set; }
        [StringLength(10)]
        public string LastSchoolPercentage { get; set; }
        [StringLength(100)]
        public string TransferFromSchool { get; set; }
        [StringLength(100)]
        public string TransferFromSchoolBoard { get; set; }
        [StringLength(100)]
        public string FatherOccupation { get; set; }
        [StringLength(20)]
        public string FatherContactNo { get; set; }
        [StringLength(20)]
        public string MotherContactNo { get; set; }
        [StringLength(100)]
        public string MotherOccupation { get; set; }
        public int? PrimaryContactFatherOrMother { get; set; }
        [StringLength(30)]
        public string NameOfContactPerson { get; set; }
        [StringLength(30)]
        public string RelationWithContactPerson { get; set; }
        [StringLength(20)]
        public string ContactPersonContactNo { get; set; }
        public byte Active { get; set; }
        public byte? StudentDeclaration { get; set; }
        public byte? ParentDeclaration { get; set; }
        public int? ReasonForLeavingId { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public short? BatchId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        public bool Deleted { get; set; }
        public int? ClubId { get; set; }
        public int? HouseId { get; set; }
        public int? RemarkId { get; set; }
        public int? AdmissionStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(50)]
        public string IdentificationMark { get; set; }
        [StringLength(12)]
        public string Weight { get; set; }
        [StringLength(12)]
        public string Height { get; set; }
        public int? SectionId { get; set; }
        [StringLength(30)]
        public string RollNo { get; set; }
        [StringLength(15)]
        public string AdmissionNo { get; set; }
        [StringLength(15)]
        public string BoardRegistrationNo { get; set; }
        [StringLength(50)]
        public string Notes { get; set; }

       
    }
}
