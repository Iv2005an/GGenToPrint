using SQLite;

namespace GGenToPrint.Resources.Services;

public static class Database
{
    internal static readonly SQLiteAsyncConnection Connection = new(
        Path.Combine(FileSystem.AppDataDirectory, "GGenToPrint.db"));
}