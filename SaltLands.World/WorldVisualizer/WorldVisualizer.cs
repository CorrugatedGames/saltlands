
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
        Graphics graphics = Graphics.FromImage(bmp);
        Pen blackPen = new Pen(Color.Black, 1);

        Func<float, int> triangleOfEdge = (edge) => (int) Math.Floor(edge / 3);
        Func<float, int> nextHalfEdge = (edge) => (int) (edge % 3 == 2 ? edge - 2 : edge + 1);

        foreach(var chunk in world.Generator.ChunkManager.LoadedChunks)
        {
            var renderData = chunk.VoronoiData;

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

            foreach(var centroid in renderData.Centroids)
            {

                int realX = (int)(centroid.Position.X - (smallestX * chunkSize));
                int realY = (int)(centroid.Position.Y - (smallestY * chunkSize));
                bmp.SetPixel(realX, realY, Color.FromArgb(0, 0, 0));
            }

            for (int e = 0; e < renderData.NumEdges; e++)
            {
                if (e >= renderData.HalfEdges[e]) continue;

                var p = renderData.Centroids[triangleOfEdge(e)];
                var q = renderData.Centroids[triangleOfEdge(renderData.HalfEdges[e])];

                graphics.DrawLine(blackPen, p.Position.X - (smallestX * chunkSize), p.Position.Y - (smallestY * chunkSize), q.Position.X - (smallestX * chunkSize), q.Position.Y - (smallestY * chunkSize));
            }
        }

        bmp.Save($"{seed}.png", ImageFormat.Png);
    }

    private static Color GetColorFromBiome(Biome biome)
    {
        return biome switch
        {
            Biome.Ocean =>                      Color.FromArgb(0, 0, 255),
            Biome.DeepWater =>                  Color.FromArgb(11, 74, 96),
            Biome.ShallowWater =>               Color.FromArgb(37, 150, 190),
            Biome.Beach =>                      Color.FromArgb(208, 207, 130),
            Biome.Scorched =>                   Color.FromArgb(156, 155, 70),
            Biome.Tundra =>                     Color.FromArgb(89, 155, 203),
            Biome.Snow =>                       Color.FromArgb(138, 246, 243),
            Biome.TemperateDesert =>            Color.FromArgb(235, 157, 126),
            Biome.Shrubs =>                     Color.FromArgb(90, 195, 102),
            Biome.Taiga =>                      Color.FromArgb(189, 210, 103),
            Biome.Grassland =>                  Color.FromArgb(90, 195, 139),
            Biome.TemperateDeciduousForest =>   Color.FromArgb(63, 179, 80),
            Biome.TemperateRainForest =>        Color.FromArgb(84, 195, 0),
            Biome.SubtropicalDesert =>          Color.FromArgb(226, 181, 45),
            Biome.TropicalSeasonalForest =>     Color.FromArgb(67, 255, 109),
            Biome.TropicalRainForest =>         Color.FromArgb(56, 152, 104),
            _ =>                                Color.FromArgb(0, 0, 0),
        };
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
