using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class DAL {
        private static string _ReadOnlyConnectionString;
        private static string _EditOnlyConnectionString;

        internal enum dbAction {
            Read,
            Edit
        }

        #region Database Connections

        internal static string ConnectionString(dbAction action = dbAction.Read) {
            string retString = "";
            if (action == dbAction.Read) retString = _ReadOnlyConnectionString;
            else retString = _EditOnlyConnectionString;
            return retString;
        }

        internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read) {
            try {
                comm.Connection = new SqlConnection(ConnectionString(action));
                comm.CommandType = System.Data.CommandType.StoredProcedure;
            } catch (Exception ex) {
                ShowErrorMessage(ex);
            }
        }

        public static SqlDataReader GetDataReader(SqlCommand comm) {
            try {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            } catch (Exception ex) {
                ShowErrorMessage(ex);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }


        internal static int AddObject(SqlCommand comm, string parameterName) {

            int retInt = 0;

            try {
                ConnectToDatabase(comm, dbAction.Edit);
                comm.Connection.Open();
                SqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();

            } catch (Exception ex) {
                ShowErrorMessage(ex);
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = 0;
                ShowErrorMessage(ex);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        internal static int UpdateObject(SqlCommand comm) {
            int retInt = 0;
            try {
                ConnectToDatabase(comm, dbAction.Edit);
                comm.Connection.Open();
                retInt = (int)comm.ExecuteNonQuery();
                comm.Connection.Close();
            } catch (Exception ex) {
                ShowErrorMessage(ex);
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = 0;
                ShowErrorMessage(ex);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        internal static void ShowErrorMessage(Exception ex) {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        #endregion



        #region SetSettings
        public static void Set(string readConnectionString, string editConnectionString) {
            _ReadOnlyConnectionString = readConnectionString;
            _EditOnlyConnectionString = editConnectionString;
        }
        #endregion
    }
}
