using System.Web.Mvc;
using BaseProject.BusinessLogic;
using BaseProject.Common.Session;
using BaseProject.Model;

namespace BaseProject.Frontend.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            //var t = GetUser<Usuario>();
            //var r = new UsuarioBl().Query(u => u.cUsuario == "admin" && u.cPassword == "password");
            //var result = new UsuarioBl().Save(new Usuario()
            //{
            //    cUsuario = "admin",
            //    cPassword = "password",
            //    fkUsuarioCrea = 1
            //});

            //var result = new UsuarioBl().Save(new Usuario()
            //{
            //    pkUsuario = 4,
            //    cUsuario = "adminMod",
            //    cPassword = "passwordMod",
            //    fkUsuarioEdita = 1
            //});

            //var t = new UsuarioBl().GetAll();

            //var usuario = new UsuarioBl().Get(4);

            //var usuario = new UsuarioBl().Query(u => u.cUsuario == "adminMod" && u.cPassword == "passwordMod");

            //var result = new UsuarioBl().Delete(4);
            return View();
        }
    }
}