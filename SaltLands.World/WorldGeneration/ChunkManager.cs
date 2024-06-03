
using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class ChunkManager
{
    private WorldSettings Settings;
    private WorldGenerator Generator;
    
    public List<Chunk> LoadedChunks { get; private set; } = new List<Chunk>();

    public ChunkManager(WorldSettings settings, WorldGenerator generator)
    {
        Settings = settings;
        Generator = generator;
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

        CreateBlankChunk(newChunk);

        return newChunk;
    }

    private void CreateBlankChunk(Chunk chunk)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;

        chunk.Tiles = new Tile[chunkSize][];

        for (int i = 0; i < chunkSize; i++)
        {
            chunk.Tiles[i] = new Tile[chunkSize];
        }

        FillChunk(chunk);
    }

    private void FillChunk(Chunk chunk)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;

        int baseX = (int)chunk.Position.X * chunkSize;
        int baseY = (int)chunk.Position.Y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                chunk.Tiles[x][y] = new Tile
                {
                    Height = Generator.FastNoise.GetNoise(baseX + x, baseY + y),
                    Position = new Vector2(x, y)
                };
            }
        }
    }
}
