using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Nez;
using Nez.Sprites;
using Nez.BitmapFonts;

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

        BitmapFont font = menuScene.Content.LoadBitmapFont(Nez.Content.Font.PixelifySansBitmap);
        titleEntity = menuScene.CreateEntity("homeTitleText");
        titleEntity.SetPosition(new Vector2(850, 350));
        titleEntity.SetScale(new Vector2(0.5f, 0.5f));

        titleText = titleEntity.AddComponent<TextComponent>();
        titleText.SetFont(font);
        titleText.SetText("SaltLands: The Quest for the Mines of Salt and Glory");

        SaltLandsGame.Scene = menuScene;
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
    }
}
