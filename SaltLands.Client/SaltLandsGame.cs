using GumRuntime;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Nez;
using Nez.Systems;
using RenderingLibrary;
using System;

namespace SaltLands;

public class SaltLandsGame : Core
{
    public Emitter<SaltEvents> SaltEmitter;
    public SaltUI saltUI;
    public SettingsManager settings;

    private ScreenManager screenManager;

    public SaltLandsGame() : base(1920, 1080, false, "SaltLands") { }

    protected override void Initialize()
    {
        base.Initialize();

        SetupEvents();
        SetupUI();
        SetupComponents();
        SetupNez();

        SaltEmitter.Emit(SaltEvents.LoadHomeScreen);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        screenManager.Update(gameTime);
        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds); 
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        screenManager.Draw(gameTime);
        SystemManagers.Default.Draw();
    }

    private void SetupUI()
    {
        ElementSaveExtensions.RegisterGueInstantiationType("Button", typeof(GueButton));
        ElementSaveExtensions.RegisterGueInstantiationType("Checkbox", typeof(GueCheckbox));

        SystemManagers.Default = new SystemManagers();
        SystemManagers.Default.Initialize(GraphicsDevice, fullInstantiation: true);
        ToolsUtilities.FileManager.RelativeDirectory = "Content\\GumUI\\";

        saltUI = new SaltUI();

        settings = new SettingsManager(this);
    }

    private void SetupNez()
    {
        PauseOnFocusLost = false;
        ExitOnEscapeKeypress = false;
        Nez.Screen.SetSize((int)settings.Resolution.X, (int)settings.Resolution.Y);
    }

    private void SetupComponents()
    {

        screenManager = new ScreenManager();
        Components.Add(screenManager);
    }

    private void SetupEvents()
    {
        SaltEmitter = new Emitter<SaltEvents>(new SaltEventsComparer());
        SaltEmitter.AddObserver(SaltEvents.WindowResize, ResizeWindow);
        SaltEmitter.AddObserver(SaltEvents.LoadHomeScreen, LoadHomeScreen);
        SaltEmitter.AddObserver(SaltEvents.LoadOptionsScreen, LoadOptionsScreen);
        SaltEmitter.AddObserver(SaltEvents.QuitGame, QuitGame);
    }

    private void ResizeWindow()
    {
        saltUI.UpdateLayout();
    }

    private void LoadHomeScreen()
    {
        screenManager.LoadScreen(new HomeScreen(this));
    }

    private void LoadOptionsScreen()
    {
        screenManager.LoadScreen(new OptionsScreen(this));
    }

    private void QuitGame()
    {
        Environment.Exit(0);
    }
}
