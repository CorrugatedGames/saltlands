
namespace SaltLands.WorldGenerator;

internal enum Biome
{
    Ocean,
    ShallowWater,
    DeepWater,
    Beach,
    Scorched,
    Bare,
    Tundra,
    Snow,
    TemperateDesert,
    Shrubs,
    Taiga,
    Grassland,
    TemperateDeciduousForest,
    TemperateRainForest,
    SubtropicalDesert,
    TropicalSeasonalForest,
    TropicalRainForest
}

internal class WorldGenerator
{
    private WorldSettings Settings;
    public readonly ChunkManager ChunkManager;

    public WorldGenerator(WorldSettings settings)
    {
        Settings = settings;
        ChunkManager = new ChunkManager(Settings);
    }

    public void GenerateAroundCenter(int chunkX, int chunkY, int radius = -1)
    {
        if (radius < 1) radius = Settings.MagicNumbers.ChunkRadiusMax;

        for(int x = chunkX - radius; x < chunkX + radius; x++)
        {
            for(int y = chunkY - radius; y < chunkY + radius; y++)
            {
                ChunkManager.LoadChunk(x, y);
            }
        }
    }
}
