namespace GGenToPrint.Resources.Models;

public class Gcommand
{
    public static IEnumerable<Gcommand> ParseCommands(string commands)
    {
        var Commands = new List<Gcommand>();
        foreach (string command in commands.Split('\n'))
        {
            if (!string.IsNullOrEmpty(command))
            {
                string[] args = command.Split();
                Commands.Add(new Gcommand()
                {
                    Gcode = args[0],
                    XCoordinate = (float)Convert.ToDouble(args[1][1..]),
                    YCoordinate = (float)Convert.ToDouble(args[2][1..])
                });
            }
        }
        return Commands;
    }

    public string Gcode { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
}