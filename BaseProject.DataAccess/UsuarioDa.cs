using NLog;

namespace BaseProject.DataAccess
{
    public class UsuarioDa
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //public static IEnumerable<Usuario> GetAll()
        //{
        //    var result = new List<Usuario>();

        //    try
        //    {
        //        using (IDatabase db = DbContext.GetInstance())
        //        {
        //            #region Select All
        //            result = db.Fetch<Usuario>();
        //            #endregion
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message);
        //    }

        //    return result;
        //}

        //public static Usuario Get(int pkUsuario)
        //{
        //    var result = new Usuario();

        //    try
        //    {
        //        using (IDatabase db = DbContext.GetInstance())
        //        {
        //            result = db.SingleOrDefaultById<Usuario>(pkUsuario);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message);
        //    }

        //    return result;
        //}

        //public static Result Save(Usuario usuario)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        using (IDatabase db = DbContext.GetInstance())
        //        {
        //            var oUsuario = db.SingleById<Usuario>(usuario.pkUsuario);
        //            oUsuario.cUsuario = usuario.cUsuario;
        //            oUsuario.cPassword = usuario.cPassword;
        //            db.Save(oUsuario);
        //        }

        //        result.Success = true;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message);
        //        result.Message = e.Message;
        //    }

        //    return result;
        //}

        //public static Result Delete(int pkUsuario)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        using (IDatabase db = DbContext.GetInstance())
        //        {
        //            db.Delete<Usuario>(pkUsuario);
        //        }

        //        result.Success = true;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message);
        //        result.Message = e.Message;
        //    }

        //    return result;
        //}
    }
}
