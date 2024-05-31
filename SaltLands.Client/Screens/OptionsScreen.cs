

namespace SaltLands;

public class OptionsScreen : BaseScreen
{
    public OptionsScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Options");
    }

    protected override void HandleButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "HomeButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadHomeScreen);
                break;
        }
    }
}