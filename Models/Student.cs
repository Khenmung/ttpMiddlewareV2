using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(PID), nameof(StudentId), nameof(ClassId), nameof(Deleted), nameof(History), Name = "Indx_StudentOrgId_StudentId_ParentId")]
    public partial class Student
    {
        public Student()
        {
            StorageFnPs = new HashSet<StorageFnP>();
            StudentActivities = new HashSet<StudentActivity>();
            StudentAdditionals = new HashSet<StudentAdditional>();
            StudentClasses = new HashSet<StudentClass>();
            StudentFamilyNFriends = new HashSet<StudentFamilyNFriend>();
        }

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
        [StringLength(21)]
        public string FatherContactNo { get; set; }
        [StringLength(21)]
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
        [StringLength(256)]
        public string Notes { get; set; }
        public int SemesterId { get; set; }
        public short FeeTypeId { get; set; }
        public int Remark2Id { get; set; }
        public int ClassId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }
        [StringLength(11)]
        public string PEN { get; set; }
        [StringLength(10)]
        public string BoardRollNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeactivatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReActivatedDate { get; set; }

        [ForeignKey(nameof(BloodgroupId))]
        [InverseProperty(nameof(MasterItem.StudentBloodgroups))]
        public virtual MasterItem Bloodgroup { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(MasterItem.StudentCategories))]
        public virtual MasterItem Category { get; set; }
        [ForeignKey(nameof(GenderId))]
        [InverseProperty(nameof(MasterItem.StudentGenders))]
        public virtual MasterItem Gender { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.Students))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(PermanentAddressCityId))]
        [InverseProperty(nameof(MasterItem.StudentPermanentAddressCities))]
        public virtual MasterItem PermanentAddressCity { get; set; }
        [ForeignKey(nameof(PermanentAddressCountryId))]
        [InverseProperty(nameof(MasterItem.StudentPermanentAddressCountries))]
        public virtual MasterItem PermanentAddressCountry { get; set; }
        [ForeignKey(nameof(PermanentAddressStateId))]
        [InverseProperty(nameof(MasterItem.StudentPermanentAddressStates))]
        public virtual MasterItem PermanentAddressState { get; set; }
        [ForeignKey(nameof(ReasonForLeavingId))]
        [InverseProperty(nameof(MasterItem.StudentReasonForLeavings))]
        public virtual MasterItem ReasonForLeaving { get; set; }
        [ForeignKey(nameof(ReligionId))]
        [InverseProperty(nameof(MasterItem.StudentReligions))]
        public virtual MasterItem Religion { get; set; }
        [InverseProperty(nameof(StorageFnP.Student))]
        public virtual ICollection<StorageFnP> StorageFnPs { get; set; }
        [InverseProperty(nameof(StudentActivity.Student))]
        public virtual ICollection<StudentActivity> StudentActivities { get; set; }
        [InverseProperty(nameof(StudentAdditional.Student))]
        public virtual ICollection<StudentAdditional> StudentAdditionals { get; set; }
        [InverseProperty(nameof(StudentClass.Student))]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        [InverseProperty(nameof(StudentFamilyNFriend.Student))]
        public virtual ICollection<StudentFamilyNFriend> StudentFamilyNFriends { get; set; }
    }
}
