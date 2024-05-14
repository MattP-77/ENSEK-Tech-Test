using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ENSEK.Classes.DataStores.Sql
{
    public class CustomerSqlDataStore : ICustomerSqlDataStore
    {
        public string EnsekConnectionString { get; set; } = "Data Source=KING\\SQL2019;Initial Catalog=ENSEK;Persist Security Info=True;Connect Timeout=6000000;MultipleActiveResultSets=true; User ID=DevelopmentUser; Password=DevelopmentUser;TrustServerCertificate=True";

        public bool Update(List<CustomerComposite> instancesToSave)
        {
            using (var dt = new DataTable())
            {
                dt.TableName = "Customer";

                dt.Columns.Add("AccountId", typeof(int));
                dt.Columns.Add("FirstName", typeof(string));
                dt.Columns.Add("LastName", typeof(string));

                foreach (var customer in instancesToSave)
                {
                    var dr = dt.NewRow();
                    dr["AccountId"] = customer.AccountId; 
                    dr["FirstName"] = customer.FirstName;
                    dr["LastName"] = customer.LastName;
                    dt.Rows.Add(dr);
                }

                using (var conn = this.OpenConnection())
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Contact].[spUpdateCustomer]";
                    cmd.Connection = conn;

                    var tvpParam = cmd.Parameters.AddWithValue("@Customers", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        //public void CreateLogEntry(MiCorporateTraceLogEntityComposite entryLogData)
        //{
        //    try
        //    {
        //        using (var conn = this.OpenConnection())
        //        {
        //            using (var cmd = new SqlCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "[TraceLog].[spSaveMiCorporateTraceLogEntry]";
        //                cmd.Connection = conn;
        //                cmd.Parameters.AddWithValue("@Id", entryLogData.Id);
        //                cmd.Parameters.AddWithValue("@AccountId", this.AccountId);
        //                cmd.Parameters.AddWithValue("@Flags", 0);
        //                cmd.Parameters.AddWithValue("@Name", entryLogData.Name);
        //                cmd.Parameters.AddWithValue("@LogText", entryLogData.LogText);
        //                cmd.Parameters.AddWithValue("@LogType", (int)entryLogData.LogEntryType);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogEntryHelper.CreateLogEntry("Grid - MiCorporate 'SaveMiCorporateSearchEntity'", ex.Message.ToString(), LogEntryTypeEnum.Error);
        //    }
        //}

        private SqlConnection OpenConnection()
        {
            try
            {
                //var conn = new SqlConnection(ConfigurationHelper.config.GetConnectionString("ENSEKConnectionString"));
                var conn = new SqlConnection(EnsekConnectionString);

                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}