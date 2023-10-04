using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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