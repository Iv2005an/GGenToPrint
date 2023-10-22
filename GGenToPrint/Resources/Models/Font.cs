using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("fonts")]
public class Font
{
    [Column("font_id"), PrimaryKey]
    public byte FontId { get; set; }
        = 0;

    [Column("current_font")]
    public bool CurrentFont { get; set; }
        = true;

    [Column("font_name")]
    public string FontName { get; set; }
        = "Шрифт 1";
}