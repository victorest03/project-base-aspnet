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
            var t = GetUser<Usuario>();
            var r = new UsuarioBl().Query(u => u.cUsuario == "admin" && u.cPassword == "password");
            return View();
        }
    }
}