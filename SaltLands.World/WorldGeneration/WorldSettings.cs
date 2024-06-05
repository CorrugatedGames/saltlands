
using System.Numerics;

namespace SaltLands;

public class WorldSettings
{
    public readonly WorldMagicNumbers MagicNumbers = new WorldMagicNumbers();
    public readonly WorldMetadata Metadata = new WorldMetadata();

    public int MapSizeX { get { return MagicNumbers.MapSizeX; } }

    public int MapSizeY { get { return MagicNumbers.MapSizeY; } }

    public Vector2 SpawnPoint { get { return new Vector2(MagicNumbers.SpawnX, MagicNumbers.SpawnY); } }

    public readonly int WorldVersion = 1;

    public class WorldMagicNumbers
    {
        // number of ms between server ticks. we want 30 ticks per second.
        public float TickTime = 1000 / 30;

        // how many ms between each save of the world
        public int AutoSaveInterval = 300 * 1000;

        // how many chunks away an entity must be within to get updates
        public int DefaultEntityTrackingRange = 4;

        // how many entities each client can track at most
        public int TrackedEntitiesPerClient = 1000;

        // width and height of a chunk
        public int ChunkSize = 64;

        // size of the chunk ready buffer. if full, will not generate new chunks until it's less full.
        public int ReadyChunkQueueSize = 200;

        // how many chunks away to keep alive for nearby players.
        public int ChunkRadiusMax = 6;

        // number of chunks the system may send per tick.
        public int ChunksToSendPerTick = 32;

        // how often the system may send chunks (in ms)
        public int ChunkRequestTickTime = 40;

        // amount of chunks the system may generate per tick
        public int ChunksToGeneratePerThreadTick = 10;

        // amount of chunks the system may generate per tick
        public int ChunkGenerationIterations = 4;

        // how long chunks should be checked to see if they should be unloaded (in ms)
        public int ChunkUnloadInterval = 4000;

        // how often the chunk generator should run (in ms)
        public int ChunkThreadTickTime = 10;

        // width and length of the number of chunks to load for the spawn area
        public int SpawnChunksWidth = 8;

        // how many entities can naturally spawn in the world per game tick
        public int MaxEntitySpawnsPerTick = 8;

        // how many random blocks should tick per chunk. higher values tick more blocks per chunk. more random ticks = more world processing, like grass growing, etc.
        public int RandomBlockTicksPerChunk = 16;

        // how far away from the player in terms of chunks can get random ticks.
        public int BlockTickChunkRange = 4;

        // how long (in ms) each random tick should be spaced apart. lower numbers mean faster ticking.
        public int BlockTickInterval = 300;

        // the map width (in tiles)
        public int MapSizeX = 256000;

        // the map height (in tiles)
        public int MapSizeY = 256000;

        // the spawn position
        public int SpawnX = 128000;
        public int SpawnY = 128000;
    }

    public class WorldMetadata
    {
        public string WorldName = "Brand New World";
        public string WorldSeed = "1234567890";
        public string SavefileLocation = "./Save/BrandNewWorld.saltworld";
    }
}
