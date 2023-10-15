using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("profiles")]
public class Profile
{
    [PrimaryKey, AutoIncrement]
    [Column("profile_id")]
    public byte ProfileId { get; set; }

    [Column("profile_name")]
    public string ProfileName { get; set; }

    [Column("num_cells_of_vertical")]
    public byte NumCellsOfVertical { get; set; }

    [Column("num_cells_of_horizontal")]
    public byte NumCellsOfHorizontal { get; set; }

    [Column("num_cells_of_margin")]
    public byte NumCellsOfMargin { get; set; }

    [Column("sheet_type_index")]
    public byte SheetTypeIndex { get; set; }

    [Column("sheet_position_index")]
    public byte SheetPositionIndex { get; set; }

    [Column("cell_size")]
    public byte CellSize { get; set; }

    [Column("lift_for_moving")]
    public byte LiftForMoving { get; set; }

    [Column("unevenness_of_writing")]
    public bool UnevennessOfWriting { get; set; }
}