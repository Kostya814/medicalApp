using MySqlConnector;

namespace MedicalApp;

public class DBHelper
{
    public MySqlConnectionStringBuilder _connectionString { get; }

    public DBHelper()
    {
        _connectionString = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "up",
            UserID = "root",
            Password = "123456"

        };
    }
}