using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class Tile
{
    #region Public Fields

    public float Height;
    public float Moisture;
    public Vector2 Position;
    public float Temperature;

    #endregion Public Fields

    #region Public Properties

    public Biome Biome
    {
        get
        {
            if (Height < -0.6) return Biome.Ocean;
            if (Height < -0.4) return Biome.DeepWater;
            if (Height < -0.1) return Biome.ShallowWater;
            if (Height < 0.05) return Biome.Beach;

            if (Height > 0.80)
            {
                if (Moisture < 0.10) return Biome.Scorched;
                if (Moisture < 0.20) return Biome.Bare;
                if (Moisture < 0.50) return Biome.Tundra;
                return Biome.Snow;
            }

            if (Height > 0.60)
            {
                if (Moisture < 0.33) return Biome.TemperateDesert;
                if (Moisture < 0.66) return Biome.Shrubs;
                return Biome.Taiga;
            }

            if (Height > 0.30)
            {
                if (Moisture < 0.16) return Biome.TemperateDesert;
                if (Moisture < 0.50) return Biome.Grassland;
                if (Moisture < 0.83) return Biome.TemperateDeciduousForest;
                return Biome.TemperateRainForest;
            }

            if (Moisture < 0.16) return Biome.SubtropicalDesert;
            if (Moisture < 0.33) return Biome.Grassland;
            if (Moisture < 0.66) return Biome.TropicalSeasonalForest;

            return Biome.TropicalRainForest;
        }
    }

    #endregion Public Properties
}