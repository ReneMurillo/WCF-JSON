using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRolService.Models
{
    public class Roles
    {
        public int IdRol { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}