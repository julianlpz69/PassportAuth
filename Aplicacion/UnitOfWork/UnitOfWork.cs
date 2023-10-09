using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Repository;
using Dominio.Interface;
using Persistencia.Data;




namespace Aplicacion.UnitOfWork;

    public class UnitOfWork : IUnitOfWork, IDisposable
{
  
    private readonly ApiPassportContext _context;
	private IRol _roles;
    private ILoginUser _login_user;
    private IUser _users;
    public UnitOfWork(ApiPassportContext context){
        _context = context;
    }
    
    

 public IRol Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

     public ILoginUser LoginUsers
    {
        get
        {
            if (_login_user == null)
            {
                _login_user = new LoginUserRepository(_context);
            }
            return _login_user;
        }
    }

    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

   

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

