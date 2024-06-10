using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class Tile
{
    public float Height;
    public float Moisture;
    public Vector2 Position;
    public float Temperature;

    public Biome Biome
    {
        get
        {
            return BiomeTools.GetBiome(Height, Moisture);
        }
    }
}