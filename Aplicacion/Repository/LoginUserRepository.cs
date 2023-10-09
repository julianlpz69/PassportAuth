using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class LoginUserRepository : GenericRepository<LoginUser>, ILoginUser
    {
    private readonly ApiPassportContext _context;

    public LoginUserRepository(ApiPassportContext context) : base(context)
    {
        _context = context;
    }


     public async Task<LoginUser> Ultimo()
     {   
    
        var medicamentosEscasos =  await _context.LoginUser.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
         return medicamentosEscasos;
                

     }
    }
}