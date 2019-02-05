using ClienteAspNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ClienteAspNet.Controllers
{
    public class RolesController : Controller
    {
        private string urlBase = "http://localhost:63394/UserRol.svc/";

        public ActionResult Index()
        {
            var Cliente = new WebClient();
            var Cadena = Cliente.DownloadString(urlBase + "roles");
            var Js = new JavaScriptSerializer();
            var Datos = Js.Deserialize<List<RolesView>>(Cadena);

            return View(Datos.ToList());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(RolesView rol)
        {
            try
            {
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(RolesView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, rol);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(urlBase + "crearrol", "POST", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(rol);
            }
        }

        public ActionResult Detalle(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "rol/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<RolesView>(cadena);

            return View(datos);
        }

        public ActionResult Editar(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "rol/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<RolesView>(cadena);

            return View(datos);
        }

        [HttpPost]
        public ActionResult Editar(RolesView rol)
        {
            try
            {
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(RolesView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, rol);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(urlBase + "actualizarrol", "PUT", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(rol);
            }
        }

        public ActionResult Eliminar(int id)
        {
            var cliente = new WebClient();
            var cadena = cliente.DownloadString(urlBase + "rol/" + id);
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<RolesView>(cadena);

            return View(datos);
        }

        [HttpPost]
        public ActionResult Eliminar(RolesView rol, int id)
        {
            try
            {
                var cliente = new WebClient();
                var cadena = cliente.DownloadString(urlBase + "rol/" + id);
                var JS = new JavaScriptSerializer();
                var datos = JS.Deserialize<RolesView>(cadena);


                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(RolesView));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, datos);

                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente2 = new WebClient();
                cliente2.Headers["Content-type"] = "application/json";
                cliente2.Encoding = Encoding.UTF8;
                cliente2.UploadString(urlBase + "eliminarrol", "DELETE", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(rol);
            }

        }
    }
}