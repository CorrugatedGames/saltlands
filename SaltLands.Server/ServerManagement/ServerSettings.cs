

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

    // the maximum number of blocks that can tick per server tick
    int MaxMainThreadBlockTicks = 10000;

    /* Chat values */

    // how long of a gap is required between messages (in ms)
    int ChatRateLimitMs = 1000;
}
