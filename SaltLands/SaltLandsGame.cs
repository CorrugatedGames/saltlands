using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace SaltLands;

public class SaltLandsGame : Core
{

    private Entity homeEntity;
    private Entity titleEntity;
    private TextComponent titleText;

    public SaltLandsGame() : base(1920, 1080, false, "SaltLands") { }

    protected override void Initialize()
    {
        base.Initialize();

        PauseOnFocusLost = false;

        Screen.SetSize(1920, 1080);

        Scene menuScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);

        Texture2D homeBackground = menuScene.Content.LoadTexture(Nez.Content.Background.Home);

        homeEntity = menuScene.CreateEntity("homeBackground");

        SpriteRenderer renderer = homeEntity.AddComponent<SpriteRenderer>();
        renderer.SetTexture(homeBackground);

        BitmapFont font = menuScene.Content.LoadBitmapFont(Nez.Content.Font.PixelifySansBitmap);
        titleEntity = menuScene.CreateEntity("titleEntity");

        titleText = titleEntity.AddComponent<TextComponent>();
        titleText.SetFont(font);
        titleText.SetColor(Color.White);

        Scene = menuScene;
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        homeEntity.Position = Screen.Center;
        titleEntity.SetPosition(new Vector2(850, 350));
        titleEntity.SetScale(new Vector2(0.5f, 0.5f));
        titleText.SetText("SaltLands: The Quest for the Mines of Salt and Glory");
    }
}
