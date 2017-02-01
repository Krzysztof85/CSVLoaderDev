using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVLoader
{
    public interface IDbWriterService
    {
        Task InsertData(IEnumerable<Transaction> transactions);
        event EventHandler<PercentageEventArgs> DataCopiedPercentage;
    }
}
