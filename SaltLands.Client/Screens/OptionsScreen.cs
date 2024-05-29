using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Nez;
using Nez.UI;

namespace SaltLands;

public class OptionsScreen : GameScreen
{
    private Entity titleEntity;
    private TextComponent titleText;

    public OptionsScreen(SaltLandsGame game) : base(game) { }

    public override void Initialize()
    {
        base.Initialize();

        Scene optionsScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);

        titleEntity = optionsScene.CreateEntity("optionsTitleText");
        titleEntity.SetPosition(new Vector2(890, 100));
        titleEntity.SetScale(new Vector2(0.5f, 0.5f));

        titleText = titleEntity.AddComponent<TextComponent>();
        titleText.SetFont(SaltLandsGame.saltUI.font);
        titleText.SetText("Options");

        optionsScene.CreateEntity("optionsUI").AddComponent<OptionsUI>();

        SaltLandsGame.Scene = optionsScene;
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
    }
}

public class OptionsUI : UICanvas
{    
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        var table = Stage.AddElement(new Table());

        table.Defaults().SetPadTop(10).SetMinWidth(170).SetMinHeight(30);
        table.SetFillParent(true).Center();

        table.Add(new TextButton("Back to Main Menu", SaltLandsGame.saltUI.skin, "menu-button"))
            .GetElement<TextButton>()
            .OnClicked += BackToMainMenu;
        table.Row();
    }

    void BackToMainMenu(Button button)
    {
        SaltLandsGame.SaltEmitter.Emit(SaltEvents.LoadHomeScreen);
    }
}