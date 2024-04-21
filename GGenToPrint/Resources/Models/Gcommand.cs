namespace GGenToPrint.Resources.Models;

public class GCommand
{
    public byte GType { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }

    public static IEnumerable<GCommand> ParseCommands(string gCode)
    {
        List<GCommand> gCommands = [];
        foreach (string gCommand in gCode.Split('\n'))
        {
            if (!string.IsNullOrEmpty(gCommand))
            {
                string[] args = gCommand.Split();
                gCommands.Add(new GCommand()
                {
                    GType = Convert.ToByte(args[0][1..]),
                    XCoordinate = (float)Convert.ToDouble(args[1][1..]),
                    YCoordinate = (float)Convert.ToDouble(args[2][1..])
                });
            }
        }
        return gCommands;
    }
}