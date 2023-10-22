using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("profiles")]
public class Profile
{
    [PrimaryKey]
    [Column("profile_id")]
    public byte ProfileId { get; set; }
        = 0;

    [Column("current_profile")]
    public bool CurrentProfile { get; set; }
        = true;

    [Column("profile_name")]
    public string ProfileName { get; set; }
        = "Профиль 1";

    [Column("save_path")]
    public string SavePath { get; set; }
        = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    [Column("num_cells_of_vertical")]
    public byte NumCellsOfVertical { get; set; }
        = 40;

    [Column("num_cells_of_horizontal")]
    public byte NumCellsOfHorizontal { get; set; }
        = 34;

    [Column("num_cells_of_margin")]
    public byte NumCellsOfMargin { get; set; }
        = 4;

    [Column("sheet_type_index")]
    public byte SheetTypeIndex { get; set; }
        = 0;

    [Column("sheet_position_index")]
    public byte SheetPositionIndex { get; set; }
        = 0;

    [Column("cell_size")]
    public byte CellSize { get; set; }
        = 5;

    [Column("lift_for_moving")]
    public byte LiftForMoving { get; set; }
        = 10;

    [Column("unevenness_of_writing")]
    public bool UnevennessOfWriting { get; set; }
        = false;
}