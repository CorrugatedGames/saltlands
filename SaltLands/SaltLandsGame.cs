using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Nez;
using Nez.Systems;

namespace SaltLands;

public class SaltLandsGame : Core
{
    public static Emitter<SaltEvents> SaltEmitter;
    public static SaltUI saltUI;

    private ScreenManager screenManager;

    public SaltLandsGame() : base(1920, 1080, false, "SaltLands") { }

    protected override void Initialize()
    {
        base.Initialize();

        SetupUI();
        SetupComponents();
        SetupEvents();
        SetupNez();

        SaltEmitter.Emit(SaltEvents.LoadHomeScreen);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    private void SetupUI()
    {
        saltUI = new SaltUI(Content);
    }

    private void SetupNez()
    {
        PauseOnFocusLost = false;
        Nez.Screen.SetSize(1920, 1080);
    }

    private void SetupComponents()
    {

        screenManager = new ScreenManager();
        Components.Add(screenManager);
    }

    private void SetupEvents()
    {
        SaltEmitter = new Emitter<SaltEvents>(new SaltEventsComparer());
        SaltEmitter.AddObserver(SaltEvents.LoadHomeScreen, LoadHomeScreen);
    }

    private void LoadHomeScreen()
    {
        screenManager.LoadScreen(new HomeScreen(this));
    }
}
