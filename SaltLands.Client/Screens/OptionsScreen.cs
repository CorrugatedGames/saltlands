

using Gum.Wireframe;
using Nez.UI;
using System.Linq;

namespace SaltLands;

public class OptionsScreen : BaseScreen

{
    GueCheckbox fullscreenCheckbox;
    GueCheckbox borderlessCheckbox;
    GueButton resolutionPrevButton;
    GueButton resolutionNextButton;

    public OptionsScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Options");

        UpdateResolutionLabel();

        GueButton homeButton = (GueButton)screen.GetGraphicalUiElementByName("HomeButton");
        homeButton.Click += (_, _) => SaltGame.SaltEmitter.Emit(SaltEvents.LoadHomeScreen);

        resolutionPrevButton = (GueButton)screen.GetGraphicalUiElementByName("ResolutionPrev");
        resolutionPrevButton.Click += (_, _) =>
        {
            if (resolutionPrevButton.IsDisabled) return;

            int resolutionIndex = GetResolutionIndex();
            if (resolutionIndex == 0) return;

            var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex - 1];
            SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
            UpdateResolutionLabel();
        };

        resolutionNextButton = (GueButton)screen.GetGraphicalUiElementByName("ResolutionNext");
        resolutionNextButton.Click += (_, _) =>
        {
            if (resolutionNextButton.IsDisabled) return;

            int resolutionIndex = GetResolutionIndex();
            if (resolutionIndex == SaltGame.settings.AvailableResolutions.Count() - 1) return;

            var resolution = SaltGame.settings.AvailableResolutions.ToArray()[resolutionIndex + 1];
            SaltGame.settings.SetResolution((int)resolution.X, (int)resolution.Y);
            UpdateResolutionLabel();
        };

        borderlessCheckbox = (GueCheckbox) screen.GetGraphicalUiElementByName("BorderlessCheckbox");
        borderlessCheckbox.Click += (_, _) =>
        {
            bool isBorderless = SaltGame.settings.settingsData.IsBorderless;
            SaltGame.settings.SetBorderless(!isBorderless);
            UpdateBorderlessCheckbox();
        };
        UpdateBorderlessCheckbox();

        fullscreenCheckbox = (GueCheckbox)screen.GetGraphicalUiElementByName("FullscreenCheckbox");
        fullscreenCheckbox.Click += (_, _) =>
        {
            bool isFullscreen = SaltGame.settings.settingsData.IsFullscreen;
            SaltGame.settings.SetFullscreen(!isFullscreen);
            UpdateFullscreenCheckbox();
        };
        UpdateFullscreenCheckbox();

    }

    private void UpdateResolutionLabel()
    {
        var resolutionLabel = screen.GetGraphicalUiElementByName("ResolutionText");
        var currentResolution = SaltGame.settings.Resolution.X + "x" + SaltGame.settings.Resolution.Y;


        resolutionLabel.SetProperty("Text", currentResolution);
    }

    private void UpdateBorderlessCheckbox()
    {
        if (SaltGame.settings.settingsData.IsBorderless)
        {
            borderlessCheckbox.ApplyState("Checked");
        }
        else
        {
            borderlessCheckbox.ApplyState("Unchecked");
        }

        UpdateResolutionChangeButtons();
    }

    private void UpdateFullscreenCheckbox()
    {
        if (SaltGame.settings.settingsData.IsFullscreen)
        {
            fullscreenCheckbox.ApplyState("Checked");
        }
        else
        {
            fullscreenCheckbox.ApplyState("Unchecked");
        }

        UpdateResolutionChangeButtons();
    }

    private void UpdateResolutionChangeButtons()
    {
        bool shouldDisable = SaltGame.settings.settingsData.IsBorderless || SaltGame.settings.settingsData.IsFullscreen;

        if(shouldDisable)
        {
            resolutionPrevButton.IsDisabled = true;
            resolutionNextButton.IsDisabled = true;

            resolutionPrevButton.ApplyState("Disabled");
            resolutionNextButton.ApplyState("Disabled");
        } 
        else
        {
            resolutionPrevButton.IsDisabled = false;
            resolutionNextButton.IsDisabled = false;

            resolutionPrevButton.ApplyState("Enabled");
            resolutionNextButton.ApplyState("Enabled");
        }
    }

    private int GetResolutionIndex()
    {
        return SaltGame.settings.AvailableResolutions.Select((res, index) => new { res, index }).First(ptRes => ptRes.res.X == SaltGame.settings.Resolution.X && ptRes.res.Y == SaltGame.settings.Resolution.Y).index;
    }
}