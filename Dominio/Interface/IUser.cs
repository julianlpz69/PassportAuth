using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interface
{
    public interface IUser :IGenericRepository<User>
    {
        
    Task<User> GetByUserGmailAsync(string username);
    Task<User> GetByRefreshTokenAsync(string username);
    }
}