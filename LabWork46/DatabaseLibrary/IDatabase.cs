namespace DatabaseLibrary
{
    public interface IDatabase
    {
        int ExecuteQuery(string sqlCommand);
        bool UpdateGame(int id, string newName, decimal newPrice);
        bool InsertGame(string name, decimal price, int year);
    }

}