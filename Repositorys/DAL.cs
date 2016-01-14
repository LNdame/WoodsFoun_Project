using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public class DAL
    {
        //Connection String
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //Register the user
        public int RegisterUser(Users user)
        {
            //KIPSON : Check if fields that are not requred are Empty when adding a person
            using (SqlConnection dbConn = new SqlConnection(ConnectionString))
            {
                dbConn.Open();

                SqlCommand dbCmd = new SqlCommand("RegisterUser", dbConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                //User

                dbCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;//
                dbCmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;//
                dbCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.UserName;//
                dbCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = user.Password;//
                dbCmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = user.EmailAddress;//
                dbCmd.Parameters.Add("@CellNo", SqlDbType.NVarChar, 50).Value = user.CellNo;//
                dbCmd.Parameters.Add("@RefferenceName", SqlDbType.NVarChar, 50).Value = user.RefferenceName;//            
                dbCmd.Parameters.Add("@HowAboutUs", SqlDbType.NVarChar, 300).Value = user.HowAboutUs;//

                dbCmd.Parameters.Add("@CurrentCity", SqlDbType.NVarChar, 50).Value = user.CurrentCity;//            
                dbCmd.Parameters.Add("@Country", SqlDbType.NVarChar, 300).Value = user.Country;//

                int i = dbCmd.ExecuteNonQuery();


                //KIPSON: Check if a user have email address for sending an email
                //if (i == 1 && !String.IsNullOrEmpty(user.EmailAddress))
                //{
                //    SendProofOfRegistration(user.UserID);
                //}
                return i;

            }
        }
    }
}
