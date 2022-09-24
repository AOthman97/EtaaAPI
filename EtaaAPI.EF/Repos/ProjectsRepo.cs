﻿using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;
using System.Linq.Expressions;

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
        }

        public int GetNumberOfInstallments(int ProjectId)
        {
            int NumberOfInstallments = (from project in _context.Projects
                                        where project.ProjectId == ProjectId
                                        select (int)project.NumberOfInstallments).Single();

            return NumberOfInstallments;
        }
    }
}