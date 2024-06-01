
using System.Numerics;

namespace SaltLands;

public class WorldMagicNumbers
{
    // how many chunks away an entity must be within to get updates
    int DefaultEntityTrackingRange = 4;

    // how many entities each client can track at most
    int TrackedEntitiesPerClient = 1000;

    // width and height of a chunk
    int ServerChunkSize = 32;

    // size of the chunk ready buffer. if full, will not generate new chunks until it's less full.
    int ReadyChunkQueueSize = 200;

    // number of chunks the system may send per tick.
    int ChunksToSendPerTick = 32;

    // how often the system may send chunks (in ms)
    int ChunkRequestTickTime = 40;

    // amount of chunks the system may generate per tick
    int ChunksToGeneratePerThreadTick = 10;

    // how long chunks should be checked to see if they should be unloaded (in ms)
    int ChunkUnloadInterval = 4000;

    // how often the chunk generator should run (in ms)
    int ChunkThreadTickTime = 10;

    // how many ms between each save of the world
    int AutoSaveInterval = 300 * 1000;

    // width and length of the number of chunks to load for the spawn area
    int SpawnChunksWidth = 8;

    // how many entities can naturally spawn in the world per game tick
    int MaxEntitySpawnsPerTick = 8;
}

public class WorldSettings
{
    int MapSizeX = 256000;
    int MapSizeY = 256000;

    Vector2 SpawnPoint = new Vector2(128000, 128000);

    string WorldName = "Brand New World";
    string WorldSeed = "1234567890";
    string SavefileLocation = "./Save/BrandNewWorld.saltworld";

    private int NumericSeed
    {
        get
        {
            return WorldSeed.GetHashCode();
        }
    }
}
