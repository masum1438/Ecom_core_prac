using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;

namespace Ecom_core_prac.Models
{
    public class BaseAccount
    {
        private readonly IConfiguration _configuration;

        public BaseAccount(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckCustomer(string userName, string password)
        {
            bool status = false;
            string connString = _configuration.GetConnectionString("ConnString");

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                try
                {
                    string query = "SELECT * FROM CoreMember WHERE Name = @Name";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", userName);
                       

                        DataTable dataTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            status = true;
                        }
                    }
                }
                catch (Exception)
                {
                    status = false;
                }
            }

            return status;
        }
        public bool VerifyUser(string userName, string password)
        {
            bool status = false;
            string connString = _configuration.GetConnectionString("ConnString");

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                try
                {
                    string query = "SELECT * FROM CoreMember WHERE Name = @Name AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", userName);
                        cmd.Parameters.AddWithValue("@Password", password);

                        DataTable dataTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            status = true;
                        }
                    }
                }
                catch (Exception)
                {
                    status = false;
                }
            }

            return status;
        }
        public bool VerifyUser2(string txtUsername, string newPassword)
        {
            bool status = false;
            string connString = _configuration.GetConnectionString("ConnString");

            using (SqlConnection sqlConnection = new SqlConnection(connString)) {
                try
                {
                    sqlConnection.Open();
                    string query = "UPDATE CoreMember SET Password = @NewPassword WHERE Name = @UserName";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.CommandType = CommandType.Text;

                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@UserName", txtUsername);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        status = true; // Password successfully updated
                    }

                    cmd.Dispose();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    sqlConnection.Close();
                }
            }

              

            return status;
        }
        public static DataTable ListCustomer(IConfiguration _configuration)
        {
            DataTable dataTable = new DataTable();
            string connString = _configuration.GetConnectionString("ConnString");

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand("spCore_LstMember", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log the exception: ex.Message
                }
            }

            return dataTable;
        }

        public bool SaveUserToDatabase(IFormCollection formcoll)
        {
            bool status = false;
            string ConnString = _configuration.GetConnectionString("ConnString");
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                
                sqlConnection.Open();
                string Query = "spCore_InsMember";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@Name", formcoll["Name"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Password", formcoll["Password"].ToString()));
                            
                cmd.Parameters.Add(new SqlParameter("@Age", Convert.ToInt32(formcoll["Age"].ToString())));
                
                cmd.Parameters.Add(new SqlParameter("@ServiceType", formcoll["ServiceType"].ToString()));
                

                cmd.CommandTimeout = 0;

                int returnvalue = cmd.ExecuteNonQuery();//insert delete update
                if (returnvalue > 0)
                {
                    status = true;
                }

                //transaction
                cmd.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
            }
            return status;
        }
        public bool SaveCustomer(IFormCollection formcoll)
        {
            bool status = false;
            string connString = _configuration.GetConnectionString("ConnString");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spCore_InsCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustId", Convert.ToInt32(formcoll["CustId"]));
                    cmd.Parameters.AddWithValue("@CustName", formcoll["CustName"].ToString());
                    cmd.Parameters.AddWithValue("@Address", formcoll["Address"].ToString());
                    cmd.Parameters.AddWithValue("@PhoneNumber", formcoll["PhoneNumber"].ToString());
                    cmd.Parameters.AddWithValue("@ProductDetails", formcoll["ProductDetails"].ToString());

                    decimal price = 0;
                    decimal.TryParse(formcoll["Price"].ToString(), out price);
                    cmd.Parameters.AddWithValue("@Price", price);

                    cmd.Parameters.AddWithValue("@PaymentMethod", formcoll["PaymentMethod"].ToString());

                    int rows = cmd.ExecuteNonQuery();
                    status = rows > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving customer: " + ex.Message);
                }
            }

            return status;
        }


    }
}
