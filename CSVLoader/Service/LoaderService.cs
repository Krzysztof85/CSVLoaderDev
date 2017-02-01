using System.Collections.Generic;
using System.IO;

namespace CSVLoader
{
    public class LoaderService : ILoaderService
    {
        private IValidator _validator;

        public LoaderService(IValidator validator)
        {
            _validator = validator;
        }

        public LoaderResult LoadData(string path)
        {
            var invalidRows = new List<InvalidRow>();
            var transactions = new List<Transaction>();

            foreach (var line in File.ReadLines(path))
            {
                var values = line.Split(',');
                string message;
                if (_validator.Validate(values, out message))
                {
                    transactions.Add(new Transaction { Account = values[0], Description = values[1], CCYCode = values[2], Value = double.Parse(values[3]) });
                }
                else
                {
                    invalidRows.Add(new InvalidRow { Line = line, Message = message });
                }
            }
            return new LoaderResult { ValidTransactions = transactions, InvalidRows = invalidRows };
        }
    }
}
