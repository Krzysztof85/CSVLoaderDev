using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CSVLoader
{
    public class DbWriterService : IDbWriterService
    {
        public event EventHandler<PercentageEventArgs> DataCopiedPercentage;
        private readonly string[] columnNames = { "Account", "Description", "CCYCode", "Value" };
        private string _connectionString;

        public DbWriterService(string connectonString)
        {
            _connectionString = connectonString;
        }

        public async Task InsertData(IEnumerable<Transaction> transactions)
        {
            var dt = CreateTable();
            FillDataTable(dt, transactions);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
            {
                bulkCopy.BatchSize = 10000;
                bulkCopy.DestinationTableName = "[dbo].[Transaction]";
                bulkCopy.NotifyAfter = 1000;
                foreach (var columnName in columnNames) bulkCopy.ColumnMappings.Add(columnName, columnName);

                bulkCopy.SqlRowsCopied += (sender, e) => { if (DataCopiedPercentage != null) DataCopiedPercentage(this, new PercentageEventArgs((int)Math.Ceiling((e.RowsCopied * 100) / (double)dt.Rows.Count))); };
                await bulkCopy.WriteToServerAsync(dt);
            }
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(columnNames[0], typeof(string));
            dt.Columns.Add(columnNames[1], typeof(string));
            dt.Columns.Add(columnNames[2], typeof(string));
            dt.Columns.Add(columnNames[3], typeof(double));
            return dt;
        }

        private void FillDataTable(DataTable dt, IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var newRow = dt.NewRow();
                newRow[0] = transaction.Account;
                newRow[1] = transaction.Description;
                newRow[2] = transaction.CCYCode;
                newRow[3] = transaction.Value;
                dt.Rows.Add(newRow);
            }
        }
    }
}
