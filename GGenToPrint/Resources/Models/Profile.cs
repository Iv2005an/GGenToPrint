using SQLite;

namespace GGenToPrint.Resources.Models;

[Table("profiles")]
public class Profile
{
    public Profile()
    {
        ProfileId = 0;
        ProfileName = "Профиль 1";
        SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        CurrentProfile = true;
        NumCellsOfVertical = 40;
        NumCellsOfHorizontal = 34;
        NumCellsOfMargin = 4;
        SheetTypeIndex = 0;
        SheetPositionIndex = 0;
        CellSize = 5;
        LiftForMoving = 10;
        UnevennessOfWriting = false;
    }


    [PrimaryKey]
    [Column("profile_id")]
    public byte ProfileId { get; set; }

    [Column("current_profile")]
    public bool CurrentProfile { get; set; }

    [Column("profile_name")]
    public string ProfileName { get; set; }

    [Column("save_path")]
    public string SavePath { get; set; }

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