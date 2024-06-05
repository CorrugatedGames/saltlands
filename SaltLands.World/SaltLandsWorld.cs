



/**
 * TODO:
 * 
 * - world should be portable
 * - accept packets as input, return packets with only updated data necessary
 * - use UDP
 * - eventually need to do p2p for steamworks
 * - use a network session with simulated latency/packet loss (litnetelib supports this, if not, lidgren does)
 * 
 * WORLDGEN TODO:
 * 
 * - track seed
 * - track algo used so new updates don't fuck up old seeds
 */


namespace SaltLands.WorldGenerator;

public class SaltLandsWorld : IDisposable
{

    internal WorldSettings Settings { get; private set; }
    internal WorldGenerator Generator { get; private set; }

    public SaltLandsWorld()
    {
        Settings = new WorldSettings();
        Generator = new WorldGenerator(Settings);
    }

    public void Generate()
    {
        Generator.GenerateAroundCenter(Settings.MagicNumbers.MapSizeX / Settings.MagicNumbers.ChunkSize, Settings.MagicNumbers.MapSizeY / Settings.MagicNumbers.ChunkSize, 100);

        Visualization.WorldVisualizer.RenderWorld(this);
    }

    public void Dispose()
    {

    }
}
