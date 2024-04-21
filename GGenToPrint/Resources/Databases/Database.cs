using SQLite;

namespace GGenToPrint.Resources.Databases;

public static class Database
{
    internal static SQLiteAsyncConnection Connection => new(
        Path.Combine(FileSystem.AppDataDirectory, "GGenToPrint.db"));
}