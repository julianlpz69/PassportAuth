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
    public class RolRepository : GenericRepository<Rol>, IRol
    {
    private readonly ApiPassportContext _context;

    public RolRepository(ApiPassportContext context) : base(context)
    {
        _context = context;
    }
    }
}