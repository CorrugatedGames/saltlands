

namespace SaltLands;

public class ServerSettings
{
    /* Connection Information */
    string? Ip = null;
    int Port = 45698;
    int MaxPlayers = 8;

    /* Server metadata */
    string ServerName = "SaltLands World";
    string ServerDescription = "A cool person's server.";
    string? ServerPassword = "";
    string ServerUrl = "";
    string WelcomeMessage = "Welcome to the Salt Lands! May you find salt.";
    string ServerCreatedBy = "Seiyria";

    /* World main thread operation values */

    // whether or not the server should keep ticking when no one is playing
    bool PassTimeWhenEmpty = false;

    // the default timeout (in ms) for players to get disconnected after if they don't send anything to the game
    int ClientConnectionTimeout = 150;

    // how many chunks away to keep alive for nearby players.
    int MaxChunkRadius = 12;

    // the maximum number of blocks that can tick per server tick
    int MaxMainThreadBlockTicks = 10000;

    // how many random blocks should tick per chunk. higher values tick more blocks per chunk. more random ticks = more world processing, like grass growing, etc.
    int RandomBlockTicksPerChunk = 16;

    // number of ms between server ticks. we want 30 ticks per second.
    float TickTime = 1000 / 30;

    // how far away from the player in terms of chunks can get random ticks.
    int BlockTickChunkRange = 4;

    // how long (in ms) each random tick should be spaced apart. lower numbers mean faster ticking.
    int BlockTickInterval = 300;

    /* Chat values */

    // how long of a gap is required between messages (in ms)
    int ChatRateLimitMs = 1000;
}
