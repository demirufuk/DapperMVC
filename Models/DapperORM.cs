using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DapperMVC.Models
{
    public static class DapperORM
    {
        private static string connectionString = 
            @"Data Source=IT-PN11;Initial Catalog=DapperDb;Integrated Security=True;";


        public static void ExecuteWithoutReturn(string procedurName, DynamicParameters param=null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                con.Execute(procedurName, param, commandType: CommandType.StoredProcedure);
                con.Close();
            }

        }


        ///DapperORM.ExecuteReturnScalar<int>(_,_);
        public static T ExecuteReturnScalar<T>(string procedurName, DynamicParameters param =null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return (T)Convert.ChangeType(con.ExecuteScalar(procedurName, param,
                    commandType: CommandType.StoredProcedure), typeof(T));
            }
        }


        //DapperORM.ReturnList<model> IEnumerable<model>
        public static IEnumerable<T> ReturnList<T>(string procedurName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return con.Query<T>(procedurName, param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}