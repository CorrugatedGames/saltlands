namespace SaltLands.WorldGenerator;

internal enum Biome
{
    Unknown,
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

internal class BiomeStats
{
    #region Public Fields

    public Biome Biome;
    public float MaxHeight;
    public float MaxMoisture;
    public float MinHeight;
    public float MinMoisture;

    #endregion Public Fields

    #region Public Constructors

    public BiomeStats()
    {
        MaxHeight = 1f;
        MinHeight = -1f;
        MaxMoisture = 1.5f;
        MinMoisture = 0.5f;
    }

    #endregion Public Constructors
}

internal class BiomeTools
{
    #region Private Fields

    private static BiomeStats[] BiomeStats =
    {
        new BiomeStats { Biome = Biome.Ocean,                       MaxHeight = -0.60f },
        new BiomeStats { Biome = Biome.DeepWater,                   MaxHeight = -0.40f },
        new BiomeStats { Biome = Biome.ShallowWater,                MaxHeight = -0.10f },
        new BiomeStats { Biome = Biome.Beach,                       MaxHeight = 0.05f },

        new BiomeStats { Biome = Biome.Scorched,                    MinHeight = 0.80f, MaxMoisture = 0.60f },
        new BiomeStats { Biome = Biome.Bare,                        MinHeight = 0.80f, MaxMoisture = 0.70f },
        new BiomeStats { Biome = Biome.Tundra,                      MinHeight = 0.80f, MaxMoisture = 1.00f },
        new BiomeStats { Biome = Biome.Snow,                        MinHeight = 0.80f },

        new BiomeStats { Biome = Biome.TemperateDesert,             MinHeight = 0.60f, MaxHeight = 0.80f, MaxMoisture = 0.83f },
        new BiomeStats { Biome = Biome.Shrubs,                      MinHeight = 0.60f, MaxHeight = 0.80f, MaxMoisture = 1.16f },
        new BiomeStats { Biome = Biome.Taiga,                       MinHeight = 0.60f, MaxHeight = 0.80f },

        new BiomeStats { Biome = Biome.TemperateDesert,             MinHeight = 0.30f, MaxHeight = 0.60f, MaxMoisture = 0.66f },
        new BiomeStats { Biome = Biome.Grassland,                   MinHeight = 0.30f, MaxHeight = 0.60f, MaxMoisture = 1.00f },
        new BiomeStats { Biome = Biome.TemperateDeciduousForest,    MinHeight = 0.30f, MaxHeight = 0.60f, MaxMoisture = 1.33f },
        new BiomeStats { Biome = Biome.TemperateRainForest,         MinHeight = 0.30f, MaxHeight = 0.60f },

        new BiomeStats { Biome = Biome.SubtropicalDesert,           MinHeight = 0.05f, MaxHeight = 0.30f, MaxMoisture = 0.66f },
        new BiomeStats { Biome = Biome.Grassland,                   MinHeight = 0.05f, MaxHeight = 0.30f, MaxMoisture = 0.83f },
        new BiomeStats { Biome = Biome.TropicalSeasonalForest,      MinHeight = 0.05f, MaxHeight = 0.30f, MaxMoisture = 1.06f },
        new BiomeStats { Biome = Biome.TropicalRainForest },
    };

    #endregion Private Fields

    #region Public Methods

    public static Biome GetBiome(float height, float moisture)
    {
        foreach (var biomeStats in BiomeStats)
        {
            if (height < biomeStats.MinHeight || height > biomeStats.MaxHeight) continue;
            if (moisture < biomeStats.MinMoisture || moisture > biomeStats.MaxMoisture) continue;

            return biomeStats.Biome;
        }

        Console.WriteLine($"Unknown biome: {height} | {moisture}");
        return Biome.Unknown;
    }

    #endregion Public Methods
}