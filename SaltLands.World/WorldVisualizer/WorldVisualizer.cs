
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using SaltLands.WorldGenerator;

namespace SaltLands.Visualization;

public static class WorldVisualizer
{
    [SupportedOSPlatform("windows")]
    public static void RenderWorld(SaltLandsWorld world)
    {
        string seed = world.Settings.Metadata.WorldSeed;
        int chunkSize = world.Settings.MagicNumbers.ChunkSize;
        int smallestX = (int)world.Generator.ChunkManager.LoadedChunks.OrderBy(chunk => chunk.Position.X).First().Position.X;
        int smallestY = (int)world.Generator.ChunkManager.LoadedChunks.OrderBy(chunk => chunk.Position.Y).First().Position.Y;
        int largestX = (int)world.Generator.ChunkManager.LoadedChunks.OrderByDescending(chunk => chunk.Position.X).First().Position.X;
        int largestY = (int)world.Generator.ChunkManager.LoadedChunks.OrderByDescending(chunk => chunk.Position.Y).First().Position.Y;

        int imageWidth = ((largestX - smallestX) * chunkSize) + chunkSize;
        int imageHeight = ((largestY - smallestY) * chunkSize) + chunkSize;
        Bitmap bmp = new Bitmap(imageWidth, imageHeight);

        foreach(var chunk in world.Generator.ChunkManager.LoadedChunks)
        {
            for(int x = 0; x < chunk.Tiles.Length; x++)
            {
                for(int y = 0; y < chunk.Tiles[x].Length; y++)
                {
                    Tile tile = chunk.Tiles[x][y];
                    Color color = GetColorFromBiome(tile.Biome);

                    int realX = (int) (((chunk.Position.X - smallestX) * chunkSize) + tile.Position.X);
                    int realY = (int) (((chunk.Position.Y - smallestY) * chunkSize) + tile.Position.Y);
                    bmp.SetPixel(realX, realY, color);
                }
            }
        }

        bmp.Save($"{seed}.png", ImageFormat.Png);
    }

    private static Color GetColorFromBiome(Biome biome)
    {
        switch(biome)
        {
            case Biome.Ocean:                       return Color.FromArgb(0, 0, 255);
            case Biome.Beach:                       return Color.FromArgb(208, 207, 130);
            case Biome.Scorched:                    return Color.FromArgb(156, 155, 70);
            case Biome.Tundra:                      return Color.FromArgb(89, 155, 203);
            case Biome.Snow:                        return Color.FromArgb(138, 246, 243);
            case Biome.TemperateDesert:             return Color.FromArgb(235, 157, 126);
            case Biome.Shrubs:                      return Color.FromArgb(90, 195, 102);
            case Biome.Taiga:                       return Color.FromArgb(189, 210, 103);
            case Biome.Grassland:                   return Color.FromArgb(90, 195, 139);
            case Biome.TemperateDeciduousForest:    return Color.FromArgb(63, 179, 80);
            case Biome.TemperateRainForest:         return Color.FromArgb(84, 195, 0);
            case Biome.SubtropicalDesert:           return Color.FromArgb(226, 181, 45);
            case Biome.TropicalSeasonalForest:      return Color.FromArgb(67, 255, 109);
            case Biome.TropicalRainForest:          return Color.FromArgb(56, 152, 104);

        }

        return Color.FromArgb(0, 0, 0);
    }

    private static Color GetColorFromHeight(float color) {
        // deep water
        if(color < -0.9d)
        {
            return Color.FromArgb(30, 55, 100);
        }
        // water
        else if (color < -0.7d)
        {
            return Color.FromArgb(60, 110, 200);
        }
        // shallow water
        else if (color < -0.6d)
        {   
            return Color.FromArgb(64, 104, 192);
        }
        // sand
        else if (color < -0.5d)
        { 
            return Color.FromArgb(208, 207, 130);
        }
        // grass
        else if (color < 0.15d)
        { 
            return Color.FromArgb(84, 150, 29);
        }
        // forest
        else if (color < 0.25d)
        { 
            return Color.FromArgb(61, 105, 22);
        }
        // mountain
        else if (color < 0.35d)
        { 
            return Color.FromArgb(91, 68, 61);
        }
        // high mountain
        else if (color < 0.45d)
        { 
            return Color.FromArgb(75, 58, 54);
        }
        // snow
        else
        {
            return Color.FromArgb(255, 254, 255);
        }
    }
}
