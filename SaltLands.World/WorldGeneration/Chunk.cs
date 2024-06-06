using System.Numerics;

namespace SaltLands.WorldGenerator;

internal class Chunk
{
    #region Public Fields

    public long LastLoadedAt;
    public Vector2 Position;
    public Tile[][] Tiles = Array.Empty<Tile[]>();

    public VoronoiRenderData VoronoiData;

    #endregion Public Fields
}