

using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class Chunk
{
    public Vector2 Position;
    public Tile[][] Tiles = Array.Empty<Tile[]>();
}
