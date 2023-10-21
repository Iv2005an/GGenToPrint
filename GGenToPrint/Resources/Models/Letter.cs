using SQLite;
using System.ComponentModel.DataAnnotations;

namespace GGenToPrint.Resources.Models;

[Table("letters")]
public class Letter
{
    [Column("letter_id"), PrimaryKey, AutoIncrement]
    public int LetterId { get; set; }

    [Column("font_id")]
    public byte FontId { get; set; }

    [Column("character")]
    public string Character { get; set; }

    [Column("commands")]
    public string Commands { get; set; }

    [Column("connection_type")]
    public byte ConnectionType { get; set; }
}