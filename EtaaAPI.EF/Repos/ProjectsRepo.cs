using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.Dtos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    // By inheriting from the BaseRepo and sending it the Projects model we're getting everyting that is in BaseRepo,
    // I'm doing this because this model in particular has some end points that are specific to it and it won't be used by
    // any else model.
    public class ProjectsRepo : BaseRepo<Projects>, IProjectsRepo
    {
        // It's protected because this project is the only one allowed to deal with the DB directly
        protected new ApplicationDbContext _context;

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ProjectsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetNumberOfInstallments(int ProjectId)
        {
            int NumberOfInstallments = (from project in _context.Projects
                                        where project.ProjectId == ProjectId
                                        select (int)project.NumberOfInstallments).Single();

            return NumberOfInstallments;
        }

        public bool CreateProject(ProjectDto projectDto, List<ProjectsAssetsDto> projectsAssetsDto, 
                List<ProjectsSelectionReasonsDto> projectsSelectionReasonsDto, 
            List<ProjectsSocialBenefitsDto> projectsSocialBenefitsDto)
        {
            //var filePath = HttpContext.Session.GetString("filePath");
            //HttpContext.Session.Clear();
            var filePath = "";
            // * IMPORTANT Refactoring: Instead of using the new() keyword for every object and manually mapping the properties,
            // use AutoMapper
            Projects project = new();
            project.Capital = projectDto.Capital;
            project.Date = projectDto.Date;
            project.FirstInstallmentDueDate = projectDto.FirstInstallmentDueDate;
            project.WaiverPeriod = projectDto.WaiverPeriod;
            project.FamilyId = projectDto.FamilyId;
            project.IsApprovedByManagement = projectDto.IsApprovedByManagement;
            project.IsCanceled = projectDto.IsCanceled;
            project.MonthlyInstallmentAmount = projectDto.MonthlyInstallmentAmount;
            project.NameAr = projectDto.NameAr;
            project.NameEn = projectDto.NameEn;
            project.NumberOfFundsId = projectDto.NumberOfFundsId;
            project.NumberOfInstallments = projectDto.NumberOfInstallments;
            project.ProjectActivity = projectDto.ProjectActivity;
            project.ProjectPurpose = projectDto.ProjectPurpose;
            project.ProjectTypeId = projectDto.ProjectTypeId;
            project.SignatureofApplicantPath = projectDto.SignatureofApplicantPath;
            project.UserId = projectDto.UserId;
            project.ManagementUserId = projectDto.ManagementUserId;

            List<ProjectsAssets> projectsAssets = new();
            foreach (var item in projectsAssetsDto)
            {
                projectsAssets.Add(new ProjectsAssets
                {
                    Amount = item.Amount,
                    ProjectTypesAssetsId = item.ProjectTypesAssetsId,
                    Quantity = item.Quantity
                });
            }

            List<ProjectsSelectionReasons> projectsSelectionReasons = new();
            foreach (var item in projectsSelectionReasonsDto)
            {
                projectsSelectionReasons.Add(new ProjectsSelectionReasons
                {
                    ProjectSelectionReasonsId = item.ProjectSelectionReasonsId,
                    ProjectsSelectionReasonsId = item.ProjectsSelectionReasonsId
                });
            }

            List<ProjectsSocialBenefits> projectsSocialBenefits = new();
            foreach (var item in projectsSocialBenefitsDto)
            {
                projectsSocialBenefits.Add(new ProjectsSocialBenefits
                {
                    ProjectSocialBenefitsId = item.ProjectSocialBenefitsId,
                    ProjectsSocialBenefitsId = item.ProjectsSocialBenefitsId
                });
            }

            decimal? Capital = (decimal?)project.Capital;
            decimal? MonthlyInstallmentAmount = (decimal?)project.MonthlyInstallmentAmount;
            int? NumberOfInstallments = (int?)project.NumberOfInstallments;
            int MaxInstallmentsNo = _context.Installments.Select(i => i.InstallmentsId).Max();

            if (NumberOfInstallments > MaxInstallmentsNo)
            {
                // Also return the message of course
                return false;
            }
            else if (project.FamilyId.Equals(null) || project.FamilyId.Equals(0))
            {
                // Also return the message of course
                return false;
            }
            else if (project.NumberOfFundsId.Equals(null) || project.NumberOfFundsId.Equals(0))
            {
                // Also return the message of course
                return false;
            }
            else if (project.FirstInstallmentDueDate.Equals(null))
            {
                // Also return the message of course
                return false;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(filePath))
                        project.SignatureofApplicantPath = filePath;
                    //var userId = User.GetLoggedInUserId<string>();
                    var userId = "1";
                    project.UserId = userId;
                    var ProjectTypeNameEn = (from projectType in _context.ProjectTypes
                                             where projectType.ProjectTypeId == project.ProjectTypeId
                                             select projectType.NameEn).Single();
                    var ProjectTypeNameAr = (from projectType in _context.ProjectTypes
                                             where projectType.ProjectTypeId == project.ProjectTypeId
                                             select projectType.NameAr).Single();
                    var FamilyNameEn = (from family in _context.Families
                                        where family.FamilyId == project.FamilyId
                                        select family.NameEn).Single();
                    var FamilyNameAr = (from family in _context.Families
                                        where family.FamilyId == project.FamilyId
                                        select family.NameAr).Single();
                    project.NameEn = String.Concat(ProjectTypeNameEn, " ", FamilyNameEn);
                    project.NameAr = String.Concat(ProjectTypeNameAr, " ", FamilyNameAr);

                    using var transaction = _context.Database.BeginTransaction();

                    _context.Projects.Add(project);
                    _context.SaveChanges();
                    List<ProjectsAssets> projectsAssetsList = new List<ProjectsAssets>();
                    foreach (var item in projectsAssets)
                    {
                        if (item.Quantity > 0 && item.Amount > 0)
                        {
                            item.ProjectId = project.ProjectId;
                            projectsAssetsList.Add(new ProjectsAssets { Amount = item.Amount, ProjectId = item.ProjectId, ProjectsAssetsId = item.ProjectsAssetsId, ProjectTypesAssetsId = item.ProjectTypesAssetsId, Quantity = item.Quantity });
                            _context.ProjectsAssets.Add(item);
                        }
                    }

                    List<ProjectsSelectionReasons> projectsSelectionReasonsList = new List<ProjectsSelectionReasons>();
                    foreach (var item in projectsSelectionReasons)
                    {
                        item.ProjectId = project.ProjectId;
                        projectsSelectionReasonsList.Add(new ProjectsSelectionReasons
                        {
                            ProjectId = item.ProjectId,
                            ProjectSelectionReasonsId = item.ProjectSelectionReasonsId,
                            ProjectsSelectionReasonsId = item.ProjectsSelectionReasonsId
                        });
                        _context.ProjectsSelectionReasons.Add(item);
                    }

                    List<ProjectsSocialBenefits> projectsSocialBenefitsList = new List<ProjectsSocialBenefits>();
                    foreach (var item in projectsSocialBenefits)
                    {
                        item.ProjectId = project.ProjectId;
                        projectsSocialBenefitsList.Add(new ProjectsSocialBenefits { ProjectId = item.ProjectId, ProjectSocialBenefitsId = item.ProjectSocialBenefitsId, ProjectsSocialBenefitsId = item.ProjectsSocialBenefitsId });
                        _context.ProjectsSocialBenefits.Add(item);
                    }

                    _context.SaveChanges();

                    transaction.Commit();

                    // The log information
                    //_logger.LogInformation("Project added, Project: {ProjectData}, User: {User}", new { ProjectId = project.ProjectId, NameAr = project.NameAr, NameEn = project.NameEn, ProjectTypeId = project.ProjectTypeId, Capital = project.Capital, NumberOfInstallments = project.NumberOfInstallments, MonthlyInstallmentAmount = project.MonthlyInstallmentAmount, NumberOfFunds = project.NumberOfFundsId }, new { Id = User.GetLoggedInUserId<string>(), name = User.GetLoggedInUserName() });
                    //projectsAssetsList.ForEach(x => _logger.LogInformation("Project assets added for project, Project assets: {ProjectAssetsData}, User: {User}", new { ProjectsAssetsId = x.ProjectsAssetsId, ProjectId = project.ProjectId, Quantity = x.Quantity, Amount = x.Amount, ProjectTypesAssetsId = x.ProjectTypesAssetsId }, new { Id = User.GetLoggedInUserId<string>(), name = User.GetLoggedInUserName() }));
                    //projectsSelectionReasonsList.ForEach(x => _logger.LogInformation("Project selection reasons added for project, Project selection reasons: {ProjectSelectionReasonsData}, User: {User}", new { ProjectId = project.ProjectId, ProjectSelectionReasonsId = x.ProjectSelectionReasonsId, ProjectsSelectionReasonsId = x.ProjectsSelectionReasonsId }, new { Id = User.GetLoggedInUserId<string>(), name = User.GetLoggedInUserName() }));
                    //projectsSocialBenefitsList.ForEach(x => _logger.LogInformation("Project social benfits added for project, Project social benefits: {ProjectSocialBenefitsData}, User: {User}", new { ProjectId = project.ProjectId, ProjectSocialBenefitsId = x.ProjectSocialBenefitsId, ProjectsSocialBenefitsId = x.ProjectsSocialBenefitsId }, new { Id = User.GetLoggedInUserId<string>(), name = User.GetLoggedInUserName() }));

                    return true;
                }
                catch (Exception ex)
                {
                    // Also send the message
                    return false;
                }
            }
        }
    }
}