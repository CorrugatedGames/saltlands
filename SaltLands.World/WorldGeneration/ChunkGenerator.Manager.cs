using System.Numerics;

namespace SaltLands.WorldGenerator;

internal partial class ChunkGenerator
{
    #region Public Fields

    public Dictionary<string, Chunk> ChunkMap = [];

    #endregion Public Fields

    #region Public Properties

    public List<Chunk> LoadedChunks { get; private set; } = [];

    #endregion Public Properties

    #region Public Methods

    public Chunk? GetChunk(int chunkX, int chunkY)
    {
        var id = ChunkId(chunkX, chunkY);
        if (!ChunkMap.ContainsKey(id)) return null;

        var chunk = ChunkMap[id];

        return chunk;
    }

    public Chunk LoadChunk(int chunkX, int chunkY, bool requestedFromAnotherChunk = false)
    {
        var id = ChunkId(chunkX, chunkY);
        var chunk = GetChunk(chunkX, chunkY);
        if (chunk != null) return chunk;

        chunk = new Chunk()
        {
            LastLoadedAt = DateTime.Now.Ticks,
            Position = new Vector2(chunkX, chunkY)
        };

        GenerateChunk(chunk, requestedFromAnotherChunk);

        if (!requestedFromAnotherChunk)
        {
            LoadedChunks.Add(chunk);
            ChunkMap.Add(ChunkId(chunkX, chunkY), chunk);
        }

        return chunk;
    }

    public void UnloadChunk(int chunkX, int chunkY)
    {
        var id = ChunkId(chunkX, chunkY);
        var chunk = GetChunk(chunkX, chunkY);
        if (chunk == null) return;

        LoadedChunks.Remove(chunk);
        ChunkMap.Remove(id);
    }

    #endregion Public Methods

    #region Private Methods

    private static string ChunkId(int chunkX, int chunkY)
    {
        return $"{chunkX},{chunkY}";
    }

    #endregion Private Methods
}