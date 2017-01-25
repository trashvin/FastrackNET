using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using log4net;
using log4net.Config;

namespace Provider.Document
{
    public class ExcelHelper
    {

        private static readonly ILog tracer = LogManager.GetLogger(typeof(ExcelHelper));

        public ExcelHelper()
        {
            XmlConfigurator.Configure();        
        }

        public DataTable GetExcelDataTable(string filePath, string commandText)
        {
            DataTable table = new DataTable();

            try
            {
                OleDbConnectionStringBuilder connectionString = new OleDbConnectionStringBuilder();
                connectionString.Provider = "Microsoft.ACE.OLEDB.12.0";
                connectionString.DataSource = filePath;
                connectionString["Extended Properties"] = "Excel 12.0;HDR=Yes;IMEX=2";

                using (OleDbConnection connection = new OleDbConnection(connectionString.ConnectionString))
                {
                    connection.Open();
                    
                    OleDbCommand command = new OleDbCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;
                    command.Connection = connection;

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    
                    adapter.Fill(table);
                }

                return table;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());
                return null;
            }

        }

        public DataTable ConvertToFixAssetTable(DataTable table)
        {
            DataTable clone = table.Clone();

            try
            {
                clone.Columns[4].DataType = typeof(System.DateTime);
                clone.Columns[5].DataType = typeof(System.Int32);
                clone.Columns[6].DataType = typeof(System.Int32);
                clone.Columns[7].DataType = typeof(System.Int32);

                foreach (DataRow row in table.Rows)
                {
                    clone.ImportRow(row);
                }
                return clone;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());
                return null;
            }
        }

    }
    
}
