
namespace SaltLands.WorldGenerator;

internal class WorldGenerator
{
    private WorldSettings Settings;
    public readonly ChunkManager ChunkManager;

    public FastNoiseLite FastNoise { get; private set; }

    public WorldGenerator(WorldSettings settings)
    {
        Settings = settings;
        ChunkManager = new ChunkManager(Settings, this);
        FastNoise = new FastNoiseLite(Settings.NumericSeed);

        FastNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        FastNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
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
