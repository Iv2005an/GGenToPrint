using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("letters")]
public class Letter
{
    [Column("letter_id"), PrimaryKey]
    public int LetterId { get; set; }

    [Column("font_id")]
    public byte FontId { get; set; }
        = 0;

    [Column("character")]
    public string Character { get; set; }

    [Column("g_code")]
    public string GCode { get; set; }

    [Column("connection_type")]
    public byte ConnectionType { get; set; }
        = 0;
}