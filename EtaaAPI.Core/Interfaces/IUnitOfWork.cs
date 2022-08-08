using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtaaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // You should do the same for all models
        IBaseRepo<Contributor> Contributors { get; }
        // This is going to be used like the db context and it's going to return the number of rows affected
        int Complete();
    }
}