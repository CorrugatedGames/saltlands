
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SaltLands.WorldGenerator;

internal class ChunkGenerator
{
    private readonly WorldSettings Settings;

    private readonly FastNoiseLite HeightGenerator;
    private readonly FastNoiseLite MoistureGenerator;
    private readonly FastNoiseLite TemperatureGenerator;

    public List<Chunk> LoadedChunks { get; private set; } = [];

    // turn a string seed into a numeric one
    private static int NumericSeed(string seed)
    {
        return BitConverter.ToInt32(MD5.HashData(Encoding.UTF8.GetBytes(seed)));
    }

    private int RealX(Chunk chunk, Tile tile)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;
        return (int)((chunk.Position.X * chunkSize) + tile.Position.X);
    }

    private int RealY(Chunk chunk, Tile tile)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;
        return (int)((chunk.Position.Y * chunkSize) + tile.Position.Y);
    }


    private delegate void ChunkTileIterator(Chunk chunk, Tile tile);

    public ChunkGenerator(WorldSettings settings)
    {
        Settings = settings;


        HeightGenerator = new FastNoiseLite(NumericSeed($"height-{Settings.Metadata.WorldSeed}"));
        MoistureGenerator = new FastNoiseLite(NumericSeed($"moisture-{Settings.Metadata.WorldSeed}"));
        TemperatureGenerator = new FastNoiseLite(NumericSeed($"temperature-{Settings.Metadata.WorldSeed}"));

        InitializeGenerators();
    }

    private void InitializeGenerators()
    {
        HeightGenerator.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        HeightGenerator.SetFractalType(FastNoiseLite.FractalType.PingPong);
        HeightGenerator.SetFractalOctaves(5);
        HeightGenerator.SetFractalGain(0.15f);

        MoistureGenerator.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        MoistureGenerator.SetFrequency(0.003f);
        MoistureGenerator.SetFractalType(FastNoiseLite.FractalType.FBm);
        MoistureGenerator.SetFractalOctaves(3);

        TemperatureGenerator.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        TemperatureGenerator.SetFrequency(0.003f);
        TemperatureGenerator.SetFractalType(FastNoiseLite.FractalType.FBm);
        TemperatureGenerator.SetFractalOctaves(3);
        TemperatureGenerator.SetFractalGain(0.07f);

    }

    public void GenerateChunk(Chunk chunk)
    {
        CreateBlankChunk(chunk);
        FillChunk(chunk);

        IterateChunk(chunk, GetHeightForTile);

        for(int i = 0; i < Settings.MagicNumbers.ChunkGenerationIterations; i++)
        {
            SimulateChunk(chunk);
        }

        FinalizeChunk(chunk);
    }

    private void SimulateChunk(Chunk chunk)
    {
        // soil erosion
        // water carving
        // plant vegetation where applicable
        // grow vegetation if applicable
    }

    private void FinalizeChunk(Chunk chunk)
    {
    }

    private void IterateChunk(Chunk chunk, ChunkTileIterator tileFunc)
    {
        for(int x = 0; x < chunk.Tiles.Length; x++)
        {
            for(int y = 0; y < chunk.Tiles[x].Length; y++)
            {
                tileFunc(chunk, chunk.Tiles[x][y]);
            }
        }
    }

    private void GetHeightForTile(Chunk chunk, Tile tile)
    {
        tile.Height = AverageInRadius(HeightGenerator, RealX(chunk, tile), RealY(chunk, tile));
        tile.Moisture = AverageInRadius(MoistureGenerator, RealX(chunk, tile), RealY(chunk, tile));
        // tile.Temperature = AverageInRadius(TemperatureGenerator, RealX(chunk, tile), RealY(chunk, tile));
    }

    private void CreateBlankChunk(Chunk chunk)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;

        chunk.Tiles = new Tile[chunkSize][];

        for (int i = 0; i < chunkSize; i++)
        {
            chunk.Tiles[i] = new Tile[chunkSize];
        }
    }

    private void FillChunk(Chunk chunk)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                chunk.Tiles[x][y] = new Tile
                {
                    Height = 0,
                    Moisture = 0,
                    Position = new Vector2(x, y)
                };
            }
        }
    }

    private float AverageInRadius(FastNoiseLite generator, int cellX, int cellY, int radius = 1)
    {
        float totalValue = 0;

        for(int x = cellX - radius; x < cellX + radius; x++)
        {
            for(int y = cellY - radius; y < cellY + radius; y++)
            {
                totalValue += generator.GetNoise(x, y);
            }
        }

        return totalValue / (radius * radius);
    }
}
