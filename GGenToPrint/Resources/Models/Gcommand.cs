namespace GGenToPrint.Resources.Models;

public class GCommand
{
    public static IEnumerable<GCommand> ParseCommands(string commands)
    {
        var Commands = new List<GCommand>();
        foreach (string gCommand in commands.Split('\n'))
        {
            if (!string.IsNullOrEmpty(gCommand))
            {
                string[] args = gCommand.Split();
                Commands.Add(new GCommand()
                {
                    GCode = args[0],
                    XCoordinate = (float)Convert.ToDouble(args[1][1..]),
                    YCoordinate = (float)Convert.ToDouble(args[2][1..])
                });
            }
        }
        return Commands;
    }

    public string GCode { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
}