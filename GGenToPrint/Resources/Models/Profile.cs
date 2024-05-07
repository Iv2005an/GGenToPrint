using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("profiles")]
public class Profile
{
    [Column("profile_id"), PrimaryKey]
    public byte ProfileId { get; set; }
        = 0;

    [Column("current_profile"), NotNull]
    public bool CurrentProfile { get; set; }
        = true;

    [Column("profile_name"), NotNull]
    public string ProfileName { get; set; }
        = "Профиль 1";

    [Column("save_path"), NotNull]
    public string SavePath { get; set; }
        = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    [Column("num_cells_of_vertical"), NotNull]
    public byte NumCellsOfVertical { get; set; }
        = 40;

    [Column("num_cells_of_horizontal"), NotNull]
    public byte NumCellsOfHorizontal { get; set; }
        = 34;

    [Column("num_cells_of_margin"), NotNull]
    public byte NumCellsOfMargin { get; set; }
        = 4;

    [Column("sheet_type_index"), NotNull]
    public byte SheetTypeIndex { get; set; }
        = 0;

    [Column("sheet_position_index"), NotNull]
    public byte SheetPositionIndex { get; set; }
        = 0;

    [Column("cell_size"), NotNull]
    public byte CellSize { get; set; }
        = 5;

    [Column("lift_for_moving"), NotNull]
    public byte LiftForMoving { get; set; }
        = 10;

    [Column("unevenness_of_writing"), NotNull]
    public bool UnevennessOfWriting { get; set; }
        = false;
}