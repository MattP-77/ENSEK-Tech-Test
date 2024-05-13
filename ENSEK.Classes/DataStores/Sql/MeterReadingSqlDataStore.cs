using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ENSEK.Classes.DataStores.Sql
{
    public class MeterReadingSqlDataStore : IMeterReadingSqlDataStore
    {
        public string EnsekConnectionString { get; set; } = "Data Source=KING\\SQL2019;Initial Catalog=ENSEK;Persist Security Info=True;Connect Timeout=6000000;MultipleActiveResultSets=true; User ID=DevelopmentUser; Password=DevelopmentUser;TrustServerCertificate=True";

        public bool Update(List<MeterReadingComposite> instancesToSave)
        {
            using (var dt = new DataTable())
            {
                dt.TableName = "MeterReadings";

                dt.Columns.Add("AccountId", typeof(int));
                dt.Columns.Add("MeterReadingDateTime", typeof(DateTime));
                dt.Columns.Add("MeterReadValue", typeof(int));

                foreach (var meterReading in instancesToSave)
                {
                    var dr = dt.NewRow();
                    dr["AccountId"] = meterReading.AccountId;
                    dr["MeterReadingDateTime"] = meterReading.MeterReadingDateTime;
                    dr["MeterReadValue"] = meterReading.MeterReadValue;
                    dt.Rows.Add(dr);
                }

                using (var conn = this.OpenConnection())
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Reading].[spUpdateCustomerMeterReading]";
                    cmd.Connection = conn;

                    var tvpParam = cmd.Parameters.AddWithValue("@MeterReadings", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        private SqlConnection OpenConnection()
        {
            try
            {
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