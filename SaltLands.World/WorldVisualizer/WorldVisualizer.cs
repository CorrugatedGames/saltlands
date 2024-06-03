
using System.Drawing;
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
                    Color color = GetColor(tile.Height);

                    int realX = (int) (((chunk.Position.X - smallestX) * chunkSize) + tile.Position.X);
                    int realY = (int) (((chunk.Position.Y - smallestY) * chunkSize) + tile.Position.Y);
                    bmp.SetPixel(realX, realY, color);
                }
            }
        }

        bmp.Save($"{seed}.bmp");
    }

    private static Color GetColor(float color) {
        // water
        if (color < -0.2d)
        {
            return Color.FromArgb(60, 110, 200);
        }
        // shallow water
        else if (color < -0.1d)
        {   
            return Color.FromArgb(64, 104, 192);
        }
        // sand
        else if (color < 0.00d)
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
