
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SaltLands.WorldGenerator;

internal class ChunkManager
{
    private readonly WorldSettings Settings;

    public FastNoiseLite HeightGenerator { get; private set; }
    public FastNoiseLite MoistureGenerator { get; private set; }

    public List<Chunk> LoadedChunks { get; private set; } = [];

    public ChunkManager(WorldSettings settings)
    {
        Settings = settings;


        HeightGenerator = new FastNoiseLite(NumericSeed($"height-{Settings.Metadata.WorldSeed}"));
        MoistureGenerator = new FastNoiseLite(NumericSeed($"moisture-{Settings.Metadata.WorldSeed}"));

        InitializeGenerators();
    }

    private void InitializeGenerators()
    {
        HeightGenerator.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
        HeightGenerator.SetFractalType(FastNoiseLite.FractalType.PingPong);
        HeightGenerator.SetFractalOctaves(7);

        MoistureGenerator.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        MoistureGenerator.SetFrequency(0.003f);
        MoistureGenerator.SetFractalType(FastNoiseLite.FractalType.Ridged);
        MoistureGenerator.SetFractalOctaves(3);

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

    // make the noise 0..1 instead of -1..1
    private float NormalizeNoise(float noise)
    {
        return (noise + 1) / 2;
    }

    private int NumericSeed(string seed)
    {
        return BitConverter.ToInt32(MD5.HashData(Encoding.UTF8.GetBytes(seed)));
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
                float height = NormalizeNoise(HeightGenerator.GetNoise(baseX + x, baseY + y));
                float moisture = NormalizeNoise(MoistureGenerator.GetNoise(baseX + x, baseY + y));
                Biome biome = BiomeFromParams(height, moisture);

                chunk.Tiles[x][y] = new Tile
                {
                    Height = height,
                    Moisture = moisture,
                    Biome = biome,
                    Position = new Vector2(x, y)
                };
            }
        }
    }

    private Biome BiomeFromParams(float height, float moisture)
    {
        if (height < 0.15) return Biome.Ocean;
        if (height < 0.20) return Biome.Beach;

        if (height > 0.80)
        {
            if (moisture < 0.10) return Biome.Scorched;
            if (moisture < 0.20) return Biome.Bare;
            if (moisture < 0.50) return Biome.Tundra;
            return Biome.Snow;
        }

        if (height > 0.60)
        {
            if (moisture < 0.33) return Biome.TemperateDesert;
            if (moisture < 0.66) return Biome.Shrubs;
            return Biome.Taiga;
        }

        if (height > 0.30)
        {
            if (moisture < 0.16) return Biome.TemperateDesert;
            if (moisture < 0.50) return Biome.Grassland;
            if (moisture < 0.83) return Biome.TemperateDeciduousForest;
            return Biome.TemperateRainForest;
        }

        if (moisture < 0.16) return Biome.SubtropicalDesert;
        if (moisture < 0.33) return Biome.Grassland;
        if (moisture < 0.66) return Biome.TropicalSeasonalForest;

        return Biome.TropicalRainForest;
    }
}
