global using Console = System.Diagnostics.Debug;
using System.Diagnostics;

Console.WriteLine("Generating debug world...");

using var world = new SaltLands.WorldGenerator.SaltLandsWorld();
world.Generate();