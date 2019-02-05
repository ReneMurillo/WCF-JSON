using ClienteAspNet.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClienteAspNet.Controllers
{
    public class UsuariosController : Controller
    {
        private string urlBase = "http://localhost:63394/UserRol.svc/";

        public ActionResult Index()
        {
            var Cliente = new WebClient();
            var Cadena = Cliente.DownloadString(urlBase + "usuarios");
            var Js = new JavaScriptSerializer();
            var Datos = Js.Deserialize<List<UserView>>(Cadena);

            return View(Datos.ToList());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(UserView usuario)
        {
            try
            {
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(UserView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, usuario);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(urlBase + "crearuser", "POST", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(usuario);
            }
        }

        public ActionResult Detalle(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "usuario/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<UserView>(cadena);

            return View(datos);
        }

        public ActionResult Editar(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "usuario/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<UserView>(cadena);

            return View(datos);
        }

        [HttpPost]
        public ActionResult Editar(UserView usuario)
        {
            try
            {
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(UserView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, usuario);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(urlBase + "actualizaruser", "PUT", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(usuario);
            }
        }

        public ActionResult Eliminar(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "usuario/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<UserView>(cadena);

            return View(datos);
        }

        [HttpPost]
        public ActionResult Eliminar(UserView usuario, int id)
        {
            try
            {
                var cliente = new WebClient();
                var cadena = cliente.DownloadString(urlBase + "usuario/" + id);
                var JS = new JavaScriptSerializer();
                var datos = JS.Deserialize<UserView>(cadena);


                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(UserView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, datos);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente2 = new WebClient();
                cliente2.Headers["Content-type"] = "application/json";
                cliente2.Encoding = Encoding.UTF8;
                cliente2.UploadString(urlBase + "eliminaruser", "DELETE", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(usuario);
            }
            
        }
    }
}