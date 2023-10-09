using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interface
{
    public interface ILoginUser : IGenericRepository<LoginUser>
    {
        Task<LoginUser> Ultimo();
    }
}