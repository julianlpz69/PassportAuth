using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Interface;

        public interface IUnitOfWork
    {
        IRol Roles { get; }
        IUser Users { get; }
        ILoginUser LoginUsers { get; }
        Task<int> SaveAsync(); 
    }
