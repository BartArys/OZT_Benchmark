namespace DbCreation.Data
{
    public interface IDatabaseConnection
    {
        string ConnectionString { get; }
    }

    public class DatabaseConnection : IDatabaseConnection
    {
        public string ConnectionString => "Server=LAPTOPBARTARYS\\SQLEXPRESS;Integrated security=SSPI;database=master";
    }
}
