using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using UserRolService.Context;
using UserRolService.Models;

namespace UserRolService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserRol" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserRol.svc or UserRol.svc.cs at the Solution Explorer and start debugging.
    public class UserRol : IUserRol
    {
        private UserRolEntities db = new UserRolEntities();

        public bool ActualizarRol(Roles rol)
        {
            try
            {
                var actualizarRol = db.Rols.Find(rol.IdRol);
                actualizarRol.Nombre = rol.Nombre;
                actualizarRol.Descripcion = rol.Descripcion;

                db.Entry(actualizarRol).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ActualizarUser(Users usuario)
        {
            try
            {
                var actualizarUser = db.Users.Find(usuario.IdUser);

                actualizarUser.Nombre = usuario.Nombre;
                actualizarUser.Correo = usuario.Correo;
                actualizarUser.Password = Encriptar(usuario.Password);

                db.Entry(actualizarUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool CrearRol(Roles rol)
        {
            try
            {
                Rol rolSave = new Rol();
                rolSave.Nombre = rol.Nombre;
                rolSave.Descripcion = rol.Descripcion;

                db.Rols.Add(rolSave);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CrearUser(Users usuario)
        {
            try
            {
                User userSave = new User();
                userSave.Nombre = usuario.Nombre;
                userSave.Correo = usuario.Correo;
                userSave.Password = Encriptar(usuario.Password);

                db.Users.Add(userSave);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool EliminarRol(Roles role)
        {
            try
            {

                var rol = db.Rols.Find(role.IdRol);
                db.Rols.Remove(rol);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool EliminarUser(Users usuario)
        {
            try
            {
                

                var user = db.Users.Find(usuario.IdUser);
                db.Users.Remove(user);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Roles> ListarRoles()
        {
            var roles = (from r in db.Rols
                         select new Roles
                         {
                             IdRol = r.IdRol,
                             Nombre = r.Nombre,
                             Descripcion = r.Descripcion
                         }).ToList();

            return roles;
        }

        public List<Users> ListarUsuarios()
        {
            var users = (from u in db.Users
                         select new Users
                         {
                             IdUser = u.IdUser,
                             Nombre = u.Nombre,
                             Correo = u.Correo,
                             Password = u.Password
                         }).ToList();

            return users;
        }

        public Roles VerRol(string id)
        {
            int idRol = int.Parse(id);
            var rol = db.Rols.Find(idRol);
            Roles retornoRol = new Roles();
            retornoRol.IdRol = rol.IdRol;
            retornoRol.Nombre = rol.Nombre;
            retornoRol.Descripcion = rol.Descripcion;

            return retornoRol;
        }

        public Users VerUser(string id)
        {
            int idUser = int.Parse(id);
            var user = db.Users.Find(idUser);
            Users usuario = new Users();
            usuario.IdUser = user.IdUser;
            usuario.Nombre = user.Nombre;
            usuario.Correo = user.Correo;
            usuario.Password = user.Password;

            return usuario;
        }

        private string Encriptar(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
