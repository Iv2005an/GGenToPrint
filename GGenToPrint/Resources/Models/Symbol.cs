using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("symbols")]
public class Symbol
{
    [Column("symbol_id"), PrimaryKey]
    public int SymbolId { get; set; }

    [Column("font_id")]
    public byte FontId { get; set; }
        = 0;

    [Column("sign")]
    public string Sign { get; set; }

    [Column("g_code")]
    public string GCode { get; set; }

    [Column("connection_type")]
    public byte ConnectionType { get; set; }
        = 0;
}