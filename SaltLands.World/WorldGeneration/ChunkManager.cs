
using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class ChunkManager
{
    private readonly WorldSettings Settings;
    private readonly ChunkGenerator ChunkGenerator;

    public List<Chunk> LoadedChunks { get; private set; } = [];

    public ChunkManager(WorldSettings settings)
    {
        Settings = settings;
        ChunkGenerator = new ChunkGenerator(settings);
    }

    public void LoadChunk(int chunkX, int chunkY)
    {
        var chunk = CreateChunk(chunkX, chunkY);

        LoadedChunks.Add(chunk);
    }

    public Chunk CreateChunk(int chunkX, int chunkY)
    {
        var newChunk = new Chunk()
        {
            Position = new Vector2(chunkX, chunkY)
        };

        ChunkGenerator.GenerateChunk(newChunk);

        return newChunk;
    }
}
