﻿using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SaltLands.WorldGenerator;

internal class Centroid
{
    public float Height;
    public float Moisture;
    public Vector2 Position;
    public float Temperature;
}

internal class VoronoiRenderData
{
    public List<Centroid> Centroids = [];
    public List<int> HalfEdges = [];
    public int NumEdges;
    public int NumRegions;
    public int NumTriangles;
    public List<Vector2> Points = [];
    public List<int> Triangles = [];
}

internal partial class ChunkGenerator
{
    private delegate void ChunkTileIterator(Chunk chunk, Tile tile);

    private static float AverageInRadius(FastNoiseLite generator, int cellX, int cellY, int radius = 1)
    {
        float totalValue = 0;

        for (int x = cellX - radius; x < cellX + radius; x++)
        {
            for (int y = cellY - radius; y < cellY + radius; y++)
            {
                totalValue += generator.GetNoise(x, y);
            }
        }

        return totalValue / (radius * radius);
    }

    private static List<Tile> ChunkTilesToList(Chunk chunk)
    {
        List<Tile> tiles = [];

        for (int x = 0; x < chunk.Tiles.Length; x++)
        {
            for (int y = 0; y < chunk.Tiles[x].Length; y++)
            {
                tiles.Add(chunk.Tiles[x][y]);
            }
        }

        return tiles;
    }

    private static double DistanceBetween(Vector2 start, Vector2 end)
    {
        double dx = start.X - end.X;
        double dy = start.Y - end.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    private static Centroid GetClosestCentroid(List<Centroid> points, Vector2 tilePosition)
    {
        return points.OrderBy(c => DistanceBetween(c.Position, tilePosition)).First();
    }

    private static float NormalizedNoise(FastNoiseLite generator, float x, float y)
    {
        float wavelength = 0.5f;
        return 1 + (generator.GetNoise(x / wavelength, y / wavelength) / 2);
    }

    // turn a string seed into a numeric one
    private static int NumericSeed(string seed)
    {
        return BitConverter.ToInt32(MD5.HashData(Encoding.UTF8.GetBytes(seed)));
    }

    private Vector2 RealPosition(Chunk chunk, Tile tile)
    {
        return new Vector2(RealX(chunk, tile), RealY(chunk, tile));
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
}