

using Gum.Wireframe;
using Nez.UI;
using System.Linq;

namespace SaltLands;

public class OptionsScreen : BaseScreen
{
    public OptionsScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Options");

        UpdateResolutionLabel();

        GueButton homeButton = (GueButton)screen.GetGraphicalUiElementByName("HomeButton");
        homeButton.Click += (_, _) => SaltGame.SaltEmitter.Emit(SaltEvents.LoadHomeScreen);

        GueCheckbox borderlessCheckbox = (GueCheckbox) screen.GetGraphicalUiElementByName("BorderlessCheckbox");
        borderlessCheckbox.Click += (_, _) =>
        {
            bool isBorderless = SaltGame.settings.settingsData.IsBorderless;
            SaltGame.settings.SetBorderless(!isBorderless);

            if (SaltGame.settings.settingsData.IsBorderless)
            {
                borderlessCheckbox.ApplyState("Checked");
            }
            else
            {
                borderlessCheckbox.ApplyState("Unchecked");
            }
        };

        if (SaltGame.settings.settingsData.IsBorderless)
        {
            borderlessCheckbox.ApplyState("Checked");
        }

        GueCheckbox fullscreenCheckbox = (GueCheckbox)screen.GetGraphicalUiElementByName("FullscreenCheckbox");
        borderlessCheckbox.Click += (_, _) =>
        {
            bool isFullscreen = SaltGame.settings.settingsData.IsFullscreen;
            SaltGame.settings.SetFullscreen(!isFullscreen);

            if (SaltGame.settings.settingsData.IsFullscreen)
            {
                fullscreenCheckbox.ApplyState("Checked");
            }
            else
            {
                fullscreenCheckbox.ApplyState("Unchecked");
            }
        };
        if (SaltGame.settings.settingsData.IsFullscreen)
        {
            fullscreenCheckbox.ApplyState("Checked");
        }

        GueButton resolutionPrevButton = (GueButton)screen.GetGraphicalUiElementByName("ResolutionPrev");
        resolutionPrevButton.Click += (_, _) =>
        {
            int resolutionIndex = GetResolutionIndex();
            if (resolutionIndex == 0) return;

            var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex - 1];
            SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
            UpdateResolutionLabel();
        };

        GueButton resolutionNextButton = (GueButton)screen.GetGraphicalUiElementByName("ResolutionNext");
        resolutionNextButton.Click += (_, _) =>
        {
            int resolutionIndex = GetResolutionIndex();
            if (resolutionIndex == SaltGame.settings.AvailableResolutions.Count() - 1) return;

            var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex + 1];
            SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
            UpdateResolutionLabel();
        };

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