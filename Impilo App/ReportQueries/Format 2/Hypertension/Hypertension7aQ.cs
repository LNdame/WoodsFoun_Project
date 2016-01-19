using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Impilo_App.ReportQueries.Format_2.Hypertension
{
    class Hypertension7aQ
    {
        public static string Query()
        {
            string Result = "x";

            SqlConnection tempConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            try
            {
                tempConnection.Open();
                SqlCommand tempCommand = new SqlCommand("ReportFormat2-7a", tempConnection);
                tempCommand.CommandType = CommandType.StoredProcedure;
                tempCommand.Parameters.AddWithValue("@StartDate", Views.Reports.Format2Report.StartDate);
                tempCommand.Parameters.AddWithValue("@EndDate", Views.Reports.Format2Report.EndDate);
                tempCommand.Parameters.AddWithValue("@Type", 2);

                Result = ((int)tempCommand.ExecuteScalar()).ToString();
            }
            catch { }
            finally
            {
                tempConnection.Close();
            }

            return Result;
        }
    }
}
