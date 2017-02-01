using System.Collections.Generic;

namespace CSVLoader
{
    public class LoaderResult
    {
        public IEnumerable<Transaction> ValidTransactions { get; set; }
        public IEnumerable<InvalidRow> InvalidRows { get; set; }
    }
}
