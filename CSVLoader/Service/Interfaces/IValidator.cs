
namespace CSVLoader
{
    public interface IValidator
    {
        bool Validate(string[] values, out string message);
    }
}
