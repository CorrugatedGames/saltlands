
using Gum.Wireframe;

namespace SaltLands;

public class HomeScreen : BaseScreen
{

    public HomeScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Home");
    }

    protected override void HandleButtonClick(GraphicalUiElement button)
    {
        switch(button.Name)
        {
            case "PlayButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadPlayGameScreen);
                break;

            case "OptionsButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadOptionsScreen);
                break;

            case "QuitButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.QuitGame);
                break;
        }
    }
}