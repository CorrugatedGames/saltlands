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
    public SaltLandsWorld()
    {
        Settings = new WorldSettings();
        Generator = new WorldGenerator(Settings);
    }

    internal WorldGenerator Generator { get; private set; }
    internal WorldSettings Settings { get; private set; }

    public void Dispose()
    {
    }

    public void Generate()
    {
        Generator.GenerateAroundCenter(Settings.MagicNumbers.MapSizeX / Settings.MagicNumbers.ChunkSize, Settings.MagicNumbers.MapSizeY / Settings.MagicNumbers.ChunkSize, 50);

        Visualization.WorldVisualizer.RenderWorld(this);
    }
}