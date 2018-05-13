using NPoco;

namespace BaseProject.DataAccess.Repository
{
    public class DbContext
    {
        public static Database GetInstance()
        {
            return new Database("DataBaseConnection");
        }
    }
}
