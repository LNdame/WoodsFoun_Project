using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Impilo_App.ReportQueries.Format_2.Diabetes
{
    class Diabetes33bQ
    {
        public static string Query()
        {
            string Result = "x";

            //SqlConnection tempConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;);

            //try
            //{
            //    tempConnection.Open();
            //    SqlCommand tempCommand = new SqlCommand("SPName",tempConnection);
            //    tempCommand.CommandType = CommandType.StoredProcedure;
            //    // Parameters

            //    Result = ((int)tempCommand.ExecuteScalar()).ToString();
            //}
            //catch{}
            //finally
            //{
            //    tempConnection.Close();
            //}

            return Result;
        }
    }
}
