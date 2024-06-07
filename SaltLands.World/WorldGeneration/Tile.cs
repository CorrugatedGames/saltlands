using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class Tile
{
    #region Public Fields

    public float Height;
    public float Moisture;
    public Vector2 Position;
    public float Temperature;

    #endregion Public Fields

    #region Public Properties

    public Biome Biome
    {
        get
        {
            return BiomeTools.GetBiome(Height, Moisture);
        }
    }

    #endregion Public Properties
}