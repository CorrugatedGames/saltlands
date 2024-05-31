
using System.Numerics;

namespace SaltLands;

public class Settings
{

    public readonly Vector2[] AvailableResolutions =
    {
        new Vector2(1440, 900),
        new Vector2(1600, 900),
        new Vector2(1920, 1080),
        new Vector2(1920, 1200),
        new Vector2(2560, 1440),
        new Vector2(2560, 1600),
        new Vector2(3440, 1440),
        new Vector2(3440, 2160)
    };

    public Vector2 Resolution = new Vector2(1920, 1080);
    public bool BorderlessWindows = false;

    public Settings()
    {

    }


}