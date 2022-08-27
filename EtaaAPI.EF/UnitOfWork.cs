using EtaaApi.Core.Models;
using EtaaAPI.Core.Interfaces;
using EtaaAPI.Core.Repos;
using EtaaAPI.EF.Repos;
using MoviesProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtaaAPI.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // You should do this for all models, The 'private set is because we only want to assign it values privately here'
        public IBaseRepo<Contributor> Contributors { get; private set; }
        public IBaseRepo<State> States { get; private set; }
        public IBaseRepo<City> Cities { get; private set; }
        public IBaseRepo<District> Districts { get; private set; }
        public IBaseRepo<AccommodationType> AccommodationTypes { get; private set; }
        public IProjectsRepo Projects { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // You should do this for all models
            Contributors = new BaseRepo<Contributor>(_context);
            States = new BaseRepo<State>(_context);
            Cities = new BaseRepo<City>(_context);
            Districts = new BaseRepo<District>(_context);
            AccommodationTypes = new BaseRepo<AccommodationType>(_context);
            Projects = new ProjectsRepo(_context);
        }

        // All this what do is to return the number of rows affected
        public int Complete()
        {
            return _context.SaveChanges();
        }

        // Dispose the connection
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}