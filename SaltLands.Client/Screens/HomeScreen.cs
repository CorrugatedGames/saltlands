
using Gum.Wireframe;
using System;

namespace SaltLands;

public class HomeScreen : BaseScreen
{

    public HomeScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Home");

        GueButton playButton = (GueButton)screen.GetGraphicalUiElementByName("PlayButton");
        playButton.Click += (_, _) => SaltGame.SaltEmitter.Emit(SaltEvents.LoadPlayGameScreen);

        GueButton optionsButton = (GueButton) screen.GetGraphicalUiElementByName("OptionsButton");
        optionsButton.Click += (_, _) => SaltGame.SaltEmitter.Emit(SaltEvents.LoadOptionsScreen);

        GueButton quitButton = (GueButton)screen.GetGraphicalUiElementByName("QuitButton");
        quitButton.Click += (_, _) => SaltGame.SaltEmitter.Emit(SaltEvents.QuitGame);
    }
}