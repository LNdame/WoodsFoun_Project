using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Common;
using System.Data.Entity;
using System.Configuration;

namespace Repository
{
    public class DAL
    {
        //Connection String
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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
                dbCmd.Parameters.Add("@HeadOfHousehold", SqlDbType.NVarChar, 50).Value = user.HeadOfHousehold;//
                dbCmd.Parameters.Add("@GPSLatitude", SqlDbType.NVarChar, 50).Value = user.GPSLatitude;//
                dbCmd.Parameters.Add("@GPSLongitude", SqlDbType.NVarChar, 50).Value = user.GPSLongitude;//
                dbCmd.Parameters.Add("@IDNo", SqlDbType.NVarChar, 50).Value = user.IdentityNo;//
                dbCmd.Parameters.Add("@ClinicUsed", SqlDbType.NVarChar, 50).Value = user.ClinicUsed;//            
                dbCmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = user.DateOfBirth;//

                dbCmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = user.Gender;//            
                dbCmd.Parameters.Add("@AttendingSchool", SqlDbType.NVarChar, 50).Value = user.AttendingSchool;//
                dbCmd.Parameters.Add("@Grade", SqlDbType.NVarChar, 50).Value = user.Grade;//
                dbCmd.Parameters.Add("@NameofSchool", SqlDbType.NVarChar, 300).Value = user.NameofSchool;//
                //dbCmd.Parameters.Add("@Country", SqlDbType.NVarChar, 300).Value = user.ChowName;//

                int i = dbCmd.ExecuteNonQuery();


                //KIPSON: Check if a user have email address for sending an email
                //if (i == 1 && !String.IsNullOrEmpty(user.EmailAddress))
                //{
                //    SendProofOfRegistration(user.UserID);
                //}
                return i;

            }
        }

        public Client GetClient(string id)
        {
            var client = new Client();

            if (id != null)
            {
                using (var context = new impiloEntities())
                {
                    var found = context.Client.FirstOrDefault(x => x.IDNo == id);
                    if (found != null)
                        return found;

                }
            }
            return client;
        }

        public int SaveScreening(Screening screening)
        {
            using (var context = new impiloEntities())
            {
                if (screening.ScreeningID != String.Empty)
                {

                    context.Screening.Attach(screening);
                    context.Entry(screening).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.Screening.Add(screening);
                    context.SaveChanges();
                }

            }
            return 0;
        }

        public int RegisterChow(Users user)
        {
            //KIPSON : Check if fields that are not requred are Empty when adding a person
            using (SqlConnection dbConn = new SqlConnection(ConnectionString))
            {
                dbConn.Open();

                SqlCommand dbCmd = new SqlCommand("RegisterChow", dbConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                //User

                dbCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;//
                dbCmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;//    
                dbCmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = user.Gender;//            
                dbCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;//            
                dbCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = user.Password;//
                dbCmd.Parameters.Add("@CellNumber", SqlDbType.NVarChar, 50).Value = user.CellNumber;//
                int i = dbCmd.ExecuteNonQuery();

                return i;

            }
        }
    }

    
}
