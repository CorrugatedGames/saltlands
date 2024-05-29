using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Nez;
using Nez.Sprites;
using Nez.UI;
using System;

namespace SaltLands;

public class HomeScreen : GameScreen
{
    private Entity homeEntity;
    private Entity titleEntity;
    private TextComponent titleText;

    public HomeScreen(SaltLandsGame game) : base(game) { }

    public override void Initialize()
    {
        base.Initialize();

        Scene menuScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);

        Texture2D homeBackground = menuScene.Content.LoadTexture(Nez.Content.Background.Home);

        homeEntity = menuScene.CreateEntity("homeBackground");
        homeEntity.Position = Nez.Screen.Center;

        SpriteRenderer renderer = homeEntity.AddComponent<SpriteRenderer>();
        renderer.SetTexture(homeBackground);

        titleEntity = menuScene.CreateEntity("homeTitleText");
        titleEntity.SetPosition(new Vector2(850, 350));
        titleEntity.SetScale(new Vector2(0.5f, 0.5f));

        titleText = titleEntity.AddComponent<TextComponent>();
        titleText.SetFont(SaltLandsGame.saltUI.font);
        titleText.SetText("SaltLands: The Quest for the Mines of Salt and Glory");

        menuScene.CreateEntity("homeUI").AddComponent<HomeScreenUI>();

        SaltLandsGame.Scene = menuScene;
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
    }
}

public class HomeScreenUI : UICanvas
{    
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        var table = Stage.AddElement(new Table());

        table.Defaults().SetPadBottom(20);
        table.SetX(1350).SetY(600);
        table.Right().Center();

        table.Add(new ImageButton(SaltLandsGame.saltUI.skin, "menu-play-button"))
            .GetElement<ImageButton>()
            .OnClicked += TransitionPlayGame;
        table.Row();

        table.Add(new ImageButton(SaltLandsGame.saltUI.skin, "menu-options-button"))
            .GetElement<ImageButton>()
            .OnClicked += TransitionOptions;
        table.Row();

        table.Add(new ImageButton(SaltLandsGame.saltUI.skin, "menu-quit-button"))
            .GetElement<ImageButton>()
            .OnClicked += TransitionQuit;
        table.Row();
    }

    void TransitionPlayGame(Button button)
    {
        SaltLandsGame.SaltEmitter.Emit(SaltEvents.LoadPlayGameScreen);
    }

    void TransitionOptions(Button button)
    {
        SaltLandsGame.SaltEmitter.Emit(SaltEvents.LoadOptionsScreen);
    }

    void TransitionQuit(Button button)
    {
        SaltLandsGame.SaltEmitter.Emit(SaltEvents.QuitGame);
    }
}