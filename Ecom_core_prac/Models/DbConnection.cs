using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecom_core_prac.Models
{
    public class DbConnection
    {
        public static IConfiguration Configuration { get; set; }
        public static string GetDBConstring()
        {
            string strConnection = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string connString = Configuration.GetConnectionString("ConnString");
            return connString;
            //return Configuration["ConnString"].ToString();
        }


        public static void SaveErrorLog(string fromScreen, string ErrorDescription)
        {
            string ConnString = GetDBConstring();
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_InsErrorLog";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@EventRaiseScreen", fromScreen));
            cmd.Parameters.Add(new SqlParameter("@ErrorDescription", ErrorDescription));
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }
    }
}
