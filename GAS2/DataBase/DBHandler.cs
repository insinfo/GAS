using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace GAS
{
    public class DBHandler
    {
        public string Server = "192.168.133.12";//"127.0.0.1"
        public string Port = "5432";
        public string UserId = "sisadmin";//"postgres"
        public string Password = "s1sadm1n";//"Ins257257"
        public string Database = "sistemas";
        public string Scheme = "ciente";
        public NpgsqlConnection Connection = null;

        public DBHandler Connect()
        {
            var connectionString = "Server=" + Server + ";Port=" + Port + ";UserId=" + UserId + ";Password=" + Password + ";Database=" + Database + ";"+ "Search Path="+ Scheme+";";
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);            
            Connection = conn;
            return this;
        }

        public List<KeyValuePair<string, string>> GetColumnsName(string nomeTabela)
        {
            try
            {
                var query = @"select column_name, data_type from information_schema.columns
                            where  table_schema='"+ Scheme +"' AND  table_name = '" + nomeTabela + "';";

                //var command = new NpgsqlCommand(query, conn);
                /*NpgsqlDataReader dr = query.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                        txtResults.Text += dr[i] + " ";
                }
                txtResults.Text += "\r\n \r\n";*/

                Connection.Open();
                DataSet dset = new DataSet("data");
                NpgsqlDataAdapter NpAdapter = new NpgsqlDataAdapter();
                NpAdapter.SelectCommand = new NpgsqlCommand(query, Connection);
                NpAdapter.Fill(dset, "data");
                var dtsource = dset.Tables["data"];

                //txtResults.Text = dtsource.Rows[0]["column_name"].ToString();

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                foreach (DataRow row in dtsource.Rows)
                {
                    list.Add(new KeyValuePair<string, string>(row["column_name"].ToString().Trim(), row["data_type"].ToString().Trim()));
                }
                return list;
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<KeyValuePair<string, string>> GetPrimaryKeys(string nomeTabela)
        {
            try
            {
                var query = @"SELECT a.attname, format_type(a.atttypid, a.atttypmod) AS data_type
                            FROM   pg_index i
                            JOIN   pg_attribute a ON a.attrelid = i.indrelid
                                                 AND a.attnum = ANY(i.indkey)
                            WHERE  i.indrelid = '"+ nomeTabela+"'::regclass AND    i.indisprimary;";

                
                Connection.Open();
                DataSet dset = new DataSet("data");
                NpgsqlDataAdapter NpAdapter = new NpgsqlDataAdapter();
                NpAdapter.SelectCommand = new NpgsqlCommand(query, Connection);
                NpAdapter.Fill(dset, "data");
                var dtsource = dset.Tables["data"];

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                foreach (DataRow row in dtsource.Rows)
                {
                    list.Add(new KeyValuePair<string, string>(row["attname"].ToString().Trim(), row["data_type"].ToString().Trim()));                    
                }
                return list;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<string> GetAllTables()
        {
            try
            {
                var query = @"SELECT table_name FROM information_schema.tables WHERE table_schema = '"+Scheme+"';";
                Connection.Open();
                DataSet dset = new DataSet("data");
                NpgsqlDataAdapter NpAdapter = new NpgsqlDataAdapter();
                NpAdapter.SelectCommand = new NpgsqlCommand(query, Connection);
                NpAdapter.Fill(dset, "data");
                var dtsource = dset.Tables["data"];

                List<string> list = new List<string>();
                foreach (DataRow row in dtsource.Rows)
                {
                    list.Add(row["table_name"].ToString().Trim());
                }
                return list;
            }
            finally
            {
                Connection.Close();
            }
        }

    }
}
