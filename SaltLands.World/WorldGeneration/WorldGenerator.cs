namespace SaltLands.WorldGenerator;

internal class WorldGenerator
{
    public readonly ChunkGenerator ChunkGenerator;

    private WorldSettings Settings;

    public WorldGenerator(WorldSettings settings)
    {
        Settings = settings;
        ChunkGenerator = new ChunkGenerator(Settings);
    }

    public void GenerateAroundCenter(int chunkX, int chunkY, int radius = -1)
    {
        if (radius < 1) radius = Settings.MagicNumbers.ChunkRadiusMax;

        for (int x = chunkX - radius; x < chunkX + radius; x++)
        {
            for (int y = chunkY - radius; y < chunkY + radius; y++)
            {
                ChunkGenerator.LoadChunk(x, y);
            }
        }
    }
}