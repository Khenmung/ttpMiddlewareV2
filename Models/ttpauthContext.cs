using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ttpMiddleware.Data.Entities;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class ttpauthContext : IdentityDbContext<ApplicationUser>
    {
        public ttpauthContext()
        {
        }

        public ttpauthContext(DbContextOptions<ttpauthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountNature> AccountNatures { get; set; }
        public virtual DbSet<AccountingLedgerTrialBalance> AccountingLedgerTrialBalances { get; set; }
        public virtual DbSet<AccountingVoucher> AccountingVouchers { get; set; }
        public virtual DbSet<AchievementAndPoint> AchievementAndPoints { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<ApplicationFeatureRolesPerm> ApplicationFeatureRolesPerms { get; set; }
        public virtual DbSet<ApplicationPrice> ApplicationPrices { get; set; }
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AttendanceReport> AttendanceReports { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<CertificateConfig> CertificateConfigs { get; set; }
        public virtual DbSet<ClassEvaluation> ClassEvaluations { get; set; }
        public virtual DbSet<ClassEvaluationOption> ClassEvaluationOptions { get; set; }
        public virtual DbSet<ClassFee> ClassFees { get; set; }
        public virtual DbSet<ClassGroup> ClassGroups { get; set; }
        public virtual DbSet<ClassGroupMapping> ClassGroupMappings { get; set; }
        public virtual DbSet<ClassMaster> ClassMasters { get; set; }
        public virtual DbSet<ClassSubject> ClassSubjects { get; set; }
        public virtual DbSet<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        public virtual DbSet<CourseYearSemester> CourseYearSemesters { get; set; }
        public virtual DbSet<CustomFeature> CustomFeatures { get; set; }
        public virtual DbSet<CustomFeatureRolePermission> CustomFeatureRolePermissions { get; set; }
        public virtual DbSet<CustomerInvoice> CustomerInvoices { get; set; }
        public virtual DbSet<CustomerInvoiceComponent> CustomerInvoiceComponents { get; set; }
        public virtual DbSet<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
        public virtual DbSet<CustomerPlan> CustomerPlans { get; set; }
        public virtual DbSet<CustomerPlanFeature> CustomerPlanFeatures { get; set; }
        public virtual DbSet<DataSync> DataSyncs { get; set; }
        public virtual DbSet<DynamicTable> DynamicTables { get; set; }
        public virtual DbSet<DynamicTableValue> DynamicTableValues { get; set; }
        public virtual DbSet<EmpComponent> EmpComponents { get; set; }
        public virtual DbSet<EmpEmployee> EmpEmployees { get; set; }
        public virtual DbSet<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistories { get; set; }
        public virtual DbSet<EmpEmployeeGroup> EmpEmployeeGroups { get; set; }
        public virtual DbSet<EmpEmployeeSalaryComponent> EmpEmployeeSalaryComponents { get; set; }
        public virtual DbSet<EmpEmployeeSkill> EmpEmployeeSkills { get; set; }
        public virtual DbSet<EmpManagerGroupMapping> EmpManagerGroupMappings { get; set; }
        public virtual DbSet<EmpWorkHistory> EmpWorkHistories { get; set; }
        public virtual DbSet<EmployeeActivity> EmployeeActivities { get; set; }
        public virtual DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual DbSet<EmployeeEducationHistory> EmployeeEducationHistories { get; set; }
        public virtual DbSet<EmployeeFamily> EmployeeFamilies { get; set; }
        public virtual DbSet<EmployeeMonthlySalary> EmployeeMonthlySalaries { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<EvaluationExamMap> EvaluationExamMaps { get; set; }
        public virtual DbSet<EvaluationMaster> EvaluationMasters { get; set; }
        public virtual DbSet<EvaluationName> EvaluationNames { get; set; }
        public virtual DbSet<EvaluationResultMark> EvaluationResultMarks { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamClassGroupMap> ExamClassGroupMaps { get; set; }
        public virtual DbSet<ExamMarkConfig> ExamMarkConfigs { get; set; }
        public virtual DbSet<ExamNCalculate> ExamNCalculates { get; set; }
        public virtual DbSet<ExamResultSubjectMark> ExamResultSubjectMarks { get; set; }
        public virtual DbSet<ExamSlot> ExamSlots { get; set; }
        public virtual DbSet<ExamStudentResult> ExamStudentResults { get; set; }
        public virtual DbSet<ExamStudentSubjectResult> ExamStudentSubjectResults { get; set; }
        public virtual DbSet<FeeDefinition> FeeDefinitions { get; set; }
        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; }
        public virtual DbSet<GeneratedCertificate> GeneratedCertificates { get; set; }
        public virtual DbSet<GroupActivityParticipant> GroupActivityParticipants { get; set; }
        public virtual DbSet<GroupPoint> GroupPoints { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<InvoiceComponent> InvoiceComponents { get; set; }
        public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }
        public virtual DbSet<LeaveEmployeeLeaf> LeaveEmployeeLeaves { get; set; }
        public virtual DbSet<LeavePolicy> LeavePolicies { get; set; }
        public virtual DbSet<LedgerPosting> LedgerPostings { get; set; }
        public virtual DbSet<MasterItem> MasterItems { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<OrgPaymentDetail> OrgPaymentDetails { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationPayment> OrganizationPayments { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageHistory> PageHistories { get; set; }
        public virtual DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanAndMasterItem> PlanAndMasterItems { get; set; }
        public virtual DbSet<PlanFeature> PlanFeatures { get; set; }
        public virtual DbSet<QuestionBank> QuestionBanks { get; set; }
        public virtual DbSet<QuestionBankNExam> QuestionBankNExams { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<ReportConfigItem> ReportConfigItems { get; set; }
        public virtual DbSet<ReportOrgReportColumn> ReportOrgReportColumns { get; set; }
        public virtual DbSet<ReportOrgReportName> ReportOrgReportNames { get; set; }
        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<RulesOrPolicy> RulesOrPolicies { get; set; }
        public virtual DbSet<SchoolClassPeriod> SchoolClassPeriods { get; set; }
        public virtual DbSet<SchoolFeeType> SchoolFeeTypes { get; set; }
        public virtual DbSet<SchoolTimeTable> SchoolTimeTables { get; set; }
        public virtual DbSet<SlotAndClassSubject> SlotAndClassSubjects { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<SportResult> SportResults { get; set; }
        public virtual DbSet<StorageFnP> StorageFnPs { get; set; }
        public virtual DbSet<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentActivity> StudentActivities { get; set; }
        public virtual DbSet<StudentCertificate> StudentCertificates { get; set; }
        public virtual DbSet<StudentClass> StudentClasses { get; set; }
        public virtual DbSet<StudentClassSubject> StudentClassSubjects { get; set; }
        public virtual DbSet<StudentEvaluationAnswer> StudentEvaluationAnswers { get; set; }
        public virtual DbSet<StudentEvaluationResult> StudentEvaluationResults { get; set; }
        public virtual DbSet<StudentFamilyNFriend> StudentFamilyNFriends { get; set; }
        public virtual DbSet<StudentFeeReceipt> StudentFeeReceipts { get; set; }
        public virtual DbSet<StudentFeeType> StudentFeeTypes { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        public virtual DbSet<StudentStature> StudentStatures { get; set; }
        public virtual DbSet<SubjectComponent> SubjectComponents { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<SyllabusDetail> SyllabusDetails { get; set; }
        public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }
        public virtual DbSet<TaskAssignmentComment> TaskAssignmentComments { get; set; }
        public virtual DbSet<TaskConfiguration> TaskConfigurations { get; set; }
        public virtual DbSet<TeacherPeriod> TeacherPeriods { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public virtual DbSet<TotalAttendance> TotalAttendances { get; set; }
        public virtual DbSet<VariableConfiguration> VariableConfigurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=constr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountNature>(entity =>
            {
                entity.Property(e => e.AccountName).IsUnicode(false);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<AccountingLedgerTrialBalance>(entity =>
            {
                entity.HasKey(e => e.LedgerId)
                    .HasName("PK_StudentTeachLedger");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.AccountingLedgerTrialBalances)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ledgers_Batches");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AccountingLedgerTrialBalances)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Ledgers_EmpEmployeeId");

                entity.HasOne(d => d.GeneralLedger)
                    .WithMany(p => p.AccountingLedgerTrialBalances)
                    .HasForeignKey(d => d.GeneralLedgerId)
                    .HasConstraintName("FK_Ledgers_GeneralLedgerId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.AccountingLedgerTrialBalances)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ledgers_OrganizationId");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.AccountingLedgerTrialBalances)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_Ledgers_StudentClassId");
            });

            modelBuilder.Entity<AccountingVoucher>(entity =>
            {
                entity.Property(e => e.BaseAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Debit).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParentId)
                    .HasDefaultValueSql("((0))")
                    .HasComment("for Journal entry");

                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.ShortText).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassFee)
                    .WithMany(p => p.AccountingVouchers)
                    .HasForeignKey(d => d.ClassFeeId)
                    .HasConstraintName("FK_AccountingVouchers_ClassFee");

                entity.HasOne(d => d.FeeReceipt)
                    .WithMany(p => p.AccountingVouchers)
                    .HasForeignKey(d => d.FeeReceiptId)
                    .HasConstraintName("FK_AccountingVouchers_StudentFeeReceipts");

                entity.HasOne(d => d.GeneralLedgerAccount)
                    .WithMany(p => p.AccountingVoucherGeneralLedgerAccounts)
                    .HasForeignKey(d => d.GeneralLedgerAccountId)
                    .HasConstraintName("FK_AccountingVouchers_GeneralLedger");

                entity.HasOne(d => d.Ledger)
                    .WithMany(p => p.AccountingVouchers)
                    .HasForeignKey(d => d.LedgerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingVouchers_AccountingLedgerTrialBalance");

                entity.HasOne(d => d.LedgerPosting)
                    .WithMany(p => p.AccountingVoucherLedgerPostings)
                    .HasForeignKey(d => d.LedgerPostingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingVouchers_LedgerPostingId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.AccountingVouchers)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingVouchers_Organization");
            });

            modelBuilder.Entity<AchievementAndPoint>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumName).IsUnicode(false);

                entity.Property(e => e.UpdatableName).IsUnicode(false);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.ApplicationUserId)
                    .HasName("PK_ApplicationUsers");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.ContactNo).IsUnicode(false);

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_AppUsers_OrganizationOrgId");
            });

            modelBuilder.Entity<ApplicationFeatureRolesPerm>(entity =>
            {
                entity.HasKey(e => e.ApplicationFeatureRoleId)
                    .HasName("PK_ApplicationRolePerm");

                entity.Property(e => e.PlanId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.PlanFeature)
                    .WithMany(p => p.ApplicationFeatureRolesPerms)
                    .HasForeignKey(d => d.PlanFeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationFeatureRolesPerm_PlanFeatures");
            });

            modelBuilder.Entity<ApplicationPrice>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.PCPM).HasComment("Per count (student, employee) per mont");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationPriceApplications)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPrice_ApplicationPriceApplicationId");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ApplicationPriceCurrencies)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_ApplicationPrice_MasterDataCurrencyId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ApplicationPrices)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPrice_Organization");
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.StudentClassId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Batches");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .HasConstraintName("FK_Attendance_ClassSubject");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_OrganizationId");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_Attendance_StudentClassId");
            });

            modelBuilder.Entity<AttendanceReport>(entity =>
            {
                entity.Property(e => e.AttendanceReportId).ValueGeneratedOnAdd();

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AttendanceReportNavigation)
                    .WithOne(p => p.AttendanceReport)
                    .HasForeignKey<AttendanceReport>(d => d.AttendanceReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceReports_EmpEmployees");

                entity.HasOne(d => d.FinancialYearNavigation)
                    .WithMany(p => p.AttendanceReports)
                    .HasForeignKey(d => d.FinancialYear)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceReports_BatchesFinancialYear");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.AttendanceReports)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceReports_OrgId");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.BatchName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_Organization");
            });

            modelBuilder.Entity<CertificateConfig>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Logic).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<ClassEvaluation>(entity =>
            {
                entity.Property(e => e.ClassEvaluationAnswerOptionParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassEvaluationAnswerOptionParent)
                    .WithMany(p => p.ClassEvaluations)
                    .HasForeignKey(d => d.ClassEvaluationAnswerOptionParentId)
                    .HasConstraintName("FK_ClassEvaluation_ClassEvaluationOptions");

                entity.HasOne(d => d.EvaluationMaster)
                    .WithMany(p => p.ClassEvaluations)
                    .HasForeignKey(d => d.EvaluationMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassEvaluation_EvaluationName");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ClassEvaluations)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_ClassEvaluation_Exams");
            });

            modelBuilder.Entity<ClassEvaluationOption>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassEvaluation)
                    .WithMany(p => p.ClassEvaluationOptions)
                    .HasForeignKey(d => d.ClassEvaluationId)
                    .HasConstraintName("FK_ClassEvaluationOptions_ClassEvaluation");
            });

            modelBuilder.Entity<ClassFee>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Rate).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassFees)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassFee_ClassMastersClassId");

                entity.HasOne(d => d.FeeDefinition)
                    .WithMany(p => p.ClassFees)
                    .HasForeignKey(d => d.FeeDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassFee_FeeDefinition");
            });

            modelBuilder.Entity<ClassGroup>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ClassGroups)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassPrerequisites_OrgId");
            });

            modelBuilder.Entity<ClassGroupMapping>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassGroup)
                    .WithMany(p => p.ClassGroupMappings)
                    .HasForeignKey(d => d.ClassGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassGroupMapping_ClassGroupMapping");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassGroupMappings)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassGroupMapping_ClassMasters");
            });

            modelBuilder.Entity<ClassMaster>(entity =>
            {
                entity.Property(e => e.Confidential).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_ClassSubject_Batches");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubject_ClassMasterClassId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubject_OrganizationId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubject_MasterDataSubjectsId");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubject_SubjectTypesId");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_ClassSubject_EmployeeTeacherId");
            });

            modelBuilder.Entity<ClassSubjectMarkComponent>(entity =>
            {
                entity.Property(e => e.ExamId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.ClassSubjectMarkComponents)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectMarkComponents_Batches");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.ClassSubjectMarkComponents)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectMarkComponents_ClassSubjectId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ClassSubjectMarkComponents)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectMarkComponents_OrganizationId");

                entity.HasOne(d => d.SubjectComponent)
                    .WithMany(p => p.ClassSubjectMarkComponents)
                    .HasForeignKey(d => d.SubjectComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectMarkComponents_MasterDataSubjectCompomentId");
            });

            modelBuilder.Entity<CourseYearSemester>(entity =>
            {
                entity.Property(e => e.CourseYearSemesterId).ValueGeneratedNever();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.CourseYearSemesters)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseYearSemester_ClassMasters");
            });

            modelBuilder.Entity<CustomFeature>(entity =>
            {
                entity.Property(e => e.CustomFeatureName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.Property(e => e.TableNameId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CustomFeatureRolePermission>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.CustomFeature)
                    .WithMany(p => p.CustomFeatureRolePermissions)
                    .HasForeignKey(d => d.CustomFeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomFeatureRolePermission_CustomFeature");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.CustomFeatureRolePermissions)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomFeatureRolePermission_OrgId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CustomFeatureRolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomFeatureRolePermission_RoleId");
            });

            modelBuilder.Entity<CustomerInvoice>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerInvoiceCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CustomerInvoice_OrganizationCustomerId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.CustomerInvoiceOrgs)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerInvoice_Organization");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.CustomerInvoices)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerInvoice_MasterDataPaymentStatus");
            });

            modelBuilder.Entity<CustomerInvoiceComponent>(entity =>
            {
                entity.Property(e => e.CustomerInvoiceComponentId).ValueGeneratedNever();

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<CustomerInvoiceItem>(entity =>
            {
                entity.HasOne(d => d.CustomerAppItem)
                    .WithMany(p => p.CustomerInvoiceItems)
                    .HasForeignKey(d => d.CustomerAppItemId)
                    .HasConstraintName("FK_CustomerInvoiceItems_AppItemId");

                entity.HasOne(d => d.CustomerInvoice)
                    .WithMany(p => p.CustomerInvoiceItems)
                    .HasForeignKey(d => d.CustomerInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerInvoiceItems_CustomerInvoice");

                entity.HasOne(d => d.InventoryItem)
                    .WithMany(p => p.CustomerInvoiceItems)
                    .HasForeignKey(d => d.InventoryItemId)
                    .HasConstraintName("FK_CustomerInvoiceItems_InventoryId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.CustomerInvoiceItems)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerInvoiceItems_Organization");
            });

            modelBuilder.Entity<CustomerPlan>(entity =>
            {
                entity.Property(e => e.Formula).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.CustomerPlans)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomterPlans_Organization");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.CustomerPlans)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlans_PlanId");
            });

            modelBuilder.Entity<CustomerPlanFeature>(entity =>
            {
                entity.Property(e => e.FeatureName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.CustomerPlanFeatures)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlanFeatures_Plans");
            });

            modelBuilder.Entity<DataSync>(entity =>
            {
                entity.Property(e => e.DataMode).IsUnicode(false);

                entity.Property(e => e.History).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DynamicTable>(entity =>
            {
                entity.Property(e => e.DynamicTableId).IsFixedLength(true);

                entity.Property(e => e.ColumnName).IsUnicode(false);
            });

            modelBuilder.Entity<DynamicTableValue>(entity =>
            {
                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<EmpComponent>(entity =>
            {
                entity.HasKey(e => e.EmpSalaryComponentId)
                    .HasName("PK_EmpEmployeeSalary");

                entity.Property(e => e.FormulaOrAmount).IsUnicode(false);

                entity.Property(e => e.SalaryComponent).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.EmpComponents)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpComponents_ComponentType");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpComponents)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpComponents_OrganizationId");
            });

            modelBuilder.Entity<EmpEmployee>(entity =>
            {
                entity.Property(e => e.AdhaarNo).IsUnicode(false);

                entity.Property(e => e.AlternateContactNo).IsUnicode(false);

                entity.Property(e => e.BankAccountNo).IsUnicode(false);

                entity.Property(e => e.BloodgroupId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ContactNo).IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DesignationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.EmergencyContactNo).IsUnicode(false);

                entity.Property(e => e.EmpGradeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeCode).IsUnicode(false);

                entity.Property(e => e.EmploymentStatusId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmploymentTypeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.FatherName).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.IFSCcode).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.MICRNo).IsUnicode(false);

                entity.Property(e => e.MaritalStatusId).HasDefaultValueSql("((0))");

                entity.Property(e => e.MotherName).IsUnicode(false);

                entity.Property(e => e.NatureId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PAN).IsUnicode(false);

                entity.Property(e => e.PFAccountNo).IsUnicode(false);

                entity.Property(e => e.PassportNo).IsUnicode(false);

                entity.Property(e => e.PermanentAddressCityId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermanentAddressCountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermanentAddressPincode).IsUnicode(false);

                entity.Property(e => e.PermanentAddressStateId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhotoPath).IsUnicode(false);

                entity.Property(e => e.PresentAddressCityId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PresentAddressCountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PresentAddressPincode).IsUnicode(false);

                entity.Property(e => e.PresentAddressStateId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReligionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.Spouse)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubOrgId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.WhatsappNo).IsUnicode(false);

                entity.Property(e => e.WorkAccountId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<EmpEmployeeGradeSalHistory>(entity =>
            {
                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_MasterItemDepartment");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryDesignations)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_MasterItemsDesignation");

                entity.HasOne(d => d.EmpGrade)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryEmpGrades)
                    .HasForeignKey(d => d.EmpGradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_MasterDataGradeId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_EmpEmployees");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_EmpEmployeesManager");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpEmployeeGradeSalHistories)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_OrganizationId");

                entity.HasOne(d => d.WorkAccount)
                    .WithMany(p => p.EmpEmployeeGradeSalHistoryWorkAccounts)
                    .HasForeignKey(d => d.WorkAccountId)
                    .HasConstraintName("FK_EmpEmployeeGradeSalHistory_MasterDataWorkAccountId");
            });

            modelBuilder.Entity<EmpEmployeeGroup>(entity =>
            {
                entity.HasKey(e => e.EmployeeGroupId)
                    .HasName("PK_EmpEmployeeGroup");

                entity.Property(e => e.GroupName).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpEmployeeGroupOrgs)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeGroups_OrganizationOrgId");

                entity.HasOne(d => d.SubOrg)
                    .WithMany(p => p.EmpEmployeeGroupSubOrgs)
                    .HasForeignKey(d => d.SubOrgId)
                    .HasConstraintName("FK_EmpEmployeeGroups_OrganizationSubOrgId");
            });

            modelBuilder.Entity<EmpEmployeeSalaryComponent>(entity =>
            {
                entity.HasKey(e => e.EmployeeSalaryComponentId)
                    .HasName("PK_EmployeeSalary");

                entity.Property(e => e.ActualFormulaOrAmount).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.EmpComponent)
                    .WithMany(p => p.EmpEmployeeSalaryComponents)
                    .HasForeignKey(d => d.EmpComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeSalaryComponents_ComponentId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpEmployeeSalaryComponents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSalaryComponents_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpEmployeeSalaryComponents)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSalaryComponents_Organization");
            });

            modelBuilder.Entity<EmpEmployeeSkill>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpEmployeeSkills)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeSkills_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpEmployeeSkills)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpEmployeeSkills_Organization");
            });

            modelBuilder.Entity<EmpManagerGroupMapping>(entity =>
            {
                entity.HasKey(e => e.ManagerGroupMappingId)
                    .HasName("PK_ManagerTeacherGroupMapping");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<EmpWorkHistory>(entity =>
            {
                entity.Property(e => e.Designation).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.Responsibility).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpWorkHistories)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmpWorkHistory_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmpWorkHistories)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpWorkHistory_OrganizationOrgId");
            });

            modelBuilder.Entity<EmployeeActivity>(entity =>
            {
                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeActivities)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeActivity_EmpEmployees");
            });

            modelBuilder.Entity<EmployeeAttendance>(entity =>
            {
                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.EmployeeAttendances)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_EmployeeAttendance_Batches");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmployeeAttendances)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAttendance_OrganizationId");
            });

            modelBuilder.Entity<EmployeeEducationHistory>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BoardName).IsUnicode(false);

                entity.Property(e => e.CourseName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEducationHistories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEducationHistory_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmployeeEducationHistories)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEducationHistory_Organization");
            });

            modelBuilder.Entity<EmployeeFamily>(entity =>
            {
                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeFamilies)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeFamily_EmpEmployees");

                entity.HasOne(d => d.FamilyRelationShip)
                    .WithMany(p => p.EmployeeFamilyFamilyRelationShips)
                    .HasForeignKey(d => d.FamilyRelationShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeFamily_MasterDataRelationshipId");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.EmployeeFamilyGenderNavigations)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeFamily_MasterDataGenderId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmployeeFamilies)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeFamily_Organization");
            });

            modelBuilder.Entity<EmployeeMonthlySalary>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeMonthlySalaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeMonthlySalary_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EmployeeMonthlySalaries)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeMonthlySalary_Organization");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.Detail).IsUnicode(false);

                entity.Property(e => e.ModuleName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<EvaluationExamMap>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.EvaluationMaster)
                    .WithMany(p => p.EvaluationExamMaps)
                    .HasForeignKey(d => d.EvaluationMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationClassSubjectMap_EvaluationName");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.EvaluationExamMaps)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_OnlineEvaluation_Organization");
            });

            modelBuilder.Entity<EvaluationMaster>(entity =>
            {
                entity.Property(e => e.AppendAnswer).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartTime).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<EvaluationName>(entity =>
            {
                entity.HasKey(e => e.EvaluationMasterId)
                    .HasName("PK_EvaluationName_1");

                entity.Property(e => e.EvaluationMasterId).ValueGeneratedNever();

                entity.Property(e => e.Duration).IsUnicode(false);
            });

            modelBuilder.Entity<EvaluationResultMark>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EventName).IsUnicode(false);

                entity.Property(e => e.Participants).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Venue).IsUnicode(false);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Batches");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.MarkFormula).IsUnicode(false);

                entity.Property(e => e.ReleaseResult).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_Exams_BatchesBatchId");

                entity.HasOne(d => d.ExamName)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ExamNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_MasterDataExamNameId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_OrganizationId");
            });

            modelBuilder.Entity<ExamClassGroupMap>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassGroup)
                    .WithMany(p => p.ExamClassGroupMaps)
                    .HasForeignKey(d => d.ClassGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamClassGroupMap_ClassGroup");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamClassGroupMaps)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamClassGroupMap_Exams");
            });

            modelBuilder.Entity<ExamMarkConfig>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Formula).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ExamMarkConfigs)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamMarkConfig_ClassMasters");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.ExamMarkConfigs)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamMarkConfig_ClassSubject");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamMarkConfigs)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamMarkConfig_ExamMarkConfig");
            });

            modelBuilder.Entity<ExamNCalculate>(entity =>
            {
                entity.Property(e => e.Formula)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamNCalculates)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamNCalculate_Exams");
            });

            modelBuilder.Entity<ExamResultSubjectMark>(entity =>
            {
                entity.Property(e => e.ActualMarks).HasDefaultValueSql("((0))");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamResultSubjectMarks)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamResultSubjectMark_Exams");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.ExamResultSubjectMarks)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamResultSubjectMark_StudentClass");

                entity.HasOne(d => d.StudentClassSubject)
                    .WithMany(p => p.ExamResultSubjectMarks)
                    .HasForeignKey(d => d.StudentClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamResultSubjectMark_StudentClassSubject");
            });

            modelBuilder.Entity<ExamSlot>(entity =>
            {
                entity.Property(e => e.EndTime).IsUnicode(false);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartTime).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.ExamSlots)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamSlot_Batches");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamSlots)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamSlot_ExamsExamId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ExamSlots)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamSlot_OrganizationId");
            });

            modelBuilder.Entity<ExamStudentResult>(entity =>
            {
                entity.Property(e => e.Attendance).IsUnicode(false);

                entity.Property(e => e.ClassId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Division).IsUnicode(false);

                entity.Property(e => e.FailCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.PassCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.SectionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.ExamStudentResults)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamStudentResult_Batches");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamStudentResults)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamStudentClass_ExamsId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ExamStudentResults)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamStudentClass_OrganizationId");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.ExamStudentResults)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamStudentClass_ExamStudentClassId");
            });

            modelBuilder.Entity<ExamStudentSubjectResult>(entity =>
            {
                entity.Property(e => e.SectionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassSubjectMarkComponent)
                    .WithMany(p => p.ExamStudentSubjectResults)
                    .HasForeignKey(d => d.ClassSubjectMarkComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamStudentSubjectResult_ExamStudentSubjectResult");
            });

            modelBuilder.Entity<FeeDefinition>(entity =>
            {
                entity.Property(e => e.AmountEditable).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FeeName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.FeeCategory)
                    .WithMany(p => p.FeeDefinitions)
                    .HasForeignKey(d => d.FeeCategoryId)
                    .HasConstraintName("FK_FeeMaster_MasterItemsFeeTypeId");
            });

            modelBuilder.Entity<GeneralLedger>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AssetSequence).HasDefaultValueSql("((999))");

                entity.Property(e => e.BatchId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ContactName).IsUnicode(false);

                entity.Property(e => e.ContactNo).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.ExpenseSequence).HasDefaultValueSql("((999))");

                entity.Property(e => e.GeneralLedgerName).IsUnicode(false);

                entity.Property(e => e.IncomeStatementPlus).HasComment("-1 minus from previous, 0 do nothing, 1 add to preivous sequence amount");

                entity.Property(e => e.IncomeStatementSequence).HasDefaultValueSql("((999))");

                entity.Property(e => e.LnESequence).HasDefaultValueSql("((999))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TBPlus).HasComment("-1 minus from previous, 0 do nothing, 1 add to preivous sequence amount");

                entity.Property(e => e.TBSequence).HasDefaultValueSql("((999))");

                entity.HasOne(d => d.AccountGroup)
                    .WithMany(p => p.GeneralLedgerAccountGroups)
                    .HasForeignKey(d => d.AccountGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralLedger_Accountgroup");

                entity.HasOne(d => d.AccountNature)
                    .WithMany(p => p.GeneralLedgerAccountNatures)
                    .HasForeignKey(d => d.AccountNatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralLedger_AccountNature");
            });

            modelBuilder.Entity<GeneratedCertificate>(entity =>
            {
                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<GroupActivityParticipant>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.SportResult)
                    .WithMany(p => p.GroupActivityParticipants)
                    .HasForeignKey(d => d.SportResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityParticipant_SportResult");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.GroupActivityParticipants)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityParticipant_StudentClass");
            });

            modelBuilder.Entity<GroupPoint>(entity =>
            {
                entity.Property(e => e.GroupPointId).ValueGeneratedNever();

                entity.Property(e => e.Active).HasDefaultValueSql("((0))");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ItemCode).IsUnicode(false);

                entity.Property(e => e.PPP).HasComment("Price per Piece");

                entity.Property(e => e.PPU).HasComment("Price per unit");

                entity.Property(e => e.SKU).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.InventoryItemCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_InventoryItems_MasterDataCategoryId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryItems_Organization");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.InventoryItemUnits)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_InventoryItems_MasterDataUnitId");
            });

            modelBuilder.Entity<InvoiceComponent>(entity =>
            {
                entity.Property(e => e.InvoiceComponentId).ValueGeneratedNever();

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.InvoiceComponents)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceComponents_InvoiceComponents");

                entity.HasOne(d => d.CustomerInvoice)
                    .WithMany(p => p.InvoiceComponents)
                    .HasForeignKey(d => d.CustomerInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceComponents_CustomerInvoice");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.InvoiceComponents)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceComponents_Organization");
            });

            modelBuilder.Entity<LeaveBalance>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.LeaveBalances)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveBalances_BatchesFinancialYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveBalances)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveBalances_EmpEmployees");

                entity.HasOne(d => d.LeavePolicy)
                    .WithMany(p => p.LeaveBalances)
                    .HasForeignKey(d => d.LeavePolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveBalances_LeavePolicy");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.LeaveBalances)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveBalances_Organization");
            });

            modelBuilder.Entity<LeaveEmployeeLeaf>(entity =>
            {
                entity.HasKey(e => e.EmployeeLeaveId)
                    .HasName("PK_EmployeeLeaves");

                entity.Property(e => e.LeaveReason).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveEmployeeLeaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeLeaves_EmpEmployees");

                entity.HasOne(d => d.LeaveStatus)
                    .WithMany(p => p.LeaveEmployeeLeaves)
                    .HasForeignKey(d => d.LeaveStatusId)
                    .HasConstraintName("FK_EmployeeLeaves_MasterDataLeaveStatus");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveEmployeeLeaves)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeLeaves_MasterDataLeaveTypeId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.LeaveEmployeeLeaves)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeLeaves_Organization");
            });

            modelBuilder.Entity<LeavePolicy>(entity =>
            {
                entity.Property(e => e.ExcludeDays).IsUnicode(false);

                entity.Property(e => e.FormulaOrDays).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.LeavePolicies)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeavePolicy_BatchesFinancialYear");

                entity.HasOne(d => d.LeaveName)
                    .WithMany(p => p.LeavePolicies)
                    .HasForeignKey(d => d.LeaveNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeavePolicy_MasterDataLeaveNameId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.LeavePolicies)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeavePolicy_Organization");
            });

            modelBuilder.Entity<LedgerPosting>(entity =>
            {
                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.ShortText).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AccountingVoucher)
                    .WithMany(p => p.LedgerPostings)
                    .HasForeignKey(d => d.AccountingVoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LedgerPosting_AccountingVouchers");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.LedgerPostings)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LedgerPosting_Organization");

                entity.HasOne(d => d.PostingGeneralLedger)
                    .WithMany(p => p.LedgerPostings)
                    .HasForeignKey(d => d.PostingGeneralLedgerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LedgerPosting_GeneralLedger");
            });

            modelBuilder.Entity<MasterItem>(entity =>
            {
                entity.HasKey(e => e.MasterDataId)
                    .HasName("PK_MasterData");

                entity.Property(e => e.Confidential).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Logic).IsUnicode(false);

                entity.Property(e => e.MasterDataName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.MasterItems)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MasterData_Organization");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.MessageBody).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Subject).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Organization");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.NewsId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<OrgPaymentDetail>(entity =>
            {
                entity.Property(e => e.OrgPaymentDetailId).ValueGeneratedNever();

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.OrgPaymentDetailNavigation)
                    .WithOne(p => p.OrgPaymentDetail)
                    .HasForeignKey<OrgPaymentDetail>(d => d.OrgPaymentDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgPaymentDetail_OrganizationPayment");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Contact).IsUnicode(false);

                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.RegistrationNo).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.WebSite).IsUnicode(false);
            });

            modelBuilder.Entity<OrganizationPayment>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.OrganizationPayments)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgnizationPaymentDetails_Organization");

                entity.HasOne(d => d.OrganizationPlan)
                    .WithMany(p => p.OrganizationPayments)
                    .HasForeignKey(d => d.OrganizationPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgnizationPaymentDetails_CustomerPlans");

                entity.HasOne(d => d.PaymentModeNavigation)
                    .WithMany(p => p.OrganizationPayments)
                    .HasForeignKey(d => d.PaymentMode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgnizationPayment_MasterItemsPaymentmode");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.FullPath).IsUnicode(false);

                entity.Property(e => e.PageTitle).IsUnicode(false);

                entity.Property(e => e.PhotoPath).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.faIcon).IsUnicode(false);

                entity.Property(e => e.label).IsUnicode(false);

                entity.Property(e => e.link).IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pages_MasterDataApplicationId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_Pages_Organization");
            });

            modelBuilder.Entity<PageHistory>(entity =>
            {
                entity.Property(e => e.PageBody).IsUnicode(false);

                entity.Property(e => e.PageLeft).IsUnicode(false);

                entity.Property(e => e.PageRight).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.PageHistories)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_PageHistory_Organization");

                entity.HasOne(d => d.ParentPage)
                    .WithMany(p => p.PageHistories)
                    .HasForeignKey(d => d.ParentPageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageHistory_Pages");
            });

            modelBuilder.Entity<PhotoGallery>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.PhotoGalleries)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_PhotoGallery_Albums");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.PhotoGalleries)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhotoGallery_Organization");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Logic).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<PlanAndMasterItem>(entity =>
            {
                entity.HasKey(e => e.PlanAndMasterDataId)
                    .HasName("PK_PlanAndMasterData");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.MasterData)
                    .WithMany(p => p.PlanAndMasterItems)
                    .HasForeignKey(d => d.MasterDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanAndMasterData_MasterItems");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanAndMasterItems)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanAndMasterData_Plans");
            });

            modelBuilder.Entity<PlanFeature>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PlanFeatures)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanFeatures_Pages");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanFeatures)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanFeatures_Plans");
            });

            modelBuilder.Entity<QuestionBank>(entity =>
            {
                entity.Property(e => e.Diagram).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Syllabus)
                    .WithMany(p => p.QuestionBanks)
                    .HasForeignKey(d => d.SyllabusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionBank_SyllabusDetail");
            });

            modelBuilder.Entity<QuestionBankNExam>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.QuestionBankNExams)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionBankNExam_Exams");

                entity.HasOne(d => d.QuestionBank)
                    .WithMany(p => p.QuestionBankNExams)
                    .HasForeignKey(d => d.QuestionBankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionBankN_QuestionBankNExam");
            });

            modelBuilder.Entity<ReportConfigItem>(entity =>
            {
                entity.Property(e => e.Formula).IsUnicode(false);

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TableNames).IsUnicode(false);
            });

            modelBuilder.Entity<ReportOrgReportColumn>(entity =>
            {
                entity.Property(e => e.ColumnDisplayName).IsUnicode(false);

                entity.Property(e => e.FormulaOrColumnName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ReportOrgReportColumns)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportOrgReportColumns_Organization");

                entity.HasOne(d => d.ReportOrgReportName)
                    .WithMany(p => p.ReportOrgReportColumns)
                    .HasForeignKey(d => d.ReportOrgReportNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportOrgReportColumns_ReportOrgReports");
            });

            modelBuilder.Entity<ReportOrgReportName>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserReportName).IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.ReportOrgReportNames)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportOrgReports_Organization");

                entity.HasOne(d => d.ReportConfigData)
                    .WithMany(p => p.ReportOrgReportNames)
                    .HasForeignKey(d => d.ReportConfigDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportOrgReports_ReportConfigurations");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_RoleUser_Batches");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUser_OrganizationId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUser_MasterDataRoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUser_AspNetUsers");
            });

            modelBuilder.Entity<RulesOrPolicy>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<SchoolClassPeriod>(entity =>
            {
                entity.Property(e => e.FromToTime).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolClassPeriods)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolClassPeriods_ClassMasters");
            });

            modelBuilder.Entity<SchoolFeeType>(entity =>
            {
                entity.HasKey(e => e.FeeTypeId)
                    .HasName("PK_FeeTypes");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FeeTypeName).IsUnicode(false);

                entity.Property(e => e.Formula).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<SchoolTimeTable>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.SchoolTimeTables)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolTimeTable_Batches");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolTimeTables)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolTimeTable_ClassMasters");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.SchoolTimeTables)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolTimeTable_OrgId");

                entity.HasOne(d => d.SchoolClassPeriod)
                    .WithMany(p => p.SchoolTimeTables)
                    .HasForeignKey(d => d.SchoolClassPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolTimeTable_SchoolClassPeriodId");

                entity.HasOne(d => d.TeacherSubject)
                    .WithMany(p => p.SchoolTimeTables)
                    .HasForeignKey(d => d.TeacherSubjectId)
                    .HasConstraintName("FK_SchoolTimeTable_TeacherSubject");
            });

            modelBuilder.Entity<SlotAndClassSubject>(entity =>
            {
                entity.HasKey(e => e.SlotClassSubjectId)
                    .HasName("PK_SlotAndClassSubjectId");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.SlotAndClassSubjects)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotAndClassSubject_Batches");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.SlotAndClassSubjects)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotAndClassSubject_ClassSubjectId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.SlotAndClassSubjects)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotAndClassSubject_Organization");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.SlotAndClassSubjects)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotAndClassSubject_ExamSlot");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.SportsName).IsUnicode(false);
            });

            modelBuilder.Entity<SportResult>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.SportResults)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportResult_AchievementAndPoints");
            });

            modelBuilder.Entity<StorageFnP>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("PK_FilesNPhotos");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileName).IsUnicode(false);

                entity.Property(e => e.Parent).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UpdatedFileFolderName).IsUnicode(false);

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_FilesNPhotos_DocTypeId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_StorageFnP_EmpEmployees");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_FilesNPhotos_Organization");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_StorageFnP_QuestionBank");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_StorageFnP_StudentClass");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StorageFnPs)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StorageFnP_Students");
            });

            modelBuilder.Entity<StudTeacherClassMapping>(entity =>
            {
                entity.Property(e => e.HelperId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudTeacherClassMappings)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudTeacherClassMapping_Batches");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudTeacherClassMappings)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudTeacherClassMapping_ClassMasterClassId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudTeacherClassMappings)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudTeacherClassMapping_Organization");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudTeacherClassMappings)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudTeacherClassMapping_MasterDataSectionId");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.StudTeacherClassMappings)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudTeacherClassMapping_EmpEmployeesTeacherId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.AccountHolderName).IsUnicode(false);

                entity.Property(e => e.AdhaarNo).IsUnicode(false);

                entity.Property(e => e.AdmissionNo).IsUnicode(false);

                entity.Property(e => e.AdmissionStatusId).HasDefaultValueSql("((0))");

                entity.Property(e => e.AlternateContact).IsUnicode(false);

                entity.Property(e => e.BankAccountNo).IsUnicode(false);

                entity.Property(e => e.BatchId).HasDefaultValueSql("((0))");

                entity.Property(e => e.BloodgroupId).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoardRegistrationNo).IsUnicode(false);

                entity.Property(e => e.BoardRollNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ClassAdmissionSought).HasDefaultValueSql("((0))");

                entity.Property(e => e.ClubId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ContactPersonContactNo).IsUnicode(false);

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.FatherContactNo).IsUnicode(false);

                entity.Property(e => e.FatherName).IsUnicode(false);

                entity.Property(e => e.FatherOccupation).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.GenderId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Height).IsUnicode(false);

                entity.Property(e => e.HouseId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IFSCCode).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.LastSchoolPercentage).IsUnicode(false);

                entity.Property(e => e.MICRNo).IsUnicode(false);

                entity.Property(e => e.MotherContactNo).IsUnicode(false);

                entity.Property(e => e.MotherName).IsUnicode(false);

                entity.Property(e => e.MotherOccupation).IsUnicode(false);

                entity.Property(e => e.NameOfContactPerson).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PEN)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PermanentAddress).IsUnicode(false);

                entity.Property(e => e.PermanentAddressCityId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermanentAddressCountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermanentAddressPincode).IsUnicode(false);

                entity.Property(e => e.PermanentAddressStateId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PersonalNo).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.PresentAddress).IsUnicode(false);

                entity.Property(e => e.PresentAddressCityId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PresentAddressCountryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PresentAddressPincode).IsUnicode(false);

                entity.Property(e => e.PresentAddressStateId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReasonForLeavingId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RelationWithContactPerson).IsUnicode(false);

                entity.Property(e => e.ReligionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RemarkId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RollNo).IsUnicode(false);

                entity.Property(e => e.SectionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TransferFromSchool).IsUnicode(false);

                entity.Property(e => e.TransferFromSchoolBoard).IsUnicode(false);

                entity.Property(e => e.Weight).IsUnicode(false);

                entity.Property(e => e.WhatsAppNumber).IsUnicode(false);

                entity.HasOne(d => d.Bloodgroup)
                    .WithMany(p => p.StudentBloodgroups)
                    .HasForeignKey(d => d.BloodgroupId)
                    .HasConstraintName("FK_Students_Bloodgroup");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.StudentCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Students_Category");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.StudentGenders)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Students_Gender");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_OrganizationOrgId");

                entity.HasOne(d => d.PermanentAddressCity)
                    .WithMany(p => p.StudentPermanentAddressCities)
                    .HasForeignKey(d => d.PermanentAddressCityId)
                    .HasConstraintName("FK_Students_City");

                entity.HasOne(d => d.PermanentAddressCountry)
                    .WithMany(p => p.StudentPermanentAddressCountries)
                    .HasForeignKey(d => d.PermanentAddressCountryId)
                    .HasConstraintName("FK_Students_Country");

                entity.HasOne(d => d.PermanentAddressState)
                    .WithMany(p => p.StudentPermanentAddressStates)
                    .HasForeignKey(d => d.PermanentAddressStateId)
                    .HasConstraintName("FK_Students_State");

                entity.HasOne(d => d.ReasonForLeaving)
                    .WithMany(p => p.StudentReasonForLeavings)
                    .HasForeignKey(d => d.ReasonForLeavingId)
                    .HasConstraintName("FK_Students_ReasonForLeaving");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.StudentReligions)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("FK_Students_Religion");
            });

            modelBuilder.Entity<StudentActivity>(entity =>
            {
                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentActivities)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_StudentActivity_Batches");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentActivities)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_StudentActivity_OrganizationId");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentActivities)
                    .HasForeignKey(d => d.StudentClassId)
                    .HasConstraintName("FK_StudentActivity_StudentClassId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentActivities)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentActivity_StudentId");
            });

            modelBuilder.Entity<StudentCertificate>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentCertificates)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCertificate_Batches");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentCertificates)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCertificate_Organization");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentCertificates)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCertificate_StudentClass");
            });

            modelBuilder.Entity<StudentClass>(entity =>
            {
                entity.Property(e => e.AdmissionDate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AdmissionNo).IsUnicode(false);

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.PhotoPath).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.RollNo).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClass_Batches");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClass_ClassMaster");

                entity.HasOne(d => d.FeeType)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.FeeTypeId)
                    .HasConstraintName("FK_StudentClass_Feetype");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClass_OrganizationOrgId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClass_Student");
            });

            modelBuilder.Entity<StudentClassSubject>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentClassSubjects)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClassSubject_BatchesBatchId");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.StudentClassSubjects)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClassSubject_ClassSubjectId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentClassSubjects)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClassSubject_OrganizationOrgId");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentClassSubjects)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentClassSubject_StudentClassId");
            });

            modelBuilder.Entity<StudentEvaluationAnswer>(entity =>
            {
                entity.HasOne(d => d.ClassEvaluationAnswerOptions)
                    .WithMany(p => p.StudentEvaluationAnswers)
                    .HasForeignKey(d => d.ClassEvaluationAnswerOptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEvaluationAnswer_ClassEvaluationOptions");

                entity.HasOne(d => d.StudentEvaluationResult)
                    .WithMany(p => p.StudentEvaluationAnswers)
                    .HasForeignKey(d => d.StudentEvaluationResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEvaluation_StudentEvaluationResult");
            });

            modelBuilder.Entity<StudentEvaluationResult>(entity =>
            {
                entity.Property(e => e.HistoryText).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassEvaluation)
                    .WithMany(p => p.StudentEvaluationResults)
                    .HasForeignKey(d => d.ClassEvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEvaluationResult_ClassEvaluation");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentEvaluationResults)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEvaluationResult_ClassMasters");

                entity.HasOne(d => d.EvaluationExamMap)
                    .WithMany(p => p.StudentEvaluationResults)
                    .HasForeignKey(d => d.EvaluationExamMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEvaluationResult_EvaluationClassSubjectMap");
            });

            modelBuilder.Entity<StudentFamilyNFriend>(entity =>
            {
                entity.Property(e => e.ContactNo).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentFamilyNFriends)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFamilyNFriend_Organization");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentFamilyNFriends)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentFamilyNFriend_Students");
            });

            modelBuilder.Entity<StudentFeeReceipt>(entity =>
            {
                entity.Property(e => e.OffLineReceiptNo).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentFeeReceipts)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFeeReceipts_Batches");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.StudentFeeReceipts)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFeeReceipts_Organization");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentFeeReceipts)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFeeReceipts_StudentClass");
            });

            modelBuilder.Entity<StudentFeeType>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.FeeType)
                    .WithMany(p => p.StudentFeeTypes)
                    .HasForeignKey(d => d.FeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFeeType_SchoolFeeTypes");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentFeeTypes)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentFeeType_StudentClass");
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.Property(e => e.ExamId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Formula).IsUnicode(false);

                entity.Property(e => e.GradeName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_StudentGrade_Exams");

                entity.HasOne(d => d.SubjectCategory)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.SubjectCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGrade_MasterItems");
            });

            modelBuilder.Entity<StudentStature>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.StudentStatures)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentStatus_StudentClass");
            });

            modelBuilder.Entity<SubjectComponent>(entity =>
            {
                entity.Property(e => e.ComponentName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.SubjectComponents)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectComponent_ClassSubject");
            });

            modelBuilder.Entity<SubjectType>(entity =>
            {
                entity.Property(e => e.SubjectTypeName).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<SyllabusDetail>(entity =>
            {
                entity.HasKey(e => e.SyllabusId)
                    .HasName("PK_Syllabus");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.Property(e => e.AssignmentName).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AssignedByEmployee)
                    .WithMany(p => p.TaskAssignmentAssignedByEmployees)
                    .HasForeignKey(d => d.AssignedByEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssignments_EmpEmployeesAssignedBy");

                entity.HasOne(d => d.AssignedToClass)
                    .WithMany(p => p.TaskAssignments)
                    .HasForeignKey(d => d.AssignedToClassId)
                    .HasConstraintName("FK_TaskAssignments_StudentClass");

                entity.HasOne(d => d.AssignedToEmployee)
                    .WithMany(p => p.TaskAssignmentAssignedToEmployees)
                    .HasForeignKey(d => d.AssignedToEmployeeId)
                    .HasConstraintName("FK_TaskAssignments_EmpEmployeesAssignedTo");

                entity.HasOne(d => d.AssignmentStatus)
                    .WithMany(p => p.TaskAssignments)
                    .HasForeignKey(d => d.AssignmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssignments_MasterDataAssignmentStatusId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.TaskAssignments)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssignments_Organization");
            });

            modelBuilder.Entity<TaskAssignmentComment>(entity =>
            {
                entity.HasKey(e => e.AssignmentCommentId)
                    .HasName("PK_AssignmentComments");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.TaskAssignmentComments)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssignmentComments_OrgId");

                entity.HasOne(d => d.TaskAssignment)
                    .WithMany(p => p.TaskAssignmentComments)
                    .HasForeignKey(d => d.TaskAssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskAssignmentComments_TaskAssignments");
            });

            modelBuilder.Entity<TaskConfiguration>(entity =>
            {
                entity.Property(e => e.AlertMessage).IsUnicode(false);

                entity.Property(e => e.ColNameNValue).IsUnicode(false);

                entity.Property(e => e.DBConnection).IsUnicode(false);

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.Property(e => e.TaskName).IsUnicode(false);

                entity.Property(e => e.UserEmailForAlert).IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.TaskConfigurations)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_TaskConfiguration_OrganizationId");
            });

            modelBuilder.Entity<TeacherPeriod>(entity =>
            {
                entity.Property(e => e.OffPeriod).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TeacherPeriods)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherOffPeriod_EmpEmployees");

                entity.HasOne(d => d.SchoolClassPeriod)
                    .WithMany(p => p.TeacherPeriods)
                    .HasForeignKey(d => d.SchoolClassPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherOffPeriod_SchoolClassPeriods");
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubject_Batches");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubject_ClassSubject");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubject_EmpEmployees");
            });

            modelBuilder.Entity<TotalAttendance>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TotalAttendances)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TotalAttendance_ClassMasters");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.TotalAttendances)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TotalAttendance_Exams");
            });

            modelBuilder.Entity<VariableConfiguration>(entity =>
            {
                entity.Property(e => e.SyncId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.VariableDescription).IsUnicode(false);

                entity.Property(e => e.VariableFormula).IsUnicode(false);

                entity.Property(e => e.VariableName).IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.VariableConfigurations)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VariableConfiguration_Organization");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
