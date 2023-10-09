using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class LoginUser : BaseEntity
    {
        public string UserName {get; set;}
        public string Picture {get; set;}
        public string UserEmail {get; set;}

    }
}