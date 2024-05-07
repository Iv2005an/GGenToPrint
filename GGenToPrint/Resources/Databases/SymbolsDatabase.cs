using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Databases;

public class SymbolsDatabase
{
    private SymbolsDatabase() { }

    private static SymbolsDatabase instance;

    async public static Task<SymbolsDatabase> GetInstance()
    {
        if (instance == null)
        {
            instance = new SymbolsDatabase();
            await instance.Init();
        }
        return instance;
    }
    private SQLiteAsyncConnection Connection;

    private async Task Init()
    {
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Symbol>();
    }

    public async Task<IEnumerable<Symbol>> GetAllSymbols() =>
        await Connection.Table<Symbol>().ToListAsync();

    public async Task<IEnumerable<Symbol>> GetSymbols(byte fontId) =>
        await Connection.Table<Symbol>().Where(symbol => symbol.FontId == fontId).ToListAsync();
    public async Task AddSymbol(Symbol symbol)
    {
        var symbols = await GetAllSymbols();
        if (symbols.Any())
        {
            symbol.SymbolId = (byte)(symbols.LastOrDefault().SymbolId + 1);
        }
        else
        {
            symbol.SymbolId = 0;
        }
        await Connection.InsertAsync(symbol);
    }
    public async Task DeleteSymbol(Symbol symbol)
    {
        await Connection.DeleteAsync(symbol);
        var symbols = (await GetAllSymbols()).ToList();
        if (symbols.Count != 0)
        {
            for (int symbolIndex = 0; symbolIndex < symbols.Count; symbolIndex++)
            {
                symbols[symbolIndex].SymbolId = symbolIndex;
            }
            await Connection.DeleteAllAsync<Symbol>();
            await Connection.InsertAllAsync(symbols);
        }
    }
    public async Task UpdateSymbol(Symbol updatedSymbol) =>
        await Connection.UpdateAsync(updatedSymbol);
}