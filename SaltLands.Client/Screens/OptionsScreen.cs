

using Gum.Wireframe;
using System.Linq;

namespace SaltLands;

// TODO: stuff needs to actually be disabled properly

public class OptionsScreen : BaseScreen
{
    public OptionsScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Options");

        var borderlessCheckbox = screen.GetGraphicalUiElementByName("BorderlessCheckbox");
        if(SaltGame.settings.settingsData.IsBorderless)
        {
            borderlessCheckbox.ApplyState("Checked");
        }

        var fullscreenCheckbox = screen.GetGraphicalUiElementByName("FullscreenCheckbox");
        if (SaltGame.settings.settingsData.IsFullscreen)
        {
            fullscreenCheckbox.ApplyState("Checked");
        }

        UpdateResolutionLabel();

    }

    protected override void HandleButtonClick(GraphicalUiElement button)
    {
        int resolutionIndex = GetResolutionIndex();

        switch (button.Name)
        {
            case "HomeButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadHomeScreen);
                break;

            case "BorderlessCheckbox":
                bool isBorderless = SaltGame.settings.settingsData.IsBorderless;
                SaltGame.settings.SetBorderless(!isBorderless);

                if (SaltGame.settings.settingsData.IsBorderless)
                {
                    button.ApplyState("Checked");
                }
                else
                {
                    button.ApplyState("Unchecked");
                }
                break;

            case "FullscreenCheckbox":
                bool isFullscreen = SaltGame.settings.settingsData.IsFullscreen;
                SaltGame.settings.SetFullscreen(!isFullscreen);

                if (SaltGame.settings.settingsData.IsFullscreen)
                {
                    button.ApplyState("Checked");
                }
                else
                {
                    button.ApplyState("Unchecked");
                }
                break;

            case "ResolutionPrev":
                if(resolutionIndex != 0)
                {
                    var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex - 1];
                    SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
                    UpdateResolutionLabel();
                }
                break;

            case "ResolutionNext":
                if(resolutionIndex != SaltGame.settings.AvailableResolutions.Count() - 1)
                {
                    var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex + 1];
                    SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
                    UpdateResolutionLabel();
                }
                break;
        }
    }

    private void UpdateResolutionLabel()
    {
        var resolutionLabel = screen.GetGraphicalUiElementByName("ResolutionText");
        var currentResolution = SaltGame.settings.Resolution.X + "x" + SaltGame.settings.Resolution.Y;
        resolutionLabel.SetProperty("Text", currentResolution);


    }

    private int GetResolutionIndex()
    {
        return SaltGame.settings.AvailableResolutions.Select((res, index) => new { res, index }).First(ptRes => ptRes.res.X == SaltGame.settings.Resolution.X && ptRes.res.Y == SaltGame.settings.Resolution.Y).index;
    }
}