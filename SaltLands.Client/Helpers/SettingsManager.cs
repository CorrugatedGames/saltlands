
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Gum.Wireframe;
using RenderingLibrary;

namespace SaltLands;


[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true)]
[JsonSerializable(typeof(Settings))]
internal partial class SettingsContext : JsonSerializerContext { }

public class Settings
{
    public int WindowX { get; set; } = -1;
    public int WindowY { get; set; } = -1;
    public int WindowWidth { get; set; } = 1920;
    public int WindowHeight { get; set; } = 1080;
    public bool IsFixedTimeStep { get; set; } = true;
    public bool IsVSync { get; set; } = false;
    public bool IsFullscreen { get; set; } = false;
    public bool IsBorderless { get; set; } = false;
}

public class SettingsManager
{

    private readonly string FileName = "settings.json";

    private readonly SaltLandsGame Game;

    private readonly Vector2 DefaultResolution = new Vector2(1920, 1080);

    public readonly List<Vector2> AvailableResolutions =
    new List<Vector2>() {
        new Vector2(1440, 900),
        new Vector2(1600, 900),
        new Vector2(1920, 1080),
        new Vector2(1920, 1200),
        new Vector2(2560, 1440),
        new Vector2(2560, 1600),
        new Vector2(3440, 1440),
        new Vector2(3440, 2160)
    };

    public Settings settingsData = new Settings();

    public Vector2 Resolution
    {
        get
        {
            return new Vector2(settingsData.WindowWidth, settingsData.WindowHeight);
        }
    }

    public SettingsManager(SaltLandsGame game)
    {
        Game = game;

        LoadSettings();
    }

    private void LoadSettings()
    {

        settingsData = Serializer.EnsureJson(FileName, SettingsContext.Default.Settings);
        Game.IsFixedTimeStep = settingsData.IsFixedTimeStep;
        Nez.Screen.GraphicsManager.SynchronizeWithVerticalRetrace = settingsData.IsVSync;
        settingsData.IsFullscreen = settingsData.IsFullscreen || settingsData.IsBorderless;
        RestoreWindow();

        if (settingsData.IsFullscreen)
        {
            ApplyFullscreenChange(false);
        }
    }

    private void SaveSettings()
    {
        Serializer.SaveJson("Settings.json", settingsData, SettingsContext.Default.Settings);
    }

    private void ApplyFullscreenChange(bool oldIsFullscreen)
    {
        if (settingsData.IsFullscreen)
        {
            if (oldIsFullscreen)
            {
                ApplyHardwareMode();
            }
            else
            {
                GoFullscreen();
            }
        }
        else
        {
            UngoFullscreen();
        }

        SaveSettings();
    }

    private void ApplyHardwareMode()
    {
        Nez.Screen.GraphicsManager.HardwareModeSwitch = !settingsData.IsBorderless;
        Nez.Screen.GraphicsManager.ApplyChanges();

        SaveSettings();
    }

    private void GoFullscreen()
    {
        SaveWindow();

        Nez.Screen.GraphicsManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        Nez.Screen.GraphicsManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Nez.Screen.GraphicsManager.HardwareModeSwitch = !settingsData.IsBorderless;

        Nez.Screen.GraphicsManager.IsFullScreen = true;
        Nez.Screen.GraphicsManager.ApplyChanges();

        SaveSettings();
    }

    private void UngoFullscreen()
    {
        Nez.Screen.GraphicsManager.IsFullScreen = false;
        RestoreWindow();

        SaveSettings();
    }

    private void SaveWindow()
    {
        settingsData.WindowX = Game.Window.ClientBounds.X;
        settingsData.WindowY = Game.Window.ClientBounds.Y;
        settingsData.WindowWidth = Game.Window.ClientBounds.Width;
        settingsData.WindowHeight = Game.Window.ClientBounds.Height;

        SaveSettings();
    }

    private void RestoreWindow()
    {

        if (settingsData.WindowX != -1 && settingsData.WindowY != -1)
        {
            Game.Window.Position = new Point(settingsData.WindowX, settingsData.WindowY);
        }

        var camera = SystemManagers.Default.Renderer.Camera;
        // camera.Zoom = DefaultResolution.Y / settingsData.WindowHeight;

        GraphicalUiElement.CanvasWidth = settingsData.WindowWidth * camera.Zoom;
        GraphicalUiElement.CanvasHeight = settingsData.WindowHeight * camera.Zoom;

        Nez.Screen.GraphicsManager.PreferredBackBufferWidth = settingsData.WindowWidth;
        Nez.Screen.GraphicsManager.PreferredBackBufferHeight = settingsData.WindowHeight;
        Nez.Screen.GraphicsManager.ApplyChanges();

        SaveSettings();

        Game.SaltEmitter.Emit(SaltEvents.WindowResize);
    }

    public void SetFullscreen(bool value)
    {
        settingsData.IsFullscreen = value;
        ApplyFullscreenChange(!value);
    }

    public void SetBorderless(bool value)
    {
        settingsData.IsBorderless = value;
        ApplyHardwareMode();
    }

    public void SetResolution(int width, int height)
    {
        settingsData.WindowWidth = width;
        settingsData.WindowHeight = height;

        RestoreWindow();
    }
}