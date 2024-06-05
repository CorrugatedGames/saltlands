
using System.Numerics;

namespace SaltLands.WorldGenerator;

/**
 * Heavily inspired by https://www.redblobgames.com/x/2022-voronoi-maps-tutorial/
 * 
 * Long list of potential improvements:
 * 
 * - Generate voronoi for 3x3 chunks (middle being the current one), but only keep geometry for the middle one: 
 *      - https://www.reddit.com/r/proceduralgeneration/comments/4ejhzj/infinite_voronoi_tesselation/d20vmmb/
 *      - https://www.reddit.com/r/proceduralgeneration/comments/5ykjz4/applying_voronoi_to_an_infinite_2d_tile_map/dew7csm/
 * 
 * - Weighted biome selection: https://www.reddit.com/r/proceduralgeneration/comments/7yrh9c/confused_on_how_to_implement_biomes/duj0hzl/
 * - Apply Lloyd's Relaxation: 
 *      - https://pvigier.github.io/2019/05/12/vagabond-map-generation.html
 *      - https://github.com/BilHim/minecraft-world-generation/blob/main/src/Minecraft%20Terrain%20Generation%20in%20Python%20-%20By%20Bilal%20Himite.ipynb
 * - Add rivers: 
 *      - https://pvigier.github.io/2019/05/26/vagabond-generating-tiles.html
 *      - http://www-cs-students.stanford.edu/~amitp/game-programming/polygon-map-generation/
 * - Add trees: https://freedium.cfd/https://towardsdatascience.com/replicating-minecraft-world-generation-in-python-1b491bc9b9a4
 * 
 * Next up: 
 * - cache a random instance on a chunk
 * - allow for getting nearby cached chunks (if not existent, create)
 * - use 3x3 voronoi instead of 1x1
 */

internal partial class ChunkGenerator
{
    private readonly WorldSettings Settings;

    private readonly FastNoiseLite HeightGenerator;
    private readonly FastNoiseLite MoistureGenerator;
    private readonly FastNoiseLite TemperatureGenerator;

    public List<Chunk> LoadedChunks { get; private set; } = [];

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
        HeightGenerator.SetFrequency(0.025f);
        HeightGenerator.SetFractalType(FastNoiseLite.FractalType.FBm);
        HeightGenerator.SetFractalOctaves(5);
        HeightGenerator.SetFractalGain(0.5f);

        MoistureGenerator.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        MoistureGenerator.SetFrequency(0.040f);
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
        // this will need to be able to be called as a chunk is loaded
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

    private void CreateBlankChunk(Chunk chunk)
    {
        int chunkSize = Settings.MagicNumbers.ChunkSize;

        chunk.Tiles = new Tile[chunkSize][];

        for (int x = 0; x < chunkSize; x++)
        {
            chunk.Tiles[x] = new Tile[chunkSize];

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

        DetermineInitialChunkValues(chunk);
    }

    private void FillChunk(Chunk chunk)
    {
    }

    private void DetermineInitialChunkValues(Chunk chunk)
    {
        int gridSize = chunk.Tiles.Length;
        Random random = new Random(NumericSeed($"random-{chunk.Position.X}-{chunk.Position.Y}-{Settings.Metadata.WorldSeed}"));

        // pick random points
        var allTiles = ChunkTilesToList(chunk);
        int tiles = 10 + random.Next(5);
        List<Tile> chosenTiles = [];

        for(int i = 0; i < tiles; i++)
        {
            var chosenIndex = random.Next(allTiles.Count);
            var chosenTile = allTiles[chosenIndex];
            chosenTiles.Add(chosenTile);
            allTiles.Remove(chosenTile);
        }

        // generate voronoi
        List<Vector2> points = [];
        float jitter = 0.5f;

        foreach(var tile in chosenTiles)
        {
            float fx = (float)(RealX(chunk, tile) + jitter + (random.NextDouble() - random.NextDouble()));
            float fy = (float)(RealY(chunk, tile) + jitter + (random.NextDouble() - random.NextDouble()));
            points.Add(new Vector2(fx, fy));
        }

        // generate delaunay
        Delaunator.Triangulation delaunator = Delaunator.Triangulation.From(points, p => p.X, p => p.Y);

        List<Centroid> centroids = [];
        int numTriangles = delaunator.halfedges.Count / 3;

        for (int t = 0; t < numTriangles; t++)
        {
            float sumOfX = 0;
            float sumOfY = 0;

            for (int i = 0; i < 3; i++) 
            {
                int s = 3 * t + i;
                Vector2 p = points[delaunator.triangles[s]];

                sumOfX += p.X;
                sumOfY += p.Y;
            }

            centroids.Add(new Centroid()
            {
                Position = new Vector2(sumOfX / 3, sumOfY / 3)
            });
        }

        // generate values for centroids
        foreach (var centroid in centroids)
        {
            float nx = centroid.Position.X / gridSize - (1 / 2);
            float ny = centroid.Position.Y / gridSize - (1 / 2);

            centroid.Height = HeightGenerator.GetNoise(nx, ny);
            centroid.Moisture = NormalizedNoise(MoistureGenerator, nx, ny);
            centroid.Temperature = 1 - centroid.Height;
        }

        // apply points to all tiles in the chunk
        ChunkTilesToList(chunk).ForEach(tile =>
        {
            var tilePosition = RealPosition(chunk, tile);
            var closestCentroid = GetClosestCentroid(centroids, tilePosition);
            tile.Height = closestCentroid.Height;
            tile.Temperature = closestCentroid.Temperature;
            tile.Moisture = closestCentroid.Moisture;
        });

        chunk.VoronoiData = new VoronoiRenderData()
        {
            Points = points,
            NumRegions = points.Count,
            NumTriangles = delaunator.halfedges.Count / 3,
            NumEdges = delaunator.halfedges.Count,
            HalfEdges = delaunator.halfedges,
            Triangles = delaunator.triangles,
            Centroids = centroids
        };
    }

    private void GetHeightForTile(Chunk chunk, Tile tile)
    {

    }
}
