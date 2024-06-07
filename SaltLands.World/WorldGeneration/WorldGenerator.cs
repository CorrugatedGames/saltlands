namespace SaltLands.WorldGenerator;

internal class WorldGenerator
{
    #region Public Fields

    public readonly ChunkGenerator ChunkGenerator;

    #endregion Public Fields

    #region Private Fields

    private WorldSettings Settings;

    #endregion Private Fields

    #region Public Constructors

    public WorldGenerator(WorldSettings settings)
    {
        Settings = settings;
        ChunkGenerator = new ChunkGenerator(Settings);
    }

    #endregion Public Constructors

    #region Public Methods

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

    #endregion Public Methods
}