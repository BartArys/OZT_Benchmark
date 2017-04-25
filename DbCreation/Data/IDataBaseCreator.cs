namespace DbCreation.Data
{
    public interface IDataBaseUrls
    {
        string CreationQueryName { get; }
        string RemovalQueryName { get; }
    }

    public class MySqDataBaseUrls : IDataBaseUrls
    {
        public string CreationQueryName => "C:/Users/Bart Arys/Documents/Visual Studio 2015/Projects/Benchmark/DbCreation/Queries/MySQLCreation.txt";

        public string RemovalQueryName => "C:/Users/Bart Arys/Documents/Visual Studio 2015/Projects/Benchmark/DbCreation/Queries/MySQLRemoval.txt";
    }
}
